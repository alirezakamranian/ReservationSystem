using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ServiceProviderAggregate
{
    public class ServiceReservation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id{ get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public string CustomerUserId { get; set; }
        public string ServiceProviderId { get; set; }
        public string ServiceId { get; set; }
    }
}
