using App.Data;
using App.Web.Mvc1.EmailServices;
using App.Web.Mvc1.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// Entityframework i�lemlerini yapabilmek a�a��daki bu sat�r� ekliyoruz. Veritaban� yap�land�rmas�n� yapm�� olduk.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnection")));//AppDbContext i�in connection string ba�lamas� yapt�k. "Configuration" ile "appsettings.json" dosyas�na ula��r�z. "GetConnectionString" ile appsettings i�erisindeki "ConnectionStrings" alt�ndaki verilere ula��r�z ve orada hangi connection string'i kullanacaksak onun ismini veririz.(SqlConnection).
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnection")));
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(options =>
{
    // password
    options.Password.RequireDigit = true;// �ifrede mutlaka say�sal bir de�er girmek zorunda true oldu�u i�in.
    options.Password.RequireLowercase = true;// �ifrede mutlaka k���k harf olmak zorunda
    options.Password.RequireUppercase = true;// �ifrede mutlaka b�y�k harf olmak zorunda
    options.Password.RequiredLength = 6;// �ifre min 6 karakter olmak zorunda
    options.Password.RequireNonAlphanumeric = true;// @, _ gibi i�aretler olmak zorunda

    // lockout(kullan�c�n�n hesab�n�n kilitlenip kilitlenmemesi ile ilgili)
    options.Lockout.MaxFailedAccessAttempts = 5;// Kullan�c� max 5 kez yanl�� bilgi girebilir, sonras�nda hesap kitlenir
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);// 5 dk kilitli kald�ktan sonra giri� yapmaya devam eder
    options.Lockout.AllowedForNewUsers = true;// Tekrardan giri� yapmas�na izin vermek i�in bunu aktif etmeliyiz.


    //options.User.AllowedUserNameCharacters = "";(kullan�c� ad� i�erisinde kullan�lmas�n� yada kullan�lmamas�n� istedi�iniz karakter tan�mlamas� yap�l�r)
    options.User.RequireUniqueEmail = true;// Her kullan�c�n�n bir birinden farkl� email adresinin olmas� gerekiyor. Ayn� mail adresi ile iki kullan�c� olamaz

    options.SignIn.RequireConfirmedEmail = true;// Kullan�c� �ye olduktan sonra mutlaka hesab�n� onaylamas� laz�m. Onay mailinden onaylama yap�lmas� laz�m
    options.SignIn.RequireConfirmedPhoneNumber = false;// Kullan�c� �ye olduktan sonra verdi�i telefon �zerinden onay olmas� gerekmez, "true" olursa gerekiyor
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Auth/Login";
    options.LogoutPath = "/Auth/Logout";
    options.AccessDeniedPath = "/Auth/Accessdenied";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
    options.Cookie = new CookieBuilder
    {
        HttpOnly = true,
        Name = ".AppMvcBlog.Security.Cookie",
        SameSite = SameSiteMode.Strict
    };
});

builder.Services.AddScoped<IEmailSender, SmtpEmailSender>(i => new SmtpEmailSender(
    builder.Configuration["EmailSender:Host"],
    builder.Configuration.GetValue<int>("EmailSender:Port"),
    builder.Configuration.GetValue<bool>("EmailSender:EnableSSL"),
    builder.Configuration["EmailSender:UserName"],
    builder.Configuration["EmailSender:Password"]
));


var app = builder.Build();


// Migration yap�ld�ktan sonra veritaban�n�n otomatik olu�mas� i�in a�a��daki kodlar� yazar�z.
using (var scope = app.Services.CreateScope())
{
    using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "custom",
    pattern: "{customurl?}/{controller=Home}/{action=Index}/{id?}");


var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    SeedIdentity.Seed(userManager, roleManager, configuration).Wait();
}
    app.Run();
