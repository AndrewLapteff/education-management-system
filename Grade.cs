using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Grade
    {
        public string Subject { get; set; }
        public Student Student { get; set; }
        public int value { get; set; }
        public int lab { get; set; }

        public Grade(int lab, string subject, Student student, int value)
        {
            this.lab = lab;
            Subject = subject;
            Student = student;
            this.value = value;
        }
    }
}
