using HMS.Areas.Identity.Model;
using HMS.Data;
using HMS.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HMS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentity<Admin, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 0;


            })
                .AddDefaultTokenProviders()

            })

                .AddEntityFrameworkStores<ApplicationDbContext>();
                
            builder.Services.AddRazorPages();

            builder.Services.AddDbContext<HotelDbContext>(options =>
            options.UseSqlServer(connectionString));

            builder.Services.AddTransient<ICustomerService, CustomerService>();
            
            //Add auto mapper here
            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            builder.Services.AddTransient<IAdminService, AdminService>();

            var app = builder.Build();
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseStatusCodePagesWithReExecute("/Error/{0}");
            app.UseAuthorization();

            app.MapRazorPages();
            
            app.Run();
        }
    }
}
