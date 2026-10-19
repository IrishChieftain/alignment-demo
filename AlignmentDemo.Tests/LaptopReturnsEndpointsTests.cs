using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AlignmentDemo;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AlignmentDemo.Tests
{
    public sealed class LaptopReturnsEndpointsTests
    {
        [Fact]
        public async Task ReportingReturnChangesCheckedOutLaptopToReturnReported()
        {
            using var factory = new WebApplicationFactory<Program>();
            using var client = factory.CreateClient();
            var laptop = factory.Services.GetRequiredService<DemoLaptop>();

            await CheckOutAsync(client);
            var response = await client.PostAsync("/laptops/demo/report-return", null);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(LaptopState.ReturnReported, laptop.State);
        }

        [Fact]
        public async Task ReportingReturnNeverMakesLaptopAvailable()
        {
            using var factory = new WebApplicationFactory<Program>();
            using var client = factory.CreateClient();
            var laptop = factory.Services.GetRequiredService<DemoLaptop>();

            await CheckOutAsync(client);
            var response = await client.PostAsync("/laptops/demo/report-return", null);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEqual(LaptopState.Available, laptop.State);
        }

        [Fact]
        public async Task ConfirmingReceiptChangesReturnReportedLaptopToAvailable()
        {
            using var factory = new WebApplicationFactory<Program>();
            using var client = factory.CreateClient();
            var laptop = factory.Services.GetRequiredService<DemoLaptop>();

            await CheckOutAsync(client);
            await ReportReturnAsync(client);
            var response = await client.PostAsync("/laptops/demo/confirm-receipt", null);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(LaptopState.Available, laptop.State);
        }

        [Fact]
        public async Task CheckoutIsRejectedWhileReturnIsReportedAndStateIsPreserved()
        {
            using var factory = new WebApplicationFactory<Program>();
            using var client = factory.CreateClient();
            var laptop = factory.Services.GetRequiredService<DemoLaptop>();

            await CheckOutAsync(client);
            await ReportReturnAsync(client);
            var response = await client.PostAsync("/laptops/demo/checkout", null);

            Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
            Assert.Equal(LaptopState.ReturnReported, laptop.State);
        }

        [Fact]
        public async Task ReportingReturnIsRejectedBeforeLaptopIsCheckedOut()
        {
            using var factory = new WebApplicationFactory<Program>();
            using var client = factory.CreateClient();
            var laptop = factory.Services.GetRequiredService<DemoLaptop>();

            var response = await client.PostAsync("/laptops/demo/report-return", null);

            Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
            Assert.Equal(LaptopState.Available, laptop.State);
        }

        [Fact]
        public async Task ConfirmingReceiptIsRejectedBeforeReturnIsReported()
        {
            using var factory = new WebApplicationFactory<Program>();
            using var client = factory.CreateClient();
            var laptop = factory.Services.GetRequiredService<DemoLaptop>();

            var response = await client.PostAsync("/laptops/demo/confirm-receipt", null);

            Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
            Assert.Equal(LaptopState.Available, laptop.State);
        }

        [Fact]
        public async Task RepeatedCheckoutIsRejectedWhileReturnIsReportedAndStateIsPreserved()
        {
            using var factory = new WebApplicationFactory<Program>();
            using var client = factory.CreateClient();
            var laptop = factory.Services.GetRequiredService<DemoLaptop>();

            await CheckOutAsync(client);
            await ReportReturnAsync(client);

            var firstResponse = await client.PostAsync("/laptops/demo/checkout", null);
            var secondResponse = await client.PostAsync("/laptops/demo/checkout", null);

            Assert.Equal(HttpStatusCode.Conflict, firstResponse.StatusCode);
            Assert.Equal(HttpStatusCode.Conflict, secondResponse.StatusCode);
            Assert.Equal(LaptopState.ReturnReported, laptop.State);
        }

        [Fact]
        public async Task RepeatedReceiptConfirmationIsRejectedAfterLaptopIsAvailable()
        {
            using var factory = new WebApplicationFactory<Program>();
            using var client = factory.CreateClient();
            var laptop = factory.Services.GetRequiredService<DemoLaptop>();

            await CheckOutAsync(client);
            await ReportReturnAsync(client);
            await client.PostAsync("/laptops/demo/confirm-receipt", null);

            var firstResponse = await client.PostAsync("/laptops/demo/confirm-receipt", null);
            var secondResponse = await client.PostAsync("/laptops/demo/confirm-receipt", null);

            Assert.Equal(HttpStatusCode.Conflict, firstResponse.StatusCode);
            Assert.Equal(HttpStatusCode.Conflict, secondResponse.StatusCode);
            Assert.Equal(LaptopState.Available, laptop.State);
        }

        private static async Task CheckOutAsync(HttpClient client)
        {
            var response = await client.PostAsync("/laptops/demo/checkout", null);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        private static async Task ReportReturnAsync(HttpClient client)
        {
            var response = await client.PostAsync("/laptops/demo/report-return", null);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
