using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudyCaseAGIT.Context;
using StudyCaseAGIT.Models;
using StudyCaseAGIT.Services;

namespace StudyCaseAGIT.Controllers
{
    public class ProductionPlansController : Controller
    {
        //Task 2
        private readonly ILogger<ProductionPlansController> _logger;
        private readonly ApplicationDbContext _context;

        public ProductionPlansController(ILogger<ProductionPlansController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        // GET: ProductionPlans
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductionPlans.ToListAsync());
        }

        // GET: ProductionPlans/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Calculate(int monday, int tuesday, int wednesday, int thursday, int friday, int saturday, int sunday, DateTime plandate, ProductionPlan prodPlan)
        {
            int[] initialPlan = { monday, tuesday, wednesday, thursday, friday, saturday, sunday};
            int[] balancedPlan = ProductionBalancer.BalanceProduction(initialPlan);

            // Simpan data rencana awal ke dalam tabel ProductionPlan
            var productionPlan = new ProductionPlan
            {
                Monday = monday,
                Tuesday = tuesday,
                Wednesday = wednesday,
                Thursday = thursday,
                Friday = friday,
                Saturday = saturday,
                Sunday = sunday,
                PlanDate = plandate
            };

            _context.ProductionPlans.Add(productionPlan);
            _context.SaveChanges();

            // Simpan data hasil adjustment ke dalam tabel ProductionAdjustment
            var productionAdjustment = new ProductionAdjustment
            {
                ProductionPlanId = productionPlan.Id,
                Monday = balancedPlan[0],
                Tuesday = balancedPlan[1],
                Wednesday = balancedPlan[2],
                Thursday = balancedPlan[3],
                Friday = balancedPlan[4],
                Saturday = balancedPlan[5],
                Sunday = balancedPlan[6],
                AdjustmentDate = DateTime.Now
            };
            _context.ProductionAdjustments.Add(productionAdjustment);
            _context.SaveChanges();

            ViewBag.InitialPlan = initialPlan;
            ViewBag.BalancedPlan = balancedPlan;

            return RedirectToAction(nameof(Index));

        }


        // GET: ProductionPlans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productionPlan = await _context.ProductionPlans
                .FirstOrDefaultAsync(m => m.Id == id);
            var adjustPlan = await _context.ProductionAdjustments
                .FirstOrDefaultAsync(n => n.ProductionPlanId == id);

            if (productionPlan == null && adjustPlan == null)
            {
                return NotFound();
            }

            ViewBag.ProductionPlan = productionPlan;
            ViewBag.AdjustPlan = adjustPlan;

            return View();
        }
    }
}
