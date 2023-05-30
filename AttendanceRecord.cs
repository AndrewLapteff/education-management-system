using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class AttendanceRecord
    {
        public string StudentName { get; set; }
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }
        public string Subject { get; set; }
    }
}
