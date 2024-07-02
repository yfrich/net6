using FluentValidation.AspNetCore;
using Identity框架;
using Identity框架.SignalR;
using Identity框架.SignalR案例导入英汉词典;
using Identity框架.托管服务;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Swagger 中添加授权按钮 可是Swagger中使用授权处理
builder.Services.AddSwaggerGen(c =>
{
    var scheme = new OpenApiSecurityScheme()
    {
        Description = "Authorization header. \r\nExample: 'Bearer 12345abcdef'",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Authorization"
        },
        Scheme = "oauth2",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
    };
    c.AddSecurityDefinition("Authorization", scheme);
    var requirement = new OpenApiSecurityRequirement();
    requirement[scheme] = new List<string>();
    c.AddSecurityRequirement(requirement);
});

//注入Identity标识框架 使用Zack的批量注入就不需要如下方式
builder.Services.AddDbContext<MyDbContext>(opt =>
{
    string connStr = builder.Configuration.GetSection("ConnStrEF").Value;
    opt.UseSqlServer(connStr);
});
builder.Services.AddDataProtection();
//初始化用户的密码规则
builder.Services.AddIdentityCore<MyUser>(opt =>
{
    //配置用户密码相关：
    //是否必须有数字
    opt.Password.RequireDigit = false;
    //是否有小写字母
    opt.Password.RequireLowercase = false;
    //是否有非字母非数字
    opt.Password.RequireNonAlphanumeric = false;
    //是否有大写字母
    opt.Password.RequireUppercase = false;
    //密码最小长度
    opt.Password.RequiredLength = 6;
    //设置密码错误锁定次数
    opt.Lockout.MaxFailedAccessAttempts = 3;
    //设置密码错误锁定的时间 第一次登录锁定时间是1 第二次登录锁定时间增加，依次处理等可以调用UserManager 中的手动设置锁定时间这个
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(60);
    //同一个字符出现几次
    //opt.Password.RequiredUniqueChars = 1;
    //如果是把重置链接发到用户邮箱，那就不用配置
    //如果是用户输入的验证码
    //密码生成验证码的规则 6位数字
    opt.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
    //给邮件的激活验证码的生成规则
    opt.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
});
//配置Identity
var idBuilder = new IdentityBuilder(typeof(MyUser), typeof(MyRole), builder.Services);
idBuilder.AddEntityFrameworkStores<MyDbContext>()//初始化使用的使用哪个DbContext 管理
    .AddDefaultTokenProviders()//添加默认的密码 算法
    .AddRoleManager<RoleManager<MyRole>>()//角色管理器 此方法包含了注入
    .AddUserManager<UserManager<MyUser>>();//用户管理器  此方法包含了注入

//设置活动超时 未生效，不知道具体作用
builder.Services.ConfigureApplicationCookie(o =>
{
    o.ExpireTimeSpan = TimeSpan.FromSeconds(5);
    o.SlidingExpiration = true;
});
//设置所有token的使用期限 不生效呢 
builder.Services.Configure<DataProtectionTokenProviderOptions>(o =>
{
    o.TokenLifespan = TimeSpan.FromSeconds(3);
});
//读取配置 JWT配置进行配置注入， 次方是对后续控制器中注入对象进行初始化
builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWT"));
//注入JWT及进行配置
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        //此方式是对JWT所需的构造注入初始化
        var jwtOpt = builder.Configuration.GetSection("JWT").Get<JWTSettings>();
        byte[] keyBytes = Encoding.UTF8.GetBytes(jwtOpt.SecKey);
        var secKey = new SymmetricSecurityKey(keyBytes);
        opt.TokenValidationParameters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = secKey
        };

        //SignalR 身份验证处理 
        opt.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                //WebSocket是不支持自定义报文头
                //所以我们需要把JWT通过url中的QueryString传递
                //然后在服务器端的OnMessageRecevied中，把QueryString中的JWT读出来，然后给context.Token
                var accessToken = context.Request.Query["access_token"];
                //获取路径 
                var path = context.HttpContext.Request.Path;
                //Token 不为空，且是SignalR的路径才进行授权。
                if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/Hubs/ChartRoomHub"))
                {
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    });
//注入过滤器
builder.Services.Configure<MvcOptions>(opt =>
{
    opt.Filters.Add<JWTVersionFilter>();
});

//已学习完 不用在开启托管服务
/*
//注入托管服务 简单使用
builder.Services.AddHostedService<HostedService1>();
//注入托管服务所使用的类 或者说其他注入的也一样用，这里测试用
builder.Services.AddScoped<TestService>();

//托管案例，每5秒钟统计一次数量
builder.Services.AddHostedService<ScheduledService>();
*/

//FluentValidation数据校验注入
builder.Services.AddFluentValidation(opt =>
{
    //将所需要FluentValidation验证的类 ，加载其程序集
    opt.RegisterValidatorsFromAssembly(Assembly.GetEntryAssembly());

});

//注入Cors
string[] urls = new[] { "http://localhost:5173" };
builder.Services.AddCors(options =>
    options.AddDefaultPolicy(builder => builder.WithOrigins(urls)
        .AllowAnyMethod().AllowAnyHeader().AllowCredentials())
);
//注入SignalR
//builder.Services.AddSignalR();
//注入分布式部署 
builder.Services.AddSignalR().AddStackExchangeRedis("127.0.0.1", opt =>
{
    opt.Configuration.ChannelPrefix = "Web_SiganlR";
});
//注入英汉词典用的DBContext 
builder.Services.AddDbContext<WordItemDbContext>(opt =>
{
    string connStr = "Data Source=localhost\\SQLEXPRESS01;Initial Catalog=aspnetcoreef; Integrated Security=SSPI;Encrypt=false;";
    opt.UseSqlServer(connStr);
});
//注入导入的类ImportExecutor
builder.Services.AddScoped<ImportExecutor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//指定SignalR中间件调用
app.MapHub<ChatRoomHub>("/Hubs/ChartRoomHub");
//英汉字典导入
app.MapHub<ImportDictHub>("/Hubs/ImportDictHub");
app.UseCors();

app.UseHttpsRedirection();
//添加登录认证 在 UseAuthorization 之前
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
