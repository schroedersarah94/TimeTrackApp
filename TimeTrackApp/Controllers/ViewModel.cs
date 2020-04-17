using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TimeTrackApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TimeTrackingApplication.Data;
using TimeTrackingApplication.Models;
using System.Dynamic;

namespace TimeTrackApp.Controllers
{
    public class ViewModel : Controller
    {
        public TimeEntry TimeEntry { get; set; }
        public PTORequest PTORequest { get; set; }
    }
}
