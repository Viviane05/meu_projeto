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
            // Configuração do DbContext com SQL Server
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Configuração do MVC
            services.AddControllersWithViews();

            // Adiciona suporte a sessão (opcional)
            services.AddSession();

            // Configuração do Identity (caso esteja usando autenticação)
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

            // Usar autenticação (se estiver usando Identity)
            // app.UseAuthentication();
            app.UseAuthorization();

            // Usar sessão (se configurado)
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                // Rota para áreas (se estiver usando)
                // endpoints.MapControllerRoute(
                //     name: "areas",
                //     pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                // Rota para Razor Pages (se estiver usando)
                // endpoints.MapRazorPages();
            });
        }
    }
}