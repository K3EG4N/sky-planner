namespace SkyPlanner.Entities
{
    public class City
    {
        public Guid CityId { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid CountryId { get; set; }
        public Country Country { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
