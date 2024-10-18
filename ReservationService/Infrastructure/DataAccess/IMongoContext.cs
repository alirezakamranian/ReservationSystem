using Domain.CustomerAggregate;
using Domain.ServiceProviderAggregate;
using Domain.UserAggregate;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess
{
    public interface IMongoContext
    {

        public IMongoCollection<User> Users { get; set; }
        public IMongoCollection<ServiceProvider> ServiceProviders{ get; set; }
        public IMongoCollection<ServiceReservation> ServiceReservations { get; set; }
        public IMongoCollection<Customer> Customers { get; set; }
    }
}
