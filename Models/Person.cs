using System;
using System.Collections.Generic;

namespace chartreuse.Models
{
    public class Person: BaseEntity
    {
        public int personid {get;set;}
        public string fname {get;set;}
        public string lname {get;set;}
        public string uname {get;set;}
        public DateTime dob {get;set;}
        public string email {get;set;}
        public string pw {get;set;}
        public DateTime created_at {get;set;}
        public DateTime updated_at {get;set;}
    }
}