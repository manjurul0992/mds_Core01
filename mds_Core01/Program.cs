using mds_Core01.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<mds_Core01Context>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("appCon")));
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Players}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "unknown",
    pattern: "unknown",
    defaults: new { controller = "Players", action = "Index" }
    );



app.Run();
