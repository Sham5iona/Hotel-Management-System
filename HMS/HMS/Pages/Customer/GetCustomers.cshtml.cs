using HMS.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace HMS.Pages.Customer
{
    public class GetCustomersModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<GetCustomersModel> _logger;
        public IEnumerable<HMS.Model.Customer> Customers { get; set; }
        public GetCustomersModel(ICustomerService customerService,
                                 ILogger<GetCustomersModel> logger)
        {
            this._customerService = customerService;
            _logger = logger;
        }
        
        public async Task OnGetAsync()
        {
            Customers = await _customerService.GetCustomersAsync();
                        
        }

        [BindProperty]
        public Guid CustomerId { get; set; }
        public async Task<IActionResult> OnPost()
        {
            await _customerService.DeleteCustomerAsync(CustomerId);
            _logger.LogInformation("Successfully deleted!");
            return LocalRedirect("/Customer/GetCustomers");
            
        }
    }
}
