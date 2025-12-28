using Afc.DTOs.Responses;

namespace Afc.Business.Interfaces
{
    public interface IUserBusiness
    {
        Task<AuthResponse> SignupAsync(string username, string phoneNumber);
        Task<AuthResponse> LoginAsync(string phoneNumber);
        Task<AuthResponse> VerifyOtpAsync(string phoneNumber, string otp);
    }
}