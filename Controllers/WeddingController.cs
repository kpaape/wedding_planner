using Microsoft.EntityFrameworkCore;
using WeddingPlanner.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
namespace WeddingPlanner.Controllers
{
    public class WeddingController : Controller
    {
        private WeddingContext dbContext;
    
        public WeddingController(WeddingContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            if(ViewBag.UserId == null)
            {
                return RedirectToAction("Index", "Logins");
            }
            var AllWeddings = dbContext.Weddings
                .Include(w => w.responses)
                .OrderByDescending(w => w.date).ToList();
            return View(AllWeddings);
        }

        [HttpPost("AddWedding")]
        public IActionResult AddWedding(Wedding NewWedding)
        {
            if(ModelState.IsValid)
            {
                int? UserId = HttpContext.Session.GetInt32("UserId");
                if(UserId == null)
                {
                    return View("Index", "Logins");
                }
                NewWedding.user_id = (int)UserId;
                dbContext.Weddings.Add(NewWedding);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("CreateWedding");
        }

        [HttpGet("CreateWedding")]
        public IActionResult CreateWedding()
        {
            return View("CreateWedding");
        }

        [HttpGet("AddRSVP/{rsvp_to}")]
        public IActionResult AddRSVP(int rsvp_to)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if(UserId == null)
            {
                return View("Index", "Logins");
            }
            var SelectedWedding = dbContext.Weddings
                .FirstOrDefault(w => w.wedding_id == rsvp_to);
            var User = dbContext.Users
                .FirstOrDefault(u => u.user_id == UserId);
            // I have no idea how to create a response
            return RedirectToAction("Index");
        }
    }
}