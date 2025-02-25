using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EasyTripProject.Models
{
    public class Contact
    {   [Key]
        public int Id { get; set; }
        public string ?Name { get; set; }
        public string ?Email { get; set; }
        public string ?Message { get; set; }
    }
}