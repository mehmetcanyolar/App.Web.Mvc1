using App.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace App.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        //public DbSet<CategoryPost> CategoriesPost { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<PostImage> PostImages { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // Burası veritabanı yapılandırma ayarlarını yapabileceğimiz metot
        //    optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB; database=AspNetMvcBlog; integrated security=true;");
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
            // Veritabanı oluşacağı zaman içerisine statik bir şekilde oluşturduğumuz entity'lerin özelliklerini içeren veriler ekleyebiliriz. "DbSeeder" diye oluşturduğumuz dosya içerisine "Seed" metodu içerisine tanımlamasını yapacağımız verileri gireriz. Veritabanı tablolarını yapılandırmasını oluşturacağımız "AppDbContext"(bu dosya) içerisinde tanımladığımız "Seed" metodunu çağırırız.
        }


        // Yukarıda tanımlanan veri tabanı yapılanması tek bir database için. İlerleyen zamanlarda farklı database provider kullanılabilir. Mysql, mssql gibi farklı "connection string" kullanılabilir. Bu "connection string" leri data içerisine saklamaktansa appsetting.json içerisinde tanımlayabiliriz. İlerleyen aşamalarda web olsun webapi olsun farklı projeler kullanılabilir. İstediğimiz bir arayüz uygulamasından ilgili "connection string"'i uygulamaya tanıtarak dışarıdan context'e "connection string" göndererek uygulamadan veri tabanına bağlantı sağlarız. Bunuda yukarıda constructor oluşturarak sağlarız. Sonrasında "appsettings.json" dosyası içerisine "ConnectionStrings" altına "MsSqlConnection" ismini verdiğimiz veritabanı özelliklerini yazarız. Sonrasında "program.cs" dosyası içerisine veritabanı yapılandırmamızı yazarız.
    }
}
