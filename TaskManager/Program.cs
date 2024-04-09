using System.Text;

namespace TaskManager
{

    internal class Program
    {
        public enum Categories
        {
            Personal,
            Work,
            Errands,
            School



        }


        public class The_Task
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public Categories Category { get; set; }
            public bool IsCompleted { get; set; } = false;


            public The_Task(string name, string description, string category, bool isCompleted = default)
            {
                Name = name;
                Description = description;
                Category = (Categories)Enum.Parse(typeof(Categories), category);
                IsCompleted = isCompleted;
            }





        }

        public class TaskManager
        {
            public List<The_Task> All_Tasks = new List<The_Task>();
            public void AddTask(The_Task task)
            {
                All_Tasks.Add(task);

            }

            public void Display()
            {
                foreach (var task in All_Tasks)
                {
                    Console.WriteLine(" ********** " + task.Name + "******");
                    Console.WriteLine("Description: " + task.Description);
                    Console.WriteLine("Category: " + task.Category);
                }
            }

            public void View(string category)
            {
                Categories categoryEnum;
                if (Enum.TryParse(category, true, out categoryEnum)) // 'true' for case-insensitive parsing
                {
                    All_Tasks.Where(task => task.Category == categoryEnum).ToList().ForEach(task =>
                    {
                        Console.WriteLine($" ********** {task.Name} ******");
                        Console.WriteLine($"Description: {task.Description}");
                    });
                }
                else
                {
                    Console.WriteLine("Invalid category.");
                }
            }

            public async Task WriteTasksToFileAsync(string filePath, List<The_Task> tasks)
            {
                var csv = new StringBuilder();

                foreach (var task in tasks)
                {
                    var newLine = $"{task.Name}, {task.Description}, {task.Category}, {task.IsCompleted} ";
                    csv.AppendLine(newLine);
                }

                try
                {
                    await File.WriteAllTextAsync(filePath, csv.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }

            }

            public async Task<List<The_Task>> ReadTasksFromFileAsync(string filePath)
            {
                var tasks = new List<The_Task>();
                try
                {
                    var fileContent = await File.ReadAllTextAsync(filePath);
                    var lines = fileContent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var line in lines)
                    {
                        var columns = line.Split(",");
                        if (columns.Length >= 4)
                        {
                            tasks.Add(new The_Task(columns[0], columns[1], columns[2], bool.Parse(columns[3])));
                        }
                    }

                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }

                return tasks;
            }

        }

        public static async Task Main()
        {
            The_Task task1 = new The_Task("Design", "Design flutter page", "Work");
            TaskManager taskManager1 = new TaskManager();
            taskManager1.AddTask(task1);
            taskManager1.View("Work");
            //taskManager1.WriteTasksToFileAsync("C:\\Users\\Michael  Adu-Gyamfi\\IdeaProjects\\A2SV_Project_Phase\\TaskManager\\all_tasks.csv", taskManager1.All_Tasks);

            The_Task task2 = new The_Task("study", "Study C#", "School", false);
            taskManager1.AddTask(task2);
            taskManager1.WriteTasksToFileAsync("C:\\Users\\Michael  Adu-Gyamfi\\IdeaProjects\\A2SV_Project_Phase\\TaskManager\\all_tasks.csv", taskManager1.All_Tasks);




            var tasks = await taskManager1.ReadTasksFromFileAsync("C:\\Users\\Michael  Adu-Gyamfi\\IdeaProjects\\A2SV_Project_Phase\\TaskManager\\all_tasks.csv");
            foreach (var task in tasks)
            {
                Console.WriteLine($"Name: {task.Name}");
                Console.WriteLine($"Description: {task.Description}");
                Console.WriteLine($"Category: {task.Category}");
                Console.WriteLine($"Is Completed: {task.IsCompleted}");
                Console.WriteLine("-------------------------------"); 
            }

            


        }
    }
}

