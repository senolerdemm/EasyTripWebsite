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
        public string Username { get; set; }

        [Required]
        public string Comment { get; set; }

       
        public string Date { get; set; } = DateTime.Now.ToString("dd/MM/yyyy");

        [Required]
        public int FreeTravelGuidesId { get; set; }

        public virtual FreeTravelGuides? FreeTravelGuides { get; set; }
    }
}