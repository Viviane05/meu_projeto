using meu_projeto.Web.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace meu_projeto.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configura��o do DbContext com SQL Server
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Configura��o do MVC
            services.AddControllersWithViews();

            // Adiciona suporte a sess�o (opcional)
            services.AddSession();

            // Configura��o do Identity (caso esteja usando autentica��o)
            // services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //     .AddEntityFrameworkStores<DataContext>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Usar autentica��o (se estiver usando Identity)
            // app.UseAuthentication();
            app.UseAuthorization();

            // Usar sess�o (se configurado)
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                // Rota para �reas (se estiver usando)
                // endpoints.MapControllerRoute(
                //     name: "areas",
                //     pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                // Rota para Razor Pages (se estiver usando)
                // endpoints.MapRazorPages();
            });
        }
    }
}