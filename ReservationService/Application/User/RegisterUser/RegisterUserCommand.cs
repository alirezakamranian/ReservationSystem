using Domain.Constants;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.RegisterUser
{
    public record RegisterUserCommand(string Name, string Email, string Password,RegisterUserRoles Role)
     : IRequest<RegisterUserCommandResponse>;
}
