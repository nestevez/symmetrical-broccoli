using System.ComponentModel.DataAnnotations;
using System;

namespace chartreuse.Models
{
    public class PostViewModel : BaseEntity
    {
        [Required]
        public string posttext {get;set;}
    }
}