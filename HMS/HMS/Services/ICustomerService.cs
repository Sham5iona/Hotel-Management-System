using HMS.DTOs;
using HMS.Model;

namespace HMS.Services
{
    public interface ICustomerService
    {
        public Task<bool> AddCustomerAsync(CustomerDTO customerDTO);
        public Task<bool> UpdateCustomerAsync(CustomerDTO customerDTO);
        public Task<bool> DeleteCustomerAsync(Guid id);
        public Task<IEnumerable<Customer>> GetCustomersAsync();
        public Task<Guid> GetCustomerIDAsync(CustomerDTO customerDTO);

        public Task<bool> CheckIfAlreadyExistsAsync(CustomerDTO customerDTO);

        public Task<Customer> GetCustomerByIdAsync(Guid id);

    }
}