using CustomerDemo.Models;
using CustomerDemo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 namespace CustomerDemo.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }


        public async Task<Customer> Create(Customer customer)
        {
            customer.LastUpdatedDateTimeUtc = DateTime.UtcNow;

            var success = await _repository.Create(customer);

            if (success)
                return customer;
            else
                return null;
        }

        public async Task<Customer> Update(Customer customer)
        {
            customer.LastUpdatedDateTimeUtc = DateTime.UtcNow;

            var success = await _repository.Update(customer);

            if (success)
                return customer;
            else
                return null;
        }

        public Customer Get(string customerId)
        {
            var result = _repository.Get(customerId);

            return result;
        }

        public IOrderedQueryable<Customer> GetAll()
        {
            var result = _repository.GetAll();

            return result;
        }

        public async Task<bool> Delete(string customerId)
        {
            var success = await _repository.Delete(customerId);

            return success;
        }

    }
}
