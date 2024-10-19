using Application.InternalServices.Abstractions;
using Application.User.RegisterUser;
using Domain.UserAggregate;
using Infrastructure.DataAccess;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.LoginUser
{
    public class LoginUserCommandHandler (IMongoContext context,
        ITokenGenerator tokenGenerator) : IRequestHandler<LoginUserCommand, LoginUserCommandResponse>
    {
        private readonly IMongoContext _context = context;
        private readonly ITokenGenerator _tokenGenerator = tokenGenerator;

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<Domain.UserAggregate.User>
                .Filter.Eq(u => u.Email, request.Email);

            var user = await _context.Users
                .Find(filter).FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (user == null)
                throw new InvalidUserCredentialsException();

            if (!user.Password.Equals(request.Password))
                throw new InvalidUserCredentialsException();

            var authClaims = new List<Claim>
            {
                new("Id", user.Id)
            };

            var token = _tokenGenerator
                .CreateToken(authClaims);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
             
            var refreshToken = _tokenGenerator.GenerateRefreshToken();

            var update = Builders<Domain.UserAggregate.User>.Update.Set(u => 
                u.RefreshToken, new() {Token=refreshToken,
                     ExpireTime = DateTime.Now.AddDays(30)});

            var result = await _context.Users
                .UpdateOneAsync(filter, update, cancellationToken: cancellationToken);

            return new LoginUserCommandResponse(accessToken,refreshToken);
        }
    }
}
