using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Jawwi.Models
{
    public class User : ModelBase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }


        public virtual ICollection<Location> Locations { get; set; }
    }
}