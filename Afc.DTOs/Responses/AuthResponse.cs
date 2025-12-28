// Afc.DTOs/Responses/AuthResponse.cs
namespace Afc.DTOs.Responses
{
    public class AuthResponse
    {
        public string Message { get; set; } = string.Empty;
        public string? Token { get; set; }
    }
}