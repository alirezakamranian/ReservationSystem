using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.RefreshToken
{
    public record RefreshTokenCommand(string ExpiredAccessToken, string RefreshToken) : IRequest<RefreshTokenCommandResponse>;
}
