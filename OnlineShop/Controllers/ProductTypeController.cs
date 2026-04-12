using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    public class ProductTypeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        // DI
        public ProductTypeController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var data = _dbContext.PitchTypes.ToList();// GetAll
            return View(data);
        }

        [HttpGet]
        public IActionResult Create() 
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PitchType PitchTypes)
        {
            if(ModelState.IsValid)
            {
                await _dbContext.PitchTypes.AddAsync(PitchTypes);
                await _dbContext.SaveChangesAsync();
                TempData["save"] = "Product Type created successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(PitchTypes);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id is null)
                return NotFound();
            var productType = _dbContext.PitchTypes.Find(id);
            if(productType == null)
                return NotFound();
            return View(productType);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PitchType PitchTypes)
        {
            if (ModelState.IsValid)
            {
                 _dbContext.Update(PitchTypes);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(PitchTypes);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null)
                return NotFound();
            var productType = _dbContext.PitchTypes.Find(id);
            if (productType == null)
                return NotFound();
            return View(productType);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult  Details(PitchType PitchTypes)
        {
             return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return NotFound();
            var productType = _dbContext.PitchTypes.Find(id);
            if (productType == null)
                return NotFound();
            return View(productType);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id,PitchType PitchTypes)
        {
            if (id is null)
                return NotFound();

            if(id != PitchTypes.Id)
                return NotFound();

            var productType = _dbContext.PitchTypes.Find(id);
            TempData["delete"] = "Product Type Deleted";
            if (productType is null)
                return NotFound();
            
            if (ModelState.IsValid)
            {
                _dbContext.Remove(productType);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }             
            return View(productType);
        }
    }
}
