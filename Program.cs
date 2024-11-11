using csharp_reference.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
// az adatb�zis kiv�teleket kezel� oldal hozz�ad�sa fejleszt�si m�dban
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// az opci�kban be�ll�tjuk, hogy a fi�k csak meger�s�tett email c�m ut�n legyen haszn�lhat�
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>(); // adatb�zis t�rol�s az Identity rendszer sz�m�ra
builder.Services.AddControllersWithViews(); // a vez�rl�k �s n�zetek t�mogat�s�nak hozz�ad�sa

var app = builder.Build(); // az alkalmaz�s �p�t�se, ami elind�tja a szervert

// HTTP be�ll�t�sa
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint(); // migr�ci�s v�gpont haszn�lata fejleszt�si m�dban az adatb�zis friss�t�s�hez
}
else
{
    app.UseExceptionHandler("/Home/Error"); // hibaoldal be�ll�t�sa hibakezel�shez �les k�rnyezetben
    app.UseHsts(); // HSTS (HTTP Strict Transport Security) be�ll�t�sa biztons�g �rdek�ben, 30 napos alap�rt�kkel
}

app.UseHttpsRedirection(); // minden k�r�st �tir�ny�t https-re
app.UseStaticFiles(); // statikus f�jlok el�r�se enged�lyezve (pl. k�pek, CSS, JavaScript f�jlok)

app.UseRouting(); // �tvonalak konfigur�l�s�nak bekapcsol�sa

app.UseAuthorization(); // enged�lyez�si szolg�ltat�s haszn�lata

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // alap�rtelmezett �tvonal be�ll�t�sa: Home vez�rl�, Index akci�, opcion�lis id param�ter
app.MapRazorPages(); // Razor Pages �tvonalak enged�lyez�se

app.Run(); // az alkalmaz�s elind�t�sa
