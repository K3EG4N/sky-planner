namespace SkyPlanner.Services.Response.Airplane
{
    public class AirplaneTableDTO
    {
        public Guid AirplaneId { get; set; }
        public string Model { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Airline { get; set; } = string.Empty;
        public int MaxSpeed { get; set; }
    }
}
