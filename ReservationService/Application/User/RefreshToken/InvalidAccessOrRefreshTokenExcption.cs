﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.RefreshToken
{
    public class InvalidAccessOrRefreshTokenExcption:Exception
    {
        public InvalidAccessOrRefreshTokenExcption() : base("InvalidAccessOrRefreshToken") 
        {
                
        }
    }
}
