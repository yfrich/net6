using EFCoreʵ�����Բ���������1;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//ע��MediatR
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
//ע��DbContext
builder.Services.AddDbContext<MyDbContext>(opt =>
{
    opt.UseSqlServer("Data Source=localhost\\SQLEXPRESS01;Initial Catalog=netcoreddd1; Integrated Security=SSPI;Encrypt=false;");
});

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
