using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyTripProject.Models
{
    public class FreeTravelGuides
    {   
        [Key]
        public int Id { get; set; }
        public string? Header { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public string? ImageURL { get; set; }
       
        public ICollection<Comments>? Commentss { get; set; }
    }
}