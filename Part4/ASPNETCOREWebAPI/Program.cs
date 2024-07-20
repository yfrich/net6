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
//**********ʹ��Zack AnyDBConfigProvider �������ݿ�ļ������÷���
builder.WebHost.ConfigureAppConfiguration((hostCtx, configBuilder) =>
{
    //var configRoot = configBuilder.Build();
    //string connStr = configRoot.GetConnectionString("ConnStr2");
    string connStr = builder.Configuration.GetSection("ConnStr").Value;
    configBuilder.AddDbConfiguration(() => new SqlConnection(connStr), reloadOnChange: true, reloadInterval: TimeSpan.FromSeconds(2));
});
//**********ͨ�����л����ã�ע��Redis
//**********���л������� ͨ��StackExchange.Redis ��� IConnectionMultiplexer ����ע��
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    //��Program.cs ��ȡ���õ�һ�ַ���
    string constr = builder.Configuration.GetSection("Redis").Value;
    return ConnectionMultiplexer.Connect(constr);
});
//**********��Smtp�󶨵�ʵ���� �ڿ�����������������ʹ�� ֱ��ͨ������ע�뼴��
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("Smpt"));
//**********ע��Options������Ϣ�� ӳ��ʵ�� �ο�Zack�����ݿⷽʽ���ע�롣�����ַ�ʽע��
builder.Services.AddOptions()
    //ֱ�ӿ��԰󶨡������ һ����������
    .Configure<TConfig>(t => builder.Configuration.Bind(t))
    //����������£����·�ʽ��ȡ
    //.Configure<Logging>(t => builder.Configuration.GetSection("Logging").Bind(t))
    ;

//**********����ע��
builder.Services.AddScoped<Calculator>();
builder.Services.AddScoped<TestService>();
//**********�ֲ���Ŀ����ע��  Zack.Commons.ReflectionHelper ͨ��������򼯵ķ�ʽ����ע��
//builder.Services.AddScoped<Class1>();
//builder.Services.AddScoped<Class2>();
//builder.Services.AddScoped<Class2>();
//builder.Services.AddScoped<Class3>();
//builder.Services.AddScoped<Class4>();
//**********��ʼ���ֲ����ע��
//**********��ȡ��Ŀ���õ����г���
var asmsLib = ReflectionHelper.GetAllReferencedAssemblies();
//**********��ȡ�ĳ��򼯽���ִ�е���ע��Ľӿ�
builder.Services.RunModuleInitializers(asmsLib);
//**********ע���ڴ滺�����
builder.Services.AddMemoryCache();
//**********ע���ڴ滺���������� ZACK
builder.Services.AddScoped<IMemoryCacheHelper, MemoryCacheHelper>();
//**********ע��ֲ�ʽ����Redis  ��Microsoft.Extensions.Caching.StackExchangeRedis ��̬�ⷽʽע�롣
//**********���£�ʹ������ͨ�� IDistributedCache �����ע��ʹ�ã���Ϊ�ڲ���ע�������ӿڡ�
builder.Services.AddStackExchangeRedisCache(t =>
{
    t.Configuration = "localhost";
    t.InstanceName = "bzy_";
});

//**********ע��ֲ�ʽ������������ ZACK
builder.Services.AddScoped<IDistributedCacheHelper, DistributedCacheHelper>();

//********** Zack ����ע�������� ʡ���ֶ�ע�룬ǰ���Ƕ�ָ������һ�����ݿ�
//ָ����Ҫע����ĸ�����DB
//var asmsDb = new Assembly[] { Assembly.Load("EFCoreBooks") };
//Ҳ����ɨ��ȫ�����򼯽���ע��
var asmsDb = ReflectionHelper.GetAllReferencedAssemblies();
builder.Services.AddAllDbContexts(t =>
{
    string connStr = builder.Configuration.GetSection("ConnStrEF").Value;
    t.UseSqlServer(connStr);
}, asmsDb);
//��־ע��
builder.Services.AddLogging(logBuilder =>
{
    //����̨ע��
    logBuilder.AddConsole();
    //NLogע��
    //Serilogע���
});


//**********ע��DBContext ��Scoped
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


//**********����CORS����
string[] urls = new[] { "http://localhost:5173" };
builder.Services.AddCors(t =>
{
    t.AddDefaultPolicy(b =>
    {
        //����֧��CORS���Ե�������
        //AllowAnyMethod(��������� HTTP ����)
        //AllowAnyHeader(Ҫ�������� ��������ͷ)
        //AllowCredentials(��������������ƾ�ݡ� Ҫ�����Դƾ��)
        //AllowAnyOrigin() ��Ҫ��
        b.WithOrigins(urls).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
    });
});


//**********ע��Filter
builder.Services.Configure<MvcOptions>(t =>
{
    //ע�뷽ʽ���ͷ�ʽ
    //context.ExceptionHandled = true; �쳣�ڲ�Ҫ��ǰ��
    //ִ��˳�����ע�������
    //ע��ExceptionFilter
    t.Filters.Add<MyExceptionFilter>();
    t.Filters.Add<LogExceptionFilter>();
    //ע��ActionFilter
    t.Filters.Add<MyActionFilter1>();
    t.Filters.Add<MyActionFilter2>();
    //ע���Զ���������Filter
    t.Filters.Add<TransactionScopeFilter>();
    //ע��������Filter
    t.Filters.Add<RateLimitActionFilter>();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//**********����CORS����Ҫ�����UseHttpsRedirection
app.UseCors();
//**********���÷���������Ӧ���� ע��Ҫ��UseCors�� MapControllersǰ
app.UseResponseCaching();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
