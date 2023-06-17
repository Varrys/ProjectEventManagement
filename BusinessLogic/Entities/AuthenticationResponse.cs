// AuthenticationResponse.cs
namespace BusinessLogic.Entities
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }
        public AuthDto Auth { get; set; }
    }
}