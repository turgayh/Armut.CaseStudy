

using MongoDB.Bson.Serialization.Attributes;

namespace Armut.CaseStudy.Model
{
    public class SendMessageRequest
    {
        public string SenderUsername { get; set; }
        public string ReceiveUsername { get; set; }
        public string Message { get; set; }

    }
}
