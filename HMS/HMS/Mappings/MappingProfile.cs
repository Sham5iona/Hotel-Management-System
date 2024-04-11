using AutoMapper;
using HMS.Areas.Identity.Model;
using HMS.DTOs;
using HMS.Model;

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

            //Reservation: CheckOutDate must be later than CheckInDate.

        }
    }
}
