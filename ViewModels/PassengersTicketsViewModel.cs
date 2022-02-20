using lab_4.Models;
using lab_4.EFCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using lab_4.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace lab4.Controllers
{
    public class TicketI
    {
        public Int32 TicketId { get; set; }
        public Int32 PassengerId { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public Int32 TrainId { get; set; }
        public string Train { get; set; }
        public Int32 CarId { get; set; }
        public string Car { get; set; }
        public string CarType { get; set; }
        public double ExtraCarFee { get; set; }
        public DateTime DepartureDate { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public Int32 StationId { get; set; }
        public string Station { get; set; }
        public double Cost { get; set; }
        public double? ExtraTimeFee { get; set; }


      
    }

    public class TicketInfo : Controller
    {
        private readonly AppDbContext _context;

        public TicketInfo(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            var ticketsInfo = (from ticket in _context.Tickets
                           join passenger in _context.Passangers on ticket.PassengerId equals passenger.PassengerId
                           join train in _context.Trains on ticket.TrainId equals train.TrainId
                           join car in _context.Cars on ticket.CarId equals car.CarId
                           join station in _context.Stations on ticket.StationId equals station.StationId

                           select new
                           {
                               FullName = passenger.FullName,
                               Address = passenger.Address,
                               Phone = passenger.Phone,

                               Train = train.Train,
                               TrainType = train.TrainType,

                               Car = car.Car,
                               CarType = car.CarType,
                               ExtraCarFee = car.ExtraCarFee,

                               DepartureDate = ticket.DepartureDate,
                               DepartureTime = ticket.DepartureTime,
                               ArrivalTime = ticket.ArrivalTime,

                               Station = station.Station,
                               Distance = station.Distance,
                               Cost = station.Cost,

                               ExtraTimeFee = ticket.ExtraTimeFee
                           }).ToList();


            ViewBag.ticketsInfo = ticketsInfo;
            return View();
        }


    }
}
