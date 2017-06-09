using AutoMapper;
using AutenticadorV2.API.Model;
using Autenticador.Entities;
using Autenticador.Entities.V2;


namespace AutenticadorV2.API.Mappers
{
    public class EntityToModelProfile : Profile
    {
        public override string ProfileName
        {
            get
            {
                return this.GetType().Name;
            }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<SYS_Entidade, Entidade>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ent_id))
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.ent_codigo))
                .ForMember(dest => dest.NomeFantasia, opt => opt.MapFrom(src => src.ent_nomeFantasia))
                .ForMember(dest => dest.RazaoSocial, opt => opt.MapFrom(src => src.ent_razaoSocial))
                .ForMember(dest => dest.Sigla, opt => opt.MapFrom(src => src.ent_sigla))
                .ForMember(dest => dest.CNPJ, opt => opt.MapFrom(src => src.ent_cnpj))

                .ForMember(dest => dest.InscricaoMunicipal, opt => opt.MapFrom(src => src.ent_inscricaoMunicipal))
                .ForMember(dest => dest.InscricaoEstadual, opt => opt.MapFrom(src => src.ent_inscricaoEstadual))
                .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(src => src.ent_dataCriacao))
                .ForMember(dest => dest.DataAlteracao, opt => opt.MapFrom(src => src.ent_dataAlteracao))
                .ForMember(dest => dest.IdEntidadeSuperior, opt => opt.MapFrom(src => src.ent_idSuperior));

            Mapper.CreateMap<SYS_TipoEntidade, TipoEntidade>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ten_id))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.ten_nome));

            Mapper.CreateMap<EntidadeDTO, Entidade>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ent_id))
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.ent_codigo))
                .ForMember(dest => dest.NomeFantasia, opt => opt.MapFrom(src => src.ent_nomeFantasia))
                .ForMember(dest => dest.RazaoSocial, opt => opt.MapFrom(src => src.ent_razaoSocial))
                .ForMember(dest => dest.Sigla, opt => opt.MapFrom(src => src.ent_sigla))
                .ForMember(dest => dest.CNPJ, opt => opt.MapFrom(src => src.ent_cnpj))

                .ForMember(dest => dest.InscricaoMunicipal, opt => opt.MapFrom(src => src.ent_inscricaoMunicipal))
                .ForMember(dest => dest.InscricaoEstadual, opt => opt.MapFrom(src => src.ent_inscricaoEstadual))
                .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(src => src.ent_dataCriacao))
                .ForMember(dest => dest.DataAlteracao, opt => opt.MapFrom(src => src.ent_dataAlteracao))
                .ForMember(dest => dest.IdEntidadeSuperior, opt => opt.MapFrom(src => src.ent_idSuperior));

            Mapper.CreateMap<TipoEntidadeDTO, TipoEntidade>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ten_id))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.ten_nome));

            Mapper.CreateMap<SYS_UnidadeAdministrativa, UnidadeAdministrativa>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.uad_id))
                .ForMember(dest => dest.IdEntidade, opt => opt.MapFrom(src => src.ent_id))
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.uad_codigo))
                .ForMember(dest => dest.CodigoIntegracao, opt => opt.MapFrom(src => src.uad_codigoIntegracao))
                //.ForMember(dest => dest.CodigoInep, opt => opt.MapFrom(src => src.uad_codigoInep))
                .ForMember(dest => dest.IdUnidadeSuperior, opt => opt.MapFrom(src => src.uad_idSuperior))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.uad_nome))
                .ForMember(dest => dest.Sigla, opt => opt.MapFrom(src => src.uad_sigla))
                .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(src => src.uad_dataCriacao))
                .ForMember(dest => dest.DataAlteracao, opt => opt.MapFrom(src => src.uad_dataAlteracao));

            Mapper.CreateMap<SYS_TipoUnidadeAdministrativa, TipoUnidadeAdministrativa>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.tua_id))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.tua_nome));

            Mapper.CreateMap<SYS_Grupo, Grupo>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.gru_id))
                .ForMember(dest => dest.IdSistema, opt => opt.MapFrom(src => src.sis_id))
                .ForMember(dest => dest.IdVisao, opt => opt.MapFrom(src => src.vis_id))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.gru_nome))
                .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(src => src.gru_dataCriacao))
                .ForMember(dest => dest.DataAlteracao, opt => opt.MapFrom(src => src.gru_dataAlteracao));

            Mapper.CreateMap<SYS_Modulo, Modulo>()
                .ForMember(dest => dest.IdModulo, opt => opt.MapFrom(src => src.mod_id))
                .ForMember(dest => dest.IdModuloPai, opt => opt.MapFrom(src => src.mod_idPai))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.mod_nome));

            Mapper.CreateMap<SYS_Usuario, Usuario>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.usu_id))
                .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.usu_login))
                .ForMember(dest => dest.Dominio, opt => opt.MapFrom(src => src.usu_dominio))
                .ForMember(dest => dest.CodigoIntegracaoAD, opt => opt.MapFrom(src => src.usu_integracaoAD))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.usu_email))
                .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(src => src.usu_dataCriacao))
                .ForMember(dest => dest.DataAlteracao, opt => opt.MapFrom(src => src.usu_dataAlteracao));

            Mapper.CreateMap<PES_Pessoa, Pessoa>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.pes_id))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.pes_nome))
                .ForMember(dest => dest.DataNascimento, opt => opt.MapFrom(src => src.pes_dataNascimento))
                .ForMember(dest => dest.EstadoCivil, opt => opt.MapFrom(src => src.pes_estadoCivil))
                .ForMember(dest => dest.Sexo, opt => opt.MapFrom(src => src.pes_sexo))
                .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(src => src.pes_dataCriacao))
                .ForMember(dest => dest.DataAlteracao, opt => opt.MapFrom(src => src.pes_dataAlteracao));

            Mapper.CreateMap<ModuloPermisaoDTO, ModuloPermissao>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.mod_id))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.mod_nome))
                .ForMember(dest => dest.IdModuloPai, opt => opt.MapFrom(src => src.mod_idPai));

            Mapper.CreateMap<GrupoPermissaoDTO, GrupoPermissao>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.gru_id))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.gru_nome))
                .ForMember(dest => dest.Modulos, opt => opt.MapFrom(src => src.Modulos));

            #region Nova Infra

            Mapper.CreateMap<Entities.Models.Entidade, Entidade>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ent_id))
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.ent_codigo))
                .ForMember(dest => dest.NomeFantasia, opt => opt.MapFrom(src => src.ent_nomeFantasia))
                .ForMember(dest => dest.RazaoSocial, opt => opt.MapFrom(src => src.ent_razaoSocial))
                .ForMember(dest => dest.Sigla, opt => opt.MapFrom(src => src.ent_sigla))
                .ForMember(dest => dest.CNPJ, opt => opt.MapFrom(src => src.ent_cnpj))
                .ForMember(dest => dest.InscricaoMunicipal, opt => opt.MapFrom(src => src.ent_inscricaoMunicipal))
                .ForMember(dest => dest.InscricaoEstadual, opt => opt.MapFrom(src => src.ent_inscricaoEstadual))
                .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(src => src.ent_dataCriacao))
                .ForMember(dest => dest.DataAlteracao, opt => opt.MapFrom(src => src.ent_dataAlteracao))
                .ForMember(dest => dest.IdEntidadeSuperior, opt => opt.MapFrom(src => src.ent_idSuperior))
                .ForMember(dest => dest.TipoEntidade, opt => opt.MapFrom(src => src.SYS_TipoEntidade));

            Mapper.CreateMap<Entities.Models.TipoEntidade, TipoEntidade>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ten_id))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.ten_nome));

            Mapper.CreateMap<Entities.Models.SYS_UnidadeAdministrativa, UnidadeAdministrativa>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.uad_id))
                .ForMember(dest => dest.IdEntidade, opt => opt.MapFrom(src => src.ent_id))
                .ForMember(dest => dest.IdUnidadeSuperior, opt => opt.MapFrom(src => src.uad_idSuperior))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.uad_nome))
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.uad_codigo))
                .ForMember(dest => dest.CodigoIntegracao, opt => opt.MapFrom(src => src.uad_codigoIntegracao))
                //.ForMember(dest => dest.CodigoInep, opt => opt.MapFrom(src => src.uad_codigoInep))
                .ForMember(dest => dest.Sigla, opt => opt.MapFrom(src => src.uad_sigla))
                .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(src => src.uad_dataCriacao))
                .ForMember(dest => dest.DataAlteracao, opt => opt.MapFrom(src => src.uad_dataAlteracao))
                .ForMember(dest => dest.TipoUnidade, opt => opt.MapFrom(src => src.SYS_TipoUnidadeAdministrativa));

            Mapper.CreateMap<Entities.Models.SYS_TipoUnidadeAdministrativa, TipoUnidadeAdministrativa>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.tua_id))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.tua_nome));

            Mapper.CreateMap<UnidadeAdministrativaDTO, UnidadeAdministrativa>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.uad_id))
                .ForMember(dest => dest.IdEntidade, opt => opt.MapFrom(src => src.ent_id))
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.uad_codigo))
                //.ForMember(dest => dest.CodigoInep, opt => opt.MapFrom(src => src.uad_codigoInep))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.tua_nome))
                .ForMember(dest => dest.Sigla, opt => opt.MapFrom(src => src.uad_sigla))
                .ForMember(dest => dest.CodigoIntegracao, opt => opt.MapFrom(src => src.uad_codigoIntegracao));

            Mapper.CreateMap<TipoUnidadeDTO, TipoUnidadeAdministrativa>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.tua_id))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.tua_nome));

            #endregion Nova Infra
        }
    }
}