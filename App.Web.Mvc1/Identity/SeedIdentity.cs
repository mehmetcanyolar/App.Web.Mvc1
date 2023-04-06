using Microsoft.AspNetCore.Identity;

namespace App.Web.Mvc1.Identity
{
    public static class SeedIdentity
    {
        public static async Task Seed(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,  IConfiguration configuration)
        {
            var username = configuration["Data:AdminUser:username"];// appsettings.json dosyası içerisindeki Data altındaki AdminUser altındaki username'yi burada tanımlarız.
            var email = configuration["Data:AdminUser:email"];
            var password = configuration["Data:AdminUser:password"];
            var role = configuration["Data:AdminUser:role"];

            if (await userManager.FindByNameAsync(username)==null)// username ile alakalı bir kullanıcı var mı ona bakarız. Eğer null ise aşağıdaki kullanıcı oluşturma işlemlerini yaparız.
            {
                await roleManager.CreateAsync(new IdentityRole(role));// ilk başta bir role bilgisi oluştururuz. Role bilgisi içerisine ise yukarıda ayarladığımız role bilgisini veririz ve oluşur.
                var user = new User()
                {
                    UserName = username,
                    Email = email,
                    FirstName = "admin",
                    LastName = "admin",
                    EmailConfirmed = true,
                };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}
