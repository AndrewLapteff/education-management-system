using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Project
{
    public class Student : User
    {
        public string Group { get; set; }

        public Student(string name, string email, string password, string group)
        : base(name, email, password)
        {
            Group = group;
        }

        public void SummarizeGrades()
        {
            string json = File.ReadAllText("grades.json");
            List<Grade> grades = JsonSerializer.Deserialize<List<Grade>>(json);

            List<Grade> studentGrades = grades.FindAll(g => g.Student.Name.Equals(this.Name));

            if (studentGrades.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Your ratings {0}:" + this.Name);

                foreach (Grade grade in studentGrades)
                {
                    if (grade.lab != 0)
                    {
                        Console.WriteLine($"Subject: {grade.Subject}, grade: {grade.value}, laboratory: {grade.lab}");
                    } 
                    else
                    {
                        Console.WriteLine($"Subject: {grade.Subject}, grade: {grade.value}");
                    }
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("There are no grades for you: " + this.Name);
            }
        }

        public void ForecastExamResult(string subject)
        {
            string json = File.ReadAllText("grades.json");
            List<Grade> grades = JsonSerializer.Deserialize<List<Grade>>(json);

            List<Grade> studentGrades = grades.FindAll(g => g.Student.Name.Equals(this.Name) && g.Subject.Equals(subject));

            if (studentGrades.Count > 0)
            {
                double averageGrade = CalculateAverageGrade(studentGrades);
                Console.WriteLine();
                Console.WriteLine("Prediction of the result of the exam by discipline: " + subject);
                Console.WriteLine("Estimated score: ", Math.Round(averageGrade, 2));
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("There are no grades for you in the discipline: " + subject);
            }
        }

        private double CalculateAverageGrade(List<Grade> grades)
        {
            double sum = 0;
            foreach (Grade grade in grades)
            {
                sum += grade.value;
            }

            return sum / grades.Count;
        }
    }
}
