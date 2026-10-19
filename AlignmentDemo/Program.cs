using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AlignmentDemo
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSingleton<DemoLaptop>();

            var app = builder.Build();
            app.MapPost("/laptops/demo/checkout", (DemoLaptop laptop) =>
                laptop.TryCheckOut() ? Results.Ok() : Results.Conflict());
            app.MapPost("/laptops/demo/report-return", (DemoLaptop laptop) =>
                laptop.TryReportReturn() ? Results.Ok() : Results.Conflict());
            app.MapPost("/laptops/demo/confirm-receipt", (DemoLaptop laptop) =>
                laptop.TryConfirmReceipt() ? Results.Ok() : Results.Conflict());

            app.Run();
        }
    }
}
