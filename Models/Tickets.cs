using System;

namespace lab_4.Models
{
    public class Tickets
    {
        public Int32 TicketId { get; set; }
        public Int32 PassengerId { get; set; }
        public Int32 TrainId { get; set; }
        public Int32 CarId { get; set; }
        public DateTime DepartureDate { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public Int32 StationId { get; set; }
        public double? ExtraTimeFee { get; set; }

    }
}
