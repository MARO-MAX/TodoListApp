using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TodoListApp
{
    public enum Category
    {
        Work,
        Training,
        Hobby,
        Fun
    }

    public enum Priority
    {
        Low,
        Medium,
        High
    }

    public class Task
    {
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsDone { get; set; }
        public Category TaskCategory { get; set; }
        public Priority TaskPriority { get; set; }
        public string ShortDescription { get; set; }

        public override string ToString()
        {
            return $"{Title,-20} | {DueDate.ToString("yyyy-MM-dd HH:mm"),-17} | {(IsDone ? "Done" : "Pending"),-10} | {TaskCategory,-10} | {TaskPriority,-6} | {ShortDescription,-30}";
        }
    }
}
