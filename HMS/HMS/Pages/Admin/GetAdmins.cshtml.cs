using HMS.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HMS.Pages.Admin
{
    public class GetAdminsModel : PageModel
    {
        private readonly IAdminService _adminService;
        public IEnumerable<HMS.Areas.Identity.Model.Admin> Admins {  get; set; }
        public GetAdminsModel(IAdminService adminService)
        {
            this._adminService = adminService;
        }
        public IActionResult OnGet()
        {
            Admins = _adminService.GetAdmins();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string Id)
        {

            await _adminService.DeleteAdminAsync(Id);

            return RedirectToPage();

        }
    }
}
