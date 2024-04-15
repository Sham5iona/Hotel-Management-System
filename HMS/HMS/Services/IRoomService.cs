using HMS.DTOs;
using HMS.Model;

namespace HMS.Services
{
    public interface IRoomService
    {
        public Task<bool> AddRoomAsync(RoomDTO RoomDTO);
        public Task<bool> UpdateRoomAsync(RoomDTO RoomDTO);
        public Task<bool> DeleteRoomAsync(Guid id);
        public Task<IEnumerable<Room>> GetRoomsAsync();
        public Task<Guid> GetRoomIDAsync(RoomDTO RoomDTO);

        public Task<bool> CheckIfAlreadyExistsAsync(RoomDTO RoomDTO);

        public Task<Room> GetRoomByIdAsync(Guid id);

        public Task<bool> IsRoomActiveAsync(RoomDTO RoomDTO);

    }
}
