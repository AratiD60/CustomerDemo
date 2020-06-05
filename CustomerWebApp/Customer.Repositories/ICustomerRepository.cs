using CustomerDemo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


 namespace CustomerDemo.Repositories
{
    public interface ICustomerRepository
    {
        IList<Customer> GetAll();
        Task<Customer> GetByFirstName(string firstName);
        Task<bool> Create(Customer customer);

        Task<bool> Update(Customer customer);

      //  IOrderedQueryable<Customer> GetAllByUserAccountId(string userAccountId);

        Task<bool> Delete(string customerId);
    }
}
