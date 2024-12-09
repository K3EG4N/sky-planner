namespace SkyPlanner.Services.Response.Airplane
{
    public class AirplaneByAirportDTO
    {
        public string Airport {  get; set; } = string.Empty;
        public List<AirplaneTableDTO> Airplanes { get; set; } = [];
    }
}
