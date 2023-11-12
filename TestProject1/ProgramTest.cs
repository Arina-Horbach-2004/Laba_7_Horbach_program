using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Laba_7_Horbach_program;

namespace TestProject
{
    [TestClass]
    public class ProgramTest
    {
        [TestMethod]
        public void Test_Add_Person_With_Default_Constructor()
        {
            // Arrange
            List<Person> people = new List<Person>();
            int maxObjects = 5;

            // Act
            using (StringReader sr = new StringReader("1\n1\n"))
            {
                Console.SetIn(sr);
                Program.Add_Person(people, maxObjects);
            }

            // Assert
            Assert.AreEqual(1, people.Count);
        }

        [TestMethod]
        public void Test_Add_Person_With_Specific_Name_And_Gender()
        {
            // Arrange
            List<Person> people = new List<Person>();
            int maxObjects = 5;

            // Act
            Console.SetIn(new System.IO.StringReader("1\n2\nAlice\nFemale\n"));
            global::Program.Add_Person(people, maxObjects);

            // Assert
            Assert.AreEqual(1, people.Count);
        }

        [TestMethod]
        public void Test_Add_Person_With_Invalid_Gender()
        {
            // Arrange
            List<Person> people = new List<Person>();
            int maxObjects = 5;

            // Act
            Console.SetIn(new System.IO.StringReader("1\n2\nAlice\nInvalidGender\n"));
            global::Program.Add_Person(people, maxObjects);

            // Assert
            Assert.AreEqual(0, people.Count);
        }

        [TestMethod]
        public void Test_Add_Person_With_All_Parametry()
        {
            // Arrange
            List<Person> people = new List<Person>();
            int maxObjects = 5;


            using (StringReader sr = new StringReader("1\n3\nJohn\nMale\n01.01.1990\n48538626501\n"))
            {
                Console.SetIn(sr);

                // Act
                Program.Add_Person(people, maxObjects);

                // Assert
                Assert.AreEqual(1, people.Count);
                // Перевірка інших атрибутів створеного об'єкта (ім'я, стать, дата народження, номер телефону)
                var addedPerson = people[0];
                Assert.AreEqual("John", addedPerson.Name);
                Assert.AreEqual(Gender.Male, addedPerson.Gender);
                Assert.AreEqual(new DateTime(1990, 1, 1), addedPerson.BirthDate);
                Assert.AreEqual("48538626501", addedPerson.Number);
            }
        }

        [TestMethod]
        public void Test_Add_Person_With_Invalid_BirthDate()
        {
            // Arrange
            List<Person> people = new List<Person>();
            int maxObjects = 5;

            using (StringReader sr = new StringReader("1\n3\nSeyran\nFemale\nInvaliedBirthDate\n48538626501\n"))
            {
                Console.SetIn(sr);

                // Act
                Program.Add_Person(people, maxObjects);

                // Assert
                Assert.AreEqual(0, people.Count);
            }
        }

        [TestMethod]
        public void Test_Add_Person_With_Invalid_Input()
        {
            // Arrange
            List<Person> people = new List<Person>();
            int maxObjects = 5;

            // Act

            using (StringReader sr = new StringReader("1\n3\nFerit\nmale\nInvalidBirthDate\n48538602101\n"))
            {
                Console.SetIn(sr);
                Program.Add_Person(people, maxObjects);
            }

            // Assert
            Assert.AreEqual(0, people.Count);
        }

        [TestMethod]
        public void Test_Add_Person_With_User_Input_String()
        {
            // Arrange
            List<Person> people = new List<Person>();
            int maxObjects = 5;

            // Act
            Console.SetIn(new System.IO.StringReader("2\nAlice,Female,01.01.1990,48538353801\n"));
            global::Program.Add_Person(people, maxObjects);

            // Assert
            Assert.AreEqual(1, people.Count);
        }

        [TestMethod]
        public void Test_Add_Person_With_Invalid_User_Input_String()
        {
            // Arrange
            List<Person> people = new List<Person>();
            int maxObjects = 5;

            // Act
            Console.SetIn(new System.IO.StringReader("2\nInvalidInput\n"));
            global::Program.Add_Person(people, maxObjects);

            // Assert
            Assert.AreEqual(0, people.Count);
        }

        [TestMethod]
        public void Test_Find_Person_By_Name()
        {
            // Arrange
            List<Person> people = new List<Person>
            {
                new Person("John", Gender.Male),
                new Person("Alice", Gender.Female)
            };
            string expectedName = "John";

            // Act
            using (StringReader sr = new StringReader($"Name\n{expectedName}\n"))
            {
                Console.SetIn(sr);
                using (StringWriter sw = new StringWriter())
                {
                    Console.SetOut(sw);
                    Program.Find_Person(people);
                    string output = sw.ToString();
                    // Assert
                    Assert.IsTrue(output.Contains("Результат пошуку:"));
                    Assert.IsTrue(output.Contains(expectedName));
                }
            }
        }

