using Afc.Core.Entities;

namespace Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByPhoneAsync(string phoneNumber);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<bool> PhoneExistsAsync(string phoneNumber);
        Task<bool> UsernameExistsAsync(string username);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task SaveChangesAsync();
    }
}