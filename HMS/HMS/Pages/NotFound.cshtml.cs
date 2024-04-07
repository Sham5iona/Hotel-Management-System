using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HMS.Pages
{
    public class NotFoundModel : PageModel
    {
        public void ErrorPage(int StatusCode)
        {
            if(StatusCode == 404)
            {
                RedirectToPage("/NotFound");
            }
        }
    }
}
