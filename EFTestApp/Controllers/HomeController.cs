using EFTestApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace EFTestApp.Controllers
{
    public class HomeController : Controller
    {
       

        public ApplicationDB db;

        public HomeController( ApplicationDB db)
        {
            this.db = db;
        }

        
        public IActionResult Index()
        {
            var ret = db.People.ToList();






            return View(ret);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Person person)
        {

            db.People.AddRange(person);

            
            db.SaveChanges();

            return RedirectToAction("Index");

            
            

            
        }

        

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Person person = db.People.Find(id);

            db.People.Remove(person);

            db.SaveChanges();
            

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Person person)
        {
            if (person.Name == "admin" && person.Age == 12345)
            {
                List<Claim> claims = new List<Claim>
                {
                     new Claim(ClaimTypes.Name, "admin"),
                     new Claim(ClaimTypes.Email, "admin@mail.ru")

                     
                };
                var identity = new ClaimsIdentity(claims,"MyCookieAuth");

                var principal = new ClaimsPrincipal(identity);

                HttpContext.SignInAsync("MyCookieAuth", principal);

                RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }
       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}