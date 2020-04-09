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
    public class HomeController : Controller
    {
        //DB CONTEXT VARIABLE AND METHOD
        private readonly TimeTrackingContext _context;

        public HomeController(TimeTrackingContext context)
        {
            _context = context;
        }


        //METHOD RETURNS MAIN LOG IN PAGE VIEW (Home/index.cshtml)
        public IActionResult Index()
        {
            return View();
        }


        // METHOD TAKES LOGIN PIN NUMBER AND CHECK DB FOR VALID ID, RETURNS A VIEW OF THE MAIN PAGE
        [HttpPost]
        public async Task<IActionResult> Main(int? pin)
        {
            var employee = _context.Employees.Where(emp => emp.Id == pin).FirstOrDefault();

            if (employee == null)
            {
                return null;
            }
            else
            {
                var timeTrackingContext = _context.PTORequests.Include(p => p.Employee);
                return View(await timeTrackingContext.ToListAsync());
            }
        }
    }
}
