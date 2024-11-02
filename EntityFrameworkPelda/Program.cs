using Sql;
using Newtonsoft.Json;
using Entities;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkPelda
{
    public class Program
    {
        static void Main(string[] args)
        {
            Context ctx = new Context();
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

            JObject JsonFile = JObject.Parse(File.ReadAllText(@".\uploadUsers.json"));

            ICollection<User> users = JsonConvert.DeserializeObject<ICollection<User>>(JsonFile["Users"].ToString())!;
            ICollection<Blog> blog = JsonConvert.DeserializeObject<ICollection<Blog>>(JsonFile["Blogs"].ToString())!;
            ICollection<Post> posts = JsonConvert.DeserializeObject<ICollection<Post>>(JsonFile["Posts"].ToString())!;

            ctx.Users.AddRange(users);
            ctx.Blogs.AddRange(blog);
            ctx.SaveChanges();
            ctx.Post.AddRange(posts);
            ctx.SaveChanges();


            ICollection<Blog> blogs = ctx.Blogs.Select(e=>
                new Blog()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Posts = ctx.Post.Where(h=>e.Id == h.Id).Include(b=>b.Author).ToList()
                }
            ).ToList();
            ;
        }
    }
}
