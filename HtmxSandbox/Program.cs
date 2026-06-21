using HtmxSandbox;
using HtmxSandbox.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

#region Add services to the container.

var services = builder.Services;

var connectionString = configuration.GetConnectionString("PostsDbContext")
                       ?? throw new InvalidOperationException("Connection string 'PostsDbContext' not found.");
services.AddDbContext<PostsDbContext>(options => options.UseSqlServer(connectionString));

services.AddControllersWithViews();

#endregion Add services to the container.

#region Configure the HTTP request pipeline.

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    // Apply pending migrations if any.
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<PostsDbContext>();
    db.Database.Migrate();

    // Seed the database with sample posts if it's empty.
    if (!db.Posts.Any())
    {
        db.Posts.AddRange(PostSeeder.Seed(70));
        db.SaveChanges();
    }
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app
    .MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets()
    ;

#endregion Configure the HTTP request pipeline.

app.Run();