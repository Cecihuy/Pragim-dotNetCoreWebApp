using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddMvc(B => B.EnableEndpointRouting = false);
            var app = builder.Build();
            //app.MapGet("/", () => "Hello World!");            
            app.UseStaticFiles();            
            app.UseMvcWithDefaultRoute();
            app.Run();
        }
    }
}
