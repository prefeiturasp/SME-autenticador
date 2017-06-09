using AutoMapper;

//using Autenticador.Entities.ComplexType;
using AutenticadorAPI.ViewModels;
using Autenticador.Entities;

//using Autenticador.Entities;
using Profile = AutoMapper.Profile;

namespace AutenticadorAPI.Mappers
{
    public class EntityToViewModelProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<SYS_Entidade, EntidadeViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ent_id))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.ent_razaoSocial));

            Mapper.CreateMap<SYS_UnidadeAdministrativa, UnidadeAdministrativaViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.uad_id))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.uad_nome))
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.tua_nome));

            Mapper.CreateMap<SYS_TipoUnidadeAdministrativa, TipoUnidadeAdministrativaViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.tua_id))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.tua_nome));

            Mapper.CreateMap<SYS_Grupo, GrupoViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.gru_id))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.gru_nome))
                .ForMember(dest => dest.IdSistema, opt => opt.MapFrom(src => src.sis_id))
                .ForMember(dest => dest.IdVisao, opt => opt.MapFrom(src => src.vis_id));
        }

        public override string ProfileName
        {
            get
            {
                return this.GetType().Name;
            }
        }
    }
}