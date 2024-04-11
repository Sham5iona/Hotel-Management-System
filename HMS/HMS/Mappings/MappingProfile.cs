using AutoMapper;
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

            //Reservation: CheckOutDate must be later than CheckInDate.
            
        }
    }
}
