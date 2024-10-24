using Application.User.RefreshToken;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServiceProvider.CompleteProviderDetails
{
    public record CompleteProviderDetailsCommand(string BusinessName, string Biography,string UserId) : IRequest<CompleteProviderDetailsCommandResponse>;
}
