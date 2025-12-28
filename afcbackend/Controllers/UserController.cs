using Microsoft.AspNetCore.Mvc;
using Afc.Business.Interfaces;
using Afc.DTOs.Requests.Auth;
using Afc.DTOs.Responses;

namespace afcbackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;

        public UsersController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        [HttpPost("signup")]
        public async Task<ActionResult<AuthResponse>> Signup([FromBody] SignupRequest request)
        {
            var response = await _userBusiness.SignupAsync(request.Username, request.PhoneNumber);
            return response.Message.Contains("already") ? Conflict(response) : Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest request)
        {
            var response = await _userBusiness.LoginAsync(request.PhoneNumber);
            return response.Message.Contains("not found") ? NotFound(response) : Ok(response);
        }

        [HttpPost("verify-otp")]
        public async Task<ActionResult<AuthResponse>> VerifyOtp([FromBody] VerifyOtpRequest request)
        {
            var response = await _userBusiness.VerifyOtpAsync(request.PhoneNumber, request.Otp);
            return response.Message == "Login successful" ? Ok(response) : Unauthorized(response);
        }
    }
}