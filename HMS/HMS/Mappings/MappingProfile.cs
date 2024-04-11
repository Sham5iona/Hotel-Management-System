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
            CreateMap<AdminDTO, Admin>()
                .ForMember(dest => dest.PasswordHash,
                opt => opt.Ignore());


            CreateMap<Admin, AdminDTO>()
                .ForMember(dest => dest.AlreadyExists,
                opt => opt.Ignore());

            //Reservation: CheckOutDate must be later than CheckInDate.

            //Reservation: CheckOutDate must be later than CheckInDate.
            

        }
    }
}
