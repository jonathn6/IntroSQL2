using System;
using System.Collections.Generic;
using System.Text;

namespace IntroSQL
{
    public class Reviews
    {
        public int ReviewID { get; set; }
        public int ProductID { get; set; }
        public string Reviewer { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
