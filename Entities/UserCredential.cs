namespace SkyPlanner.Entities
{
    public class UserCredential
    {
        public Guid UserCredentialId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
