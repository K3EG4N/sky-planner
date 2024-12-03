namespace SkyPlanner.Services.Response
{
    public class UserTableDTO
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MemberShipDate { get; set; } = string.Empty;
    }
}
