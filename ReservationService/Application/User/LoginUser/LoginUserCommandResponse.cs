﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.LoginUser
{
    public record LoginUserCommandResponse(string AccessToken,string RefreshToken);
}
