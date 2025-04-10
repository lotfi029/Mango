using Carter;
using Mango.Services.ProductAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProductServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapCarter();

app.Run();
