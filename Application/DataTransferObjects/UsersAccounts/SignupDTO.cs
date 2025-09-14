using System.ComponentModel.DataAnnotations;

namespace Application.DataTransferObjects.UsersAccounts
{
    public class SignupDTO
    {
        [Required]
        public string Email { get; set; } 

        [Required]
        public string Password { get; set; }

        [Required]
        public string DisplayName { get; set; }

        public DateTime DOB {  get; set; }

    }
}
