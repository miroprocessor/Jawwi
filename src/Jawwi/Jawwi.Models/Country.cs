using System;
using System.ComponentModel.DataAnnotations;

namespace Jawwi.Models
{
    public class Location : ModelBase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string CountryCode { get; set; }

        [Required]
        public string CountryName { get; set; }

        [Required]
        public int UserId { get; set; }


        public virtual User User{get; set;}
    }
}
