﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.LoginUser
{
    public class InvalidUserCredentialsException:Exception
    {
        public InvalidUserCredentialsException() : base("InvalidUserCredentials")
        {

        }
    }
}