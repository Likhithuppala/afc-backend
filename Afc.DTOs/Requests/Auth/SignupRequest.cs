// Afc.DTOs/Requests/Auth/SignupRequest.cs
namespace Afc.DTOs.Requests.Auth
{
    public class SignupRequest
    {
        public string Username { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}