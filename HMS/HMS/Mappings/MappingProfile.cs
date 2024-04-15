using AutoMapper;
using HMS.Areas.Identity.Model;
using HMS.DTOs;
using HMS.Model;
using HMS.Services;

namespace HMS.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            //Create AutoM Mapper for Customer and CustomerDTO and vise versa
            CreateMap<Customer, CustomerDTO>()
                .ForMember(dest => dest.UserAlreadyExists,
                           opt => opt.Ignore());

            CreateMap<CustomerDTO, Customer>();

            //Create Auto Mapper for Admin and AdminDTO and vise versa

            CreateMap<Admin, AdminDTO>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash))
                .ForMember(dest => dest.AlreadyExists,
                opt => opt.Ignore());

            CreateMap<AdminDTO, Admin>();

            //Create Auto Mapper for Room and RoomDTO and vise versa

            CreateMap<RoomDTO, Room>()
                .ForMember(dest => dest.RoomDailyCost,
                opt => opt.MapFrom(src =>
                Math.Round(
                    decimal.Parse(src.RoomDailyCost), 2)
                ));

            CreateMap<Room, RoomDTO>()
                .ForMember(dest => dest.RoomAlreadyExists,
                 opt => opt.Ignore());


            //Create Auto Mapper for Reservation and ReservationDTO and vise versa
            //Reservation: CheckOutDate must be later than CheckInDate.
            CreateMap<Reservation, ReservationDTO>()

                .ForMember(dest => dest.ReservationAlreadyExists,
                opt => opt.Ignore())

                .ForMember(dest => dest.ErrorMessageForIsRoomActive,
                opt => opt.Ignore())

                .ForMember(dest => dest.ErrorMessageForDate,
                opt => opt.Ignore())

                .ForMember(dest => dest.ErrorMessageForInvalidRoomOrUser,
                opt => opt.Ignore());

            CreateMap<ReservationDTO, Reservation>();

        }
    }
}
