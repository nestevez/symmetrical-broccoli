using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace chartreuse.Models
{
    public class Post: BaseEntity
    {
        public int postid {get;set;}
        [Required]
        public string posttext {get;set;}
        public int posterid  {get;set;}
        public Person poster {get;set;}
        public List<Like> likes {get;set;}
        public DateTime created_at {get;set;}
        public DateTime updated_at {get;set;}

        public Post()
        {
            likes = new List<Like>();
        }
    }
}