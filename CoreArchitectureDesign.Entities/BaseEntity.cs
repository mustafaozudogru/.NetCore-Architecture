using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreArchitectureDesign.Entities
{
    public abstract class BaseEntity
    {
        private DateTime dateTime;
        [NotMapped]
        public DateTime UsedTime { get { this.dateTime = DateTime.Now; return dateTime; } }
        public void WriteLog()
        {
            Console.WriteLine("".PadRight(40, '*'));
            Console.WriteLine("Log is written!");
            Console.WriteLine($"UseTime: {UsedTime.ToLongDateString()}");
            Console.WriteLine("".PadRight(40, '*'));
        }
    }
}
