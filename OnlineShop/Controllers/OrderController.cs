using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Enums;
using OnlineShop.Helpers;
using OnlineShop.Models;
using OnlineShop.Services;

namespace OnlineShop.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private ApplicationDbContext _context;
        private readonly IPaymentService _paymentService;

        public OrderController(ApplicationDbContext context,IPaymentService paymentService)
        {
            this._context = context;
            this._paymentService = paymentService;
        }
        public async Task<IActionResult> Index()
        {
            var orders =await _context.orders.OrderByDescending(c=>c.Id).ToListAsync();
            return View(orders);
        }

        [HttpGet]
        public IActionResult Checkout(Pitch pitch)
        {
            ViewBag.Pitch = pitch;  
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(Order  order)
        {
            var pitch = await _context.Pitches.FindAsync(order.PitchId);      
            order.OrderNo = GetOrderNo();
            order.Id = 0;
            order.Status=OrderStatus.Pending;
            order.UserName = User.Identity.Name;
            order.PitchName = pitch.Name;
           await _context.orders.AddAsync(order);



            await _context.SaveChangesAsync();
            return RedirectToAction("Pay");
        }
        public IActionResult Pay()
        {
            return View("Pay");
        }









        

        public string GetOrderNo()
        {
            int rowCount = _context.orders.ToList().Count() + 1;//Orders.ToList().Count() + 1;
            return rowCount.ToString("000");
        }
        ////public async Task<IActionResult> GetDetails(int id)
        ////{
        ////    var order =await _context.orders.Where(c=>c.Id==id)
        ////        .Include(c=>c.Items).FirstOrDefaultAsync();
        ////    return View(order); 
        ////}



    }
}
