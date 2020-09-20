

using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Armut.CaseStudy.Model.Dtos
{
    public class UserDto
    {
        public string UserId { get; set; }
        [BsonRequired]
        public string Username { get; set; }

    }
}
