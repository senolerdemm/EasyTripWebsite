using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EasyTripProject.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyTripProject.Controllers
{
    [Route("[controller]")]
    public class FreeTravelGuideController : Controller
    {
        private readonly ILogger<FreeTravelGuideController> _logger;
        private readonly Context _context;

        public FreeTravelGuideController(ILogger<FreeTravelGuideController> logger, Context context)
        {
            _logger = logger;
            _context = context;
        }
       
        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        { 
            var TravelList = _context.FreeTravelGuides?.ToList() ?? new List<FreeTravelGuides>();
            
            // Get recent posts
            var recentPosts = _context.FreeTravelGuides?
                .OrderByDescending(x => x.Date)
                .Take(3)
                .ToList();

            // Get recent comments
            var recentComments = _context.Comments?
                .Include(c => c.FreeTravelGuides)
                .OrderByDescending(c => c.Date)
                .Take(5)
                .ToList();

            ViewBag.RecentPosts = recentPosts;
            ViewBag.RecentComments = recentComments;
            
            return View(TravelList);
        }
        
        [HttpGet]
        [Route("Details/{id}")]
        public IActionResult Details(int id)
        {
            var travel = _context.FreeTravelGuides?
                .Include(f => f.Comments)
                .FirstOrDefault(x => x.Id == id);
                
            if (travel == null)
            {
                return NotFound();
            }

            // Get recent 3 posts
            var recentPosts = _context.FreeTravelGuides?
                .OrderByDescending(x => x.Date)
                .Take(3)
                .ToList();

            ViewBag.RecentPosts = recentPosts;
            return View(travel);
        }

        [HttpPost]
        [Route("AddComment")]
        public IActionResult AddComment([Bind("Username,Comment,FreeTravelGuidesId")] Comments comments)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("ModelState is invalid: " + string.Join("; ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)));
                    return RedirectToAction(nameof(Details), new { id = comments.FreeTravelGuidesId });
                }

                comments.Date = DateTime.Now.ToString("dd/MM/yyyy");
                _context.Comments?.Add(comments);
                _context.SaveChanges();
                
                TempData["Success"] = "Comment added successfully!";
                return RedirectToAction(nameof(Details), new { id = comments.FreeTravelGuidesId });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding comment: {ex.Message}");
                TempData["Error"] = "Error saving comment. Please try again.";
                return RedirectToAction(nameof(Details), new { id = comments.FreeTravelGuidesId });
            }
        }
    }
}