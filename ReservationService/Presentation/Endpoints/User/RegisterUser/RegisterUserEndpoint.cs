using Application.User.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Presentation.Endpoints.Abstractions;
using System.Runtime.CompilerServices;

namespace Presentation.Endpoints.User.RegisterUser
{
    public class RegisterUserEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/user/register", async ([FromBody] RegisterUserRequest request, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var command = new RegisterUserCommand(request.Name, request.Email, request.Password, request.Role);

                var response = await mediator.Send(command, cancellationToken);

                return "ok";
            }).WithName("RegisterUser").WithOpenApi();
        }
    }
}
