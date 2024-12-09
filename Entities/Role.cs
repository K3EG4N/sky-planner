namespace SkyPlanner.Entities
{
    public class Role
    {
        public Guid RoleId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public ICollection<UserRole> UserRoles { get; set; }
    }

    public static class ROLE_ENUM
    {
        public const string
            USER = "01",
            ADMIN = "02";
    }
}
