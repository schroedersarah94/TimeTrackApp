using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTrackingApplication.Models;


namespace TimeTrackingApplication.Data
{
    //THIS CLASS HELPS INITIALIZE A NEW LOCAL DATABASE IF ONE DOES NOT EXIST. DB IS UNIQUE FOR EACH MACHINE SINCE EMPLOYEES AND MANAGERS FOR EACH RESTAURANT DIFFER.
    public class DbInitializer
    {
        //METHOD IS CALLED IN Program.cs MAIN METHOD
        public static void Initialize(TimeTrackingContext context)
        {
            context.Database.EnsureCreated();

            //UNCOMMENTING THIS SECTION WILL CLEAR THE DATABASE ENTIRELY AND SET NEW ID'S TO ALL OBJECTS. PLEASE USE THIS ONLY FOR TESTING PURPOSES!!!!
            /*context.Contacts.RemoveRange(context.Contacts);
            context.SaveChanges();
            context.Roles.RemoveRange(context.Roles);
            context.SaveChanges();
            context.Employees.RemoveRange(context.Employees);
            context.SaveChanges();
            context.Statuses.RemoveRange(context.Statuses);
            context.SaveChanges();
            context.Tasks.RemoveRange(context.Tasks);
            context.SaveChanges();
            context.PTORequests.RemoveRange(context.PTORequests);
            context.SaveChanges();
            context.TimeEntries.RemoveRange(context.TimeEntries);
            context.SaveChanges();*/

            if (context.Roles.Any())
            {
                Console.WriteLine(context); //*****If the database has already been initialized, add breakpoint here to view the database tables and records!
                return;
            }

            //MAIN ROLES

            var roles = new Role[]
            {
                new Role{ Name="Employee"},
                new Role{ Name="Manager"},
                new Role{ Name="Admin"}
            };

            foreach (Role role in roles)
            {
                context.Roles.Add(role);
            }
            context.SaveChanges();

            //MAIN TASKS
            var tasks = new Models.Task[]
                {
                    new Models.Task{ Name="Janitorial", ManagerialTask=false },
                    new Models.Task{ Name="Front Cashier", ManagerialTask=false },
                    new Models.Task{ Name="Window Cashier", ManagerialTask=false },
                    new Models.Task{ Name="Cook", ManagerialTask=false },
                    new Models.Task{ Name="Restock", ManagerialTask=false },
                    new Models.Task{ Name="Shift Manager", ManagerialTask=true },
                    new Models.Task{ Name="Scheduling / Paperwork", ManagerialTask=true }
                };

            foreach (Models.Task task in tasks)
            {
                context.Tasks.Add(task);
            }
            context.SaveChanges();

            //MAIN REQUEST STATUSES
            var statuses = new Status[]
            {
                new Status{ Name="In Process"},
                new Status{ Name="Accepted"},
                new Status{ Name="Rejected"},
            };

            foreach (Status status in statuses)
            {
                context.Statuses.Add(status);
            }
            context.SaveChanges();

            //ADMIN EMPLOYEE (AND CONTACT)
            var contact = new Contact { Address = "123 Road Drive", City = "cityville", State = "MI", Phone = "1234567890" };
            context.Contacts.Add(contact);
            context.SaveChanges();

            var adminRole = context.Roles.Where(role => role.Name == "Admin").FirstOrDefault();

            var employee = new Employee {
                FirstName = "Admin",
                LastName = "Istrator",
                PTO = 80,
                Role = adminRole,
                Contact = contact,
                ClockedIn = false
            };

            context.Employees.Add(employee);

            context.SaveChanges();

            Console.WriteLine(context); //*****add breakpoint here to view the database tables and records!

        }
    }
}
