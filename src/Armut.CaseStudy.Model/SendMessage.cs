using System;
using System.Collections.Generic;
using System.Text;

namespace Armut.CaseStudy.Model
{
    public class SendMessage
    {
        public string SenderId { get; set; }
        public string RecieveId { get; set; }
        public string Message { get; set; }
        public DateTime TimeStap { get; set; } = DateTime.Now;
    }
}
