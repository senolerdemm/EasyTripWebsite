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
        public string? Username { get; set; }
        public string? Comment { get; set; }
        public string? Mail { get; set; }
        public string? Date { get; set; }
       
        public int? FreeTravelGuidesId { get; set; }
        [ForeignKey("FreeTravelGuidesId")]
        public FreeTravelGuides? FreeTravelGuides { get; set; }
        
    }
}