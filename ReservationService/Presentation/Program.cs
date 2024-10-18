using Application.User.RegisterUser;
using Domain.ServiceProviderAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Endpoints.User;
using Presentation.ServicesCofiguration;

var builder = WebApplication.CreateBuilder(args);

//                                      services container.

builder.Services.RegisterAppServices();
builder.Services.AddEndpoints();
var app = builder.Build();

//                                     HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.MapEndpoints();

app.Run();
