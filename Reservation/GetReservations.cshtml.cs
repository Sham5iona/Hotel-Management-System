using HMS.Model;
using HMS.Pages.Customer;
using HMS.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HMS.Pages.Reservation
{
    public class GetReservationsModel : PageModel
    {
        private readonly IReservationService _reservationService;
        private readonly ILogger<GetReservationsModel> _logger;
        public IEnumerable<HMS.Model.Reservation> Reservations { get; set; }
        public GetReservationsModel(IReservationService reservationService,
                                 ILogger<GetReservationsModel> logger)
        {
            this._reservationService = reservationService;
            this._logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Reservations = await _reservationService.GetReservationsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid ReservationId)
        {
            await _reservationService.DeleteReservationAsync(ReservationId);
            _logger.LogInformation("Successfully deleted a reservation!");
            return LocalRedirect("/Reservation/GetReservations");
        }

    }
}
