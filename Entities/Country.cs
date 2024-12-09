namespace SkyPlanner.Entities
{
    public class Country
    {
        public Guid CountryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Abbreviation { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
