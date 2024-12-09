namespace SkyPlanner.Entities
{
    public class Airport
    {
        public Guid AirportId { get; set; }
        public string Name { get; set; }
        public Guid CityId { get; set; }
        public City City { get; set; }
        public DateTime FoundedDate { get; set; }
        public List<AirlineAirport> AirlineAirports { get; set; } = [];
        public bool IsActive { get; set; } = true;
    }
}
