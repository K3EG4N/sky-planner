namespace SkyPlanner.Entities
{
    public class Role
    {
        public Guid RoleId { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
