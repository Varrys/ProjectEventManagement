namespace BusinessLogic.Entities
{
    public class RegistrationRequest
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool Enable { get; set; }
        public string Role { get; set; } = "User";
    }
}