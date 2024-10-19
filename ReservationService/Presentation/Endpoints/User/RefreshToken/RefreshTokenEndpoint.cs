using Application.User.RefreshToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Endpoints.Abstractions;
using Presentation.Endpoints.User.RegisterUser;

namespace Presentation.Endpoints.User.RefreshToken
{
    public class RefreshTokenEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/user/refreshtoken", async ([FromBody] RefreshTokenRequest request, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var command = new RefreshTokenCommand(request.ExpiredAccessToken,request.RefreshToken);

                var response = await mediator.Send(command, cancellationToken);

                return "ok";
            }).WithName("RefreshToken").WithOpenApi();
        }
    }
}
