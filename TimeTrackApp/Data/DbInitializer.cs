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

            //clearing database so it is rebuilt every time, this is for programming purposes ONLY.
            context.Contacts.RemoveRange(context.Contacts);
            //context.SaveChanges();
            context.Roles.RemoveRange(context.Roles);
            //context.SaveChanges();
            context.Employees.RemoveRange(context.Employees);
            //context.SaveChanges();
            context.Statuses.RemoveRange(context.Statuses);
            //context.SaveChanges();

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

            //ADMIN EMPLOYEE
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
