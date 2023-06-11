namespace KidegaApp.Services
{
    public interface IUserService
    {
        Task<UserLoginResponse?> ValidateUserLogin(UserLoginRequest request);
        Task<UserLoginResponse?> ValidateUserSignUp(UserSignUpRequest request);
    }
}
