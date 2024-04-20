using AutoMapper;
using HMS.DTOs;
using HMS.Model;
using HMS.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HMS.Pages.Room
{
    public class EditRoomModel : PageModel
    {
        private readonly ILogger<EditRoomModel> _logger;
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;
        public Model.Room Room { get; set; }
        [BindProperty]
        public RoomDTO RoomDTO { get; set; }
        public EditRoomModel(ILogger<EditRoomModel> logger, IRoomService roomService,
                             IMapper mapper)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._roomService = roomService;
        }

        public async Task<IActionResult> OnPostEditAsync(Guid Id)
        {
            Room = await _roomService.GetRoomByIdAsync(Id);
            RoomDTO = _mapper.Map(Room, RoomDTO);
            return Page();
        }

        public async Task<IActionResult> OnPostEditRoomAsync()
        {
            if(ModelState.IsValid)
            {
                bool is_succeeded = await _roomService.UpdateRoomAsync(RoomDTO);
                if(is_succeeded)
                {
                    _logger.LogInformation("Successfully updated a room!");
                    return RedirectToPage("/Room/GetRooms");
                }
            }

            return Page();
        }
    }
}
