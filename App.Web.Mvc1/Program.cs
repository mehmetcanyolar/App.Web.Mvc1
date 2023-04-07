using App.Data;
using App.Web.Mvc1.EmailServices;
using App.Web.Mvc1.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// Entityframework iþlemlerini yapabilmek aþaðýdaki bu satýrý ekliyoruz. Veritabaný yapýlandýrmasýný yapmýþ olduk.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnection")));//AppDbContext için connection string baðlamasý yaptýk. "Configuration" ile "appsettings.json" dosyasýna ulaþýrýz. "GetConnectionString" ile appsettings içerisindeki "ConnectionStrings" altýndaki verilere ulaþýrýz ve orada hangi connection string'i kullanacaksak onun ismini veririz.(SqlConnection).
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnection")));
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(options =>
{
    // password
    options.Password.RequireDigit = true;// Þifrede mutlaka sayýsal bir deðer girmek zorunda true olduðu için.
    options.Password.RequireLowercase = true;// Þifrede mutlaka küçük harf olmak zorunda
    options.Password.RequireUppercase = true;// Þifrede mutlaka büyük harf olmak zorunda
    options.Password.RequiredLength = 6;// Þifre min 6 karakter olmak zorunda
    options.Password.RequireNonAlphanumeric = true;// @, _ gibi iþaretler olmak zorunda

    // lockout(kullanýcýnýn hesabýnýn kilitlenip kilitlenmemesi ile ilgili)
    options.Lockout.MaxFailedAccessAttempts = 5;// Kullanýcý max 5 kez yanlýþ bilgi girebilir, sonrasýnda hesap kitlenir
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);// 5 dk kilitli kaldýktan sonra giriþ yapmaya devam eder
    options.Lockout.AllowedForNewUsers = true;// Tekrardan giriþ yapmasýna izin vermek için bunu aktif etmeliyiz.


    //options.User.AllowedUserNameCharacters = "";(kullanýcý adý içerisinde kullanýlmasýný yada kullanýlmamasýný istediðiniz karakter tanýmlamasý yapýlýr)
    options.User.RequireUniqueEmail = true;// Her kullanýcýnýn bir birinden farklý email adresinin olmasý gerekiyor. Ayný mail adresi ile iki kullanýcý olamaz

    options.SignIn.RequireConfirmedEmail = true;// Kullanýcý üye olduktan sonra mutlaka hesabýný onaylamasý lazým. Onay mailinden onaylama yapýlmasý lazým
    options.SignIn.RequireConfirmedPhoneNumber = false;// Kullanýcý üye olduktan sonra verdiði telefon üzerinden onay olmasý gerekmez, "true" olursa gerekiyor
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


// Migration yapýldýktan sonra veritabanýnýn otomatik oluþmasý için aþaðýdaki kodlarý yazarýz.
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
