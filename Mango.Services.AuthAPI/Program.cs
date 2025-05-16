using Carter;
using Store.Services.AuthAPI;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthServices(builder.Configuration);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseCors(x =>
{
    x.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapCarter();

app.Run();

