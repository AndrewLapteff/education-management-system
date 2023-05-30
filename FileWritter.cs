using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project
{
    public class FileWritter
    {
        public static void WriteToFile<T>(List<T> users, string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = JsonSerializer.Serialize(users);
                File.WriteAllText(filePath, json);
            }
            else
            {
                using (StreamWriter fileWriter = File.CreateText(filePath))
                {
                    string json = JsonSerializer.Serialize(users);
                    fileWriter.Write(json);
                }
            }
        }
    }
}
