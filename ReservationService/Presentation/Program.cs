using Application.User.RegisterUser;
using Domain.ServiceProviderAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation;
using Presentation.Endpoints.User;
using Presentation.ServicesCofiguration;

var builder = WebApplication.CreateBuilder(args);

//                                      services container.

builder.Services.RegisterAppServices(builder);
builder.Services.RegisterCustomServices();
builder.Services.AddEndpoints();

var app = builder.Build();

//                                     HTTP request pipeline.
app.UseExceptionHandler();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapEndpoints();

app.Run();
