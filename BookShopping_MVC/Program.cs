using BookShopping_MVC.Data;
using BookShopping_MVC.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookShopping_MVC
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services
                .AddIdentity<IdentityUser , IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()                  // here auto create views for identity like (login and regestration ...) 
                .AddDefaultTokenProviders();    // use in validation by token like check password or email 
            builder.Services.AddControllersWithViews();

            builder.Services.AddTransient<IHomeRepository, HomeRepository>(); // (DI)
            builder.Services.AddTransient<ICartRepository, CartRepository>(); // (DI)
            builder.Services.AddTransient<IUserOrderRepository, UserOrderRepository>(); // (DI)
            builder.Services.AddTransient<IStockRepository, StockRepository>(); // (DI)
            builder.Services.AddTransient<IGenreRepository, GenreRepository>(); // (DI)
            builder.Services.AddTransient<IFileService, FileService>(); // (DI)
            builder.Services.AddTransient<IBookRepository, BookRepository>(); // (DI)
            builder.Services.AddTransient<IReportRepository, ReportRepository>(); // (DI)

            var app = builder.Build();


            // create scope to DbSeeder 
            using(var scope = app.Services.CreateScope())
            {
                await DbSeeder.SeedDefaultData(scope.ServiceProvider);   // here send the provider that hase(usermanager and identity) to class DbSeeder like (DI) 
            }


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
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
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
