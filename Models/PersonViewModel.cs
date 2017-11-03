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
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Display(Name="Password")]
        public string pw {get;set;}
    }
    public class RegisterViewModel : LoginViewModel
    {
        [Required]
        [Display(Name="Name")]
        public string name {get;set;}
        [Required]
        [Display(Name="Alias")]
        public string uname {get;set;}
        [Required]
        [MinLength(8)]
        [Compare("pw", ErrorMessage="Passwords must match")]
        [Display(Name="Confirm Password")]
        [DataType(DataType.Password)]
        public string cpw {get;set;}
    }
}