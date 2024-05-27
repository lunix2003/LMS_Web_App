
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoanManagementSystem.Data;
using LoanManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace LoanManagementSystem.Controllers
{
    [Authorize(Roles = "Administrator,Collections Agent,Loan Auditor")]
    public class CollateralTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CollateralTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CollateralTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.CollateralType.ToListAsync());
        }

        // GET: CollateralTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collateralType = await _context.CollateralType
                .FirstOrDefaultAsync(m => m.CollateralTypeId == id);
            if (collateralType == null)
            {
                return NotFound();
            }

            return View(collateralType);
        }

        // GET: CollateralTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CollateralTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CollateralTypeId,CollateralTypeName")] CollateralType collateralType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(collateralType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(collateralType);
        }

        // GET: CollateralTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collateralType = await _context.CollateralType.FindAsync(id);
            if (collateralType == null)
            {
                return NotFound();
            }
            return View(collateralType);
        }

        // POST: CollateralTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CollateralTypeId,CollateralTypeName")] CollateralType collateralType)
        {
            if (id != collateralType.CollateralTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(collateralType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollateralTypeExists(collateralType.CollateralTypeId))
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
            return View(collateralType);
        }

        // GET: CollateralTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collateralType = await _context.CollateralType
                .FirstOrDefaultAsync(m => m.CollateralTypeId == id);
            if (collateralType == null)
            {
                return NotFound();
            }

            return View(collateralType);
        }

        // POST: CollateralTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var collateralType = await _context.CollateralType.FindAsync(id);
            if (collateralType != null)
            {
                _context.CollateralType.Remove(collateralType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CollateralTypeExists(int id)
        {
            return _context.CollateralType.Any(e => e.CollateralTypeId == id);
        }
    }
}
