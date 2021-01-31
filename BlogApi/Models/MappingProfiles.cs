using AutoMapper;

namespace BlogApi.Models
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserRegisterationModel, UserModel>();
        }
    }
}