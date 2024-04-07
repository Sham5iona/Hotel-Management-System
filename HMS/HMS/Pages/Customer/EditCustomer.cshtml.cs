using AutoMapper;
using HMS.DTOs;
using HMS.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HMS.Pages.Customer
{
    public class EditCustomerModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<EditCustomerModel> _logger;
        private readonly IMapper _mapper;
        public HMS.Model.Customer Customer { get; set; }
        public EditCustomerModel(ICustomerService customerService,
                                 ILogger<EditCustomerModel> logger,
                                 IMapper mapper)
        {
            this._customerService = customerService;
            this._logger = logger;
            this._mapper = mapper;

        }
        [BindProperty]
        public Guid Id { get; set; }

        [BindProperty]
        public CustomerDTO CustomerDTO { get; set; }
        public async Task<IActionResult> OnPostEditAsync()
        {
            Customer = await _customerService.GetCustomerByIdAsync(Id);
            CustomerDTO = _mapper.Map<CustomerDTO>(Customer);
            return Page();
        }

        
        public async Task<IActionResult> OnPostEditUserAsync()
        {
            if(ModelState.IsValid)
            {
                if(await _customerService.UpdateCustomerAsync(CustomerDTO))
                {
                    _logger.LogInformation("Successfully edited a user!");
                    return RedirectToPage("/Customer/GetCustomers");
                }
            }
            return Page();
        }
    }
}
