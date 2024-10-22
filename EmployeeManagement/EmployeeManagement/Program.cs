using Microsoft.AspNetCore.Builder;

namespace EmployeeManagement {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();
            //app.MapGet("/", () => "Hello World!");
            //DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            //defaultFilesOptions.DefaultFileNames.Clear();
            //defaultFilesOptions.DefaultFileNames.Add("home.html");
            //app.UseDefaultFiles(defaultFilesOptions);
            //app.UseStaticFiles();
            FileServerOptions fileServerOptions = new FileServerOptions();
            fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("home.html");
            app.UseFileServer(fileServerOptions);
            app.Run();
        }
    }
}
