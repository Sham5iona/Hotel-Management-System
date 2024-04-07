using AutoMapper;
using HMS.Data;
using HMS.DTOs;
using HMS.Model;
using Microsoft.EntityFrameworkCore;


namespace HMS.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ILogger<CustomerService> _logger;
        private readonly HotelDbContext _hotelDbContext;
        private readonly IMapper _mapper;
        private static Guid _customer_id;
        public CustomerService(ILogger<CustomerService> logger,
                               HotelDbContext hotelDbContext,
                               IMapper mapper)
        {

            this._logger = logger;
            this._hotelDbContext = hotelDbContext;
            this._mapper = mapper;

        }

        public async Task<bool> AddCustomerAsync(CustomerDTO customerDTO)
        {

            try
            {
                
                if (!(await CheckIfAlreadyExistsAsync(customerDTO)))
                {
                    Customer customer = new Customer();
                   //Use the auto mapper to map from CustomerDTO to Customer
                    _mapper.Map(customerDTO, customer);
                    await _hotelDbContext.Customers.AddAsync(customer);
                    await _hotelDbContext.SaveChangesAsync();
                    return true;
                }

                return false;

            }catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> CheckIfAlreadyExistsAsync(
                                CustomerDTO customerDTO)
        {

            var customers = await GetCustomersAsync();

            bool email_already_exists = customers.Any(c =>
                c.CustomerEmail == customerDTO?.CustomerEmail
                && c.CustomerId != _customer_id);

            if(email_already_exists) 
            {
                customerDTO.UserAlreadyExists = "User with this email already " +
                    "exists! Try other one!";

                return true; // user exists
            }

            //user doesn't exist
            return false;
            
        }

        public async Task<bool> DeleteCustomerAsync(Guid id)
        {
            try
            {
                var customer = await _hotelDbContext.Customers
                    .FirstOrDefaultAsync(c => c.CustomerId == id);
                _hotelDbContext.Remove(customer);

                await _hotelDbContext.SaveChangesAsync();
                return true;

            } catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<Customer> GetCustomerByIdAsync(Guid id)
        {
            var customer = await _hotelDbContext.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == id);

            _customer_id = id;

            return customer;
        }

        public async Task<Guid> GetCustomerIDAsync(CustomerDTO customerDTO)
        {

            try
            {
                _mapper.Map<Customer>(customerDTO);
                Customer? wanted_customer = await _hotelDbContext.Customers
                                          .FirstOrDefaultAsync(
                            c => c.CustomerId == _customer_id);

                if(wanted_customer == null)
                {
                    throw new Exception("There is no user with this data!");

                }

                return wanted_customer.CustomerId;

            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return Guid.Empty;
            }
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            try
            {
                var customers = await _hotelDbContext.Customers.ToListAsync();
                               
                return customers;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred: {ex.Message}");
                throw;
            }
        }


        public async Task<bool> UpdateCustomerAsync(CustomerDTO customerDTO)
        {
            try
            {
                
                if (!(await CheckIfAlreadyExistsAsync(customerDTO)))
                {
                    var customer = await GetCustomerByIdAsync(_customer_id);
                    _mapper.Map(customerDTO, customer);
                    await _hotelDbContext.SaveChangesAsync();
                    return true;
                }

                return false;

            }catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }

        }
    }
}
