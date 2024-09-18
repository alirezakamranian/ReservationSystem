var builder = WebApplication.CreateBuilder(args);

//                                               services container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//                                             HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/weatherforecast", () =>
{


})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

