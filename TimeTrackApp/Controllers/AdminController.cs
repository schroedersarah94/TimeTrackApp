using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TimeTrackingApplication.Data;
using TimeTrackingApplication.Models;

namespace TimeTrackApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly TimeTrackingContext _context;

        public AdminController(TimeTrackingContext context)
        {
            _context = context;
        }

        //Admin Page view functions
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ManageEmployee()
        {
            var timeTrackingContext = _context.Employees.Include(e => e.Contact).Include(e => e.Role);
            return View(await timeTrackingContext.ToListAsync());
        }

        public async Task<IActionResult> ManageTimeEntry()
        {
            var timeTrackingContext = _context.TimeEntries;
            return View(await timeTrackingContext.ToListAsync());
        }

        //Admin functionality

        public async Task<IActionResult> Edit(int? id)
        {
            //WIP: WORKING ON GETTING "EDIT" PAGE FOR EACH EMPLOYEE'S INFO, MUCH LIKE
            //THE FUNCTIONALITY ALREADY PUT IN PLACE BY THE GENERATED EF FRAMEWORK
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["ContactId"] = new SelectList(_context.Contacts, "Id", "Id", employee.ContactId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", employee.RoleId);
            return View(employee);
        }
    }
}