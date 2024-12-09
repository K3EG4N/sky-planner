namespace SkyPlanner.Entities
{
    public class Airline
    {
        public Guid AirLineId { get; set; }
        public string ShortName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime FoundedDate { get; set; }
        public Guid CityId { get; set; }
        public City City { get; set; }
        public List<Airplane> Airplanes { get; set; } = [];
        public List<AirlineAirport> AirlineAirports { get; set; } = [];
        public bool IsActive { get; set; } = true;
    }
}
