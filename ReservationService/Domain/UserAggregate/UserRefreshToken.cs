﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserAggregate
{
    public class UserRefreshToken
    {
        public string Token { get; set; }
        public DateTime ExpireTime { get; set; }
    }
}
