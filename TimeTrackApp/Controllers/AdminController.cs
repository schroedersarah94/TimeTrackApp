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
            var timeTrackingContext = _context.TimeEntries.Include(t => t.Employee).Include(t => t.Task);
            return View(await timeTrackingContext.ToListAsync());
        }


        /*
         * EMPLOYEE CRUD OPERATION METHODS BELOW
         */

        //THIS EDIT METHOD GRABS THE EMPLOYEE OBJECT AND RETURNS THE "EditEmployee.cshtml" VIEW CONTAINING THE EMPLOYEE INFO
        public async Task<IActionResult> EditEmployee(int? id)
        {
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

        //THIS EDIT METHOD SAVES THE EDITED CHANGES MADE ON THE "EditEmployee.cshtml" PAGE
        [HttpPost]
        public async Task<IActionResult> EditEmployee(int id, [Bind("Id,FirstName,LastName,PTO,RoleId,ContactId,ClockedIn")] Employee employee) //rename this method
        {

            if(employee.Id != id)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                }
            }

            return RedirectToAction("ManageEmployee");
        }

        //THIS CREATE METHOD RETURNS THE "CreateEmployee.cshtml" VIEW CONTAINING EMPLOYEE INFO
        public IActionResult CreateEmployee()
        {
            ViewData["RoleName"] = new SelectList(_context.Roles, "Id", "Name"); //used to fill in form selection values
            return View();
        }

        //THIS CREATE METHOD SAVES THE INPUTS GIVEN ON THE "CreateEmployee.cshtml" PAGE
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([Bind("FirstName,LastName,PTO,Contact,Role,ClockedIn")] Employee employee, [Bind("Address,City,State,Phone") ] Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contact);
                await _context.SaveChangesAsync();

                employee.RoleId = employee.Role.Id;
                employee.Role = null;

                _context.Add(employee);
                await _context.SaveChangesAsync();

                return RedirectToAction("ManageEmployee");
            }
            return RedirectToAction("CreateEmployee");
        }


        //THIS DELETE METHOD REMOVES THE CHOSEN EMPLOYEE AND IT'S CONNECTED CONTACT OBJECT
        public async Task<IActionResult> DeleteEmployee(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.Include(contact => contact.Contact).FirstOrDefaultAsync(m => m.Id == id);

            var contact = _context.Contacts.Where(cont => cont.Id == employee.Contact.Id).FirstOrDefault();

            _context.Remove(employee);
            _context.Remove(contact);
            await _context.SaveChangesAsync();

            return RedirectToAction("ManageEmployee");
        }


        /*
         * TIME ENTRY CRUD OPERATION METHODS BELOW
         */

        //THIS EDIT METHOD GRABS THE TIME ENTRY OBJECT AND RETURNS THE "EditTimeEntry.cshtml" VIEW CONTAINING THE TIME ENTRY INFO
        public async Task<IActionResult> EditTimeEntry(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeEntry = await _context.TimeEntries.FindAsync(id);

            if (timeEntry == null)
            {
                return NotFound();
            }

            ViewData["EmployeeNames"] = new SelectList(_context.Employees, "Id", "FullName", timeEntry.EmployeeId);
            ViewData["TaskNames"] = new SelectList(_context.Tasks, "Id", "Name", timeEntry.TaskId);
            return View(timeEntry);
        }

        // THIS EDIT METHOD SAVES THE EDITED CHANGES MADE ON THE "EditTimeEntry.cshtml" PAGE
        [HttpPost]
        public async Task<IActionResult> EditTimeEntry(int id, [Bind("Id,EmployeeId,TaskId,ClockIn,ClockOut")] TimeEntry timeEntry)
        {
            if (id != timeEntry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timeEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                }
                return RedirectToAction("ManageTimeEntry");
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", timeEntry.EmployeeId);
            ViewData["TaskId"] = new SelectList(_context.Tasks, "Id", "Id", timeEntry.TaskId);
            return View(timeEntry);
        }

        //THIS CREATE METHOD RETURNS THE "CreateTimeEntry.cshtml" VIEW CONTAINING THE TIME ENTRY INFO
        public IActionResult CreateTimeEntry()
        {
            ViewData["EmployeeNames"] = new SelectList(_context.Employees, "Id", "FullName");
            ViewData["TaskNames"] = new SelectList(_context.Tasks, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTimeEntry([Bind("Id,EmployeeId,TaskId,ClockIn,ClockOut")] TimeEntry timeEntry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(timeEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("CreateTimeEntry");
        }

        //THIS DELETE METHOD REMOVES THE CHOSEN TIME ENTRY OBJECT
        public async Task<IActionResult> DeleteTimeEntry(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeEntry = await _context.TimeEntries.FirstOrDefaultAsync(m => m.Id == id);
            if (timeEntry == null)
            {
                return NotFound();
            }

            return View(timeEntry);
        }
    }

}