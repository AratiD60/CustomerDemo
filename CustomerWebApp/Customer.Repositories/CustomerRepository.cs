using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using CustomerDemo.Context;
using CustomerDemo.Models;
using CustomerDemo.Repositories;

 namespace CustomerDemo.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IServiceScope _scope;
        private readonly CustomerDbContext _databaseContext;

        public CustomerRepository(IServiceProvider services)
        {
            _scope = services.CreateScope();

            _databaseContext = _scope.ServiceProvider.GetRequiredService<CustomerDbContext>();
        }

        public async Task<bool> Create(Customer customer)
        {
            var success = false;

            _databaseContext.Customers.Add(customer);

            var ItemsCreated = await _databaseContext.SaveChangesAsync();

            if (ItemsCreated == 1)
                success = true;

            return success;
        }

        public async Task<bool> Update(Customer customer)
        {
            var success = false;

            var existingCustomer = Get(customer.Id);

            if (existingCustomer != null)
            {
                existingCustomer.FirstName = customer.FirstName;
                existingCustomer.LastName = customer.LastName;
                existingCustomer.Email = customer.Email;
                existingCustomer.ContactNumber = customer.ContactNumber;
                existingCustomer.LastUpdatedDateTimeUtc = customer.LastUpdatedDateTimeUtc;

                _databaseContext.Customers.Attach(existingCustomer);

                var numberOfItemsUpdated = await _databaseContext.SaveChangesAsync();

                if (numberOfItemsUpdated == 1)
                    success = true;
            }

            return success;
        }

        public Customer Get(string customerId)
        {
            var result = _databaseContext.Customers
                                .Where(x => x.Id == customerId)
                                .FirstOrDefault();

            return result;
        }

        public IOrderedQueryable<Customer> GetAll()
        {
            var result = _databaseContext.Customers
                                .OrderByDescending(x => x.Id);

            return result;
        }

        public async Task<bool> Delete(string customerId)
        {
            var success = false;

            var existingCustomer = Get(customerId);

            if (existingCustomer != null)
            {
                _databaseContext.Customers.Remove(existingCustomer);

                var ItemsDeleted = await _databaseContext.SaveChangesAsync();

                if (ItemsDeleted == 1)
                    success = true;
            }

            return success;
        }
    }
}
