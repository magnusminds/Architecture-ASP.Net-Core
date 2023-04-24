using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace Architecture.Dto.User
{
    public class ChangePasswordRequestDto
    {
        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Please Enter 8 to 16 With Upper Case, Lower Case and Special Character.")]
        [DataType(DataType.Password)]
        [Remote("ValidateOldPassword", "User", AdditionalFields = "Id,OldPassword", ErrorMessage = ("entered password does not match with old password"))]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }

        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Please Enter 8 to 16 With Upper Case, Lower Case and Special Character.")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string Password { get; set; }

        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Please Enter 8 to 16 With Upper Case, Lower Case and Special Character.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
