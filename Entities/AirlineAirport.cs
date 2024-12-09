namespace SkyPlanner.Entities
{
    public class AirlineAirport
    {
        public Guid AirlineId { get; set; }
        public Airline Airline { get; set; }
        public Guid AirportId { get; set; }
        public Airport Airport { get; set; }
    }
}
