using Domain.Constants;
using Domain.UserAggregate;
using Infrastructure.DataAccess;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Application.User.RegisterUser
{
    public class RegisterUserCommandHandler(IMongoContext context) : IRequestHandler<RegisterUserCommand, RegisterUserCommandResponse>
    {
        private readonly IMongoContext _context = context;
        public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<Domain.UserAggregate.User>
                .Filter.Eq(u => u.Email, request.Email);

            if (await _context.Users.Find(u =>
                u.Password.Equals(request.Password))
                    .AnyAsync(cancellationToken))
                throw new UserAlredyExsistsException();

            await _context.Users.InsertOneAsync(new()
            {
                Name = request.Name,
                Email =request.Email,
                Password= request.Password,
                Role = (UserRoles)request.Role
            });

            return new RegisterUserCommandResponse();
        }
    }
}
