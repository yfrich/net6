using ASPNETCOREWebAPI.Config;
using ASPNETCOREWebAPI.Filter;
using ASPNETCOREWebAPI.Models;
using EFCoreBooks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Reflection;
using Zack.ASPNETCore;
using Zack.Commons;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//**********使用Zack AnyDBConfigProvider 进行数据库的集中配置服务
builder.WebHost.ConfigureAppConfiguration((hostCtx, configBuilder) =>
{
    //var configRoot = configBuilder.Build();
    //string connStr = configRoot.GetConnectionString("ConnStr2");
    string connStr = builder.Configuration.GetSection("ConnStr").Value;
    configBuilder.AddDbConfiguration(() => new SqlConnection(connStr), reloadOnChange: true, reloadInterval: TimeSpan.FromSeconds(2));
});
//**********通过集中化配置，注入Redis
//**********集中化的配置 通过StackExchange.Redis 类的 IConnectionMultiplexer 进行注入
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    //在Program.cs 读取配置的一种方法
    string constr = builder.Configuration.GetSection("Redis").Value;
    return ConnectionMultiplexer.Connect(constr);
});
//**********将Smtp绑定到实体中 在控制器或者其他类中使用 直接通过配置注入即可
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("Smpt"));
//**********注入Options配置信息类 映射实体 参考Zack的数据库方式如何注入。↑这种方式注入
builder.Services.AddOptions()
    //直接可以绑定。如果是 一个类的情况下
    .Configure<TConfig>(t => builder.Configuration.Bind(t))
    //多个类的情况下，由下方式读取
    //.Configure<Logging>(t => builder.Configuration.GetSection("Logging").Bind(t))
    ;

//**********依赖注入
builder.Services.AddScoped<Calculator>();
builder.Services.AddScoped<TestService>();
//**********分层项目服务注入  Zack.Commons.ReflectionHelper 通过反射程序集的方式进行注入
//builder.Services.AddScoped<Class1>();
//builder.Services.AddScoped<Class2>();
//builder.Services.AddScoped<Class2>();
//builder.Services.AddScoped<Class3>();
//builder.Services.AddScoped<Class4>();
//**********初始化分层服务注入
//**********获取项目引用的所有程序集
var asmsLib = ReflectionHelper.GetAllReferencedAssemblies();
//**********获取的程序集进行执行调用注入的接口
builder.Services.RunModuleInitializers(asmsLib);
//**********注入内存缓存服务
builder.Services.AddMemoryCache();
//**********注入内存缓存服务帮助类 ZACK
builder.Services.AddScoped<IMemoryCacheHelper, MemoryCacheHelper>();
//**********注入分布式缓存Redis  由Microsoft.Extensions.Caching.StackExchangeRedis 动态库方式注入。
//**********如下，使用则是通过 IDistributedCache 这个类注入使用，因为内部是注入的这个接口。
builder.Services.AddStackExchangeRedisCache(t =>
{
    t.Configuration = "localhost";
    t.InstanceName = "bzy_";
});

//**********注入分布式缓存服务帮助类 ZACK
builder.Services.AddScoped<IDistributedCacheHelper, DistributedCacheHelper>();

//********** Zack 批量注入上下文 省略手动注入，前提是都指定连接一个数据库
//指定需要注入的哪个类库的DB
//var asmsDb = new Assembly[] { Assembly.Load("EFCoreBooks") };
//也可以扫描全部程序集进行注入
var asmsDb = ReflectionHelper.GetAllReferencedAssemblies();
builder.Services.AddAllDbContexts(t =>
{
    string connStr = builder.Configuration.GetSection("ConnStrEF").Value;
    t.UseSqlServer(connStr);
}, asmsDb);
//日志注入
builder.Services.AddLogging(logBuilder =>
{
    //控制台注入
    logBuilder.AddConsole();
    //NLog注入
    //Serilog注入等
});


//**********注入DBContext 是Scoped
builder.Services.AddDbContext<BookDbContext>(opt =>
{
    string connStr = builder.Configuration.GetSection("ConnStrEF").Value;
    opt.UseSqlServer(connStr);
});
builder.Services.AddDbContext<PersonDbContext>(opt =>
{
    string connStr = builder.Configuration.GetSection("ConnStrEF").Value;
    opt.UseSqlServer(connStr);
});


//**********跨区CORS策略
string[] urls = new[] { "http://localhost:5173" };
builder.Services.AddCors(t =>
{
    t.AddDefaultPolicy(b =>
    {
        //设置支持CORS策略的域名。
        //AllowAnyMethod(设置允许的 HTTP 方法)
        //AllowAnyHeader(要允许所有 作者请求头)
        //AllowCredentials(服务器必须允许凭据。 要允许跨源凭据)
        //AllowAnyOrigin() 不要用
        b.WithOrigins(urls).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
    });
});


//**********注入Filter
builder.Services.Configure<MvcOptions>(t =>
{
    //注入方式泛型方式
    //context.ExceptionHandled = true; 异常内部要靠前。
    //执行顺序最后注入的优先
    //注入ExceptionFilter
    t.Filters.Add<MyExceptionFilter>();
    t.Filters.Add<LogExceptionFilter>();
    //注入ActionFilter
    t.Filters.Add<MyActionFilter1>();
    t.Filters.Add<MyActionFilter2>();
    //注入自动启用事务Filter
    t.Filters.Add<TransactionScopeFilter>();
    //注入限流器Filter
    t.Filters.Add<RateLimitActionFilter>();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//**********启用CORS策略要添加在UseHttpsRedirection
app.UseCors();
//**********启用服务器端响应缓存 注册要在UseCors后 MapControllers前
app.UseResponseCaching();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
