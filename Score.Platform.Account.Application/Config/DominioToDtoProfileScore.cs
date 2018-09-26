using Score.Platform.Account.Domain.Entitys;
using Score.Platform.Account.Dto;

namespace Score.Platform.Account.Application.Config
{
    public class DominioToDtoProfileScore : AutoMapper.Profile
    {

        public DominioToDtoProfileScore()
        {
            CreateMap(typeof(Program), typeof(ProgramDto)).ReverseMap();
            CreateMap(typeof(Program), typeof(ProgramDtoSpecialized));
            CreateMap(typeof(Program), typeof(ProgramDtoSpecializedResult));
            CreateMap(typeof(Program), typeof(ProgramDtoSpecializedReport));
            CreateMap(typeof(Program), typeof(ProgramDtoSpecializedDetails));
            CreateMap(typeof(Tenant), typeof(TenantDto)).ReverseMap();
            CreateMap(typeof(Tenant), typeof(TenantDtoSpecialized));
            CreateMap(typeof(Tenant), typeof(TenantDtoSpecializedResult));
            CreateMap(typeof(Tenant), typeof(TenantDtoSpecializedReport));
            CreateMap(typeof(Tenant), typeof(TenantDtoSpecializedDetails));
            CreateMap(typeof(Thema), typeof(ThemaDto)).ReverseMap();
            CreateMap(typeof(Thema), typeof(ThemaDtoSpecialized));
            CreateMap(typeof(Thema), typeof(ThemaDtoSpecializedResult));
            CreateMap(typeof(Thema), typeof(ThemaDtoSpecializedReport));
            CreateMap(typeof(Thema), typeof(ThemaDtoSpecializedDetails));

        }

    }
}
