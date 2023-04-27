using Calendar.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddDbContext<DbContext>(
    option => option
    .UseSqlServer(builder.Configuration.GetConnectionString("Calendar"))
    );

var app = builder.Build();

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
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "create",
        pattern: "Reservations/Create",
        defaults: new { controller = "Reservations", action = "Create" });

    endpoints.MapControllerRoute(
        name: "edit",
        pattern: "Reservations/Edit/{id?}",
        defaults: new { controller = "Reservations", action = "Edit" });

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "reservations",
        pattern: "{controller=Reservations}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
       name: "about",
       pattern: "{controller=Home}/{action=About}/{id?}");

});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

