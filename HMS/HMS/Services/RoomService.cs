using AutoMapper;
using HMS.Data;
using HMS.DTOs;
using HMS.Model;
using Microsoft.EntityFrameworkCore;

namespace HMS.Services
{
    public class RoomService : IRoomService
    {
        private readonly ILogger<RoomService> _logger;
        private readonly HotelDbContext _hotelDbContext;
        private readonly IMapper _mapper;
        private static Guid _room_id;
        public RoomService(ILogger<RoomService> logger,
                               HotelDbContext hotelDbContext,
                               IMapper mapper)
        {
            this._logger = logger;
            this._hotelDbContext = hotelDbContext;
            this._mapper = mapper;
        }

        public async Task<bool> AddRoomAsync(RoomDTO RoomDTO)
        {
            try
            {

                if (!(await CheckIfAlreadyExistsAsync(RoomDTO)))
                {
                    Room room = new Room();

                    //Use the auto mapper to map from RoomDTO to Room
                    _mapper.Map(RoomDTO, room);
                    
                    await _hotelDbContext.Rooms.AddAsync(room);
                    await _hotelDbContext.SaveChangesAsync();
                    return true;
                }

                return false;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> CheckIfAlreadyExistsAsync(RoomDTO RoomDTO)
        {
            var rooms = await GetRoomsAsync();

            bool room_number_already_exists = rooms.Any(r =>
                r.RoomNumber == RoomDTO.RoomNumber &&
                r.RoomId != _room_id);


            if (room_number_already_exists)
            {
                
                RoomDTO.RoomAlreadyExists = "Room with this number already exists!" +
                    " Try other one!";

                return true; // room exists
            }

            //room doesn't exist
            return false;

        }

        public async Task<bool> DeleteRoomAsync(Guid id)
        {
            try
            {
                var room = await _hotelDbContext.Rooms
                    .FirstOrDefaultAsync(r => r.RoomId == id);
                _hotelDbContext.Rooms.Remove(room);

                await _hotelDbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<Room> GetRoomByIdAsync(Guid id)
        {
            var room = await _hotelDbContext.Rooms
                .FirstOrDefaultAsync(r => r.RoomId == id);

            _room_id = id;

            return room;
        }

        public async Task<Guid> GetRoomIDAsync(RoomDTO RoomDTO)
        {
            try
            {
                Room? wanted_room = await _hotelDbContext.Rooms
                                          .FirstOrDefaultAsync(
                            r => r.RoomNumber == RoomDTO.RoomNumber &&
                            r.RoomType == RoomDTO.RoomType &&
                            r.RoomCapacity == RoomDTO.RoomCapacity &&
                            r.RoomDailyCost == decimal.Parse(RoomDTO.RoomDailyCost));
                

                if (wanted_room == null)
                {
                    throw new Exception("There is no room with this data!");

                }

                return wanted_room.RoomId;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Guid.Empty;
            }
        }

        public async Task<IEnumerable<Room>> GetRoomsAsync()
        {
            try
            {
                var rooms = await _hotelDbContext.Rooms.ToListAsync();

                return rooms;
            }

            catch (Exception ex)
            {
                _logger.LogError($"An error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateRoomAsync(RoomDTO RoomDTO)
        {
            try
            {

                if (!(await CheckIfAlreadyExistsAsync(RoomDTO)))
                {

                    var reservation = await _hotelDbContext.Reservations
                                     .FirstOrDefaultAsync(res =>
                                     res.RoomId == _room_id);


                        if (await IsRoomActiveAsync(RoomDTO) &&
                            reservation == null)
                        {
                            var room = await GetRoomByIdAsync(_room_id);
                            _mapper.Map(RoomDTO, room);
                            await _hotelDbContext.SaveChangesAsync();
                            return true;
                        }

                        RoomDTO.ErrorMessageForIsRoomActive = "Can't update the room because there is already" +
                            " a reservation on it!";

                        return false;
  
                }

                return false;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }

        }

        public async Task<bool> IsRoomActiveAsync(RoomDTO RoomDTO)
        {

            var room = await _hotelDbContext.Rooms.FirstOrDefaultAsync(
                              r => r.RoomIsActive == true);

            if (room != null)
            {
                return true;
            }

            return false;
        }
    }
}


