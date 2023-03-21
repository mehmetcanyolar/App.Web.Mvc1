using App.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// Entityframework i�lemlerini yapabilmek a�a��daki bu sat�r� ekliyoruz. Veritaban� yap�land�rmas�n� yapm�� olduk.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnection")));//AppDbContext i�in connection string ba�lamas� yapt�k. "Configuration" ile "appsettings.json" dosyas�na ula��r�z. "GetConnectionString" ile appsettings i�erisindeki "ConnectionStrings" alt�ndaki verilere ula��r�z ve orada hangi connection string'i kullanacaksak onun ismini veririz.(SqlConnection).


// Authentication : Oturum a�ma servisi
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
    x.LoginPath = "/Admin/Login"; // giri� yapma sayfas�
    x.AccessDeniedPath = "/AccessDenied"; // giri� yapan kullan�c�n�n admin yetkisi yoksa AccessDenied sayfas�na y�nlendir
    x.LogoutPath = "/Admin/Login/Logout"; // ��k�� sayfas�
    x.Cookie.Name = "Administrator"; // olu�acak kukinin ad�
    x.Cookie.MaxAge = TimeSpan.FromDays(1); // olu�acak kukinin ya�am s�resi
});

// Authorization : Yetkilendirme
builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("AdminPolicy", policy => policy.RequireClaim("Role", "Admin"));
    x.AddPolicy("UserPolicy", policy => policy.RequireClaim("Role", "User"));
});


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
