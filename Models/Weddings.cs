using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
    public class Wedding
    {
        [Key]
        public int wedding_id { get; set; }
        [Required]
        [MinLength(2)]
        [Display(Name="Wedder One")]
        public string wedder_one { get; set; }
        [Required]
        [MinLength(2)]
        [Display(Name="Wedder Two")]
        public string wedder_two { get; set; }
        [Required]
        [FutureDate]
        [Display(Name="Date")]
        public DateTime date { get; set; }
        [Required]
        [MinLength(2)]
        [Display(Name="Address")]
        public string address { get; set; }
        public int user_id { get; set; }
        public User creator { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime updated_at { get; set; } = DateTime.Now;
        public IEnumerable<Response> responses {get;set;}
    }
    public class Response
    {
        [Key]
        public int response_id {get;set;}
        public int wedding_id {get;set;}
        public int user_id {get;set;}
        public User guest {get;set;}
    }
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(!(value is DateTime))
            {
                return new ValidationResult("Not a valid date.");
            }
                
            DateTime date = Convert.ToDateTime(value);

            if(date < DateTime.Now)
            {
                return new ValidationResult("Date must be a future date.");
            }

            return ValidationResult.Success;

        }
}
}