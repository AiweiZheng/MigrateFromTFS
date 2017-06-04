namespace GigHub.Core.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool Activated { get; set; }
        public string Description { get; set; }
        public string AccountStatus => Activated ? "Deactivate" : "Activate";
    }
}