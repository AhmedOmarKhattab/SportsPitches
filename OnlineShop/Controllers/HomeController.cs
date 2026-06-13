using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FiveStadium.Data;
using FiveStadium.Helpers;
using FiveStadium.Models;
using Stripe.Events;
using System.Diagnostics;
using X.PagedList.Extensions;

namespace FiveStadium.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext dbContext)
        {
            this._context = dbContext;
        }
		public IActionResult Index(int? page)
		{
            var product = _context.Pitches.Include(c => c.PitchType).Include(c => c.SpecialTag)
                .ToList().ToPagedList(page ?? 1,9);
			return View(product);
		}

		[HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            
            var product = _context.Pitches
                .Include(e => e.PitchType)
                .Include(e => e.SpecialTag)

                .FirstOrDefault(e => e.Id == id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreateOrder(int? id)
        {
            
            if (id == null)
                return NotFound();


            var pitch = _context.Pitches
                .Include(e => e.PitchType)
                .Include(e => e.SpecialTag)
                .FirstOrDefault(e => e.Id == id);
            if (pitch == null)
                return NotFound();
            //if (quantity <= 0 || product.Quantity < quantity)
            //{
            //    ModelState.AddModelError(string.Empty, "Quantity must be greater than 0 and less than available quantity");
            //    return View("Details",product);
            //}
            ////product.Quantity = quantity;
            return RedirectToAction("Checkout", "Order", pitch);
        }

        [HttpGet]
        [ActionName("Remove")]
        [Authorize]
        public IActionResult RemoveToCart(int? id)
        {
            List<Pitch> products = HttpContext.Session
                .Get<List<Pitch>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(e => e.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction(nameof(Index));

        }
        [HttpPost]
        public IActionResult Remove(int? id)
        {
            List<Pitch> products = HttpContext.Session.Get<List<Pitch>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(e => e.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Cart()
        {
            List<Pitch> products = HttpContext.Session.Get<List<Pitch>>("products");
            if (products == null)
            {
                products = new List<Pitch>();
            }
            return View(products);
        }
        public IActionResult WhoUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> ShowPitches(int? type,int? tag,string? name ,int pageNumber=1)
        {
            var query = _context.Pitches.AsQueryable();
            if(type.HasValue)
                query=query.Where(c=>c.PitchTypeId==type.Value);
            if (tag.HasValue)
                query = query.Where(c => c.PitchTypeId == tag.Value);

            if (!string.IsNullOrEmpty(name))
                query=query.Where(c=>c.Name.ToLower().Contains(name.ToLower()));
            var products = await query
                .Skip((pageNumber-1)*12)
                .Take(12)
                .Include(C=>C.SpecialTag)
                .ToListAsync();
            var count=await query.CountAsync();
          //  ViewBag.Brands = await _context.productBrands.ToListAsync();

            ViewBag.Types=await _context.PitchTypes.ToListAsync();
            ViewBag.Tags = await _context.specialTags.ToListAsync();

            ViewBag.currentType=type;
            ViewBag.currentTag = tag;

            ViewBag.name = name;
            ViewBag.pageNumber = pageNumber;
            return View(products);

        }
        public async Task<IActionResult> Compailant()
        {
            return View();
        }
    }
}
