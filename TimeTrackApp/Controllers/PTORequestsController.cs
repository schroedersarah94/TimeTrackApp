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
    public class PTORequestsController : Controller
    {
        private readonly TimeTrackingContext _context;

        public PTORequestsController(TimeTrackingContext context)
        {
            _context = context;
        }

        // GET: PTORequests
        public async Task<IActionResult> Index()
        {
            var timeTrackingContext = _context.PTORequests.Include(p => p.Employee);
            return View(await timeTrackingContext.ToListAsync());
        }

        // GET: PTORequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pTORequest = await _context.PTORequests
                .Include(p => p.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pTORequest == null)
            {
                return NotFound();
            }

            return View(pTORequest);
        }

        // GET: PTORequests/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id");
            return View();
        }

        // POST: PTORequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,Hours,Reason")] PTORequest pTORequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pTORequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", pTORequest.EmployeeId);
            return View(pTORequest);
        }

        // GET: PTORequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pTORequest = await _context.PTORequests.FindAsync(id);
            if (pTORequest == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", pTORequest.EmployeeId);
            return View(pTORequest);
        }

        // POST: PTORequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,Hours,Reason")] PTORequest pTORequest)
        {
            if (id != pTORequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pTORequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PTORequestExists(pTORequest.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", pTORequest.EmployeeId);
            return View(pTORequest);
        }

        // GET: PTORequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pTORequest = await _context.PTORequests
                .Include(p => p.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pTORequest == null)
            {
                return NotFound();
            }

            return View(pTORequest);
        }

        // POST: PTORequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pTORequest = await _context.PTORequests.FindAsync(id);
            _context.PTORequests.Remove(pTORequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PTORequestExists(int id)
        {
            return _context.PTORequests.Any(e => e.Id == id);
        }
    }
}
