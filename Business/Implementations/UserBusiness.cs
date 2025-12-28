// Business/Implementations/UserBusiness.cs
using Afc.Core.Entities;  
using Afc.DTOs.Responses;
using Afc.Business.Interfaces;
using Afc.Business.Services;
using Microsoft.Extensions.Configuration;
using Repository.Interfaces;

namespace Afc.Business.Implementations
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepository _repository;
        private readonly JwtService _jwtService;

        public UserBusiness(IUserRepository repository, JwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }

        public async Task<AuthResponse> SignupAsync(string username, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(username))
                return new AuthResponse { Message = "Username is required" };

            if (phoneNumber.Length != 10)
                return new AuthResponse { Message = "Valid 10-digit phone number is required" };

            if (await _repository.PhoneExistsAsync(phoneNumber))
                return new AuthResponse { Message = "Phone number already registered" };

            if (await _repository.UsernameExistsAsync(username))
                return new AuthResponse { Message = "Username already taken" };

            var user = new User
            {
                Username = username.Trim(),
                PhoneNumber = phoneNumber.Trim(),
                RoleId = 1, // student
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _repository.AddUserAsync(user);
            await _repository.SaveChangesAsync();

            return new AuthResponse { Message = "Signup successful. You can now login with your phone number." };
        }

        public async Task<AuthResponse> LoginAsync(string phoneNumber)
        {
            if (phoneNumber.Length != 10)
                return new AuthResponse { Message = "Valid 10-digit phone number required" };

            var user = await _repository.GetUserByPhoneAsync(phoneNumber.Trim());

            if (user == null)
                return new AuthResponse { Message = "User not found. Please signup first." };

            if ((bool)!user.IsActive)
                return new AuthResponse { Message = "Account is inactive" };

            var otp = new Random().Next(100000, 999999).ToString();
            var expiry = DateTime.UtcNow.AddMinutes(10);

            user.CurrentOtp = otp;
            user.OtpExpiresAt = expiry;
            user.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateUserAsync(user);
            await _repository.SaveChangesAsync();

            Console.WriteLine($"[TEST OTP] Username: {user.Username} | Phone: {phoneNumber} | OTP: {otp}");

            return new AuthResponse { Message = "OTP sent successfully" };
        }

        public async Task<AuthResponse> VerifyOtpAsync(string phoneNumber, string otp)
        {
            var user = await _repository.GetUserByPhoneAsync(phoneNumber.Trim());

            if (user == null)
                return new AuthResponse { Message = "Invalid phone number" };

            if (user.CurrentOtp != otp || user.OtpExpiresAt < DateTime.UtcNow)
                return new AuthResponse { Message = "Invalid or expired OTP" };

            // Clear OTP
            user.CurrentOtp = null;
            user.OtpExpiresAt = null;
            user.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateUserAsync(user);
            await _repository.SaveChangesAsync();

            var permissions = user.Role?.RolePermissions?
                    .Select(rp => rp.Permission.PermissionName)
                    .ToList() ?? new List<string>(); var token = _jwtService.GenerateToken(user, permissions);

            return new AuthResponse
            {
                Message = "Login successful",
                Token = token
            };
        }
    }
}