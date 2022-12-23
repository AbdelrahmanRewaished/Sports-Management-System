using System.ComponentModel.DataAnnotations;

namespace Sports_Management_System.Pages.authentication
{
    public class StadManager_ClubRepWrapper
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Username { get; set; }

        public string Entity { get; set; }
        [Required]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
