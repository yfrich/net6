using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.CompilerServices;
using Zack.EventBus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//集成框架使用
builder.Services.Configure<IntegrationEventRabbitMQOptions>(o =>
{
    o.HostName = "127.0.0.1";
    o.ExchangeName = "exchangeEventBusDemo1";

});
builder.Services.AddEventBus("queueEventBusDemo1", Assembly.GetExecutingAssembly());

var app = builder.Build();


//启用UseEventBus
app.UseEventBus();
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
