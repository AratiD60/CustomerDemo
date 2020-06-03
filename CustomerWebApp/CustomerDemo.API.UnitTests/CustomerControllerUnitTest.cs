using CustomerDemo.API.Controllers;
using CustomerDemo.Models;
using CustomerDemo.Repositories;
using CustomerDemo.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CustomerDemo.API.UnitTests
{
    public class CustomerControllerUnitTest
    {
       
        // protected static Mock<ICustomerService> _mockCustomerService = new Mock<ICustomerService>();
        //private ICustomerService custService;

        [Fact]
        public async Task TestGetCustomersAsync()
        {
            // Arrange
            var dbContext = DbContextMocker.GetCustomerDbContext(nameof(TestGetCustomersAsync));
            var mockRepo = new Mock<ICustomerRepository>();
            var mockService = new Mock<ICustomerService>();

            mockRepo.Setup(repo => repo.GetAll()).Returns(GetCustomerList());

            var controller = new CustomerController(mockService.Object);

            // Act
            var response = controller.GetAll();

            var okObjectResult = response as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var model = okObjectResult.Value as Models.Customer;
            Assert.NotNull(model);

            var actual = model.FirstName;

            // dbContext.Dispose();

            // Assert
            Assert.NotNull(response);
            Assert.Equal("Neal", actual);
        }

        private IOrderedQueryable<Customer> GetCustomerList()
        {
            var sessions = new List<Customer>();
            sessions.Add(new Customer
            {
                Id = "1",
                FirstName = "Neal",
                LastName = "Brown",
                Email = "abc@gmail.com",
                ContactNumber = "09876543",
                LastUpdatedDateTimeUtc = DateTime.UtcNow
            });
            sessions.Add(new Customer
            {
                Id = "2",
                FirstName = "Tom",
                LastName = "Cate",
                Email = "abcd12@gmail.com",
                ContactNumber = "0234567",
                LastUpdatedDateTimeUtc = DateTime.UtcNow
            });
            return (IOrderedQueryable<Customer>) sessions;
        }

    }
}
