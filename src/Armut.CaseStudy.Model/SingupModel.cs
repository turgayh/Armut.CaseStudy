
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Armut.CaseStudy.Model
{
    public class SingupModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        [BsonRequired]
        public string Username { get; set; }
        [BsonRequired]
        public string Password { get; set; }
    }
}
