using App.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace App.Data
{
    public static class DbSeeder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category() { Id = 1, Name = "Gezi", Description = "İyi Gezi" },
                new Category() { Id = 2, Name = "Yemek", Description = "Güzel Yemek" },
                new Category() { Id = 3, Name = "Yazılım", Description = "Süper Yazılım" }
                );
            modelBuilder.Entity<User>().HasData(
                new User() { Id = 1, Email = "admin@gmail.com", Name = "admin", Password = "123", City = "İstanbul", Phone = "1" },
                new User() { Id = 2, Email = "admin1@gmail.com", Name = "admin1", Password = "123", City = "Ankara", Phone = "2" }
                );
            modelBuilder.Entity<Post>().HasData(
                new Post() { Id = 1, Title = "İçerik1" , Content = "İçerik1" , UserId = 1, CategoryId = 1 },
                new Post() { Id = 2, Title = "İçerik2" , Content = "İçerik2" , UserId = 2, CategoryId = 2 },
                new Post() { Id = 3, Title = "İçerik3" , Content = "İçerik3" , UserId = 1, CategoryId = 3 },
                new Post() { Id = 4, Title = "İçerik4" , Content = "İçerik4" , UserId = 2, CategoryId = 1 },
                new Post() { Id = 5, Title = "İçerik5" , Content = "İçerik5" , UserId = 1, CategoryId = 2 },
                new Post() { Id = 6, Title = "İçerik6" , Content = "İçerik6" , UserId = 2, CategoryId = 3 }
                );
            //modelBuilder.Entity<CategoryPost>().HasData(
            //    new CategoryPost() { Id = 1, CategoryId = 1 , PostId = 1 },
            //    new CategoryPost() { Id = 2, CategoryId = 1 , PostId = 2 },
            //    new CategoryPost() { Id = 3, CategoryId = 2 , PostId = 1 },
            //    new CategoryPost() { Id = 4, CategoryId = 2 , PostId = 2 },
            //    new CategoryPost() { Id = 5, CategoryId = 3 , PostId = 1 },
            //    new CategoryPost() { Id = 6, CategoryId = 3 , PostId = 2 }
            //    );
        }
    }
}
