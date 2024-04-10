using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity_FrameWork.Models;

namespace Entity_FrameWork
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var db = new BackendContext())
            {
                // Creating a new item and saving it to the database
                var newItem = new Item();
                newItem.Name = "Red Apple";
                newItem.Description = "Description of red apple";
                db.Items.Add(newItem);
                var count = db.SaveChanges();
                Console.WriteLine("{0} records saved to database", count);
                // Retrieving and displaying data
                Console.WriteLine();
                Console.WriteLine("All items in the database:");
                foreach (var item in db.Items)
                {
                    Console.WriteLine("{0} | {1}", item.Name, item.Description);
                }
            }
        }
    }
}



