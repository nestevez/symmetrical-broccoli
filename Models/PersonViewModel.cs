using System.ComponentModel.DataAnnotations;
using System;

namespace chartreuse.Models
{
    public class PersonViewModel : BaseEntity
    {
        public RegisterViewModel register {get;set;}
        public LoginViewModel login {get;set;}
    }

    public class LoginViewModel : BaseEntity
    {

        [Required]
        [EmailAddress]
        [Display(Name="Email")]
        public string email {get;set;}
        [Required]
        [Display(Name="Username")]
        public string uname {get;set;}
        [Required]
        [DataType(DataType.Password)]
        [Display(Name="Password")]
        public string pw {get;set;}
    }
    public class RegisterViewModel : LoginViewModel
    {
        [Required]
        [Display(Name="First Name")]
        public string fname {get;set;}
        [Required]
        [Display(Name="Last Name")]
        public string lname {get;set;}
        [Required]
        [DataType(DataType.Date)]
        [Display(Name="Date of Birth")]
        [PastDate(ErrorMessage = "Date of birth must be in the past.")]
        public DateTime dob {get;set;}
        [Required]
        [MinLength(8)]
        [Compare("pw", ErrorMessage="Passwords must match")]
        [Display(Name="Confirm Password")]
        [DataType(DataType.Password)]
        public string cpw {get;set;}
    }
}