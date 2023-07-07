using System.ComponentModel.DataAnnotations;

namespace SurveyApp.WebUI.Models.Requests
{
    public class SignUpRequestModel
    {
        [Required(ErrorMessage = "Adınızı giriniz")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Soyadınızı giriniz")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Geçerli mail adresi giriniz")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre giriniz")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifreyi tekrar giriniz")]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor")]
        public string ConfirmPassword { get; set; }
    }
}
