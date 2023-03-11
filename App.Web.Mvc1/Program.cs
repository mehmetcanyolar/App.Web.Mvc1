using App.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// Entityframework i�lemlerini yapabilmek a�a��daki bu sat�r� ekliyoruz. Veritaban� yap�land�rmas�n� yapm�� olduk.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnection")));//AppDbContext i�in connection string ba�lamas� yapt�k. "Configuration" ile "appsettings.json" dosyas�na ula��r�z. "GetConnectionString" ile appsettings i�erisindeki "ConnectionStrings" alt�ndaki verilere ula��r�z ve orada hangi connection string'i kullanacaksak onun ismini veririz.(SqlConnection).

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
		   pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
		 );

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
