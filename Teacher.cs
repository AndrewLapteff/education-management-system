using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Project
{
    public class Teacher : User
    {
        public string Subject { get; set; }

        public Teacher(string name, string email, string password, string subject)
        : base(name, email, password)
        {
            Subject = subject;
        }

        public void AddAttendanceRecord(string studentName, DateTime date, bool isPresent, string subject)
        {
            if (CheckForStudent(studentName))
            {
                Console.WriteLine("No student with this name was found");
                return;
            }

            AttendanceRecord attendanceRecord = new AttendanceRecord
            {
                StudentName = studentName,
                Date = date,
                IsPresent = isPresent,
                Subject = subject
            };

            string json = File.ReadAllText("attendance.json");
            List<AttendanceRecord> attendanceRecords = JsonSerializer.Deserialize<List<AttendanceRecord>>(json);

            attendanceRecords.Add(attendanceRecord);

            FileWritter.WriteToFile(attendanceRecords, "attendance.json");

            Console.WriteLine("\nRecorded");
        }

        public void AddGrade(int lab, string studentName, string subject, int grade)
        {
            string jsonStudents = File.ReadAllText("students.json");
            List<Student> students = JsonSerializer.Deserialize<List<Student>>(jsonStudents);

            Student student = students.Find(s => s.Name.Equals(studentName));
            if (student == null)
            {
                Console.WriteLine("Student not found");
                return;
            }

            Grade newGrade = new Grade(lab, subject, student, grade);

            string jsonGrades = File.ReadAllText("grades.json");
            List<Grade> grades = JsonSerializer.Deserialize<List<Grade>>(jsonGrades);

            grades.Add(newGrade);

            FileWritter.WriteToFile(grades, "grades.json");

            Console.WriteLine("Rating added successfully");
        }
    
        public void GetStudentInfo(string studentName)
        {
            string jsonGrades = File.ReadAllText("grades.json");
            List<Grade> grades = JsonSerializer.Deserialize<List<Grade>>(jsonGrades);

            if (CheckForStudent(studentName))
            {
                Console.WriteLine("No student with this name was found");
                return;
            }

            List<Grade> gradesOfStudent = grades.FindAll(g => g.Student.Name.Equals(studentName));
            if (gradesOfStudent.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine($"Student results {studentName}");
                foreach (var grade in gradesOfStudent)
                {
                    Console.WriteLine($"Laboratory: {grade.lab}, subject: {grade.Subject}, grade: {grade.value}");
                }
            } 
            else
            {
                Console.WriteLine("The student has no results");
            }
        }

        private bool CheckForStudent(string studentName)
        {
            string jsonStudents = File.ReadAllText("students.json");
            List<Student> students1 = JsonSerializer.Deserialize<List<Student>>(jsonStudents);

            Student student = students1.Find(s => s.Name.Equals(studentName));
            return student == null;
        }
    }
}
