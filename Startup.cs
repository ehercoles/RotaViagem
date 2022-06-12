using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using RotaViagem.Service;

namespace RotaViagem
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IRotaService, RotaService>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseExceptionHandler(configure =>
            {
                configure.Run(async context =>
                {
                    var ex = context.Features
                                    .Get<IExceptionHandlerFeature>()
                                    .Error;

                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync($"{ex.Message}");
                });
            });

            app.UseMvc(routes =>
            {
                app.Use(async (context, next) =>
                {
                    if (context.Request.Path == "/")
                    {
                        context.Response.Redirect("Rotas/", true);

                        return;
                    }
                    await next();
                });

                routes.MapRoute(
                    name: "rotas",
                    template: "Rotas/{name1}/name2");

                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}");
            });
        }
    }
}
