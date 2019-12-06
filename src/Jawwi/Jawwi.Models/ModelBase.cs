using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jawwi.Models
{
    public class ModelBase
    {
        [Required,DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedUtc { get; set; }       
        public DateTime? ModifiedUtc { get; set; }
    }
}
