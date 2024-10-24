using EmployeeManagement.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddMvc(B => B.EnableEndpointRouting = false).AddXmlSerializerFormatters();
            builder.Services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();
            var app = builder.Build();          
            app.UseStaticFiles();
            app.UseMvc(B=> B.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"));
            app.Run();
        }
    }
}
