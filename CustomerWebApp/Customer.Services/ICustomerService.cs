using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerDemo.Models;

 namespace CustomerDemo.Services
{
    public interface ICustomerService
    {
        IOrderedQueryable<Customer> GetAll();
        Customer Get(string customerId);
        Task<Customer> Create(Customer customer);
        Task<Customer> Update(Customer customer);
        Task<bool> Delete(string customerId);

        // IOrderedQueryable<BlogPost> GetAllByUserAccountId(string userAccountId);


    }
}
