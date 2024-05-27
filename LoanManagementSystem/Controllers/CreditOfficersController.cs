using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LoanManagementSystem.Data;
using LoanManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace LoanManagementSystem.Controllers
{
    [Authorize(Roles = "Administrator,Collections Agent,Loan Auditor")]
    public class CreditOfficersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CreditOfficersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CreditOfficers
        public async Task<IActionResult> Index()
        {
            return View(await _context.CreditOfficer.ToListAsync());
        }

        // GET: CreditOfficers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditOfficer = await _context.CreditOfficer
                .FirstOrDefaultAsync(m => m.CreditOfficerId == id);
            if (creditOfficer == null)
            {
                return NotFound();
            }

            return View(creditOfficer);
        }

        // GET: CreditOfficers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CreditOfficers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CreditOfficerId,IsHidden,CreditOfficerName,Sex,DOB,POB,Phone,Email")] CreditOfficer creditOfficer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(creditOfficer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(creditOfficer);
        }

        // GET: CreditOfficers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditOfficer = await _context.CreditOfficer.FindAsync(id);
            if (creditOfficer == null)
            {
                return NotFound();
            }
            return View(creditOfficer);
        }

        // POST: CreditOfficers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CreditOfficerId,IsHidden,CreditOfficerName,Sex,DOB,POB,Phone,Email")] CreditOfficer creditOfficer)
        {
            if (id != creditOfficer.CreditOfficerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(creditOfficer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CreditOfficerExists(creditOfficer.CreditOfficerId))
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
            return View(creditOfficer);
        }

        // GET: CreditOfficers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditOfficer = await _context.CreditOfficer
                .FirstOrDefaultAsync(m => m.CreditOfficerId == id);
            if (creditOfficer == null)
            {
                return NotFound();
            }

            return View(creditOfficer);
        }

        // POST: CreditOfficers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var creditOfficer = await _context.CreditOfficer.FindAsync(id);
            if (creditOfficer != null)
            {
                _context.CreditOfficer.Remove(creditOfficer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CreditOfficerExists(int id)
        {
            return _context.CreditOfficer.Any(e => e.CreditOfficerId == id);
        }
    }
}
