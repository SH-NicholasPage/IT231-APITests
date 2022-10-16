/*
* Name: [YOUR NAME HERE]
* South Hills Username: [YOUR SOUTH HILLS USERNAME HERE]
*/
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace APITests.Server.Controllers
{
    [ApiController]
    [Route("API/[controller]")]
    public class BlogPostsController : ControllerBase
    {
        public class BlogPost
        {
            public String UserName { get; set; }
            public DateTime DateTimePosted { get; set; }
            public String Post { get; set; }//Actual text of the post

            public BlogPost(String username, DateTime dateTimePosted, String post)
            {
                this.UserName = username;
                this.DateTimePosted = dateTimePosted;
                this.Post = post;
            }
        }

        private static Dictionary<Int32, BlogPost> BlogPosts = new Dictionary<Int32, BlogPost>();

        private readonly ILogger<InventoryController> _logger;

        public BlogPostsController(ILogger<InventoryController> logger)
        {
            _logger = logger;

            if (BlogPosts.Count == 0)
            {
                BlogPosts.Add(BlogPosts.Count + 1, new BlogPost("doglover777", DateTime.Parse("Oct 8, 2022 14:46:30"), "I love dogs!! :)"));
                //TODO: Put a few more default posts in here
            }
        }

        [HttpGet]
        public IEnumerable<KeyValuePair<Int32, BlogPost>> Get(String? id)
        {
            //TODO: Your code here
            //If id is null, return all BlogPosts.
            //If id is not null and valid, return all BlogPosts specified (id might contain multiple numbers space separated).
            throw new NotImplementedException();
        }

        [HttpPost]
        public String POST(String? username, String? datetime, String? post)
        {
            //TODO: Your code here
            //Make sure you return a message so the user knows whether or not their API call was successful or not.

            //Modify this line if needed
            if(datetime != null && DateTime.TryParse(datetime, out _) == false)//String datetime is not a valid date and/or time
            {

            }

            throw new NotImplementedException();
        }

        [HttpPut]
        public String Put(String? id, String? username, String? datetime, String? post)
        {
            //TODO: Your code here
            //Make sure you return a message so the user knows whether or not their API call was successful or not.

            //Modify this line if needed
            if (datetime != null && DateTime.TryParse(datetime, out _) == false)//String datetime is not a valid date and/or time
            {

            }

            throw new NotImplementedException();
        }

        [HttpDelete]
        public String Delete(String? id)
        {
            if (String.IsNullOrEmpty(id) == true || Int32.TryParse(id, out _) == false)
            {
                return "ERROR: ID is invalid or null.";
            }
            
            if(BlogPosts.ContainsKey(Convert.ToInt32(id)) == false)
            {
                return "ERROR: ID " + id + " does not exist.";
            }

            return BlogPosts.Remove(Convert.ToInt32(id)) ? "Success!" : "ERROR: Something happened when trying to remove the item.";
        }
    }
}