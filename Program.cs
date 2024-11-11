using csharp_reference.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
// az adatbázis kivételeket kezelõ oldal hozzáadása fejlesztési módban
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// az opciókban beállítjuk, hogy a fiók csak megerõsített email cím után legyen használható
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>(); // adatbázis tárolás az Identity rendszer számára
builder.Services.AddControllersWithViews(); // a vezérlõk és nézetek támogatásának hozzáadása

var app = builder.Build(); // az alkalmazás építése, ami elindítja a szervert

// HTTP beállítása
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint(); // migrációs végpont használata fejlesztési módban az adatbázis frissítéséhez
}
else
{
    app.UseExceptionHandler("/Home/Error"); // hibaoldal beállítása hibakezeléshez éles környezetben
    app.UseHsts(); // HSTS (HTTP Strict Transport Security) beállítása biztonság érdekében, 30 napos alapértékkel
}

app.UseHttpsRedirection(); // minden kérést átirányít https-re
app.UseStaticFiles(); // statikus fájlok elérése engedélyezve (pl. képek, CSS, JavaScript fájlok)

app.UseRouting(); // útvonalak konfigurálásának bekapcsolása

app.UseAuthorization(); // engedélyezési szolgáltatás használata

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // alapértelmezett útvonal beállítása: Home vezérlõ, Index akció, opcionális id paraméter
app.MapRazorPages(); // Razor Pages útvonalak engedélyezése

app.Run(); // az alkalmazás elindítása
