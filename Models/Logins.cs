using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WeddingPlanner.Models
{
    public class User
    {
        [Key]
        public int user_id { get; set; }

        [RequiredAttribute]
        [MinLength(2)]
        [Display(Name="First Name")]
        public string first_name { get; set; }

        [RequiredAttribute]
        [MinLength(2)]
        [Display(Name="Last Name")]
        public string last_name { get; set; }

        [RequiredAttribute]
        [EmailAddress]
        [Display(Name="Email")]
        public string email {get;set;}

        [RequiredAttribute]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Display(Name="Password")]
        public string password {get;set;}

        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime updated_at { get; set; } = DateTime.Now;

        [NotMapped]
        [DataType(DataType.Password)]
        [Display(Name="Confirm Password")]
        [Compare("password")]
        public string confirm_password {get;set;}
    }
    public class LoginUser
    {
        [Display(Name="Email")]
        [EmailAddress]
        public string email {get;set;}
        [Display(Name="Password")]
        [DataType(DataType.Password)]
        public string password {get;set;}
    }
}