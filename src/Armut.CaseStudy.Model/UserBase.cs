using MongoDB.Bson.Serialization.Attributes;


namespace Armut.CaseStudy.Model
{
    public class UserBase
    {
        public string UserId { get; set; }
        [BsonRequired]
        public string Username { get; set; }

    }
}
