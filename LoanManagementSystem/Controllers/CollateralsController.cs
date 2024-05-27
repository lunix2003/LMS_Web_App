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
    [Authorize(Roles = "Administrator,Collections Agent")]
    public class CollateralsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CollateralsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Collaterals
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Collateral.Include(c => c.CollateralType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Collaterals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collateral = await _context.Collateral
                .Include(c => c.CollateralType)
                .FirstOrDefaultAsync(m => m.CollateralId == id);
            if (collateral == null)
            {
                return NotFound();
            }

            return View(collateral);
        }

        // GET: Collaterals/Create
        public IActionResult Create()
        {
            ViewData["CollateralTypeId"] = new SelectList(_context.CollateralType, "CollateralTypeId", "CollateralTypeName");
            return View();
        }

        // POST: Collaterals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CollateralId,IsHidden,CollateralCode,OwnerName,OwnerNationalCardNumber,CollateralTypeId,CollateralDescription")] Collateral collateral)
        {
            if (ModelState.IsValid)
            {
                _context.Add(collateral);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CollateralTypeId"] = new SelectList(_context.CollateralType, "CollateralTypeId", "CollateralTypeName", collateral.CollateralTypeId);
            return View(collateral);
        }

        // GET: Collaterals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collateral = await _context.Collateral.FindAsync(id);
            if (collateral == null)
            {
                return NotFound();
            }
            ViewData["CollateralTypeId"] = new SelectList(_context.CollateralType, "CollateralTypeId", "CollateralTypeName", collateral.CollateralTypeId);
            return View(collateral);
        }

        // POST: Collaterals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CollateralId,IsHidden,CollateralCode,OwnerName,OwnerNationalCardNumber,CollateralTypeId,CollateralDescription")] Collateral collateral)
        {
            if (id != collateral.CollateralId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(collateral);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollateralExists(collateral.CollateralId))
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
            ViewData["CollateralTypeId"] = new SelectList(_context.CollateralType, "CollateralTypeId", "CollateralTypeName", collateral.CollateralTypeId);
            return View(collateral);
        }

        // GET: Collaterals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collateral = await _context.Collateral
                .Include(c => c.CollateralType)
                .FirstOrDefaultAsync(m => m.CollateralId == id);
            if (collateral == null)
            {
                return NotFound();
            }

            return View(collateral);
        }

        // POST: Collaterals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var collateral = await _context.Collateral.FindAsync(id);
            if (collateral != null)
            {
                _context.Collateral.Remove(collateral);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CollateralExists(int id)
        {
            return _context.Collateral.Any(e => e.CollateralId == id);
        }
    }
}
