using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleJobPortal.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add the DbContext with an in-memory database
builder.Services.AddDbContext<JobPortalContext>(options =>
    options.UseInMemoryDatabase("JobPortalDB"));

// Add session services
builder.Services.AddSession();

// Add HttpContextAccessor
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();  // Add session middleware
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Seed the database with some sample job listings
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<JobPortalContext>();
        if (!context.Jobs.Any())
        {
            // Add 10 generic job listings
            for (int i = 1; i <= 10; i++)
            {
                context.Jobs.Add(new Job
                {
                    Title = $"Job Title {i}",
                    Description = $"Description for Job {i}",
                    Employer = $"Employer {i}",
                    Location = $"Location {i}",
                    PostedDate = DateTime.Now.AddDays(-i),
                    JobType = "Full-time",
                    Salary = 50000 + i * 1000
                });
            }
            context.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.Run();
