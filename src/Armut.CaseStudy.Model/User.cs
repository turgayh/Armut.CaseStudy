using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Armut.CaseStudy.Model
{
    public class User : UserBase
    {

        public string Name { get; set; }
        public List<string> BlockedList { get; set; } = new List<string>();
    }
}
