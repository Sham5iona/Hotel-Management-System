using HMS.DTOs;
using HMS.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HMS.Pages.Room
{
    public class AddRoomModel : PageModel
    {
        private readonly ILogger<AddRoomModel> _logger;
        private readonly IRoomService _roomService;
        [BindProperty]
        public RoomDTO RoomDTO { get; set; }
        public AddRoomModel(ILogger<AddRoomModel> logger, IRoomService roomService)
        {
            this._logger = logger;
            this._roomService = roomService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(ModelState.IsValid)
            {
                var is_succeded = await _roomService.AddRoomAsync(RoomDTO);
                if (is_succeded)
                {
                    _logger.LogInformation("Successfully added a room!");
                    return RedirectToPage("/Room/GetRooms");
                }
            }

            return Page();
        }
    }
}
