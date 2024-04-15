
using AutoMapper;
using HMS.Data;
using HMS.DTOs;
using HMS.Model;
using Microsoft.EntityFrameworkCore;

namespace HMS.Services
{
    public class ReservationService : IReservationService
    {
        private readonly ILogger<ReservationService> _logger;
        private readonly HotelDbContext _hotelDbContext;
        private readonly IMapper _mapper;
        private static Guid _reservation_id;
        private readonly ICustomerService _customerService;
        private readonly IRoomService _roomService;
        public ReservationService(ILogger<ReservationService> logger,
                               HotelDbContext hotelDbContext,
                               IMapper mapper, ICustomerService customerService,
                               IRoomService roomService)
        {
            this._logger = logger;
            this._hotelDbContext = hotelDbContext;
            this._mapper = mapper;
            this._customerService = customerService;
            this._roomService = roomService;
        }

        public async Task<bool> AddReservationAsync(ReservationDTO ReservationDTO,
                                                    RoomDTO roomDTO,
                                                    CustomerDTO customerDTO)
        {
            try
            {

                if (await CheckIfAlreadyExistsAsync(ReservationDTO,
                                                      roomDTO,
                                                      customerDTO) &&
                                                      ReservationDTO.ReservationAlreadyExists == null)
                {
                    if (CheckDate(ReservationDTO))
                    {

                        Reservation reservation = new Reservation();

                        //Use the auto mapper to map from ReservationDTO
                        //to Reservation

                        _mapper.Map(ReservationDTO, reservation);

                        if (await _roomService.IsRoomActiveAsync(roomDTO))
                        {
                                

                                reservation.RoomId = await _roomService
                                                     .GetRoomIDAsync(roomDTO);


                                    reservation.CustomerId = await _customerService
                                                            .GetCustomerIDAsync(
                                                             customerDTO);

                                    await _hotelDbContext.Reservations.AddAsync(
                                                                       reservation);

                                    await _hotelDbContext.SaveChangesAsync();

                                    double daily_cost = double.Parse(
                                                        roomDTO.RoomDailyCost);

                                    ReservationDTO.TotalCost = CalculateTotalCost(
                                        daily_cost, ReservationDTO.CheckInDate,
                                        ReservationDTO.CheckOutDate);

                                    return true;
                        }
                    

                        ReservationDTO.ErrorMessageForIsRoomActive = "Error! " +
                            "The selected room is not available!";

                    }
                }

                return false;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public double CalculateTotalCost(double daily_cost, DateTime? check_in_date,
                                               DateTime? check_out_date)
        {
            int days = check_out_date.Value.Day - check_in_date.Value.Day;
            double total_cost = days * daily_cost;

            return Math.Round(total_cost, 2);
        }

        public async Task<bool> CheckIfAlreadyExistsAsync(ReservationDTO ReservationDTO,
                                                          RoomDTO roomDTO,
                                                          CustomerDTO customerDTO)
        {

            Guid room_id = await _roomService.GetRoomIDAsync(roomDTO);

            Guid customer_id = await _customerService
                               .GetCustomerIDAsync(customerDTO);

            bool reservation_exists = await _hotelDbContext.Reservations
                                .AnyAsync(res =>
                                res.RoomId == room_id &&
                                res.CustomerId == customer_id);

           if (reservation_exists)
           {

               ReservationDTO.ReservationAlreadyExists = "Reservation with this" +
                " credentials already exists! Try other one!";

               return true; // reservation exists
           }
            else if(room_id == Guid.Empty || customer_id == Guid.Empty)
            {
                //reservation doesn't exist
                ReservationDTO.ErrorMessageForInvalidRoomOrUser = "Error!" +
                " Room or Customer with this credentials doesn't exist!";
                return false;
            }

            return true;     


        }

        public async Task<bool> DeleteReservationAsync(Guid id)
        {
            try
            {
                var reservation = await _hotelDbContext.Reservations
                    .FirstOrDefaultAsync(res => res.ReservationId == id);
                _hotelDbContext.Reservations.Remove(reservation);

                await _hotelDbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<Reservation> GetReservationByIdAsync(Guid id)
        {
            var reservation = await _hotelDbContext.Reservations
                .FirstOrDefaultAsync(res => res.ReservationId == id);

            _reservation_id = id;

            return reservation;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsAsync()
        {
            try
            {
                var reservations = await _hotelDbContext.Reservations.ToListAsync();

                return reservations;
            }

            catch (Exception ex)
            {
                _logger.LogError($"An error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateReservationAsync(ReservationDTO ReservationDTO,
                                                       RoomDTO roomDTO,
                                                       CustomerDTO customerDTO)
        {
            try
            {

                if ((await CheckIfAlreadyExistsAsync(ReservationDTO, roomDTO,
                                                      customerDTO)))
                {
                    if (CheckDate(ReservationDTO))
                    {
                        if (roomDTO.RoomIsActive)
                        {
                            var reservation = await GetReservationByIdAsync(_reservation_id);
                            _mapper.Map(ReservationDTO, reservation);
                            await _hotelDbContext.SaveChangesAsync();
                            return true;
                        }
                    }
                }

                return false;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }

        }

        public bool CheckDate(ReservationDTO reservationDTO)
        {
            if(reservationDTO.CheckOutDate > reservationDTO.CheckInDate)
            {
                return true;
            }

            reservationDTO.ErrorMessageForDate = "Invalid date! Check out date can't" +
                " be less than check in date!";

            return false;
        }
    }
}

