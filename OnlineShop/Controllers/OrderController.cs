using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FiveStadium.Data;
using FiveStadium.Enums;
using FiveStadium.Helpers;
using FiveStadium.Models;
using FiveStadium.Services;

namespace FiveStadium.Controllers
{
   // [Authorize]
    public class OrderController : Controller
    {
        private ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<IActionResult> Index()
        {
            var orders =await _context.orders.OrderByDescending(c=>c.Id).ToListAsync();
            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> Checkout(Pitch pitch)
        {
            ViewBag.Pitch = pitch;
            ViewBag.Appointments = await _context.Appointments
            .Where(a => a.PitchId == pitch.Id && a.IsAvailable)
            .ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(Order  order)
        {
            var pitch = await _context.Pitches.FindAsync(order.PitchId);
            var apoointemnt = await _context.Appointments.FindAsync(order.PitchAppointmentId);;
            apoointemnt.IsAvailable = false;

            order.OrderNo = GetOrderNo();
            order.Id = 0;
            order.Status = "تم التاكيد";

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
        public async Task<IActionResult>ChangeStauts(int id,bool status)
        {
            var order=await _context.orders.FindAsync(id);
            if(order==null)
            return RedirectToAction("Index");

            if (status)
            order.Status = "تم التاكيد";
            else
                order.Status = "ملغي";
            await _context.SaveChangesAsync();
            TempData["Success"] = "تم  بنجاح ✅";
            return RedirectToAction("Index");
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
