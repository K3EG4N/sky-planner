namespace SkyPlanner.Entities
{
    public class UserType
    {
        public Guid UserTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code {  get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
