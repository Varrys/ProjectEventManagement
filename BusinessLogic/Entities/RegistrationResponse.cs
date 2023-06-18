namespace BusinessLogic.Entities
{
    public class RegistrationResponse
    {
        public string Token { get; set; }
        public AuthDto Auth { get; set; }
        public string Message { get; set; }
    }
}