namespace MVCProject.ViewModels.HomeViewModels {
    public class MostBookedAccommodationsViewModel {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public decimal PricePerNight { get; set; }
        public int BookingsHistory { get; set; }
        public string? Image { get; set; }
    }
}
