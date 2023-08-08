using AutoMapper.Extensions.ExpressionMapping;
using CaseBL.ImplementationsOfManager;
using CaseBL.InterfacesOfManager;
using CaseDL;
using CaseDL.ImplementationsOfRepo;
using CaseDL.InterfacesOfRepo;
using CaseEL.Mappings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace CaseUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<MyContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Local"));

            });

            // Add services to the container.
            builder.Services.AddAutoMapper(x =>
            {
                x.AddExpressionMapping();
                x.AddProfile(typeof(Maps));
            });


            //DI yaþam döngüleri

            builder.Services.AddScoped<IAdayCvRepo, AdayCvRepo>();
            builder.Services.AddScoped<IAdayCvManager, AdayCvManager>();

            builder.Services.AddScoped<ISicilRepo, SicilRepo>();
            builder.Services.AddScoped<ISicilManager, SicilManager>();

            builder.Services.AddScoped<ISicilUcretRepo, SicilUcretRepo>();
            builder.Services.AddScoped<ISicilUcretManager, SicilUcretManager>();

            builder.Services.AddScoped<ISicilOgrenimRepo, SicilOgrenimRepo>();
            builder.Services.AddScoped<ISicilOgrenimManager, SicilOgrenimManager>();


            builder.Services.AddControllersWithViews();

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
        }
    }
}