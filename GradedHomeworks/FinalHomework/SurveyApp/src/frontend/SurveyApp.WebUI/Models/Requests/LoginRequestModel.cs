using System.ComponentModel.DataAnnotations;

namespace SurveyApp.WebUI.Models.Requests
{
    public class LoginRequestModel
    {
        [Required(ErrorMessage = "Geçerli mail adresi giriniz")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre giriniz")]
        public string Password { get; set; }
    }
}
