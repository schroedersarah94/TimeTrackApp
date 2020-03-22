using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace TimeTrackingApplication.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int PTO { get; set; }

        [Required]
        public int RoleId { get; set; }

        public int ContactId { get; set; }

        public bool ClockedIn { get; set; }



        public Role Role { get; set; }

        public Contact Contact { get; set; }
    }
}
