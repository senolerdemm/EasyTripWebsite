using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EasyTripProject.Models
{
    public class Adress
    {   
        [Key]
        public int Id { get; set; }
        public string ?Header{ get; set; }
        public string ?Description { get; set; }
        public string ?Address { get; set; }
        public string ?Phone { get; set; }
        public string ?Email { get; set; }
        public string ?Location { get; set; }

        
    }
}