using LoanManagementSystem.Data;
using LoanManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoanManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public AdminController(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
            var applicationDbContext = _context.Loan.Include(l => l.Collateral).Include(l => l.CreditOfficer).Include(l => l.Customer);
            double total_amount = 0;
            double total_return = 0;
            double details_total_return = 0;
            double details_total_dept = 0;
            foreach (var item in applicationDbContext)
            {
                total_amount += item.LoanAmount;
            }
            var details = _context.LoadDetails.Where(x => x.IsPaid == 1).ToList();
            foreach (var de in details)
            {
                details_total_return += de.Principle;
            }
            details_total_dept = total_amount - details_total_return;
            var details_amount = _context.LoadDetails.ToList();
            foreach (var de in details_amount)
            {
                total_return += de.Payment;
            }
            double p_return  = (details_total_return / total_amount) * 100;
            double p_dept  = (details_total_dept / total_amount) * 100;
            ViewBag.TotalAmount = total_return.ToString("#,#00.00");
            ViewBag.TotalCost = total_amount.ToString("#,#00.00");
            ViewBag.TotalReturn = details_total_return.ToString("#,#00.00");
            ViewBag.TotalDept = details_total_dept.ToString("#,#00.00");
            ViewBag.PReturn = p_return.ToString("#,#00.00");
            ViewBag.PDept = p_dept.ToString("#,#00.00");
            return View();
        }

    }
}
