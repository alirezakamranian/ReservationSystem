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
    public class MongoContext : IMongoContext
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;

        public MongoContext()
        {
            _client = new MongoClient("mongodb://localhost:27017");
            _database = _client.GetDatabase("ReservationSystem");

            Users = _database.GetCollection<User>("Users");
            ServiceProviders = _database.GetCollection<ServiceProvider>("ServiceProviders");
            ServiceReservations = _database.GetCollection<ServiceReservation>("ServiceReservations");
            Customers = _database.GetCollection<Customer>("Customers");
        }

        public IMongoCollection<User> Users { get; set; }
        public IMongoCollection<ServiceProvider> ServiceProviders { get; set; }
        public IMongoCollection<ServiceReservation> ServiceReservations { get; set; }
        public IMongoCollection<Customer> Customers { get; set; }
    }
}
