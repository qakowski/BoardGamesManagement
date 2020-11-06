using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BoardGamesManagement.Database;
using BoardGamesManagement.Database.NHibernateDb;
using BoardGamesManagement.Domain;
using BoardGamesManagement.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BoardGamesManagement
{
    public class Startup
    {
        private ILifetimeScope AutofacContainer { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSwagger();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly()).AsImplementedInterfaces();
#if EF


            builder.RegisterContext<BoardGamesContext>();
            builder.RegisterRepository<Game>();
            builder.RegisterRepository<GameHistory>();
#elif NHIB
            builder.RegisterSessionFactory(Configuration.GetSection("DatabaseOptions").GetValue<string>("ConnectionString"));
            builder.RegisterSession();
            builder.RegisterNHibRepository<Game>();
            builder.RegisterNHibRepository<GameHistory>();
#endif
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/GamesList/ErrorPage");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();
            app.UseSwagger();

            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint("v1/swagger.json", "v1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default", 
                    pattern: "{controller=GamesList}/{action=Index}/{id?}");
            });
        }
    }
}
