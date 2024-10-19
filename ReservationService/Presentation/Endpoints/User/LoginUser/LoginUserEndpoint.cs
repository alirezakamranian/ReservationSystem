using Application.User.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Endpoints.Abstractions;
using Presentation.Endpoints.User.RegisterUser;

namespace Presentation.Endpoints.User.LoginUser
{
    public class LoginUserEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/user/login", async ([FromBody] LoginUserRequest request, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var command = new LoginUserCommand(request.Email, request.Password);

                var response = await mediator.Send(command, cancellationToken);

                return response;
            }).WithName("LoginUser").WithOpenApi();
        }
    }
}
