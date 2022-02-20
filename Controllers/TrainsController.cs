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
    public class TrainsController : Controller
    {
        private readonly AppDbContext _context;

        public TrainsController(AppDbContext context)
        {

            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Trains.ToList());
        }


        [HttpPost]
        public IActionResult AddTrain(Trains newTrain)
        {

            _context.Trains.Add(new Trains
            {
                TrainId = _context.Trains.Max(item => item.TrainId) + 1,
                Train = newTrain.Train,
                TrainType = newTrain.TrainType
            });

            _context.SaveChanges();
            return RedirectToRoute(new { controller = "Trains", action = "Index" });

        }

        public IActionResult AddTrain()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> DeleteTrain(Int32 trainId)
        {
            var record = _context.Trains.Find(trainId);
            _context.Trains.Remove(record);

            await _context.SaveChangesAsync();

            return RedirectToRoute(new { controller = "Trains", action = "Index" });

        }

        [HttpGet]
        public IActionResult EditTrain(Int32 trainId)
        {
            if (trainId == 0) return RedirectToRoute(new { controller = "Trains",
                action = "Index" });

            var record = _context.Trains.FirstOrDefault(s => s.TrainId == trainId);


            if (record == null)

            {
                return RedirectToRoute(new { controller = "Trains", action = "Index" });
            }


            return View("EditTrain", record);
        }

        [HttpPost]
        public IActionResult EditTrain(Trains newTrain, Int32 id)
        {
            if (newTrain == null)
            {
                return RedirectToRoute(new { controller = "Trains", action = "Index" });
            }

            var record = _context.Trains.FirstOrDefault(s => s.TrainId == id);

            record.Train = newTrain.Train;
            record.TrainType = newTrain.TrainType;

            _context.SaveChanges();

            return RedirectToRoute(new { controller = "Trains", action = "Index" });
        }

    }
}
