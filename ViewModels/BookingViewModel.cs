using System;

namespace lab_4.ViewModels
{
    public class BookingViewModel
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
}
