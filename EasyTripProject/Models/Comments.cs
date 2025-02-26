using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyTripProject.Models
{
    public class Comments
    {   
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Username { get; set; }

        [Required]
        [StringLength(500)]
        public string? Comment { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string? Mail { get; set; }

        public string? Date { get; set; } 
       
        [Required]
        public int FreeTravelGuidesId { get; set; }

        [ForeignKey("FreeTravelGuidesId")]
        public virtual FreeTravelGuides? FreeTravelGuides { get; set; }
    }
}