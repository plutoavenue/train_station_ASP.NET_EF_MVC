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
    public class StationsController : Controller
    {
        private readonly AppDbContext _context;

        public StationsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Stations.ToList());
        }

        [HttpPost]
        public IActionResult AddStation(Stations newStation)
        {

            _context.Stations.Add(new Stations {
                StationId = _context.Stations.Max(item => item.StationId)+1,
                Station = newStation.Station,
                Distance = newStation.Distance,
                Cost = newStation.Cost
            } );
          
            _context.SaveChanges();
            return RedirectToRoute(new { controller = "Stations", action = "Index" });

        }

        public IActionResult AddPosition()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> DeleteStation(Int32 stationId)
        {
          //  var record = _context.Stations.FirstOrDefault(s => s.StationId == stationId);
            var record = _context.Stations.Find(stationId);
            _context.Stations.Remove(record);

            await _context.SaveChangesAsync();

            return RedirectToRoute(new { controller = "Stations", action = "Index" });

        }

        [HttpGet]
        public IActionResult EditStation(Int32 stationId)
        {
            if (stationId == 0) return RedirectToRoute(new { controller = "Stations", action = "Index" });

            var record = _context.Stations.FirstOrDefault(s => s.StationId == stationId);


            if (record == null)

            {
                return RedirectToRoute(new { controller = "Stations", action = "Index" });
            }


            return View("EditStation", record);
        }

        [HttpPost]
        public IActionResult EditStation(Stations newStation, Int32 id)
        {
            if (newStation == null)
            {
                return RedirectToRoute(new { controller = "Stations", action = "Index" });
            }

            var record = _context.Stations.FirstOrDefault(s => s.StationId == id);

            record.Station = newStation.Station;
            record.Distance = newStation.Distance;
            record.Cost = newStation.Cost;
               
            _context.SaveChanges();

            return RedirectToRoute(new { controller = "Stations", action = "Index" });
        }


    }
}
