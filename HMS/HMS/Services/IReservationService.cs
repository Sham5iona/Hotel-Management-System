using HMS.DTOs;
using HMS.Model;

namespace HMS.Services
{
    public interface IReservationService
    {
        public Task<bool> AddReservationAsync(ReservationDTO ReservationDTO,
                                                    RoomDTO roomDTO,
                                                    CustomerDTO customerDTO);
        public Task<bool> UpdateReservationAsync(ReservationDTO ReservationDTO,
                                                 RoomDTO roomDTO,
                                                 CustomerDTO customerDTO);
        public Task<bool> DeleteReservationAsync(Guid id);
        public Task<IEnumerable<Reservation>> GetReservationsAsync();

        public Task<bool> CheckIfAlreadyExistsAsync(ReservationDTO ReservationDTO,
                                                    RoomDTO roomDTO,
                                                    CustomerDTO customerDTO);

        public Task<Reservation> GetReservationByIdAsync(Guid id);
        public double CalculateTotalCost(double daily_cost, DateTime? check_in_date,
                                               DateTime? check_out_date);

        public bool CheckDate(ReservationDTO reservationDTO);
        
    
    }
}
