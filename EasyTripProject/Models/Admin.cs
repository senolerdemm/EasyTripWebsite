using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace EasyTripProject.Models
{
    public class Admin : IdentityUser
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}