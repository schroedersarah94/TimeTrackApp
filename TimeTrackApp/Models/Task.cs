using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTrackingApplication.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool ManagerialTask { get; set; }
    }
}
