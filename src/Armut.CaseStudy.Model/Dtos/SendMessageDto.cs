

using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Armut.CaseStudy.Model.Dtos
{
    public class SendMessageDto
    {
        [BsonId]
        [BsonRequired]
        public string SenderId { get; set; }
        public List<SendMessage> Messages { get; set; } = new List<SendMessage>();

    }
}
