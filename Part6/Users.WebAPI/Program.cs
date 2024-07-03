using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Users.Domain;
using Users.Infrastructure;
using Users.WebAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//注入Filter
builder.Services.Configure<MvcOptions>(o =>
{
    o.Filters.Add<UnitOfWorkFilter>();
});
//演示先用内存，正式的一定要用Redis类
builder.Services.AddDistributedMemoryCache();

builder.Services.AddDbContext<UserDbContext>(o =>
{
    o.UseSqlServer("Data Source=localhost\\SQLEXPRESS01;Initial Catalog=netcoredddSZ; Integrated Security=SSPI;Encrypt=false;");
});
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<UserDomainService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISmsCodeSender, MockSmsCodeSender>();//应用层进行服务的拼装



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
