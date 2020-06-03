using CustomerDemo.Models;
using System.Linq;
using System.Threading.Tasks;


 namespace CustomerDemo.Repositories
{
    public interface ICustomerRepository
    {
        IOrderedQueryable<Customer> GetAll();
        Customer Get(string customerId);
        Task<bool> Create(Customer customer);

        Task<bool> Update(Customer customer);

      //  IOrderedQueryable<Customer> GetAllByUserAccountId(string userAccountId);

        Task<bool> Delete(string customerId);
    }
}
