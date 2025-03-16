using Presentation.Controllers;
using Infrastruture.Data;
using Microsoft.EntityFrameworkCore;
using Serivces.Abstraction;
using Serivces;
using Domain.Repositories;
using Infrastruture.Repositories;
using FluentValidation;
using Sharded.DTO;
using Serivces.Validator;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddApplicationPart(typeof(ContactController).Assembly);
builder.Services.AddDbContext<PhoneBookContext>(option=>{
    option.UseNpgsql("Host=localhost;Username=postgres;Password=password;Database=Phonebook_db");
});
builder.Services.AddScoped<IContactSerivce,ContactSerivce>();
builder.Services.AddScoped<IContactRepository,ContactRepositoriy>();
builder.Services.AddScoped<IValidator<ContactRequestDTO>,ContactRequestDTOValidator>();
builder.Services.AddCors(option=>{
    option.AddPolicy("mycors",op=>{
        op.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options=>{
        options.SwaggerEndpoint("/Swagger/v1/swagger.json","v1");
        options.RoutePrefix=string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseCors("mycors");

app.MapControllers();

app.UseAuthorization();

app.Run();
