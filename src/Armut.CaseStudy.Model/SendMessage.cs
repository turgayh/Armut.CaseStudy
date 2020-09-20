using System;
namespace Armut.CaseStudy.Model
{
    public class SendMessage
    {
        public string SenderId { get; set; }
        public string RecieveId { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    }
}
