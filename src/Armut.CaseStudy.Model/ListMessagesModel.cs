using System;
using System.Collections.Generic;
using System.Text;

namespace Armut.CaseStudy.Model
{
    public class ListMessagesModel
    {
        public string username { get; set; }
        public string checkUser { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; } = DateTime.UtcNow;
    }
}
