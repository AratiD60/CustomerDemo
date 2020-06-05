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
        protected static Mock<Customer> _mockCustomer;
        protected static List<Customer> customerList;
        protected static CustomerService customerService;
        protected static Mock<ICustomerRepository> _mockDataRepository = new Mock<ICustomerRepository>();
        protected static Mock<ICustomerService> _mockService = new Mock<ICustomerService>();

        public CustomerControllerUnitTest()
        {
            SetupTests();
        }

        private void SetupTests()
        {
            SetUpMockCustomerList();
        }


        private void SetUpMockCustomerList()
        {
            customerService = new CustomerService(_mockDataRepository.Object);
            customerList = new List<Customer>
            {
                new Customer
                {
                    Id = "1",
                    FirstName = "Neal",
                    LastName = "Brown",
                    Email = "abc@gmail.com",
                    ContactNumber = "09876543",
                    LastUpdatedDateTimeUtc = DateTime.UtcNow
                },
                new Customer
                {
                    Id = "2",
                    FirstName = "Tom",
                    LastName = "Cate",
                    Email = "abcd12@gmail.com",
                    ContactNumber = "0234567",
                    LastUpdatedDateTimeUtc = DateTime.UtcNow
                }
            };
        }

        private void SetUpMockCustomer()
        {
            _mockCustomer = new Mock<Customer>();
            _mockCustomer.Setup(x => x.Id).Returns("1");
            _mockCustomer.Setup(x => x.FirstName).Returns("MockFName");
            _mockCustomer.Setup(x => x.LastName).Returns("MonkLName");
        }

        [Fact]
        public void GetAll_Should_Return_All_Customers()
        {
            // Arrange

            _mockDataRepository.Setup(repo => repo.GetAll()).Returns(customerList);
            //_mockService.Setup(srv => srv.GetAll()).Returns(customerList);

            var controller = new CustomerController(customerService);

            // Act
            var response = controller.GetAll();

            var okObjectResult = response as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var model = okObjectResult.Value as List<Customer>;
            Assert.NotNull(model);

            var actual = model[0].FirstName;

            // dbContext.Dispose();

            // Assert
            Assert.NotNull(response);
            Assert.Equal(2, model.Count);
            Assert.Equal("Neal", actual);
        }

        [Fact]
        public async Task Create_Should_Add_New_Customer()
        {
            // Arrange
            var newCustomer = new Customer
            {
                Id = "3",
                FirstName = "Baverly",
                LastName = "Brown",
                Email = "bbfg@gmail.com",
                ContactNumber = "5345353543",
                LastUpdatedDateTimeUtc = DateTime.UtcNow
            };

            _mockDataRepository.Setup(repo => repo.Create(newCustomer)).Returns(Task.FromResult<bool>(true));
            //_mockService.Setup(srv => srv.Create(newCustomer)).Returns(Task.FromResult<Customer>(newCustomer));

            var controller = new CustomerController(customerService);

            // Act
            var response = await controller.Create(newCustomer) as ObjectResult;

            var okObjectResult = response as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var model = okObjectResult.Value as Customer;
            Assert.NotNull(model);

            var actual = model.FirstName;

            // Assert
            Assert.NotNull(response);
            Assert.Equal("3", model.Id);
            Assert.Equal("Baverly", actual);
        }
        [Fact]
        public async Task Delete_Should_Delete_Given_CustomerId()
        {
            // Arrange
            var customerId = "3";

            _mockDataRepository.Setup(repo => repo.Delete(customerId)).Returns(Task.FromResult<bool>(true));

            var controller = new CustomerController(customerService);

            // Act
            var response = await controller.Delete(customerId) as ObjectResult;

            var okObjectResult = response as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var result = okObjectResult.Value;
            Assert.NotNull(result);

            // Assert
            Assert.Equal(true, result);

        }

    }
}
