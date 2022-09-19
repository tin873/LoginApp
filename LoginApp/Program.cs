using Application;
using Core.Exceptions;
using Core.Swagger;
using login.EntiryFrameWorkCore;
using LoginApp.Controllers.Base;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
    options.Filters.Add(typeof(ValidateModelStateAttribute));
    options.Filters.Add(typeof(HttpResponseExceptionFilterAttribute));
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//connect sql server
builder.Services.AddEntityframeWorkCore();
builder.Services.AddSwaggerUI();
builder.Services.AddApplication();
builder.Services.AddIdentity();

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

app.StartApp();
