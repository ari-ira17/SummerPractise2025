using System.Text.Json;
using System.Text.Json.Serialization;
using task13;   

public class SerializationAndDeserialization
    {
        static void Main()
        {
            var student = new Student
            {
                FirstName = "I",
                LastName = "A",
                BirthDate = new DateTime(2006, 4, 17),
                Grades = new List<Subject>
                {
                    new Subject { Name = "Math", Grade = 5 },
                    new Subject { Name = "English", Grade = 5 }
                }
            };

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            options.Converters.Add(new DateConverter());

            string json = JsonSerializer.Serialize(student, options);
            Console.WriteLine(json);

            string file_student_write = @"D:\student.json";
            File.WriteAllText(file_student_write, json);

            string file_student_read = File.ReadAllText(file_student_write);

            Student? deserialized_student = JsonSerializer.Deserialize<Student>(file_student_read, options);

            if (deserialized_student == null)
            {
                Console.WriteLine("Объект Student не создан.");
            }
            else
            {
                if (string.IsNullOrEmpty(deserialized_student.FirstName)) { Console.WriteLine("Имя отсутствует."); }

                if (string.IsNullOrEmpty(deserialized_student.LastName)) { Console.WriteLine("Фамилия отсутствует."); } 

                Console.WriteLine($"Студент: {deserialized_student.FirstName} {deserialized_student.LastName}");

                Console.WriteLine($"Дата рождения: {deserialized_student.BirthDate:dd.MM.yyyy}");

                if (deserialized_student.Grades == null || deserialized_student.Grades.Count == 0)
                {
                    Console.WriteLine("Оценки отсутствуют.");
                }
                else
                {
                    Console.WriteLine("Оценки:");
                    foreach (var subj in deserialized_student.Grades)
                    {
                        var subjName = string.IsNullOrEmpty(subj.Name) ? "Название предмета отсутствует." : subj.Name;
                        Console.WriteLine($"{subjName} - {subj.Grade}");
                    }
                }
            }    
        }
    }
