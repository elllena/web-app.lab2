using Lab2.Web.Resources.CreateKnife;
using Lab2.Web.Services;
using Lab2.Web.Services.Abstraction;
using MediatR;

using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    }); 

builder.Services.AddSingleton<IQueueService, RabbitQueueService>();
builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblies(typeof(Program).Assembly));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


app.MapPost("api/knifes", async (
    [FromBody] Request request, 
    [FromServices] ISender sender, 
    CancellationToken cancellationToken) =>
{
    var response = await sender.Send(request, cancellationToken);
    return response.StatusCode == System.Net.HttpStatusCode.OK
        ? Results.Ok()
        : Results.BadRequest(response.ErrorMessage);
});

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.DefaultModelsExpandDepth(-1);
});

app.Run("http://*:5000/");
