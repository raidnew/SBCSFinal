using System.ComponentModel.DataAnnotations;

namespace WebClient.Auth
{
    public class UserRegistrationData
    {
        [Required, MaxLength(32)]
        public string UserName { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
