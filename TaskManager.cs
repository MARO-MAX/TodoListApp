using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace TodoListApp
{
    public class TaskManager
    {
        private List<Task> tasks;

        public TaskManager()
        {
            tasks = new List<Task>();
        }

        public void AddTask(Task task)
        {
            tasks.Add(task);
            SaveTasks();
        }

        public void EditTask(int index, Task newTask)
        {
            if (index >= 0 && index < tasks.Count)
            {
                tasks[index] = newTask;
                SaveTasks();
            }
        }

        public void RemoveTask(int index)
        {
            if (index >= 0 && index < tasks.Count)
            {
                tasks.RemoveAt(index);
                SaveTasks();
            }
        }

        public void MarkAsDone(int index)
        {
            if (index >= 0 && index < tasks.Count)
            {
                tasks[index].IsDone = true;
                SaveTasks();
            }
        }

        public void DisplayTasks()
        {
            Console.WriteLine($"{"No",-3} | {"Title",-20} | {"Due Date",-17} | {"Status",-10} | {"Category",-10} | {"Priority",-6} | {"Short Description",-30}");
            Console.WriteLine(new string('-', 100));
            for (int i = 0; i < tasks.Count; i++)
            {
                // Set the console color based on the task category
                switch (tasks[i].TaskCategory)
                {
                    case Category.Work:
                        Console.ForegroundColor = ConsoleColor.DarkYellow; // Orange
                        break;
                    case Category.Training:
                        Console.ForegroundColor = ConsoleColor.Cyan; // Light Blue
                        break;
                    case Category.Hobby:
                        Console.ForegroundColor = ConsoleColor.Green; // Green
                        break;
                    case Category.Fun:
                        Console.ForegroundColor = ConsoleColor.Magenta; // Pink
                        break;
                }

                Console.WriteLine($"{i + 1,-3} | {tasks[i]}");

                // Reset the console color to default
                Console.ResetColor();
            }
        }

        public List<Task> GetTasks()
        {
            return tasks;
        }

        public void LoadTasks(List<Task> loadedTasks)
        {
            tasks = loadedTasks;
        }

        public void SortByDate()
        {
            tasks.Sort((x, y) => x.DueDate.CompareTo(y.DueDate));
        }

        public void SortByCategory()
        {
            tasks.Sort((x, y) => x.TaskCategory.CompareTo(y.TaskCategory));
        }

        public void SortByPriority()
        {
            tasks.Sort((x, y) => x.TaskPriority.CompareTo(y.TaskPriority));
        }

        public void SaveTasks()
        {
            string json = JsonSerializer.Serialize(tasks);
            File.WriteAllText("tasks.json", json);
        }

        public void LoadTasksFromFile()
        {
            if (File.Exists("tasks.json"))
            {
                string json = File.ReadAllText("tasks.json");
                var loadedTasks = JsonSerializer.Deserialize<List<Task>>(json);
                if (loadedTasks != null)
                {
                    tasks = loadedTasks;
                }
            }
        }
    }
}
