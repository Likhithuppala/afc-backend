// Afc.DTOs/Requests/Auth/VerifyOtpRequest.cs
namespace Afc.DTOs.Requests.Auth
{
    public class VerifyOtpRequest
    {
        public string PhoneNumber { get; set; } = string.Empty;
        public string Otp { get; set; } = string.Empty;
    }
}