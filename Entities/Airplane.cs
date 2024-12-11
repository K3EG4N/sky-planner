namespace SkyPlanner.Entities
{
    public class Airplane
    {
        public Guid AirplaneId { get; set; }
        public string Model { get; set; } = string.Empty;
        public string Capacity { get; set; } = string.Empty;
        public Guid StatusId { get; set; }
        public AirplaneStatus Status { get; set; }
        public int MaxSpeed { get; set; }
        public Guid AirlineId { get; set; }
        public Airline Airline { get; set; }
        public string? Photo {  get; set; } 
        public bool IsActive { get; set; } = true;
    }
}
