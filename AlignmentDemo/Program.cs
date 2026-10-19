using Microsoft.AspNetCore.Builder;

namespace AlignmentDemo
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();
            app.MapGet("/", () => "AlignmentDemo is running. Add approved requirements before new features.");
            app.Run();
        }
    }
}
