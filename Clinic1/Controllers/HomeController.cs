using Clinic1.Data;
using Clinic1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Clinic1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ClinicDbContext dbcontext;
        private readonly IWebHostEnvironment _environment;
        public HomeController(ClinicDbContext context, IWebHostEnvironment environment)
        {
            dbcontext = context;
            _environment = environment;
        }

        public IActionResult Index()
        {
            //session
            var name = HttpContext.Session.GetString("Name");
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("SignUp", "Home");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }              
        public IActionResult SignUp()
        {
            return View(new SignUp());
        }
        [HttpPost]
        public async Task<IActionResult> SignUpAsync([Bind("Fname,Lname,Email,Password,ConfirmPassword,Phone,Age,gender,img")] SignUp user ,IFormFile img)
        {

            string path = Path.Combine(_environment.WebRootPath, "Img"); // wwwroot/Img/
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (img != null)
            {
                path = Path.Combine(path, img.FileName); // for exmple : /Img/Photoname.png
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await img.CopyToAsync(stream);
                    ViewBag.Message = string.Format("<b>{0}</b> uploaded.</br>", img.FileName.ToString());
                }
                user.img = img.FileName.ToString();
            }
            else
            {
                user.img = "flower.jpg"; // to save the default image path in database.
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var userSignup = dbcontext.signUps.FirstOrDefault(u => u.Email == user.Email);
                    if (userSignup == null)
                    {
                        dbcontext.signUps.Add(user);
                        dbcontext.SaveChanges();
                        return RedirectToAction("LogIn", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "You are already Log In");
                        return RedirectToAction("LogIn", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid ");
                }
            }
            catch (Exception ex) { ViewBag.exc = ex.Message; }

            return View(user);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LogIn());
        }
        [HttpPost]
        public IActionResult Login(LogIn user1)
        {
            if (ModelState.IsValid)
            {

                var user = dbcontext.signUps.FirstOrDefault(u => u.Email == user1.EmailAddress);
                if (user != null && user.Password == user1.Password)
                {
                    //Session
                    HttpContext.Session.SetString("Name", user.Fname);
                    var userLogin = dbcontext.logins.FirstOrDefault(u => u.EmailAddress == user1.EmailAddress);
                    if (userLogin == null)
                    {
                        dbcontext.logins.Add(user1);
                        dbcontext.SaveChanges();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "You are already Log In");
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password");
                }
            }

            return View(user1);
        }
        [HttpGet]
        public IActionResult Reservation()
        {
            return View(new Reservation());
        }
        [HttpPost]
        public IActionResult Reservation(Reservation user2)
        {
            if (ModelState.IsValid)
            {
                dbcontext.reservations.Add(user2);
                dbcontext.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(user2);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}