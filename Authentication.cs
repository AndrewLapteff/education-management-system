using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Project
{
    public class Authentication
    {
        private List<Student> students;
        private List<Teacher> teachers;
        private const string studentsFilePath = "students.json";
        private const string teachersFilePath = "teachers.json";
        private bool isRunning = true;
        public string Status;
        public User UserInfo { get; set; }

        public Authentication()
        {
            students = LoadUsersFromJson<Student>(studentsFilePath);
            teachers = LoadUsersFromJson<Teacher>(teachersFilePath);
        }

        public void Run()
        {
            while (isRunning)
            {
                Console.WriteLine("1. Teacher registration");
                Console.WriteLine("2. Student registration");
                Console.WriteLine("3. Authorization");
                Console.WriteLine("4. Quit");
                Console.Write("Select an option: ");
                
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        RegisterTeacher();
                        break;
                    case "2":
                        RegisterStudent();
                        break;
                    case "3":
                        isRunning = false;
                        break;
                    case "4":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Incorrect option");
                        break;
                }

            }
        }

        private void RegisterTeacher()
        {
            Console.Clear();

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            Console.Write("Subject: ");
            string subject = Console.ReadLine();

            Teacher newTeacher = new Teacher(name, email, HashPassword(password), subject);
            teachers.Add(newTeacher);
            FileWritter.WriteToFile(teachers, teachersFilePath);
            Console.Clear();

            Console.WriteLine("Teacher registration is successful. You can log in");
        }

        private void RegisterStudent()
        {
            Console.Clear();

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            Console.Write("Group: ");
            string group = Console.ReadLine();

            Student newStudent = new Student(name, email, HashPassword(password), group);
            students.Add(newStudent);
            FileWritter.WriteToFile(students, studentsFilePath);
            Console.Clear();

            Console.WriteLine("Student registration is successful. You can log in");
        }

        public User Login()
        {
            Console.Clear();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            User user = FindUser(email, HashPassword(password));

            if (user != null)
            {
                Console.WriteLine($"Login is successful. Welcome, {user.Name}!");
                if (user is Teacher)
                {
                    isRunning = false;
                    Status = "Teacher";
                    return(Teacher)user;
                }
                else if (user is Student)
                {
                    isRunning = false;
                    Status = "Student";
                    return (Student)user;
                }
            }
            else
            {
                Console.WriteLine("Invalid email or password");
            }
            return null;
        }

        private User FindUser(string email, string password)
        {
            User user = students.Find(u => u.Email == email && u.Password == password);
            if (user == null)
            {
                user = teachers.Find(u => u.Email == email && u.Password == password);
            }
            return user;
        }



        private List<T> LoadUsersFromJson<T>(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<T>>(json);
            }
            else
            {
                return new List<T>();
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashedBytes = sha256.ComputeHash(passwordBytes);
                string hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", string.Empty);
                return hashedPassword;
            }
        }
    }

}
