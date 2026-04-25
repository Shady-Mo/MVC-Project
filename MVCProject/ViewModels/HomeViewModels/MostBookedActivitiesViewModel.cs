namespace MVCProject.ViewModels.HomeViewModels {
    public class MostBookedActivitiesViewModel {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public int BookingsHistory { get; set; }
        public string? Img { get; set; }
    }
}
