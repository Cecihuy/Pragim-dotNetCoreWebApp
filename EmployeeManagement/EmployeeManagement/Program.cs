using EmployeeManagement.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace EmployeeManagement {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContextPool<AppDbContext>(
                option => option.UseSqlServer(
                    builder.Configuration.GetConnectionString("EmployeeDbConnection")
                )
            );

            builder.Services.AddMvc(B => B.EnableEndpointRouting = false).AddXmlSerializerFormatters();
            builder.Services.AddScoped<IEmployeeRepository, SqlEmployeeRepository>();
            var app = builder.Build();
            if(app.Environment.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseStatusCodePagesWithRedirects("Error/{0}");
            }
            app.UseStaticFiles();
            app.UseMvc(B=> B.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"));
            app.Run();
        }
    }
}
