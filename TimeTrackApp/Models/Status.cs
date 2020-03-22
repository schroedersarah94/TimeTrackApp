using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTrackingApplication.Models
{
    //Status' = In Process, Accepted, Rejected
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
