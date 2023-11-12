using Laba_7_Horbach_program;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

public class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.Write("Введіть максимальну кількість користувачів (N): ");
        if (int.TryParse(Console.ReadLine(), out int max_objects) && max_objects > 0)
        {
            Console.WriteLine($"Максимальна кількість користувачів: {max_objects}");

            List<Person> people = new();

            while (true)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1 - Додати об’єкт");
                Console.WriteLine("2 - Вивести на екран об’єкти");
                Console.WriteLine("3 - Знайти об’єкт");
                Console.WriteLine("4 - Видалити об’єкт");
                Console.WriteLine("5 - Демонстрація поведінки об’єктів");
                Console.WriteLine("6 - Демонстрація роботи static методів");
                Console.WriteLine("7 - зберегти колекцію об’єктів у файлі");
                Console.WriteLine("8 - зчитати колекцію об’єктів з файлу");
                Console.WriteLine("9 - очистити колекцію об’єктів");
                Console.WriteLine("0 - Вийти з програми");
                Console.Write("Виберіть опцію: ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    try
                    {
                        switch (choice)
                        {
                            case 1:
                                Add_Person(people, max_objects);
                                break;

                            case 2:
                                Display_People(people);
                                break;

                            case 3:
                                Find_Person(people);
                                break;

                            case 4:
                                Remove_Person(people);
                                break;

                            case 5:
                                Demo_behavior(people);
                                break;

                            case 6:
                                DemoStaticMethods(people);
                                break;
                            case 7:
                                Console.WriteLine("1 – зберегти у файл *.csv (*.txt)");
                                Console.WriteLine("2 – зберегти у файл *.json");
                                int save = int.Parse(Console.ReadLine());
                                switch (save)
                                {
                                    case 1:

                                        string csvFilePath = @"C:\Users\Arina Gorbach\Desktop\Lab_7.csv";
                                        SaveToFIleCVS(people, csvFilePath);
                                        Console.WriteLine("Data loaded into a CSV file.");
                                        break;
                                    case 2:
                                        string jsonFilePath = @"C:\Users\Arina Gorbach\Desktop\Lab_7.json";
                                        SaveToFileJson(people, jsonFilePath);
                                        Console.WriteLine("Data loaded into JSON file.");
                                        break;
                                    default:
                                        Console.WriteLine("Некоректний вибір способу зберігання.");
                                        break;
                                }
                                break;
                            case 8:
                                Console.WriteLine("1 – зчитати з файлу *.csv (*.txt)");
                                Console.WriteLine("2 – зчитати з файлу *.json");
                                int read = int.Parse(Console.ReadLine());
                                switch (read)
                                {
                                    case 1:
                                        string readCVSFile = @"C:\Users\Arina Gorbach\Desktop\Lab_7.csv";
                                        people = ReadFromFileCSV(readCVSFile);
                                        Console.WriteLine("Data loaded from CSV file.");
                                        break;
                                    case 2:
                                        string readJsonFile = @"C:\Users\Arina Gorbach\Desktop\Lab_7.json";
                                        people = ReadFromFileJson(readJsonFile);
                                        Console.WriteLine("Data loaded from JSON file.");
                                        break;
                                    default:
                                        Console.WriteLine("Некоректний вибір способу завантаження.");
                                        break;
                                }
                                break;
                            case 9:
                                people.Clear();
                                Console.WriteLine("Колекція очищена");
                                break;

                            case 0:
                                Console.WriteLine("Завершення роботи програми.");
                                return;

                            default:
                                Console.WriteLine("Некоректний вибір опції.");
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Помилка: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Некоректний вибір опції.");
                }
            }
        }
        else
        {
            Console.WriteLine("Некоректне значення для максимальної кількості користувачів.");
        }
    }


}