using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.InternalServices.Abstractions
{
    public interface ITokenGenerator
    {
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        public string GenerateRefreshToken();
        public JwtSecurityToken CreateToken(List<Claim> authClaims);
    }
}
