using AutoMapper;
using HMS.DTOs;
using HMS.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HMS.Pages.Reservation
{
    public class EditReservationModel : PageModel
    {
        private readonly ILogger<AddReservationModel> _logger;
        private readonly IReservationService _reservationService;
        private readonly IRoomService _roomService;
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        [BindProperty]
        public RoomDTO RoomDTO { get; set; }

        [BindProperty]
        public CustomerDTO CustomerDTO { get; set; }

        [BindProperty]
        public ReservationDTO ReservationDTO { get; set; }

        public HMS.Model.Reservation Reservation { get; set; }

        public EditReservationModel(ILogger<AddReservationModel> logger,
                                   IReservationService reservationService,
                                   IMapper mapper, IRoomService roomService,
                                   ICustomerService customerService )
        {
            this._logger = logger;
            this._reservationService = reservationService;
            this._mapper = mapper;
            this._customerService = customerService;
            this._roomService = roomService;
        }

        public async Task<IActionResult> OnPostEditAsync(Guid Id)
        {
            Reservation  = await _reservationService.GetReservationByIdAsync(Id);
            _mapper.Map(Reservation, ReservationDTO);
            var room = await _roomService.GetRoomByIdAsync(
                                                        Reservation.RoomId);
            _mapper.Map(room, RoomDTO);

            var customer = await _customerService.GetCustomerByIdAsync(
                                                  Reservation.CustomerId);

            _mapper.Map(customer, CustomerDTO);

            return Page();
        }

        public async Task<IActionResult> OnPostEditReservationAsync()
        {
            if(ModelState.IsValid)
            {
                bool is_succeded = await _reservationService.UpdateReservationAsync(
                                         ReservationDTO, RoomDTO, CustomerDTO);
                if(is_succeded)
                {
                    _logger.LogInformation("Successfully updated a reservation!");
                    return RedirectToPage("/Reservation/GetReservations");
                }
            }

            return Page();
        }
    }
}
