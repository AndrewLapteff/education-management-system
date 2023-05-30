using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Project;

class Program
{
    static void Main()
    {
        Authentication auth = new Authentication();
        auth.Run();
        User loggedInUser = auth.Login();

        if (loggedInUser != null)
        {
            if (loggedInUser is Teacher)
            {
                Teacher loggedInTeacher = (Teacher)loggedInUser;
                bool exit = false;
                while (!exit)
                {
                    Console.Clear();
                    Console.WriteLine("Teacher's menu:");
                    Console.WriteLine("1. Give a grade for the laboratory");
                    Console.WriteLine("2. Add an additional rating");
                    Console.WriteLine("3. View student results");
                    Console.WriteLine("4. Add student attendance record");

                    Console.Write("Select an option: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            Console.Write("\nLaboratory number: ");
                            int lab = Int32.Parse(Console.ReadLine());
                            Console.Write("Student's name: ");
                            string username = Console.ReadLine();
                            Console.Write("Grade: ");
                            int grade = Int32.Parse(Console.ReadLine());
                            loggedInTeacher.AddGrade(lab, username, loggedInTeacher.Subject, grade);
                            Console.ReadKey();
                            break;
                        case "2":
                            Console.Write("\nStudent's name: ");
                            string username1 = Console.ReadLine();
                            Console.Write("Grade: ");
                            int additionalGrade = Int32.Parse(Console.ReadLine());
                            loggedInTeacher.AddGrade(0 ,username1, loggedInTeacher.Subject, additionalGrade);
                            Console.ReadKey();
                            break;
                        case "3":
                            Console.Write("\nStudent's name: ");
                            string studentsName1 = Console.ReadLine();
                            loggedInTeacher.GetStudentInfo(studentsName1);
                            Console.ReadKey();
                            break;
                        case "4":
                            Console.Write("\nStudent's name: ");
                            string studentsName2 = Console.ReadLine();
                            Console.Write("y - present, n - not present: ");
                            ConsoleKeyInfo keyInfo = Console.ReadKey();
                            char answer = keyInfo.KeyChar;
                            if (answer == 'y')
                            {
                                loggedInTeacher.AddAttendanceRecord(studentsName2, DateTime.Now, true, loggedInTeacher.Subject);
                            } 
                            else
                            {
                                loggedInTeacher.AddAttendanceRecord(studentsName2, DateTime.Now, false, loggedInTeacher.Subject);
                            }
                            Console.ReadKey();
                            break;
                        default:
                            Console.WriteLine("Wrong choice. Try again");
                            break;
                    }
                }
            }
            else if (loggedInUser is Student)
            {
                Student loggedInStudent = (Student)loggedInUser;
                bool exit = false;
                while (!exit)
                {
                    Console.Clear();
                    Console.WriteLine("Student menu:");
                    Console.WriteLine("1. Summary information");
                    Console.WriteLine("2. Predict the result of the exam by discipline");

                    Console.Write("Select an option: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            loggedInStudent.SummarizeGrades();
                            Console.ReadKey();
                            break;
                        case "2":
                            Console.Write("\nDiscipline: ");
                            string discipline = Console.ReadLine();
                            loggedInStudent.ForecastExamResult(discipline);
                            Console.ReadKey();
                            break;
                        default:
                            Console.WriteLine("Wrong choice. Try again");
                            break;
                    }
                }
            }
        }

    }
}