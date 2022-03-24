using System;
using System.Collections.Generic;

namespace Assignment3_2.Model
{
    public partial class Student
    {
        public Student()
        {
            Grade = new HashSet<Grade>();
        }

        public int Id { get; set; }
        public string Studentname { get; set; }
        public int Studentage { get; set; }

        public virtual ICollection<Grade> Grade { get; set; }
    }
}
