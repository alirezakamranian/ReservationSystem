using Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ServiceProviderAggregate
{
    public class Service
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ServiceStatuses Status { get; set; }
    }
}
