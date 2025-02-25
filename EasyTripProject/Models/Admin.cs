using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EasyTripProject.Models
{
    public class Admin
    { 
        [Key]
        public int Id { get; set; } 
        public string ?Username { get; set; }
        public string ?Password { get; set; }
    }
}