using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTrackingApplication.Models
{
    //Roles = Employee, Manager, Admin
    public class Role
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
