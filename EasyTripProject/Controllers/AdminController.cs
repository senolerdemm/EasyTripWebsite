using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EasyTripProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace EasyTripProject.Controllers
{
    
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly Context _context;

        public AdminController(ILogger<AdminController> logger, Context context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            _logger.LogInformation($"User {User.Identity?.Name} accessed Admin Index");
            var blogs = _context.FreeTravelGuides?
                .OrderByDescending(x => x.Date)
                .ToList() ?? new List<FreeTravelGuides>();
            return View(blogs);
        }

        [HttpGet]
        [Route("NewBlog")]
        public IActionResult NewBlog()
        {
            return View();
        }

        [HttpPost]
        [Route("NewBlog")]
        public IActionResult NewBlog(FreeTravelGuides blog)
        {
            if (ModelState.IsValid && _context.FreeTravelGuides != null)
            {
                try
                {
                    // Set the date to UTC
                    blog.Date = DateTime.UtcNow;
                    
                    _context.FreeTravelGuides.Add(blog);
                    _context.SaveChanges();
                    
                    TempData["Success"] = "Blog successfully added!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error saving blog: {ex.Message}");
                    ModelState.AddModelError("", "Error saving blog. Please try again.");
                }
            }
            return View(blog);
        }

        [HttpPost] // GET yerine POST kullanacağız
        [Route("DeleteBlog/{id}")]
        public IActionResult DeleteBlog(int id)
        {
            try
            {
                var blog = _context.FreeTravelGuides?.FirstOrDefault(x => x.Id == id);
                if (blog == null || _context.FreeTravelGuides == null)
                {
                    return NotFound();
                }

                _context.FreeTravelGuides.Remove(blog);
                _context.SaveChanges();
                TempData["Success"] = "Blog successfully deleted!";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting blog: {ex.Message}");
                TempData["Error"] = "Error deleting blog.";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("EditBlog/{id}")]
        public IActionResult EditBlog(int id)
        {
            var blog = _context.FreeTravelGuides?.FirstOrDefault(x => x.Id == id);
            if (blog == null || _context.FreeTravelGuides == null)
            {
                return NotFound();
            }
            return View(blog);
        }
        [HttpPost]
        [Route("EditBlog/{id}")]
        public IActionResult EditBlog(int id, FreeTravelGuides blog)
        {
            if (ModelState.IsValid && _context.FreeTravelGuides != null)
            {
                try
                {
                    var existingBlog = _context.FreeTravelGuides.FirstOrDefault(x => x.Id == id);
                    if (existingBlog == null)
                    {
                        return NotFound();
                    }
                    existingBlog.Header = blog.Header;
                    existingBlog.Description = blog.Description;
                    existingBlog.ImageURL = blog.ImageURL;
                    _context.SaveChanges();
                    TempData["Success"] = "Blog successfully updated!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error updating blog: {ex.Message}");
                    ModelState.AddModelError("", "Error updating blog. Please try again.");
                }
            }
            return View(blog);
        }

        [HttpGet]
        [Route("Comments")]
        public IActionResult Comments()
        {
            var comments = _context.Comments?
                .Include(c => c.FreeTravelGuides)
                .OrderByDescending(c => c.Date)
                .ToList() ?? new List<Comments>();
            return View(comments);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("DeleteComment/{id}")]
        public IActionResult DeleteComment(int id)
        {
            try
            {
                var comment = _context.Comments?.FirstOrDefault(x => x.Id == id);
                if (comment == null || _context.Comments == null)
                {
                    TempData["Error"] = "Comment not found.";
                    return RedirectToAction(nameof(Comments));
                }

                _context.Comments.Remove(comment);
                _context.SaveChanges();
                TempData["Success"] = "Comment successfully deleted!";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting comment: {ex.Message}");
                TempData["Error"] = "Error deleting comment.";
            }
            return RedirectToAction(nameof(Comments));
        }
    }
}