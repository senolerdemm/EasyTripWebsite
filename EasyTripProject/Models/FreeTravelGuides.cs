using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyTripProject.Models
{
    public class FreeTravelGuides
    {   
        public FreeTravelGuides()
        {
            Comments = new HashSet<Comments>();
        }

        [Key]
        public int Id { get; set; }
        public string? Header { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; } = DateTime.UtcNow;
        public string? ImageURL { get; set; }
       
        // Changed to non-nullable collection
        public virtual ICollection<Comments> Comments { get; set; }
    }
}