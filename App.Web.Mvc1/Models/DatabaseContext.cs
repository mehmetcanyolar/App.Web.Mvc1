using Microsoft.EntityFrameworkCore;

namespace App.Web.Mvc1.Models
{
	public class DatabaseContext: DbContext
	{
		public DbSet<Category> Categories { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<User> Users { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//bu metod( OnConfiguring) entity framework core içerisinde gelir ve veritabanı ayarı yapmamızı sağlar
			//Biz SqL serveri kullancagız buunu Entity Framework core a soyledik
			optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB ; datebase=App.Web.Mvc1; integrated security= true"); //burdaki server adresi değiştirilecek

			// integrated sec.= true bize kullanıcı adı vs. sormasın diye


			base.OnConfiguring(optionsBuilder);
		}
	}
}
