using CustomerDemo.Context;
using CustomerDemo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerDemo.API.UnitTests
{
    public static class DBContextDataGenerator
    {
        public static void Seed(this CustomerDbContext dbContext)
        {
            dbContext.Customers.Add(new Customer
            {
                Id = "1",
                FirstName = "Neal",
                LastName = "Brown",
                Email = "abc@gmail.com",
                ContactNumber = "09876543",
                LastUpdatedDateTimeUtc = DateTime.UtcNow
            });
            dbContext.Customers.Add(new Customer
            {
                Id = "2",
                FirstName = "Tom",
                LastName = "Cate",
                Email = "abcd12@gmail.com",
                ContactNumber = "0234567",
                LastUpdatedDateTimeUtc = DateTime.UtcNow
            });

            dbContext.Customers.Add(new Customer
            {
                Id = "3",
                FirstName = "Alex",
                LastName = "Rexy",
                Email = "abcd56@gmail.com",
                ContactNumber = "898562",
                LastUpdatedDateTimeUtc = DateTime.UtcNow
            });
            dbContext.Customers.Add(new Customer
            {
                Id = "4",
                FirstName = "John",
                LastName = "Dan",
                Email = "abcd9845@gmail.com",
                ContactNumber = "8564215454",
                LastUpdatedDateTimeUtc = DateTime.UtcNow
            });
            dbContext.Customers.Add(new Customer
            {
                Id = "5",
                FirstName = "Tommy",
                LastName = "Cruze",
                Email = "abcdfr789@gmail.com",
                ContactNumber = "45645645",
                LastUpdatedDateTimeUtc = DateTime.UtcNow
            });
            dbContext.SaveChanges();
        }
    }
}
