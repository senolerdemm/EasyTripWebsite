using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EasyTripProject.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyTripProject.Controllers
{
    [Route("[controller]")]
    public class AboutController : Controller
    {
        private readonly ILogger<AboutController> _logger;
        private readonly Context _context;

        public AboutController(ILogger<AboutController> logger , Context context)
        {
            _logger = logger;
            _context = context;
        }
        

        public async Task<IActionResult> Index()
        {
            if (_context.Abouts == null)
            {
                return Problem("Entity set 'Context.Abouts' is null.");
            }
            var about = await _context.Abouts.ToListAsync();
            return View(about);
        }

        
    }
}