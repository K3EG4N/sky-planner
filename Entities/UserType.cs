namespace SkyPlanner.Entities
{
    public class UserType
    {
        public Guid UserTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code {  get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }

    public static class USER_TYPE_ENUM
    {
        public const string
            CLIENT = "01",
            PILOT = "02";
    }
}
