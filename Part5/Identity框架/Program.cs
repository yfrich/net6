using FluentValidation.AspNetCore;
using Identity���;
using Identity���.SignalR;
using Identity���.SignalR��������Ӣ���ʵ�;
using Identity���.�йܷ���;
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
//Swagger �������Ȩ��ť ����Swagger��ʹ����Ȩ����
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

//ע��Identity��ʶ��� ʹ��Zack������ע��Ͳ���Ҫ���·�ʽ
builder.Services.AddDbContext<MyDbContext>(opt =>
{
    string connStr = builder.Configuration.GetSection("ConnStrEF").Value;
    opt.UseSqlServer(connStr);
});
builder.Services.AddDataProtection();
//��ʼ���û����������
builder.Services.AddIdentityCore<MyUser>(opt =>
{
    //�����û�������أ�
    //�Ƿ����������
    opt.Password.RequireDigit = false;
    //�Ƿ���Сд��ĸ
    opt.Password.RequireLowercase = false;
    //�Ƿ��з���ĸ������
    opt.Password.RequireNonAlphanumeric = false;
    //�Ƿ��д�д��ĸ
    opt.Password.RequireUppercase = false;
    //������С����
    opt.Password.RequiredLength = 6;
    //�������������������
    opt.Lockout.MaxFailedAccessAttempts = 3;
    //�����������������ʱ�� ��һ�ε�¼����ʱ����1 �ڶ��ε�¼����ʱ�����ӣ����δ���ȿ��Ե���UserManager �е��ֶ���������ʱ�����
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(60);
    //ͬһ���ַ����ּ���
    //opt.Password.RequiredUniqueChars = 1;
    //����ǰ��������ӷ����û����䣬�ǾͲ�������
    //������û��������֤��
    //����������֤��Ĺ��� 6λ����
    opt.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
    //���ʼ��ļ�����֤������ɹ���
    opt.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
});
//����Identity
var idBuilder = new IdentityBuilder(typeof(MyUser), typeof(MyRole), builder.Services);
idBuilder.AddEntityFrameworkStores<MyDbContext>()//��ʼ��ʹ�õ�ʹ���ĸ�DbContext ����
    .AddDefaultTokenProviders()//���Ĭ�ϵ����� �㷨
    .AddRoleManager<RoleManager<MyRole>>()//��ɫ������ �˷���������ע��
    .AddUserManager<UserManager<MyUser>>();//�û�������  �˷���������ע��

//���û��ʱ δ��Ч����֪����������
builder.Services.ConfigureApplicationCookie(o =>
{
    o.ExpireTimeSpan = TimeSpan.FromSeconds(5);
    o.SlidingExpiration = true;
});
//��������token��ʹ������ ����Ч�� 
builder.Services.Configure<DataProtectionTokenProviderOptions>(o =>
{
    o.TokenLifespan = TimeSpan.FromSeconds(3);
});
//��ȡ���� JWT���ý�������ע�룬 �η��ǶԺ�����������ע�������г�ʼ��
builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWT"));
//ע��JWT����������
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        //�˷�ʽ�Ƕ�JWT����Ĺ���ע���ʼ��
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

        //SignalR �����֤���� 
        opt.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                //WebSocket�ǲ�֧���Զ��屨��ͷ
                //����������Ҫ��JWTͨ��url�е�QueryString����
                //Ȼ���ڷ������˵�OnMessageRecevied�У���QueryString�е�JWT��������Ȼ���context.Token
                var accessToken = context.Request.Query["access_token"];
                //��ȡ·�� 
                var path = context.HttpContext.Request.Path;
                //Token ��Ϊ�գ�����SignalR��·���Ž�����Ȩ��
                if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/Hubs/ChartRoomHub"))
                {
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    });
//ע�������
builder.Services.Configure<MvcOptions>(opt =>
{
    opt.Filters.Add<JWTVersionFilter>();
});

//��ѧϰ�� �����ڿ����йܷ���
/*
//ע���йܷ��� ��ʹ��
builder.Services.AddHostedService<HostedService1>();
//ע���йܷ�����ʹ�õ��� ����˵����ע���Ҳһ���ã����������
builder.Services.AddScoped<TestService>();

//�йܰ�����ÿ5����ͳ��һ������
builder.Services.AddHostedService<ScheduledService>();
*/

//FluentValidation����У��ע��
builder.Services.AddFluentValidation(opt =>
{
    //������ҪFluentValidation��֤���� �����������
    opt.RegisterValidatorsFromAssembly(Assembly.GetEntryAssembly());

});

//ע��Cors
string[] urls = new[] { "http://localhost:5173" };
builder.Services.AddCors(options =>
    options.AddDefaultPolicy(builder => builder.WithOrigins(urls)
        .AllowAnyMethod().AllowAnyHeader().AllowCredentials())
);
//ע��SignalR
//builder.Services.AddSignalR();
//ע��ֲ�ʽ���� 
builder.Services.AddSignalR().AddStackExchangeRedis("127.0.0.1", opt =>
{
    opt.Configuration.ChannelPrefix = "Web_SiganlR";
});
//ע��Ӣ���ʵ��õ�DBContext 
builder.Services.AddDbContext<WordItemDbContext>(opt =>
{
    string connStr = "Data Source=localhost\\SQLEXPRESS01;Initial Catalog=aspnetcoreef; Integrated Security=SSPI;Encrypt=false;";
    opt.UseSqlServer(connStr);
});
//ע�뵼�����ImportExecutor
builder.Services.AddScoped<ImportExecutor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//ָ��SignalR�м������
app.MapHub<ChatRoomHub>("/Hubs/ChartRoomHub");
//Ӣ���ֵ䵼��
app.MapHub<ImportDictHub>("/Hubs/ImportDictHub");
app.UseCors();

app.UseHttpsRedirection();
//��ӵ�¼��֤ �� UseAuthorization ֮ǰ
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
