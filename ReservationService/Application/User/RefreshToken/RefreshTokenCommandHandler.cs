using Application.InternalServices.Abstractions;
using Application.InternalServices;
using Infrastructure.DataAccess;
using MediatR;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Application.User.RefreshToken
{
    public class RefreshTokenCommandHandler(IMongoContext context,
        ITokenGenerator tokenGenerator) : IRequestHandler<RefreshTokenCommand, RefreshTokenCommandResponse>
    {
        private readonly IMongoContext _context = context;
        private readonly ITokenGenerator _tokenGenerator = tokenGenerator;

        public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var principal = _tokenGenerator.GetPrincipalFromExpiredToken(request.ExpiredAccessToken);

            if (principal == null)
                throw new InvalidAccessOrRefreshTokenExcption();

            string userId = principal.Claims
                .FirstOrDefault(c => c.Type.Equals("Id")).Value;

            var filter = Builders<Domain.UserAggregate.User>
               .Filter.Eq(u => u.Id, userId);

            var user = await _context.Users
                .Find(filter).FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (user == null || user.RefreshToken.Token != request
                .RefreshToken || user.RefreshToken.ExpireTime <= DateTime.Now)
            {
                throw new InvalidAccessOrRefreshTokenExcption();
            }

            var token = _tokenGenerator.CreateToken(principal.Claims.ToList());

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            var refreshToken = _tokenGenerator.GenerateRefreshToken();

            var update = Builders<Domain.UserAggregate.User>.Update.Set(u =>
               u.RefreshToken, new()
               {
                   Token = refreshToken,
                   ExpireTime = DateTime.Now.AddDays(30)
               });

            var result = await _context.Users
                .UpdateOneAsync(filter, update, cancellationToken: cancellationToken);

            return new RefreshTokenCommandResponse(accessToken, refreshToken);
        }
    }
}
