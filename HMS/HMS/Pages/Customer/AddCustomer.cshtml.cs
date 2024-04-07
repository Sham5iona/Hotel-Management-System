using HMS.DTOs;
using HMS.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HMS.Pages.Customer
{
    public class AddCustomerModel : PageModel
    {
        private readonly ILogger<AddCustomerModel> _logger;
        private readonly ICustomerService _customerService;

        [BindProperty]
        public CustomerDTO CustomerDTO { get; set; }

        public AddCustomerModel(ICustomerService customerService,
                                ILogger<AddCustomerModel> logger)
        {
            this._customerService = customerService;
            this._logger = logger;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if(ModelState.IsValid)
            {
                bool isSuccess = await _customerService.AddCustomerAsync(CustomerDTO);
                
                if (isSuccess)
                {
                    _logger.LogWarning("Successfully added customer to db!");
                    return RedirectToPage("/Customer/GetCustomers");
                }

            }

            return Page();
            
        }
    }
}
