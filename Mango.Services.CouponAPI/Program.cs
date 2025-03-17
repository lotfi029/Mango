using Carter;
using Mango.Services.CouponAPI;
using Mango.Services.CouponAPI.HostedServices;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCouponServices(builder.Configuration);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.MapCarter();

app.Run();
