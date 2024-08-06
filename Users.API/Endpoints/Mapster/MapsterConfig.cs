using Identity.API.Core;
using Identity.API.DTOs;
using Mapster;
using Users.API.Core.Protos;

namespace Identity.API.Endpoints.Mapster
{
    public static class MapsterConfig
    {
        public static void Configure()
        {
            //LoginUser to User
            TypeAdapterConfig<LoginUserDTO, User>.NewConfig()
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.Password, src => src.Password);

            //RegisterUser to User
            TypeAdapterConfig<RegisterUserDTO, User>.NewConfig()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.JMBG, src => src.JMBG)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Surname, src => src.Surname)
                .Map(dest => dest.DateOfBirth, src => src.DateOfBirth)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.Password, src => src.Password)
                .Map(dest => dest.PlaceId, src => src.PlaceId)
                .Map(dest => dest.RoleId, src => src.RoleId);

            //GRPC MAPPINGS
            //LoginUserGRPC to User
            TypeAdapterConfig<LoginRequestGRPC, User>.NewConfig()
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.Password, src => src.Password);

            //RegisterUserGRPC to User
            TypeAdapterConfig<RegisterRequestGRPC, User>.NewConfig()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.JMBG, src => src.JMBG)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Surname, src => src.Surname)
                .Map(dest => dest.DateOfBirth, src => src.DateOfBirth)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.Password, src => src.Password)
                .Map(dest => dest.PlaceId, src => src.PlaceId)
                .Map(dest => dest.RoleId, src => src.RoleId);

        }
    }
}