        [TestMethod]
        public void Test_Find_Person_Not_Found()
        {
            // Arrange
            List<Person> people = new List<Person>
            {
                new Person("John", Gender.Male),
                new Person("Alice", Gender.Female)
            };
            string searchValue = "UnknownName";

            // Act
            using (StringReader sr = new StringReader($"Name\n{searchValue}\n"))
            {
                Console.SetIn(sr);
                using (StringWriter sw = new StringWriter())
                {
                    Console.SetOut(sw);
                    Program.Find_Person(people);
                    string output = sw.ToString();
                    // Assert
                    Assert.IsTrue(output.Contains("Об'єкт, який відповідає умові пошуку, не знайдено."));
                }
            }
        }

        private static string temp_Csv_File_Path = "temp_test.csv";
        private static string temp_Json_File_Path = "temp_test.json";


        [TestMethod]
        public void SaveToCSV_WritesToCSVFile()
        {
            // Arrange
            List<Person> people = new List<Person>
            {
                new Person("Ferit", Gender.Male),
                new Person("Seyran", Gender.Female)
            };

            // Act
            Program.SaveToFIleCVS(people, temp_Csv_File_Path);

            // Assert
            Assert.IsTrue(File.Exists(temp_Csv_File_Path), "The CSV file should have been created.");

            // Clean up
            File.Delete(temp_Csv_File_Path);
        }

        [TestMethod]
        public void SaveToJson_WritesToJSONFile()
        {
            // Arrange
            List<Person> people = new List<Person>
            {
                new Person("Ferit", Gender.Male),
                new Person("Seyran", Gender.Female)
            };

            // Act
            Program.SaveToFileJson(people, temp_Json_File_Path);

            // Assert
            Assert.IsTrue(File.Exists(temp_Json_File_Path), "The JSON file should have been created.");

            // Clean up
            File.Delete(temp_Json_File_Path);
        }

        [TestMethod]
        public void Read_From_File_JSON_Returns_List_Of_People()
        {
            // Arrange
            File.WriteAllText(temp_Json_File_Path, "{\"Name\":\"Ferit\",\"Gender\":\"Male\"}\n{\"Name\":\"Seyran\",\"Gender\":\"Female\"}");

            // Act
            List<Person> people = Program.ReadFromFileJson(temp_Json_File_Path);

            // Assert
            Assert.AreEqual(2, people.Count);
            Assert.AreEqual("Ferit", people[0].Name);
            Assert.AreEqual("Seyran", people[1].Name);

            // Clean up
            File.Delete(temp_Json_File_Path);
        }

        // Тест для перевірки читання порожнього файлу
        [TestMethod]
        public void Read_From_File_JSON_Empty_File_Returns_Empty_List()
        {
            // Arrange:
            File.WriteAllText(temp_Json_File_Path, "");

            // Act:
            List<Person> people = Program.ReadFromFileJson(temp_Json_File_Path);

            // Assert:
            Assert.AreEqual(0, people.Count);

        }


        // Тест для перевірки читання файлу з некоректними даними
        [TestMethod]
        public void Read_From_File_JSON_Invalid_Data_Returns_Empty_List()
        {
            // Arrange:
            File.WriteAllText(temp_Json_File_Path, "Invalid Data\n");

            // Act: 
            List<Person> people = Program.ReadFromFileJson(temp_Json_File_Path);

            // Assert: 
            Assert.AreEqual(0, people.Count);

        }

        // Тест для перевірки читання файлу з коректними даними
        [TestMethod]
        public void ReadFromFileCSV_Test()
        {
            // Arrange
            File.WriteAllText(temp_Csv_File_Path, "Ferit,Male,24.11.2004,48538535801\n");
            File.AppendAllText(temp_Csv_File_Path, "Seyran,Female,01.11.1997,48536636901\n");

            // Act
            List<Person> people = Program.ReadFromFileCSV(temp_Csv_File_Path);

            // Assert
            Assert.AreEqual(2, people.Count);
            Assert.AreEqual("Ferit", people[0].Name);
            Assert.AreEqual(Gender.Male, people[0].Gender);
            Assert.AreEqual("Seyran", people[1].Name);
            Assert.AreEqual(Gender.Female, people[1].Gender);
        }

        // Тест для перевірки читання порожнього файлу
        [TestMethod]
        public void Read_From_File_CSV_Empty_File_Returns_Empty_List()
        {
            // Arrange:
            File.WriteAllText(temp_Csv_File_Path, "");

            // Act:
            List<Person> people = Program.ReadFromFileCSV(temp_Csv_File_Path);

            // Assert:
            Assert.AreEqual(0, people.Count);
        }
    }
}