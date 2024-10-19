using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.RefreshToken
{
    public record RefreshTokenCommandResponse(string AccessToken,string RefreshToken);
}
