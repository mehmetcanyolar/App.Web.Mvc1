using App.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// Entityframework iþlemlerini yapabilmek aþaðýdaki bu satýrý ekliyoruz. Veritabaný yapýlandýrmasýný yapmýþ olduk.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnection")));//AppDbContext için connection string baðlamasý yaptýk. "Configuration" ile "appsettings.json" dosyasýna ulaþýrýz. "GetConnectionString" ile appsettings içerisindeki "ConnectionStrings" altýndaki verilere ulaþýrýz ve orada hangi connection string'i kullanacaksak onun ismini veririz.(SqlConnection).

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
		   pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
		 );

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
