using System;
using System.Collections.Generic;

namespace Assignment3_2.Model
{
    public partial class Grade
    {
        public int Studentid { get; set; }
        public string Coursecode { get; set; }
        public string Grade1 { get; set; }

        public virtual Course CoursecodeNavigation { get; set; }
        public virtual Student Student { get; set; }
    }
}
