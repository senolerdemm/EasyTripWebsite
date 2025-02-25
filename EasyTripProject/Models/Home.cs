using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EasyTripProject.Models
{
    public class Home
    {
        [Key]
        public int Id { get; set; }
        public string ?Header{ get; set; }

        public string ?Description { get; set; }

    }
}