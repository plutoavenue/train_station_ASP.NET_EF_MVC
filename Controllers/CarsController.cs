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
    public class CarsController : Controller
    {
        private readonly AppDbContext _context;

        public CarsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            
            return View(_context.Cars.ToList());
        }


        public IActionResult CarTrainAsync(Int32 id)
        {
            if(id == 0)
            {
                return  RedirectToRoute(new { controller = "Cars",
                    action = "Index" });

            }
            var cars = _context.Cars.Join(_context.Trains,
                        cars => cars.TrainId,
                        trains => trains.TrainId,
                        (cars, trains) => new
                        {
                            CarId = cars.CarId,
                            TrainId = cars.TrainId,
                            Train = trains.Train,
                            TrainType = trains.TrainType,
                            Car = cars.Car,
                            CarType = cars.CarType,
                            ExtraCarFee = cars.ExtraCarFee
                        }
                        ).ToList();


            return View(cars);
        }

 

        /*  public async Task<IActionResult> DepartureDateTicketAsync(Int32 ticketId, DateTime newDate)
          {
              var record = _context.Tickets.FirstOrDefault(t => t.TicketId == ticketId);

              record.DepartureDate = newDate.Date;

              _context.Update(record);

              return View("Ticket");
          }

          public async Task<IActionResult> NewTicketForPassenger(Int32 passengerId)
          {
              var result = new PassengersTicketsViewModel() 
              { PassengerId = passengerId, Tickets = await _context.Tickets.ToListAsync() };

              return View(result);
          }

          [HttpPost]
          public async Task<IActionResult> NewTicketForPassenger(Int32 ticketId, Int32 passengerId)
          {
              var record = new Tickets() { PassengerId = passengerId, 
                  TicketId = ticketId };

              _context.Add(record);
              await _context.SaveChangesAsync();

              var result = await GetPassengerTicket(passengerId);

              return View(result);
          }

          private async Task<PassengersTicketsViewModel> GetPassengerTicket(Int32 passengerId)
          {
              var result = new PassengersTicketsViewModel() {
                  PassengerId = passengerId,
                  Tickets = await _context.Tickets.ToListAsync()
              };

              return result;
          }

          [HttpPost]
          public async Task<IActionResult> DeleteTicket(Int32 ticketId)
          {
              var record = _context.Tickets.FirstOrDefault(t => t.TicketId == ticketId);

              _context.Tickets.Remove(record);
              await _context.SaveChangesAsync();

              return RedirectToAction("NewTicketForPassenger");
          }*/
    }
}
