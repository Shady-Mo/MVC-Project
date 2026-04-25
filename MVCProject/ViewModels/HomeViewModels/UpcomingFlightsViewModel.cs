namespace MVCProject.ViewModels.HomeViewModels {
    public class UpcomingFlightsViewModel {
        public int Id { get; set; }
        public string Airline { get; set; }
        public string DepartureAirport { get; set; }
        public string DestinationAirport { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public decimal Price { get; set; }
        public int AvailableSeats { get; set; }
    }
}
