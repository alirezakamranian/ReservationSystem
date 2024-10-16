using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ServiceProviderAggregate
{
    public class ServiceProvider
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string BusinessName { get; set; }
        public string Biography { get; set; }
        public string UserId { get; set; }
    }
}
