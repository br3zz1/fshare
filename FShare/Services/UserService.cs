using FShare.Data;
using FShare.Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace FShare.Services
{
    public class UserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly DataContext _dataContext;

        public UserService(IHttpContextAccessor contextAccessor, DataContext dataContext)
        {
            _contextAccessor = contextAccessor;
            _dataContext = dataContext;
        }

        private User? _currentUser;

        public async Task<User?> GetCurrentUser()
        {
            if (_currentUser is not null) return _currentUser;

            var guidString = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == "Guid")?.Value;

            if (guidString is null) return null;

            var guid = Guid.Parse(guidString);

            var users = from user in _dataContext.Users
                        where user.Guid == guid && (user.ExpiresUtc == null || user.ExpiresUtc > DateTime.UtcNow)
                        select user;

            _currentUser = await users.FirstOrDefaultAsync();

            if (_currentUser is null)
            {
                await _contextAccessor.HttpContext!.SignOutAsync();
            }

            return _currentUser;
        }

        public async Task<User?> ValidateCredentials(string username, string password)
        {
            var users = from user in _dataContext.Users
                        where user.Username == username && user.Password == password && (user.ExpiresUtc == null || user.ExpiresUtc > DateTime.UtcNow)
                        select user;

            return await users.FirstOrDefaultAsync();
        }
    }
}
