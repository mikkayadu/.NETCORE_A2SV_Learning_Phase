using System.Text.Json;

namespace Student_Management_System
{
    internal class MIS
    {
        public class Student
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public int RollNumber { get; private set; }

            public string Grade { get; set; }

            public Student(string name, int age, int rollNumber, string grade) 
                {
                    Name = name;
                Age = age;
                RollNumber = rollNumber;
                Grade = grade;
                }

        }

        public class StudentList<T> where T : Student
        {
            public List<T> students { get; set; } = new List<T>();


            public void AddStudent(T student)
            {
                students.Add(student);
            }

            public void Search(string query)
            {
                var ans = students.Where(x => x.Name.Contains(query) || x.RollNumber.ToString().Contains(query)).Select(x => x);

                if (!ans.Any()) 
                {
                    Console.WriteLine("No such student found");
                    Console.WriteLine();
                }
                else
                {
                    foreach (var student in ans)
                    {
                        Console.WriteLine("Student Details");
                        Console.WriteLine("Student_Name: " + student.Name);
                        Console.WriteLine("Student Age: " + student.Age);
                        Console.WriteLine("Student Grade: " + student.Grade);
                        Console.WriteLine("Student RollNumber: " + student.RollNumber);
                    }
                }
            }

            public void Remove(int id)
            {
                var stud = students.FirstOrDefault(x => x.RollNumber == id);
                if (stud != null)
                {
                    students.Remove(stud);
                }
                else
                {
                    Console.WriteLine("No such student exist");
                }
            }

            public List<T> GetStudents()
            {
                return students;
            }

            public static async Task SerializeStudentListAsync(StudentList<Student> studentList, string filePath)
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(studentList, options);

                await File.WriteAllTextAsync(filePath, jsonString);
            }

            public static async Task<StudentList<Student>> DeserializeStudentListAsync(string filePath)
            {
                string jsonString = await File.ReadAllTextAsync(filePath);
                return JsonSerializer.Deserialize<StudentList<Student>>(jsonString);
            }


    
        }

        public static async Task Main(string[] args)
        {
            StudentList<Student> studentList = new StudentList<Student>();

           
            Console.WriteLine(@"welcome to the Student Management System
                1. Add Student
                2. Search Student
                3. Remove Student
                4. Exit
                ");

            

            var input = Console.ReadLine().Trim();
            while (input != "4") {
                if (input == "1")
                {
                    Console.Write("Student Name: ");
                    var name = Console.ReadLine();
                    Console.WriteLine();

                    Console.Write("Student Age: ");
                    var age = int.Parse(Console.ReadLine());
                    Console.WriteLine();    

                    Console.Write("Student RollNumber: ");
                    var roll_number = int.Parse(Console.ReadLine());
                    Console.WriteLine();    

                    Console.Write("Student grade: ");
                    var grade = Console.ReadLine();
                    Console.WriteLine();    

                    studentList.AddStudent(new Student(name, age, roll_number, grade));
                    Console.WriteLine("Student Successfully Added") ;

                }

                else if (input == "2")
                {
                    Console.Write("Enter name or ID:");
                    var query = Console.ReadLine();
                    studentList.Search(query);


                }

                else if (input == "3")
                {
                    Console.Write("Enter ID:");
                    var id  = Console.ReadLine();
                    if (id != null) {
                        studentList.Remove(int.Parse(id));
                    }

                    
                }
                Console.WriteLine("Enter an Option: ");
                input = Console.ReadLine().Trim();
            }



            //foreach(var student in studentList.GetStudents())
            //{
            //    Console.WriteLine("Student Name: " + student.Name);
            //    Console.WriteLine("student Age: " + student.Age);
            //    Console.WriteLine("student RollNumber " + student.RollNumber);
            //    Console.WriteLine("student Grade " + student.Grade);
            //    Console.WriteLine();

            //}


            await StudentList<Student>.SerializeStudentListAsync(studentList, "C:\\Users\\Michael  Adu-Gyamfi\\IdeaProjects\\A2SV_Project_Phase\\Student_Management_System\\all_students.json");
            var ans = await StudentList<Student>.DeserializeStudentListAsync("C:\\Users\\Michael  Adu-Gyamfi\\IdeaProjects\\A2SV_Project_Phase\\Student_Management_System\\all_students.json");


            foreach (var student in ans.GetStudents())
            {
                Console.WriteLine("Student Name: " + student.Name);
                Console.WriteLine("student Age: " + student.Age);
                Console.WriteLine("student RollNumber " + student.RollNumber);
                Console.WriteLine("student Grade " + student.Grade);
                Console.WriteLine();

            }
            Console.WriteLine();



        }
    }
}
