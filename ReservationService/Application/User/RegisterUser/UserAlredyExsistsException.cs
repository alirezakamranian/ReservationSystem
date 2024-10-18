using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.RegisterUser
{
    public class UserAlredyExsistsException:Exception
    {
        public UserAlredyExsistsException() : base("UserAlredyExsists") 
        {   
        }
    }
}
