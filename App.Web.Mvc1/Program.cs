using App.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// Entityframework iþlemlerini yapabilmek aþaðýdaki bu satýrý ekliyoruz. Veritabaný yapýlandýrmasýný yapmýþ olduk.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnection")));//AppDbContext için connection string baðlamasý yaptýk. "Configuration" ile "appsettings.json" dosyasýna ulaþýrýz. "GetConnectionString" ile appsettings içerisindeki "ConnectionStrings" altýndaki verilere ulaþýrýz ve orada hangi connection string'i kullanacaksak onun ismini veririz.(SqlConnection).


// Authentication : Oturum açma servisi
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
    x.LoginPath = "/Admin/Login"; // giriþ yapma sayfasý
    x.AccessDeniedPath = "/AccessDenied"; // giriþ yapan kullanýcýnýn admin yetkisi yoksa AccessDenied sayfasýna yönlendir
    x.LogoutPath = "/Admin/Login/Logout"; // çýkýþ sayfasý
    x.Cookie.Name = "Administrator"; // oluþacak kukinin adý
    x.Cookie.MaxAge = TimeSpan.FromDays(1); // oluþacak kukinin yaþam süresi
});

// Authorization : Yetkilendirme
builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("AdminPolicy", policy => policy.RequireClaim("Role", "Admin"));
    x.AddPolicy("UserPolicy", policy => policy.RequireClaim("Role", "User"));
});


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "admin",
    pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "custom",
    pattern: "{customurl?}/{controller=Home}/{action=Index}/{id?}");

app.Run();
