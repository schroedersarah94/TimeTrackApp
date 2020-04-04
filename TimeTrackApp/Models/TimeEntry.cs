using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTrackingApplication.Models
{
    public class TimeEntry
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int TaskId { get; set; }
        public DateTime ClockIn { get; set; }
        public DateTime? ClockOut { get; set; }

        public Employee Employee { get; set; }
        public Task Task { get; set; }
    }
}
