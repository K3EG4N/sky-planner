namespace SkyPlanner.Entities
{
    public class AirplaneStatus
    {
        public Guid AirplaneStatusId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }
}
