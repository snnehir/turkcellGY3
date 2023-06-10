using KidegaApp.DataTransferObjects.Requests;
using KidegaApp.DataTransferObjects.Responses;
using KidegaApp.Infrastructure.Repositories.UserRepository;
using KidegaApp.Services.Helpers;
using Mapster;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace KidegaApp.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<UserLoginResponse?> ValidateUser(UserLoginRequest request)
        {
            // email format check 
            var emailCheck = Helpers.Validation.IsValidEmail(request.Email);
            if (!emailCheck)
            {
                return new UserLoginResponse() { IsSuccess=false};
            }
            // email exist check
            var user = await userRepository.GetUserByEmailAsync(request.Email);
            if (user == null)
            {
                return new UserLoginResponse() { IsSuccess = false };
            }

            // hashPassword match check
            var hashPassword = SecurityHelper.HashPassword(request.Password, user.PasswordSalt);

            if (!user.PasswordDigest.Equals(hashPassword))
            {
                return new UserLoginResponse() { IsSuccess = false };
            }

            return new UserLoginResponse() { IsSuccess = true, Email = user.Email, Role = user.Role };
        }
    }
}
