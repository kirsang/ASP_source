using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyFirstSite.Domain;
using MyFirstSite.Domain.Repositories.Abstract;
using MyFirstSite.Domain.Repositories.EntityFramework;
using MyFirstSite.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstSite
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;
        public void ConfigureServices(IServiceCollection services)
        {
            //Добавляем конфиг из аппсеттингс
            Configuration.Bind("Project", new Config());

            //Подключаем нужный функционал
            services.AddTransient<ITextFieldsRepository, EFTextFieldsRepository>();
            services.AddTransient<IServiceItemsRepository, EFServiceItemsRepository>();
            services.AddTransient<DataManager>();

            //Подключение контекста БД
            services.AddDbContext<AppDbContext>(x => x.UseNpgsql(Config.ConnectionString));
            //services.AddDbContext<AppDbContext>(x => x.UseNpgsql("Host=localhost;Database=bd_one; Username=postgres;Password=sa;"));

            //Настройка идентификации
            services.AddIdentity<IdentityUser, IdentityRole>(opts => {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            } ).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            //Настройка идент сокетов?
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "myCompanyAuth";
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/account/login";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;
            });

            // Добавил поддержку контроллеров для представлений
            services.AddControllersWithViews()
            // Добавляю поддержку версии?
            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();

            //Настройка политики авторизации для админов
            services.AddAuthorization(x =>
            {
                x.AddPolicy("AdminArea", policy => { policy.RequireRole("admin"); });
            });

            
           




        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Влад, не забывай про ошибки, твою мать! Порядок регистрации важен -_-!!!!!!!!!!!!!!!!!!!!
            //Возможность чекать ошибки
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Подключение поддержки статичных файлов в конфигурации
            app.UseStaticFiles();

            //Маршруты
            app.UseRouting();

            //Подключение авторизации, куки, аутентификации. Эта херня подключается между использованием маршрутов и их определением
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();
            

            // Регистрация нужных маршрутов (ендпоинтов)
             app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("фвьшт", "{area:exists}/{controller=Home}/{action=Index}/{id?}");//Маршрут для админа
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}" );
            });
        }
    }
}
