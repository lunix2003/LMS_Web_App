using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LoanManagementSystem.Data;
using LoanManagementSystem.Models;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;


namespace LoanManagementSystem.Controllers
{
    [Authorize(Roles = "Administrator,Collections Agent")]
    public class LoansController : Controller
    {
        private readonly ApplicationDbContext _context;
        public LoansController(ApplicationDbContext context)
        {
            _context = context;
        }

        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Loan.Include(l => l.Collateral).Include(l => l.CreditOfficer).Include(l => l.Customer);
            double total_amount = 0;
            double total_return = 0;
            foreach(var item in applicationDbContext)
            {
                total_amount += item.LoanAmount;
            }
            var details = _context.LoadDetails.Where(x => x.IsPaid == 1).ToList();
            foreach(var de in details)
            {
                total_return += de.Principle;
            }
            ViewBag.TotalAmount = (Math.Ceiling(total_amount * 100) /100).ToString("#,#00.00");
            ViewBag.TotalReturn = (Math.Ceiling(total_return * 100) /100).ToString("#,#00.00");
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loan
                .Include(l => l.Collateral)
                .Include(l => l.CreditOfficer)
                .Include(l => l.Customer)
                .FirstOrDefaultAsync(m => m.LoadId == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }
        public IActionResult Create()
        {
            var customers = _context.Customer.ToList();
            List<Customer> cus = new List<Customer>();
            foreach(var cu in customers)
            {
                if (!CustomerExists(cu.CustomerId))
                {
                    cus.Add(cu);
                }
            }
            var currencies = new List<Currency>
            {
                new Currency{Id = '$', Name="Dollar"},
                new Currency{Id = '៛', Name="Khmer"}
            };
            ViewData["Currency"] = new SelectList(currencies, "Id", "Name");
            ViewData["CollateralId"] = new SelectList(_context.Collateral, "CollateralId", "CollateralCode");
            ViewData["CreditOfficerId"] = new SelectList(_context.CreditOfficer, "CreditOfficerId", "CreditOfficerName");
            ViewData["CustomerId"] = new SelectList(cus, "CustomerId", "CustomerName");
            var viewModel = new ViewModelLoan
            {
                Loan = new Loan(),
                LoanDetail = new List<LoanDetail>()
            };
            return View(viewModel);
        }
        // handle button click
        public IActionResult Generate(string loanAmount, string interest, string date, string duration)
        {
            double _amount = Convert.ToDouble(loanAmount);
            double _rate = Convert.ToDouble(interest);
            DateTime _date = Convert.ToDateTime(date);
            double _duration = Convert.ToDouble(duration);
            //var details = _context.LoadDetails.Where(x => x.LoadId == 15).ToList();

            double _beginning, _interest, _ending, _payment, _principle, _monthlyInterest, _period = _duration * 12;
            int _years = _date.Year, _months = _date.Month, _days = _date.Day;

            _principle = _amount / _period;
            _ending = _amount;
            _monthlyInterest = _rate / 1200;
            _payment = Convert.ToDouble((Math.Ceiling((_amount * _monthlyInterest / (1 - 1 / Math.Pow(1 + _monthlyInterest, _duration * 12))) * 100 ) / 100).ToString("#,#00.00"));

            double monthly_payment = 0, total_interest = 0, total_cost_loan = 0;

            List<LoanDetail> _details = new List<LoanDetail>();
            for (int i = 1; i <= _period; i++)
            {
                LoanDetail loanDetail = new LoanDetail();
                _beginning = Math.Ceiling(_ending * 100) / 100;
                _interest = Math.Ceiling((_beginning * ((_rate / 100) / 12.0)) * 100) / 100;
                _principle = Math.Ceiling((_payment - _interest) * 100) / 100;
                _ending = Math.Ceiling((_beginning - _principle) * 100) / 100;
                loanDetail.PeriodNo = i;
                monthly_payment = _payment;
                total_interest += _interest;
                total_cost_loan += _principle;
                loanDetail.Principle = Convert.ToDouble(_principle.ToString("#,#00.00"));
                loanDetail.Interest = Convert.ToDouble(_interest.ToString("#,#00.00"));
                loanDetail.Payment = Convert.ToDouble(_payment.ToString("#,#00.00"));
                loanDetail.BeginningBalance = Convert.ToDouble(_beginning.ToString("#,#00.00"));
                loanDetail.EndingBalance = Convert.ToDouble(_ending.ToString("#,#00.00")) < 0 ? 0.00 : Convert.ToDouble(_ending.ToString("#,#00.00"));
                loanDetail.PaidDate = DateTime.Parse(_years.ToString() + "-" + _months.ToString() + "-" + _days.ToString());
                loanDetail.IsPaid = 0;
                loanDetail.Note = "";
                if (_months == 12)
                {
                    _months = 1;
                    _years += 1;
                }
                else
                {
                    _months += 1;
                }
                _details.Add(loanDetail);
            }
            var viewModel = new ViewModelLoan
            {
                LoanDetail = _details
            };
            ViewBag.Count = _period;
            total_cost_loan += total_interest;
            ViewBag.TotalInterest = (Math.Ceiling(total_interest * 100) / 100).ToString("#,#00.00");
            ViewBag.TotalCostLoan = (Math.Ceiling(total_cost_loan * 100) / 100).ToString("#,#00.00");
            ViewBag.MonthlyPayment = (Math.Ceiling(monthly_payment * 100) / 100).ToString("#,#00.00");
            TempData["loanDetails"] = JsonConvert.SerializeObject(_details);
            return PartialView("Repayment",viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( ViewModelLoan view)
        {
            if (ModelState.IsValid)
            {
            }
            if (TempData.ContainsKey("loanDetails"))
            {
                var itemsJson = TempData["loanDetails"] as string;
                var details = JsonConvert.DeserializeObject<List<LoanDetail>>(itemsJson!);
                _context.Add(view.Loan);
                view.Loan.LoanDetails!.AddRange(details!);
                await _context.SaveChangesAsync();
            
                ViewData["CollateralId"] = new SelectList(_context.Collateral, "CollateralId", "CollateralCode", view.Loan.CollateralId);
                ViewData["CreditOfficerId"] = new SelectList(_context.CreditOfficer, "CreditOfficerId", "CreditOfficerName", view.Loan.CreditOfficerId);
                ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerName", view.Loan.CustomerId);
            }
            return RedirectToAction(nameof(Index));
            //return View(view.Loan);
        }

        public async Task<IActionResult> Edit(int? id, string? returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }
            returnUrl ??= Url.Content("/Home");
            ViewBag.ReturnUrl = returnUrl;

            var loan = await _context.Loan.FindAsync(id);
            //var details =  _context.LoadDetails.FirstOrDefault(x => x.LoadId == loan!.LoadId);
            IEnumerable<LoanDetail> details =   _context.LoadDetails.Where(x=>x.LoadId == loan!.LoadId);
            var count = _context.LoadDetails.Count(x=>x.LoadId == loan!.LoadId);
            double monthly_payment = 0, total_interest = 0, total_cost_loan = 0 ;
            var years_count = count / 12;

            List<LoanDetail> loans = [];

            foreach (var item in details)
            {
                var loanDetail = new LoanDetail();
                monthly_payment = item.Payment;
                total_interest += item.Interest;
                total_cost_loan += item.Principle;
                loanDetail.LoanDetailId = item!.LoanDetailId;
                loanDetail.PeriodNo = item!.PeriodNo;
                loanDetail.BeginningBalance = item!.BeginningBalance;
                loanDetail.Principle = item!.Principle;
                loanDetail.Interest = item!.Interest;
                loanDetail.Payment = item!.Payment;
                loanDetail.EndingBalance = item!.EndingBalance;
                loanDetail.IsPaid = item!.IsPaid;
                loanDetail.PaidDate = item!.PaidDate;
                loanDetail.Note = item!.Note;
                if (loan is null) return BadRequest();
                loanDetail.LoadId = loan.LoadId;

                //details.Add(loanDetail);

                loans.Add(loanDetail);
            }
           
            var viewModel = new ViewModelLoan
            {
                Loan = loan!,
                LoanDetail = loans
            };

            if (loan == null)
            {
                return NotFound();
            }

            ViewBag.Years = years_count;
            ViewBag.Count = count;
            total_cost_loan += total_interest;
            ViewBag.TotalInterest = (Math.Ceiling(total_interest * 100) / 100).ToString("#,#00.00");
            ViewBag.TotalCostLoan = (Math.Ceiling(total_cost_loan * 100) / 100).ToString("#,#00.00");
            ViewBag.MonthlyPayment = (Math.Ceiling(monthly_payment * 100) / 100).ToString("#,#00.00");

            var currencies = new List<Currency>
            {
                new Currency{Id = '$', Name="Dollar"},
                new Currency{Id = '៛', Name="Khmer"}
            };
            ViewData["Currency"] = new SelectList(currencies, "Id", "Name");
            ViewData["CollateralId"] = new SelectList(_context.Collateral, "CollateralId", "CollateralCode", loan.CollateralId);
            ViewData["CreditOfficerId"]
                = new SelectList(_context.CreditOfficer, "CreditOfficerId", "CreditOfficerName", loan.CreditOfficerId);
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerName", loan.CustomerId);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<IActionResult> Edit(int id, Loan loan,List<string> Note, List<string> IsPaid, List<string> detailId , string? returnUrl = null)
         {
            returnUrl ??= Url.Content("/Home");
            ViewBag.ReturnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                if (id != loan.LoadId)
                {
                    return NotFound();
                }
                _context.Loan.Update(loan);


                var _loan = _context.Loan.Find(loan.LoadId);
                var _loan_details = _context.LoadDetails.Find(Convert.ToInt32(detailId[0]));
                //_context.LoadDetails.RemoveRange(_loan_details);
                List<LoanDetail> details = new List<LoanDetail>();
                for (int i = 1; i <= detailId.Count; i++)
                {
                    var loanDetail = _context.LoadDetails.Find(Convert.ToInt32(detailId[i-1]));

                    if (i <= IsPaid.Count)
                    {
                        loanDetail!.IsPaid = IsPaid[i - 1] == "on" ? 1 : 0;
                    }
                    else
                    {
                        loanDetail!.IsPaid = 0;
                    }

                    loanDetail!.Note = Note[i-1];
                    if (_loan is null) return BadRequest();
                    loanDetail.LoadId = id;

                    details.Add(loanDetail);

                }
                
                _context.LoadDetails.UpdateRange(details);
                //loan.LoanDetails!.AddRange(details);
                await _context.SaveChangesAsync();
                
            }
            ViewData["CollateralId"] = new SelectList(_context.Collateral, "CollateralId", "CollateralCode", loan.CollateralId);
            ViewData["CreditOfficerId"] = new SelectList(_context.CreditOfficer, "CreditOfficerId", "CreditOfficerName", loan.CreditOfficerId);
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerName", loan.CustomerId);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loan
                .Include(l => l.Collateral)
                .Include(l => l.CreditOfficer)
                .Include(l => l.Customer)
                .FirstOrDefaultAsync(m => m.LoadId == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loan = await _context.Loan.FindAsync(id);
            if (loan != null)
            {
                _context.Loan.Remove(loan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoanExists(int id)
        {
            return _context.Loan.Any(e => e.LoadId == id);
        }
        private bool CustomerExists(int id)
        {
            return _context.Loan.Any(e => e.CustomerId == id);
        }
        

    }
}
