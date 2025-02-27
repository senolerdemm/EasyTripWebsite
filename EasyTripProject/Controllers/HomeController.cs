using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EasyTripProject.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EasyTripProject.Controllers;


public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly Context _context;  
    public HomeController(ILogger<HomeController> logger , Context context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var blogs = _context.FreeTravelGuides?.ToList() ?? new List<FreeTravelGuides>();
        return View(blogs);
    }
    public PartialViewResult PartialView1 ()
    {
        var recentPosts = _context.FreeTravelGuides?
            .OrderByDescending(x => x.Date)
            .Take(3)
            .ToList();
        return PartialView(recentPosts);
    }
    public PartialViewResult PartialView2 ()
    {
        var mostPopularPosts = _context.FreeTravelGuides?
            .OrderByDescending(x => x.Comments.Count)
            .Take(3)
            .ToList();
        return PartialView(mostPopularPosts);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
