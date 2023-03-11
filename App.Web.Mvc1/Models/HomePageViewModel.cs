using App.Data.Entity;

namespace App.Web.Mvc1.Models
{
    public class HomePageViewModel
    {
        public List<Category>? Categories { get; set; }
        public List<Post>? Posts { get; set; }
        public List<PostImage>? PostsImage { get; set; }

    }
}
