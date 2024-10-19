using Application.User.RegisterUser;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.LoginUser
{
    public record LoginUserCommand(string Email, string Password)
        : IRequest<LoginUserCommandResponse>;
}
