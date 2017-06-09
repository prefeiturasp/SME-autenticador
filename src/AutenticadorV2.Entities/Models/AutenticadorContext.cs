using AutenticadorV2.Entities.Models.Mapping;
using AutenticadorV2.Infra;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

//using Repository.Pattern.Ef6;

namespace AutenticadorV2.Entities.Models
{

    public partial class AutenticadorContext : DbContext
    {
        static AutenticadorContext()
        {
            Database.SetInitializer<AutenticadorContext>(null);
        }

        public AutenticadorContext()
            : base(Connection.GetConnectionString("AutenticadorDB"))
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.EnsureTransactionsForFunctionsAndCommands = false;
            Configuration.ProxyCreationEnabled = false;
            Configuration.UseDatabaseNullSemantics = false;
            Configuration.ValidateOnSaveEnabled = false;
        }

        public DbSet<CFG_Arquivo> CFG_Arquivo { get; set; }
        public DbSet<CFG_Configuracao> CFG_Configuracao { get; set; }
        public DbSet<CFG_Relatorio> CFG_Relatorio { get; set; }
        public DbSet<CFG_ServidorRelatorio> CFG_ServidorRelatorio { get; set; }
        public DbSet<CFG_TemaPadrao> CFG_TemaPadrao { get; set; }
        public DbSet<CFG_TemaPaleta> CFG_TemaPaleta { get; set; }
        public DbSet<CFG_UsuarioAPI> CFG_UsuarioAPI { get; set; }
        public DbSet<CFG_Versao> CFG_Versao { get; set; }
        public DbSet<END_Cidade> END_Cidade { get; set; }
        public DbSet<END_Endereco> END_Endereco { get; set; }
        public DbSet<END_Pais> END_Pais { get; set; }
        public DbSet<END_UnidadeFederativa> END_UnidadeFederativa { get; set; }
        public DbSet<LOG_UsuarioAD> LOG_UsuarioAD { get; set; }
        public DbSet<LOG_UsuarioADErro> LOG_UsuarioADErro { get; set; }
        public DbSet<LOG_UsuarioAPI> LOG_UsuarioAPI { get; set; }
        public DbSet<PES_CertidaoCivil> PES_CertidaoCivil { get; set; }
        public DbSet<PES_Pessoa> PES_Pessoa { get; set; }
        public DbSet<PES_PessoaContato> PES_PessoaContato { get; set; }
        public DbSet<PES_PessoaDocumento> PES_PessoaDocumento { get; set; }
        public DbSet<PES_PessoaEndereco> PES_PessoaEndereco { get; set; }
        public DbSet<PES_TipoDeficiencia> PES_TipoDeficiencia { get; set; }
        public DbSet<PES_TipoEscolaridade> PES_TipoEscolaridade { get; set; }
        public DbSet<QTZ_Blob_Triggers> QTZ_Blob_Triggers { get; set; }
        public DbSet<QTZ_Calendars> QTZ_Calendars { get; set; }
        public DbSet<QTZ_Cron_Triggers> QTZ_Cron_Triggers { get; set; }
        public DbSet<QTZ_Fired_Triggers> QTZ_Fired_Triggers { get; set; }
        public DbSet<QTZ_Job_Details> QTZ_Job_Details { get; set; }
        public DbSet<QTZ_Locks> QTZ_Locks { get; set; }
        public DbSet<QTZ_Paused_Trigger_Grps> QTZ_Paused_Trigger_Grps { get; set; }
        public DbSet<QTZ_Scheduler_State> QTZ_Scheduler_State { get; set; }
        public DbSet<QTZ_Simple_Triggers> QTZ_Simple_Triggers { get; set; }
        public DbSet<QTZ_Simprop_Triggers> QTZ_Simprop_Triggers { get; set; }
        public DbSet<QTZ_Triggers> QTZ_Triggers { get; set; }
        public DbSet<SYS_BancoRelacionado> SYS_BancoRelacionado { get; set; }
        public DbSet<SYS_DiaNaoUtil> SYS_DiaNaoUtil { get; set; }
        public DbSet<Entidade> SYS_Entidade { get; set; }
        public DbSet<SYS_EntidadeContato> SYS_EntidadeContato { get; set; }
        public DbSet<SYS_EntidadeEndereco> SYS_EntidadeEndereco { get; set; }
        public DbSet<SYS_Grupo> SYS_Grupo { get; set; }
        public DbSet<SYS_GrupoPermissao> SYS_GrupoPermissao { get; set; }
        public DbSet<SYS_IntegracaoExterna> SYS_IntegracaoExterna { get; set; }
        public DbSet<SYS_MensagemSistema> SYS_MensagemSistema { get; set; }
        public DbSet<SYS_Modulo> SYS_Modulo { get; set; }
        public DbSet<SYS_ModuloSiteMap> SYS_ModuloSiteMap { get; set; }
        public DbSet<SYS_Parametro> SYS_Parametro { get; set; }
        public DbSet<SYS_ParametroGrupoPerfil> SYS_ParametroGrupoPerfil { get; set; }
        public DbSet<SYS_Servico> SYS_Servico { get; set; }
        public DbSet<SYS_Sistema> SYS_Sistema { get; set; }
        public DbSet<SYS_SistemaEntidade> SYS_SistemaEntidade { get; set; }
        public DbSet<SYS_TipoDocumentacao> SYS_TipoDocumentacao { get; set; }
        public DbSet<TipoEntidade> SYS_TipoEntidade { get; set; }
        public DbSet<SYS_TipoMeioContato> SYS_TipoMeioContato { get; set; }
        public DbSet<SYS_TipoUnidadeAdministrativa> SYS_TipoUnidadeAdministrativa { get; set; }
        public DbSet<SYS_UnidadeAdministrativa> SYS_UnidadeAdministrativa { get; set; }
        public DbSet<SYS_UnidadeAdministrativaContato> SYS_UnidadeAdministrativaContato { get; set; }
        public DbSet<SYS_UnidadeAdministrativaEndereco> SYS_UnidadeAdministrativaEndereco { get; set; }
        public DbSet<SYS_Usuario> SYS_Usuario { get; set; }
        public DbSet<SYS_UsuarioFalhaAutenticacao> SYS_UsuarioFalhaAutenticacao { get; set; }
        public DbSet<SYS_UsuarioGrupo> SYS_UsuarioGrupo { get; set; }
        public DbSet<SYS_UsuarioGrupoUA> SYS_UsuarioGrupoUA { get; set; }
        public DbSet<SYS_UsuarioSenhaHistorico> SYS_UsuarioSenhaHistorico { get; set; }
        public DbSet<SYS_Visao> SYS_Visao { get; set; }
        public DbSet<SYS_VisaoModulo> SYS_VisaoModulo { get; set; }
        public DbSet<SYS_VisaoModuloMenu> SYS_VisaoModuloMenu { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<VW_PES_Pessoa> VW_PES_Pessoa { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CFG_ArquivoMap());
            modelBuilder.Configurations.Add(new CFG_ConfiguracaoMap());
            modelBuilder.Configurations.Add(new CFG_RelatorioMap());
            modelBuilder.Configurations.Add(new CFG_ServidorRelatorioMap());
            modelBuilder.Configurations.Add(new CFG_TemaPadraoMap());
            modelBuilder.Configurations.Add(new CFG_TemaPaletaMap());
            modelBuilder.Configurations.Add(new CFG_UsuarioAPIMap());
            modelBuilder.Configurations.Add(new CFG_VersaoMap());
            modelBuilder.Configurations.Add(new END_CidadeMap());
            modelBuilder.Configurations.Add(new END_EnderecoMap());
            modelBuilder.Configurations.Add(new END_PaisMap());
            modelBuilder.Configurations.Add(new END_UnidadeFederativaMap());
            modelBuilder.Configurations.Add(new LOG_UsuarioADMap());
            modelBuilder.Configurations.Add(new LOG_UsuarioADErroMap());
            modelBuilder.Configurations.Add(new LOG_UsuarioAPIMap());
            modelBuilder.Configurations.Add(new PES_CertidaoCivilMap());
            modelBuilder.Configurations.Add(new PES_PessoaMap());
            modelBuilder.Configurations.Add(new PES_PessoaContatoMap());
            modelBuilder.Configurations.Add(new PES_PessoaDocumentoMap());
            modelBuilder.Configurations.Add(new PES_PessoaEnderecoMap());
            modelBuilder.Configurations.Add(new PES_TipoDeficienciaMap());
            modelBuilder.Configurations.Add(new PES_TipoEscolaridadeMap());
            modelBuilder.Configurations.Add(new QTZ_Blob_TriggersMap());
            modelBuilder.Configurations.Add(new QTZ_CalendarsMap());
            modelBuilder.Configurations.Add(new QTZ_Cron_TriggersMap());
            modelBuilder.Configurations.Add(new QTZ_Fired_TriggersMap());
            modelBuilder.Configurations.Add(new QTZ_Job_DetailsMap());
            modelBuilder.Configurations.Add(new QTZ_LocksMap());
            modelBuilder.Configurations.Add(new QTZ_Paused_Trigger_GrpsMap());
            modelBuilder.Configurations.Add(new QTZ_Scheduler_StateMap());
            modelBuilder.Configurations.Add(new QTZ_Simple_TriggersMap());
            modelBuilder.Configurations.Add(new QTZ_Simprop_TriggersMap());
            modelBuilder.Configurations.Add(new QTZ_TriggersMap());
            modelBuilder.Configurations.Add(new SYS_BancoRelacionadoMap());
            modelBuilder.Configurations.Add(new SYS_DiaNaoUtilMap());
            modelBuilder.Configurations.Add(new SYS_EntidadeMap());
            modelBuilder.Configurations.Add(new SYS_EntidadeContatoMap());
            modelBuilder.Configurations.Add(new SYS_EntidadeEnderecoMap());
            modelBuilder.Configurations.Add(new SYS_GrupoMap());
            modelBuilder.Configurations.Add(new SYS_GrupoPermissaoMap());
            modelBuilder.Configurations.Add(new SYS_IntegracaoExternaMap());
            modelBuilder.Configurations.Add(new SYS_MensagemSistemaMap());
            modelBuilder.Configurations.Add(new SYS_ModuloMap());
            modelBuilder.Configurations.Add(new SYS_ModuloSiteMapMap());
            modelBuilder.Configurations.Add(new SYS_ParametroMap());
            modelBuilder.Configurations.Add(new SYS_ParametroGrupoPerfilMap());
            modelBuilder.Configurations.Add(new SYS_ServicoMap());
            modelBuilder.Configurations.Add(new SYS_SistemaMap());
            modelBuilder.Configurations.Add(new SYS_SistemaEntidadeMap());
            modelBuilder.Configurations.Add(new SYS_TipoDocumentacaoMap());
            modelBuilder.Configurations.Add(new SYS_TipoEntidadeMap());
            modelBuilder.Configurations.Add(new SYS_TipoMeioContatoMap());
            modelBuilder.Configurations.Add(new SYS_TipoUnidadeAdministrativaMap());
            modelBuilder.Configurations.Add(new SYS_UnidadeAdministrativaMap());
            modelBuilder.Configurations.Add(new SYS_UnidadeAdministrativaContatoMap());
            modelBuilder.Configurations.Add(new SYS_UnidadeAdministrativaEnderecoMap());
            modelBuilder.Configurations.Add(new SYS_UsuarioMap());
            modelBuilder.Configurations.Add(new SYS_UsuarioFalhaAutenticacaoMap());
            modelBuilder.Configurations.Add(new SYS_UsuarioGrupoMap());
            modelBuilder.Configurations.Add(new SYS_UsuarioGrupoUAMap());
            modelBuilder.Configurations.Add(new SYS_UsuarioSenhaHistoricoMap());
            modelBuilder.Configurations.Add(new SYS_VisaoMap());
            modelBuilder.Configurations.Add(new SYS_VisaoModuloMap());
            modelBuilder.Configurations.Add(new SYS_VisaoModuloMenuMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new VW_PES_PessoaMap());
        }
    }
}