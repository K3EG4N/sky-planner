namespace SkyPlanner.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime MemberShipDate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
