using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTrackingApplication.Models
{
    public class PTORequest
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int Hours { get; set; }
        public string Reason { get; set; }
        public Status Status { get; set; }

        public Employee Employee { get; set; }
    }
}
