using HMS.DTOs;
using HMS.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HMS.Pages.Room
{
    public class GetRoomsModel : PageModel
    {
        private readonly ILogger<GetRoomsModel> _logger;
        private readonly IRoomService _roomService;
        public IEnumerable<Model.Room> Rooms { get; set; }
        public GetRoomsModel(ILogger<GetRoomsModel> logger, IRoomService roomService)
        {
            this._logger = logger;
            this._roomService = roomService;
        }

        public async Task OnGetAsync()
        {
            Rooms = await _roomService.GetRoomsAsync();
        }

        public async Task<IActionResult> OnPostAsync(Guid RoomId)
        {
            await _roomService.DeleteRoomAsync(RoomId);
            _logger.LogInformation("Successfully deleted a room!");
            return RedirectToPage("/Room/GetRooms");

        }
    }
}
