namespace KidegaApp.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<UserLoginResponse?> ValidateUserLogin(UserLoginRequest request)
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

        public async Task<UserLoginResponse?> ValidateUserSignUp(UserSignUpRequest request)
        {
            // email format check 
            bool isValidEmail = Helpers.Validation.IsValidEmail(request.Email);
            if (!isValidEmail)
            {
                return new UserLoginResponse() { IsSuccess = false };
            }
            // password format check
            bool isValidPassword = Helpers.Validation.IsValidPassword(request.Password);
            if (!isValidPassword)
            {
                return new UserLoginResponse() { IsSuccess = false };
            }
            // email exist check
            var existing = await userRepository.GetUserByEmailAsync(request.Email);
            if (existing is not null)
            {
                return new UserLoginResponse() { IsSuccess = false };
            }

            // hash + salt password
            var salt = SecurityHelper.GenerateSalt(70);
            var hashPassword = SecurityHelper.HashPassword(request.Password, salt);

            // save user to db
            var user = request.Adapt<User>();

            user.PasswordDigest = hashPassword;
            user.PasswordSalt = salt;
            user.Role = "Client";
            
            await userRepository.CreateAsync(user);
            return new UserLoginResponse() { IsSuccess = true, Email = request.Email, Role = "Client" };
        }
    }
}
