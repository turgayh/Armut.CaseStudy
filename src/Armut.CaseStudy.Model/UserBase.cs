using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Armut.CaseStudy.Model
{
    public class UserBase
    {
        [BsonId]
        public string UserId { get; set; }
        [BsonRequired]
        public string Username { get; set; }

    }
}
