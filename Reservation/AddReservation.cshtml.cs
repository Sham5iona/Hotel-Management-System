using HMS.DTOs;
using HMS.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HMS.Pages.Reservation
{
    public class AddReservationModel : PageModel
    {
        private readonly ILogger<AddReservationModel> _logger;
        private readonly IReservationService _reservationService;

        [BindProperty]
        public RoomDTO RoomDTO { get; set; }

        [BindProperty]
        public CustomerDTO CustomerDTO { get; set; }

        [BindProperty]
        public ReservationDTO ReservationDTO { get; set; }

        public AddReservationModel(ILogger<AddReservationModel> logger,
                                   IReservationService reservationService)
        {
            this._logger = logger;
            this._reservationService = reservationService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(ModelState.IsValid)
            {
                bool is_success = await _reservationService.AddReservationAsync(
                                                            ReservationDTO,
                                                            RoomDTO, CustomerDTO);
                if (is_success)
                {
                    _logger.LogInformation("Successfully added a reservation!");
                }
            }
            
            return Page();
        }


    }
}
