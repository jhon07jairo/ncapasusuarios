using Microsoft.EntityFrameworkCore;
using Usuarios.BLL.Service;
using Usuarios.DAL.DataContext;
using Usuarios.DAL.Repository;
using Usuarios.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DB_USUARIOSContext>(opciones =>
{
	opciones.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL"));
}); 

builder.Services.AddScoped<IGenericRepository<Usuario>, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
