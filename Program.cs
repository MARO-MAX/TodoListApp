using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace TodoListApp
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskManager taskManager = new TaskManager();
            taskManager.LoadTasksFromFile();

            bool running = true;
            while (running)
            {
                Console.Clear();
                taskManager.DisplayTasks();
                Console.WriteLine();
                Console.WriteLine("Options: Add (a), Edit (e), Remove (r), Mark as Done (m), Sort by Date (sd), Sort by Category (sc), Sort by Priority (sp), Quit (q)");
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "a":
                        AddTask(taskManager);
                        break;
                    case "e":
                        EditTask(taskManager);
                        break;
                    case "r":
                        RemoveTask(taskManager);
                        break;
                    case "m":
                        MarkTaskAsDone(taskManager);
                        break;
                    case "sd":
                        taskManager.SortByDate();
                        break;
                    case "sc":
                        taskManager.SortByCategory();
                        break;
                    case "sp":
                        taskManager.SortByPriority();
                        break;
                    case "q":
                        running = false;
                        break;
                }
            }
        }

        static void AddTask(TaskManager taskManager)
        {
            Task task = new Task();

            task.Title = Prompt("Enter title: ");

            // Date-Time validation loop
            bool validDate = false;
            while (!validDate)
            {
                string dateInput = Prompt("Enter due date (yyyy-mm-dd HH:mm): ");
                if (DateTime.TryParseExact(dateInput, "yyyy-MM-dd HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime dueDate))
                {
                    task.DueDate = dueDate;
                    validDate = true;
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please try again.");
                }
            }

            // Category validation loop
            bool validCategory = false;
            while (!validCategory)
            {
                string categoryInput = Prompt("Enter category (Work, Training, Hobby, Fun): ");
                if (Enum.TryParse(categoryInput, true, out Category category) && Enum.IsDefined(typeof(Category), category))
                {
                    task.TaskCategory = category;
                    validCategory = true;
                }
                else
                {
                    Console.WriteLine("Invalid category. Please try again.");
                }
            }

            // Priority validation loop
            bool validPriority = false;
            while (!validPriority)
            {
                string priorityInput = Prompt("Enter priority (Low, Medium, High): ");
                if (Enum.TryParse(priorityInput, true, out Priority priority) && Enum.IsDefined(typeof(Priority), priority))
                {
                    task.TaskPriority = priority;
                    validPriority = true;
                }
                else
                {
                    Console.WriteLine("Invalid priority. Please try again.");
                }
            }

            task.ShortDescription = Prompt("Enter short description: ");
            task.IsDone = false;

            taskManager.AddTask(task);
        }

        static void EditTask(TaskManager taskManager)
        {
            int index = int.Parse(Prompt("Enter task number to edit: ")) - 1;
            if (index >= 0 && index < taskManager.GetTasks().Count)
            {
                Task task = taskManager.GetTasks()[index];

                Console.WriteLine("Choose field to edit: 1. Title, 2. Due Date, 3. Category, 4. Priority, 5. Short Description");
                string fieldChoice = Prompt("Enter the number of the field you want to edit: ");

                switch (fieldChoice)
                {
                    case "1":
                        task.Title = Prompt("Enter new title: ");
                        break;
                    case "2":
                        bool validDate = false;
                        while (!validDate)
                        {
                            string dateInput = Prompt("Enter new due date (yyyy-mm-dd HH:mm): ");
                            if (DateTime.TryParseExact(dateInput, "yyyy-MM-dd HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime dueDate))
                            {
                                task.DueDate = dueDate;
                                validDate = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid date format. Please try again.");
                            }
                        }
                        break;
                    case "3":
                        bool validCategory = false;
                        while (!validCategory)
                        {
                            string categoryInput = Prompt("Enter new category (Work, Training, Hobby, Fun): ");
                            if (Enum.TryParse(categoryInput, true, out Category category) && Enum.IsDefined(typeof(Category), category))
                            {
                                task.TaskCategory = category;
                                validCategory = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid category. Please try again.");
                            }
                        }
                        break;
                    case "4":
                        bool validPriority = false;
                        while (!validPriority)
                        {
                            string priorityInput = Prompt("Enter new priority (Low, Medium, High): ");
                            if (Enum.TryParse(priorityInput, true, out Priority priority) && Enum.IsDefined(typeof(Priority), priority))
                            {
                                task.TaskPriority = priority;
                                validPriority = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid priority. Please try again.");
                            }
                        }
                        break;
                    case "5":
                        task.ShortDescription = Prompt("Enter new short description: ");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Returning to menu.");
                        return;
                }

                taskManager.EditTask(index, task);
            }
        }

        static void RemoveTask(TaskManager taskManager)
        {
            int index = int.Parse(Prompt("Enter task number to remove: ")) - 1;
            taskManager.RemoveTask(index);
        }

        static void MarkTaskAsDone(TaskManager taskManager)
        {
            int index = int.Parse(Prompt("Enter task number to mark as done: ")) - 1;
            taskManager.MarkAsDone(index);
        }

        static string Prompt(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }
    }
}
