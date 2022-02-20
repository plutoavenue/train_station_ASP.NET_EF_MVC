using lab_4.EFCore;
using lab_4.Models;
using lab_4.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace lab_4.Controllers
{
    public class PassengersController : Controller
    {
        private readonly AppDbContext _context;

        public PassengersController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Passangers.ToList());
        }

        [HttpPost]
        public IActionResult AddPassenger(Passangers newPassanger)
        {

            _context.Passangers.Add(new Passangers
            {
                PassengerId = _context.Passangers.Max(item => item.PassengerId) + 1,
                FullName = newPassanger.FullName,
                Address = newPassanger.Address,
                Phone = newPassanger.Phone
            });

            _context.SaveChanges();
            return RedirectToRoute(new { controller = "Passengers", action = "Index" });

        }

        public IActionResult AddPosition()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> DeletePassenger(Int32 passengerId)
        {
            var record = _context.Passangers.Find(passengerId);
            _context.Passangers.Remove(record);

            await _context.SaveChangesAsync();

            return RedirectToRoute(new { controller = "Passengers", action = "Index" });

        }

        [HttpGet]
        public IActionResult EditPassenger(Int32 passengerId)
        {
            if (passengerId == 0) 
                return RedirectToRoute(new { controller = "Passengers", action = "Index" });

            var record = _context.Passangers.FirstOrDefault(s => s.PassengerId == passengerId);


            if (record == null)

            {
                return RedirectToRoute(new { controller = "Passengers", action = "Index" });
            }


            return View("EditPassenger", record);
        }

        [HttpPost]
        public IActionResult EditPassenger(Passangers newPassanger, Int32 id)
        {
            if (newPassanger == null)
            {
                return RedirectToRoute(new { controller = "Passengers", action = "Index" });
            }

            var record = _context.Passangers.FirstOrDefault(s => s.PassengerId == id);

            record.FullName = newPassanger.FullName;
            record.Address = newPassanger.Address;
            record.Phone = newPassanger.Phone;

            _context.SaveChanges();

            return RedirectToRoute(new { controller = "Passengers", action = "Index" });
        }



    }
}
