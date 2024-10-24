using Application.ServiceProvider.CompleteProviderDetails;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Endpoints.Abstractions;
using Presentation.Endpoints.User.LoginUser;

namespace Presentation.Endpoints.ServiceProvider.CompleteProviderDetails
{
    public class CompleteProviderDetailsEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/provider/completedetails", async ([FromBody] CompleteProviderDetailsRequest request, IMediator mediator, HttpContext httpContext, CancellationToken cancellationToken) =>
            {
                var userId = httpContext.User.Claims
                    .FirstOrDefault(c => c.Type.Equals("Id")).Value;

                var command = new CompleteProviderDetailsCommand(request.BusinessName,request.Biography,userId);

                var response = await mediator.Send(command, cancellationToken);

                return response;
            }).WithName("CompleteProviderDetails").WithOpenApi().RequireAuthorization();
        }
    }
}
