using System;
using System.Collections.Generic;

namespace Assignment3_2.Model
{
    public partial class Course
    {
        public Course()
        {
            Grade = new HashSet<Grade>();
        }

        public string Coursecode { get; set; }
        public string Coursename { get; set; }
        public string Semester { get; set; }
        public string Teacher { get; set; }

        public virtual ICollection<Grade> Grade { get; set; }
    }
}
