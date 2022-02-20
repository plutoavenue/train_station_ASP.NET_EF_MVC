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
    public class BookingController : Controller
    {
        private readonly AppDbContext _context;

        public BookingController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {


            var tickets = (from ticket in _context.Tickets
                           join passenger in _context.Passangers on ticket.PassengerId equals passenger.PassengerId
                           join train in _context.Trains on ticket.TrainId equals train.TrainId
                           join car in _context.Cars on ticket.CarId equals car.CarId
                           join station in _context.Stations on ticket.StationId equals station.StationId

                           select new BookingViewModel()
                           {
                               TicketId = ticket.TicketId,
                               PassengerId = ticket.PassengerId,
                               TrainId = ticket.TrainId,
                               CarId = ticket.CarId,
                               StationId = ticket.StationId,

                               FullName = passenger.FullName,
                               //   Address = passenger.Address,
                               Phone = passenger.Phone,

                               Train = train.Train,
                               //  TrainType = train.TrainType,

                               Car = car.Car,
                               CarType = car.CarType,
                               ExtraCarFee = car.ExtraCarFee,

                               DepartureDate = ticket.DepartureDate,
                               DepartureTime = ticket.DepartureTime,
                               ArrivalTime = ticket.ArrivalTime,

                               Station = station.Station,
                               //    Distance = station.Distance,
                               Cost = station.Cost,

                               ExtraTimeFee = ticket.ExtraTimeFee
                           }).ToList();

            return View(tickets);

        }

        [HttpPost]
        public IActionResult AddTicket(Tickets newTicket)
        {

            _context.Tickets.Add(new Tickets
            {
                TicketId = _context.Tickets.Max(item => item.TicketId) + 1,
                PassengerId = newTicket.PassengerId,
                TrainId = newTicket.TrainId,
                CarId = newTicket.CarId,
                DepartureDate = newTicket.DepartureDate,
                DepartureTime = newTicket.DepartureTime,
                ArrivalTime = newTicket.ArrivalTime,
                StationId = newTicket.StationId,
                ExtraTimeFee = newTicket.ExtraTimeFee
            });

            _context.SaveChanges();
            return RedirectToRoute(new { controller = "Booking", action = "Index" });

        }

        public IActionResult AddPosition()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> DeleteTicket(Int32 ticketId)
        {
            var record = _context.Tickets.Find(ticketId);
            _context.Tickets.Remove(record);

            await _context.SaveChangesAsync();

            return RedirectToRoute(new { controller = "Booking", action = "Index" });

        }

        [HttpGet]
        public IActionResult EditTicket(Int32 ticketId)
        {
            if (ticketId == 0)
                return RedirectToRoute(new { controller = "Booking", action = "Index" });

            var record = _context.Tickets.FirstOrDefault(s => s.TicketId == ticketId);


            if (record == null)

            {
                return RedirectToRoute(new { controller = "Booking", action = "Index" });
            }


            return View("EditTicket", record);
        }

        [HttpPost]
        public IActionResult EditTicket(Tickets newTicket, Int32 id)
        {
            if (newTicket == null)
            {
                return RedirectToRoute(new { controller = "Booking", action = "Index" });
            }

            var record = _context.Tickets.FirstOrDefault(s => s.TicketId == id);

            record.PassengerId = newTicket.PassengerId;
            record.TrainId = newTicket.TrainId;
            record.CarId = newTicket.CarId;
            record.DepartureDate = newTicket.DepartureDate;
            record.DepartureTime = newTicket.DepartureTime;
            record.ArrivalTime = newTicket.ArrivalTime;
            record.StationId = newTicket.StationId;
            record.ExtraTimeFee = newTicket.ExtraTimeFee;


            _context.SaveChanges();

            return RedirectToRoute(new { controller = "Booking", action = "Index" });
        }
    }
}
