using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServiceProvider.CompleteProviderDetails
{
    public class UserIsAlredyServiceProviderException:Exception
    {
        public UserIsAlredyServiceProviderException():base("UserIsAlredyServiceProvider")
        {
            
        }
    }
}
