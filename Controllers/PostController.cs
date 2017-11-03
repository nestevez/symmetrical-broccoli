using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using chartreuse.Models;
using Newtonsoft.Json;

namespace chartreuse.Controllers
{
    public class PostController : Controller
    {
        chartreuseContext _context;
        HomeController Home;
        public PostController(chartreuseContext context)
        {
            _context = context;
            Home = new HomeController(context);
        }

        [HttpPost]
        [Route("bright_ideas/add/")]
        public IActionResult AddPost(Post incoming)
        {
            if(ModelState.IsValid)
            {
                Post post = new Post();
                post.posttext = incoming.posttext;
                post.posterid = HomeController.user.personid;
                post.created_at = DateTime.Now;
                post.updated_at = DateTime.Now; 
                _context.posts.Add(post);
                _context.SaveChanges();
                return RedirectToAction("Success","Home");
            }
            return View("Success");
        }

        [HttpGet]
        [Route("like/add/{postid}/")]
        public IActionResult AddLike(int postid)
        {
            Like like = new Like();
            like.postlikedid = postid;
            like.likerid = HomeController.user.personid;
            _context.likes.Add(like);
            _context.SaveChanges();
            like = _context.likes.Last();
            Post post = _context.posts.SingleOrDefault(pst=>pst.postid == like.postlikedid);
            post.likes.Add(like);
            _context.SaveChanges();
            return RedirectToAction("Success", "Home");
        }

        [HttpGet]
        [Route("/like/delete/{postid}/")]
        public IActionResult DeleteLike(int postid)
        {
            Like like = _context.likes.SingleOrDefault(lk => lk.postlikedid == postid && lk.likerid == HomeController.user.personid);
            Post post = _context.posts.SingleOrDefault(pst=>pst.postid == like.postlikedid);
            post.likes.Remove(like);
            _context.likes.Remove(like);
            _context.SaveChanges();
            return RedirectToAction("Success", "Home");
        }

        [HttpGet]
        [Route("/post/delete/{postid}/")]
        public IActionResult DeletePost(int postid)
        {
            Post post = _context.posts.SingleOrDefault(pst => pst.postid == postid);
            _context.posts.Remove(post);
            _context.SaveChanges();
            return RedirectToAction("Success", "Home");
        }

        [HttpGet]
        [Route("/bright_ideas/{postid}/")]
        public IActionResult PostDetails(int postid)
        {
            if(Home.logcheck(false))
            {
                return RedirectToAction("Index");
            }
            Post post = _context.posts.Include(pst=>pst.poster)
                                        .Include(pst=>pst.likes)
                                        .SingleOrDefault(pst => pst.postid == postid);
            if(post!= null)
            {
                ViewBag.post = post;
                ViewBag.postlikes = _context.likes.Include(lk=>lk.liker).Where(lk=>lk.postlikedid == postid).ToList();
                return View();
            }
            return RedirectToAction("Success","Home");
        }
        [HttpGet]
        [Route("/users/{personid}/")]
        public IActionResult UserProfile(int personid)
        {
            if(Home.logcheck(false))
            {
                return RedirectToAction("Index");
            }
            Person person = _context.users.SingleOrDefault(prsn => prsn.personid == personid);
            if(person!= null)
            {
                ViewBag.person = person;
                ViewBag.postsct = _context.posts.Where(pst => pst.posterid == personid).Count();
                ViewBag.likesct = _context.likes.Where(lk => lk.likerid == personid).Count();
                return View();
            }
            return RedirectToAction("Success");
        }
    }
}
