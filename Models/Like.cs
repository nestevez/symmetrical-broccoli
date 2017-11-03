using System;
using System.Collections.Generic;

namespace chartreuse.Models
{
    public class Like: BaseEntity
    {
        public int likeid {get;set;}
        public int postlikedid  {get;set;}
        public Post postliked {get;set;}
        public int likerid {get;set;}
        public Person liker {get;set;}
    }
}