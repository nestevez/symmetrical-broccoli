using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using chartreuse.Models;
using Newtonsoft.Json;
using chartreuse;

namespace chartreuse.Controllers
{
    public class HomeController : Controller
    {
        chartreuseContext _context;
        public HomeController(chartreuseContext context)
        {
            _context = context;
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            if(HttpContext.Session.GetObjectFromJson<Person>("curruser") != null)
            {
                return RedirectToAction("Success");
            }
            return View();
        }

        [HttpPost]
        [Route("/person/create/")]
        public IActionResult AddUser(PersonViewModel incoming)
        {
            RegisterViewModel registration = incoming.register;
            TryValidateModel(registration);
            Person user = new Person();
            user = _context.users.FirstOrDefault(usr => usr.email == incoming.register.email);
            if(user != null)
            {
                ViewBag.regerror = "This email already has an account.";
                return View("Index");
            }
            else if(ModelState.IsValid)
            {
                user = new Person();
                user.fname = registration.fname;
                user.lname = registration.lname;
                user.email = registration.email;
                user.dob = registration.dob;
                user.uname = registration.uname;
                user.pw = registration.pw;
                user.created_at = DateTime.Now;
                user.updated_at = DateTime.Now;                
                PasswordHasher<Person> hasher = new PasswordHasher<Person>();
                user.pw = hasher.HashPassword(user, user.pw);
                _context.users.Add(user);
                _context.SaveChanges();
                PersonViewModel nuser = new PersonViewModel{
                    login = new LoginViewModel{
                        email=user.email,
                        pw=registration.pw
                    }
                };
                return Login(nuser);
            }
            return View("Index");
        }

        [HttpPost]
        [Route("/user/login/")]
        public IActionResult Login(PersonViewModel incoming)
        {
            PasswordHasher<Person> hasher = new PasswordHasher<Person>();
            Person user = new Person();
            user = _context.users.FirstOrDefault(usr => usr.email == incoming.login.email);
            if(user!=null && 0 != hasher.VerifyHashedPassword(user, user.pw, incoming.login.pw))
            {
                HttpContext.Session.SetObjectAsJson("curruser", user);
                return RedirectToAction("Success");
            }
            ViewBag.logerror = "This email and password combination does not exist.";
            return View("Index");
        }

        [HttpGet]
        [Route("/success/")]
        public IActionResult Success()
        {
            return View();
        }
    }
}
