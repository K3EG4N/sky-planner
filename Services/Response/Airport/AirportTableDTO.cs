namespace SkyPlanner.Services.Response.Airport
{
    public class AirportTableDTO
    {
        public Guid AirportId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string FoundedDate { get; set; } = string.Empty;
    }
}
