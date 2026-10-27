using System.ComponentModel.DataAnnotations;

namespace NewsClientApp.Models
{
    public class LoginViewModel
    {

        [Required (ErrorMessage ="Введи логин")] // для отображения ошибки при вводе 
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
