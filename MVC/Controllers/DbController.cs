using DataAccess.Context;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MVC.Controllers
{
    public class DbController : Controller
    {
        private readonly DContext _context;

        public DbController(DContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            var blogtags = _context.BlogTags.ToList();
            _context.BlogTags.RemoveRange(blogtags);

            var tags = _context.Tags.ToList();
            _context.Tags.RemoveRange(tags);

            var users = _context.Users.ToList();
            _context.Users.RemoveRange(users);

            var roles = _context.Roles.ToList();
            _context.Roles.RemoveRange(roles);

            if(roles.Count > 0)
            {
                _context.Database.ExecuteSqlRaw("dbcc CHECKIDENT ('Roles', RESEED, 0");
            }

            var blogs = _context.Blogs.ToList();
            _context.Blogs.RemoveRange(blogs);

            _context.SaveChanges();

            _context.Roles.Add(new Role()
            {
                Name = "Admin",
                Users = new List<User>()
                {
                    new User()
                    {
                        UserName = "emre",
                        Password = "emre",
                        IsActive = true
                    }
                }
            });

            _context.Roles.Add(new Role()
            {
                Name = "User",
                Users = new List<User>()
                {
                    new User()
                    {
                        UserName = "asya",
                        Password = "asya",
                        IsActive = true
                    }
                }
            });

            _context.Tags.Add(new Tag()
            {
                Name = "Carnivore",
            });

            _context.Tags.Add(new Tag()
            {
                Name = "Herbivore",
            });

            _context.SaveChanges();

            _context.Blogs.Add(new Blog()
            {
                DinosaurName = "Tyrannosaurus",
                Features = "Tyrannosaurus is one of the largest carnivorous dinosaurs that ever lived on Earth.",
                Score = 5,
                UserId = _context.Users.SingleOrDefault(u => u.UserName == "emre").Id,
                BlogTags = new List<BlogTag>()
                {
                    new BlogTag()
                    {
                        TagId = _context.Tags.SingleOrDefault(t => t.Name == "Carnivore").Id
                    }
                }
            });

            _context.Blogs.Add(new Blog()
            {
                DinosaurName = "Sauropod",
                Features = "Sauropod, a herbivorous creature, is known as the 'tallest'.",
                Score = 4,
                UserId = _context.Users.SingleOrDefault(u => u.UserName == "emre").Id,
                BlogTags = new List<BlogTag>()
                {
                    new BlogTag()
                    {
                        TagId = _context.Tags.SingleOrDefault(t => t.Name == "Herbivore").Id
                    }
                }
            });

            _context.Blogs.Add(new Blog()
            {
                DinosaurName = "Tiranozor",
                Features = "One of the best-known dinosaurs of the Jurassic period is the Tyrannosaurus.",
                Score = 3,
                UserId = _context.Users.SingleOrDefault(u => u.UserName == "asya").Id,
                BlogTags = new List<BlogTag>()
                {
                    new BlogTag()
                    {
                        TagId = _context.Tags.SingleOrDefault(t => t.Name == "Herbivore").Id
                    },

                    new BlogTag()
                    {
                        TagId = _context.Tags.SingleOrDefault(t => t.Name == "Carnivore").Id
                    }
                }
            });

            _context.SaveChanges();

            return View();
        }

       
    }
}
