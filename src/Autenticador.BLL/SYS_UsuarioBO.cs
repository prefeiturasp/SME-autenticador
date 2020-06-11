using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Xml;
using System.Xml.Serialization;
using CoreLibrary.Business.Common;
using Autenticador.DAL;
using Autenticador.Entities;
using Autenticador.WebServices.Consumer;
using CoreLibrary.Data.Common;
using CoreLibrary.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Autenticador.BLL
{
    public enum LoginStatus
    {
        Sucesso, NaoEncontrado,
        SenhaInvalida
        ,

        Bloqueado
        ,

        Expirado
        ,

        Erro
        , Invalido
    }

    public enum eCriptografa
    {
        TripleDES = 1
        ,

        MD5 = 2
        ,

        SHA512 = 3
    }

    /// <summary>
    /// Objeto de negócio referente ao módulo de segurança.
    /// </summary>
    public class SYS_UsuarioBO : BusinessBase<SYS_UsuarioDAO, SYS_Usuario>
    {
        public enum eSituacao
        {
            Ativo = 1
            ,

            Bloqueado
            ,

            Excluido
            ,

            Padrao_Sistema
            ,

            Senha_Expirada
        }

        /// <summary>
        /// Enumerador com os tipos de integração do usuário com AD.
        /// </summary>
        public enum eIntegracaoAD
        {
            [Description("Usuário não integrado")]
            NaoIntegrado = 0

            ,

            [Description("Usuário integrado com AD")]
            IntegradoAD = 1

            ,

            [Description("Usuário integrado com AD/Replicação de senha")]
            IntegradoADReplicacaoSenha = 2
        }

        /// <summary>
        /// Adiciona um novo grupo na struct TmpGrupos caso ele já não esteja contido na lista
        /// </summary>
        /// <param name="gru_id">
        /// id do novo grupo
        /// </param>
        /// <param name="ltGrupos">
        /// lista dos grupos onde deve ser adicionado o novo grupo
        /// </param>
        /// <param name="usg_situacao">
        /// </param>
        public static void AddTmpGrupo(Guid gru_id, SortedDictionary<Guid, TmpGrupos> ltGrupos, int usg_situacao)
        {
            //Caso já exista registro com o mesmo id, deleta para ser atualizado
            if (ltGrupos.ContainsKey(gru_id))
            {
                ltGrupos.Remove(gru_id);
            }

            SYS_Grupo grupo = new SYS_Grupo();
            SYS_Sistema sistema = new SYS_Sistema();
            try
            {
                grupo.gru_id = gru_id;
                SYS_GrupoBO.GetEntity(grupo);
                sistema.sis_id = grupo.sis_id;
                SYS_SistemaBO.GetEntity(sistema);

                TmpGrupos tmp = new TmpGrupos
                {
                    gru_id = grupo.gru_id
                    ,
                    grupo = grupo.gru_nome
                    ,
                    sistema = sistema.sis_nome
                    ,
                    usg_situacao = usg_situacao
                };
                ltGrupos.Add(gru_id, tmp);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Adiciona uma nova entidade ou unidade administrativa na struct TmpEntidadeUA
        /// </summary>
        /// <param name="gru_id">
        /// id do grupo da entidade ou ua
        /// </param>
        /// <param name="ent_id">
        /// id da entidade(obrigatório)
        /// </param>
        /// <param name="uad_id">
        /// id da ua (se for unidade administrativa caso contrario -1)
        /// </param>
        /// <param name="ltEntidadeUA">
        /// Lista onde estão contidos as entidade ou ua do grupo do usuário
        /// </param>
        public static void AddTmpEntidadeUA(Guid gru_id, Guid ent_id, Guid uad_id, IList<TmpEntidadeUA> ltEntidadeUA)
        {
            TmpEntidadeUA entidadeUa = new TmpEntidadeUA();
            try
            {
                entidadeUa.gru_id = gru_id;
                entidadeUa.ent_id = ent_id;
                entidadeUa.uad_id = uad_id;
                if (uad_id != Guid.Empty)
                {
                    SYS_UnidadeAdministrativa ua = new SYS_UnidadeAdministrativa { ent_id = ent_id, uad_id = uad_id };
                    SYS_UnidadeAdministrativaBO.GetEntity(ua);
                    entidadeUa.EntidadeOrUA = ua.uad_nome;
                    entidadeUa.Entidade = false;
                }
                else
                {
                    SYS_Entidade entidade = new SYS_Entidade { ent_id = ent_id };
                    SYS_EntidadeBO.GetEntity(entidade);
                    entidadeUa.EntidadeOrUA = entidade.ent_razaoSocial;
                    entidadeUa.Entidade = true;
                }
                ltEntidadeUA.Add(entidadeUa);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Expira a senha dos usuários que não alteraram no prazo de dias do parâmetro
        /// </summary>
        public static void ExpirarSenhas()
        {
            SYS_UsuarioDAO dao = new SYS_UsuarioDAO();
            dao.ExpirarSenhas();
        }

        /// <summary>
        /// Apaga o grupo do usuário
        /// </summary>
        /// <param name="gru_id">
        /// </param>
        /// <param name="ltGrupos">
        /// </param>
        /// <param name="ltEntidadeUA">
        /// </param>
        public static void RemoveTmpGrupo(Guid gru_id, SortedDictionary<Guid, TmpGrupos> ltGrupos, SortedDictionary<Guid, List<TmpEntidadeUA>> ltEntidadeUA)
        {
            try
            {
                if (ltEntidadeUA.ContainsKey(gru_id))
                {
                    List<TmpEntidadeUA> backup = new List<TmpEntidadeUA>(ltEntidadeUA[gru_id]);
                    if (ltEntidadeUA.Remove(gru_id))
                    {
                        if (!ltGrupos.Remove(gru_id))
                        {
                            ltEntidadeUA.Add(gru_id, backup);
                            throw new Exception();
                        }
                    }
                    else
                        throw new Exception();
                }
                else
                {
                    if (!ltGrupos.Remove(gru_id))
                        throw new Exception();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna os usuários que não foram excluídos logicamente, filtrando por: entidade, grupo e
        /// unidade adminsitrativa
        /// </summary>
        /// <param name="ent_id">
        /// Id da entidade
        /// </param>
        /// <param name="gru_id">
        /// Id do grupo
        /// </param>
        /// <param name="uad_id">
        /// Id da unidade administrativa
        /// </param>
        /// <param name="uad_idSuperior">
        /// Id da unidade administrativa superior
        /// </param>
        /// <param name="usu_idPermissao">
        /// Id do usuário utilizado para verificar permissão
        /// </param>
        /// <param name="gru_idPermissao">
        /// Id do grupo utilizado para verificar permissão
        /// </param>
        /// <param name="paginado">
        /// Indica se será paginado
        /// </param>
        /// <param name="currentPage">
        /// Página atual
        /// </param>
        /// <param name="pageSize">
        /// Quantidade de registros por página
        /// </param>
        /// <returns>
        /// DataTable de usuários
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static DataTable ConsultarPorGrupoUA
            (
                Guid ent_id
                , Guid gru_id
                , Guid uad_id
                , Guid uad_idSuperior
                , Guid usu_idPermissao
                , Guid gru_idPermissao
                , bool paginado
                , int currentPage
                , int pageSize
            )
        {
            SYS_UsuarioDAO dao = new SYS_UsuarioDAO();
            return dao.SelectBy_GrupoUA(ent_id, gru_id, uad_id, uad_idSuperior, usu_idPermissao, gru_idPermissao, paginado, currentPage, pageSize, out totalRecords);
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static DataTable GetSelect(
                    Guid ent_id
                    , string login
                    , string email
                    , byte bloqueado
                    , string pessoa
                    , int currentPage
                    , int pageSize)
        {
            totalRecords = 0;
            SYS_UsuarioDAO dao = new SYS_UsuarioDAO();
            try
            {
                return dao.SelectBy_Pesquisa(ent_id, login, email, bloqueado, pessoa, currentPage / pageSize, pageSize, out totalRecords);
            }
            catch
            {
                throw;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static DataTable GetSelect_Ativos(
                    Guid ent_id
                    , string login
                    , string pessoa
                    , int currentPage
                    , int pageSize)
        {
            totalRecords = 0;
            SYS_UsuarioDAO dao = new SYS_UsuarioDAO();
            try
            {
                return dao.SelectBy_Pesquisa(ent_id, login, string.Empty, 1, pessoa, currentPage / pageSize, pageSize, out totalRecords);
            }
            catch
            {
                throw;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static DataTable GetSelectBy_ent_id_usu_email
                (
                    Guid ent_id
                    , string email
                )
        {
            SYS_UsuarioDAO dao = new SYS_UsuarioDAO();
            try
            {
                return dao.SelectBy_ent_id_usu_email(ent_id, email);
            }
            catch
            {
                throw;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Guid GetSelectBy_pes_id
                (
                    Guid pes_id
                )
        {
            SYS_UsuarioDAO dal = new SYS_UsuarioDAO();
            try
            {
                return dal.SelectBy_pes_id(pes_id);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna os usuários do grupo não paginados.
        /// </summary>
        /// <param name="gru_id">
        /// ID do grupo
        /// </param>
        /// <param name="paginado">
        /// </param>
        /// <param name="currentPage">
        /// </param>
        /// <param name="pageSize">
        /// </param>
        /// <returns>
        /// Lista de usuários do grupo
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelectBy_gru_id
        (
            Guid gru_id
            , bool paginado
            , int currentPage
            , int pageSize
        )
        {
            if (pageSize == 0)
                pageSize = 1;

            totalRecords = 0;

            SYS_UsuarioDAO dao = new SYS_UsuarioDAO();
            try
            {
                return dao.SelectBy_gru_id(gru_id, paginado, currentPage / pageSize, pageSize, out totalRecords);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Seleciona todos os grupos que não estejam excluído somando o nome do sistema ao nome do grupo
        /// </summary>
        /// <returns>
        /// Lista de grupos
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static IList<SYS_Grupo> GetSistemaGrupo()
        {
            SYS_GrupoDAO dal = new SYS_GrupoDAO();
            try
            {
                return dal.SelectBy_All_In_Usuario();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Seleciona todos os grupos que não foram associados ao usuário
        /// </summary>
        /// <param name="grupos">
        /// Lista de grupos associoados ao usuário
        /// </param>
        /// <returns>
        /// Lista de grupos
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static IList<SYS_Grupo> GetSistemaGrupoNotExists(SortedDictionary<Guid, TmpGrupos> grupos)
        {
            SYS_GrupoDAO dal = new SYS_GrupoDAO();
            try
            {
                List<SYS_Grupo> lt = dal.SelectBy_All_In_Usuario();
                var notExists = from ne in lt
                                where !(from e in grupos.Values
                                        select e.gru_id).Contains(ne.gru_id)
                                select ne;
                return notExists.ToList();
            }
            catch
            {
                throw;
            }
        }

        public static bool GetGruposUsuario(Guid usu_id, SortedDictionary<Guid, TmpGrupos> grupos, SortedDictionary<Guid, List<TmpEntidadeUA>> entidades)
        {
            try
            {
                //Limpar as listas a serem carregadas
                grupos.Clear();
                entidades.Clear();
                //Carrega os grupos do usuário
                DataTable dt = SYS_GrupoBO.GetSelect(usu_id);
                foreach (DataRow dr in dt.Rows)
                {
                    grupos.Add(new Guid(dr["gru_id"].ToString())
                        , new TmpGrupos
                        {
                            gru_id = new Guid(dr["gru_id"].ToString())
                             ,
                            grupo = dr["gru_nome"].ToString()
                             ,
                            sistema = dr["sis_nome"].ToString()
                             ,
                            usg_situacao = Convert.ToInt32(dr["usg_situacao"].ToString())
                        }
                    );
                    //Carrega as entidades/ua's do grupo
                    List<TmpEntidadeUA> ltUA = new List<TmpEntidadeUA>();
                    SYS_UsuarioGrupoUADAO dal = new SYS_UsuarioGrupoUADAO();
                    DataTable dtUA = dal.SelectBy_UsuarioGrupo(usu_id, new Guid(dr["gru_id"].ToString()));
                    foreach (DataRow drUA in dtUA.Rows)
                    {
                        ltUA.Add(new TmpEntidadeUA
                        {
                            gru_id = new Guid(drUA["gru_id"].ToString())
                            ,
                            ent_id = new Guid(drUA["ent_id"].ToString())
                            ,
                            uad_id = ((drUA["uad_id"] != DBNull.Value) ? new Guid(drUA["uad_id"].ToString()) : Guid.Empty)
                            ,
                            EntidadeOrUA = drUA["ugu_nome"].ToString()
                            ,
                            Entidade = (drUA["uad_id"] == DBNull.Value)
                        }
                        );
                    }
                    entidades.Add(new Guid(dr["gru_id"].ToString()), ltUA);
                }
                return ((grupos.Count > 0) && (entidades.Count > 0));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna as unidades administrativa e entidades de um determinado usuário e grupo
        /// </summary>
        /// <param name="usu_id">
        /// ID do usuário
        /// </param>
        /// <param name="gru_id">
        /// ID do grupo
        /// </param>
        /// <returns>
        /// Lista de UA/Entidades do grupo do usuário
        /// </returns>
        public static IList<SYS_UsuarioGrupoUA> GetSelectByUsuarioGrupoUA(Guid usu_id, Guid gru_id)
        {
            List<SYS_UsuarioGrupoUA> lt = new List<SYS_UsuarioGrupoUA>();
            SYS_UsuarioGrupoUADAO dal = new SYS_UsuarioGrupoUADAO();
            try
            {
                DataTable dt = dal.SelectBy_UsuarioGrupo(usu_id, gru_id);
                foreach (DataRow dr in dt.Rows)
                {
                    SYS_UsuarioGrupoUA entity = new SYS_UsuarioGrupoUA();
                    lt.Add(dal.DataRowToEntity(dr, entity));
                }
                return lt;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Carrega usuário apartir do Id da entidade e login
        /// </summary>
        /// <param name="entityUsuario">
        /// Entidade usuário contendo ent_id e login
        /// </param>
        /// <returns>
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static bool GetSelectBy_ent_id_usu_login(SYS_Usuario entityUsuario)
        {
            SYS_UsuarioDAO dao = new SYS_UsuarioDAO();
            try
            {
                return dao.CarregarBy_ent_id_usu_login(entityUsuario);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Seleciona usuário por número de CPF e data de nascimento.
        /// </summary>
        /// <param name="ent_id">
        /// Entidade do usuário
        /// </param>
        /// <param name="psd_numero">
        /// Número de cpf
        /// </param>
        /// <param name="pes_dataNascimento">
        /// Data de nascimento
        /// </param>
        /// <returns>
        /// </returns>
        public static DataTable SelecionaPorCPFDataNascimento(Guid ent_id, string psd_numero, DateTime pes_dataNascimento)
        {
            Guid tdo_idCPF = new Guid(SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.TIPO_DOCUMENTACAO_CPF));
            return new SYS_UsuarioDAO().SelecionaPorDocumentoDataNascimento(ent_id, psd_numero, tdo_idCPF, pes_dataNascimento);
        }

        /// <summary>
        /// Seleciona usuários por login.
        /// </summary>
        /// <param name="usu_login">
        /// Login do usuário.
        /// </param>
        /// <returns>
        /// </returns>
        public static List<SYS_Usuario> SelecionaPorLogin(string usu_login)
        {
            return new SYS_UsuarioDAO().SelecionaPorLogin(usu_login);
        }

        /// <summary>
        /// Seleciona usuário por unidade administrativa.
        /// </summary>
        /// <param name="ent_id">
        /// Id da entidade do usuário.
        /// </param>
        /// <param name="uad_id">
        /// Id da unidade administrativa.
        /// </param>
        /// <param name="uad_codigo">
        /// Código da unidade administrativa.
        /// </param>
        /// <param name="trazerFoto">
        /// Indica se deve trazer a foto.
        /// </param>
        /// <param name="usu_id">
        /// ID do usuário.
        /// </param>
        /// <param name="dataAlteracao">
        /// Data base (data de alteração) para retorno dos registros.
        /// </param>
        /// <returns>
        /// DataTable com os usuários selecionados.
        /// </returns>
        public static DataTable SelecionaPorUnidadeAdministrativa(Guid ent_id, Guid uad_id, string uad_codigo, bool trazerFoto, Guid usu_id, DateTime dataAlteracao = new DateTime())
        {
            SYS_UsuarioDAO dao = new SYS_UsuarioDAO();
            return dao.SelecionaPorUnidadeAdministrativa(ent_id, uad_id, uad_codigo, trazerFoto, usu_id, dataAlteracao);
        }

        /// <summary>
        /// Seleciona usuário por entidade. Filtro opcional por pessoa.
        /// </summary>
        /// <param name="ent_id">
        /// ID da entidade do usuário.
        /// </param>
        /// <param name="pes_id">
        /// ID da pessoa do usuário. (opcional)
        /// </param>
        /// <param name="trazerFoto">
        /// Indica se deve trazer a foto.
        /// </param>
        /// <param name="dataAlteracao">
        /// Data base (data de alteração) para retorno dos registros.
        /// </param>
        /// <returns>
        /// Datatable com os usuários selecionados.
        /// </returns>
        public static DataTable SelecionaPorEntidade(Guid ent_id, Guid pes_id, bool trazerFoto, DateTime dataAlteracao = new DateTime())
        {
            return new SYS_UsuarioDAO().SelecionaPorEntidade(ent_id, pes_id, trazerFoto, dataAlteracao);
        }

        /// <summary>
        /// Seleciona os usuários ativos e a senha padrão, para comparação com a senha atual.
        /// </summary>
        /// <returns>
        /// </returns>
        public static DataTable SelecionaUsuariosSenhaPadrao()
        {
            return new SYS_UsuarioDAO().SelecionaUsuariosSenhaPadrao();
        }

        /// <summary>
        /// Verifica se o email que está sendo cadastrado já existe na tabela SYS_Usuario
        /// </summary>
        /// <param name="entity">
        /// Entidade SYS_Usuario
        /// </param>
        /// <returns>
        /// true = o email já existe na tabela SYS_Usuario / false = o email não existe na tabela SYS_Usuario
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static bool VerificaEmailExistente
        (
            SYS_Usuario entity
        )
        {
            SYS_UsuarioDAO dal = new SYS_UsuarioDAO();
            try
            {
                return dal.SelectBy__ent_id_usu_login_usu_email_pes_id(entity.ent_id, entity.usu_id, string.Empty, string.Empty, entity.usu_email, Guid.Empty, 0);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Verifica se é letra ou número
        /// </summary>
        /// <param name="c">
        /// Char que será testado
        /// </param>
        /// <returns>
        /// Bool informando se é letra ou número
        /// </returns>
        public static bool IsLetterOrDigit(char c)
        {
            int ch = Convert.ToInt32(c);

            if (((ch >= 48) && (ch <= 57)) || ((ch >= 65) && (ch <= 90)) || ((ch >= 97) && (ch <= 122)))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Valida string
        /// </summary>
        /// <param name="str">
        /// String a ser validada
        /// </param>
        /// <param name="utilizaEspeciais">
        /// Bool informando se serão permitidos caracteres especiais
        /// </param>
        /// <returns>
        /// Bool informando se é válida ou não a string
        /// </returns>
        public static bool ValidaString(string str, bool utilizaEspeciais)
        {
            if (!string.IsNullOrEmpty(str))
            {
                string especiaisPermitidos = "-_@/.";

                foreach (char c in str)
                {
                    if (!IsLetterOrDigit(c))
                    {
                        if (utilizaEspeciais)
                        {
                            if (especiaisPermitidos.IndexOf(c) < 0)
                                return false;
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Checa o identificador do usuário no provider
        /// </summary>
        /// <param name="providerName">
        /// Nome do Provider
        /// </param>
        /// <param name="providerKey">
        /// Chave do usuário
        /// </param>
        /// <returns>
        /// </returns>
        public static SYS_Usuario ValidarComLoginExterno(string providerName, string providerKey)
        {
            // Deve retornar o usuário conforme providerName e providerKey. Tabela
            // SYS_UsuarioLoginProvider Se não encontrar retornar null.

            SYS_UsuarioDAO dal = new SYS_UsuarioDAO();

            SYS_Usuario entity = dal.Select_Usuario_By_LoginProvider_ProviderKey(providerName, providerKey);

            return entity;
        }

        /// <summary>
        /// Utilizado para alterar apenas a senha do usuário
        /// </summary>
        /// <param name="entity">
        /// Entidade SYS_Usuario
        /// </param>
        /// <returns>
        /// True = sucesso | False = fracasso
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static bool AlterarSenhaUsuario(SYS_Usuario entity, TalkDBTransaction banco = null)
        {
            return AlterarSenhaUsuario(entity, true, banco);
        }

        /// <summary>
        /// Utilizado para alterar apenas a senha do usuário
        /// </summary>
        /// <param name="entity">
        /// Entidade SYS_Usuario
        /// </param>
        /// <param name="criptografarSenha">
        /// Flag para senha criptografada (True - criptografada | False - não criptografada)
        /// </param>
        /// <returns>
        /// True = sucesso | False = fracasso
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static bool AlterarSenhaUsuario(SYS_Usuario entity, bool criptografarSenha, TalkDBTransaction banco = null)
        {
            SYS_UsuarioDAO dao = banco == null ? new SYS_UsuarioDAO() : new SYS_UsuarioDAO { _Banco = banco };
            if (banco == null)
            {
                dao._Banco.Open(IsolationLevel.ReadCommitted);
            }

            try
            {
                string senhaDescriptografada = entity.usu_senha;
                bool salvarHistoricoSenha = SYS_ParametroBO.ParametroValorBooleano(SYS_ParametroBO.eChave.SALVAR_HISTORICO_SENHA_USUARIO);

                // Configura usuário live, caso exista integração externa e conta live
                ManageUserLive live = new ManageUserLive();
                if ((live.ExistsIntegracaoExterna()) && (live.IsContaEmail(entity.usu_email)))
                {
                    // Verifica se existe conta de email
                    UserLive entityUserLive = new UserLive();
                    entityUserLive.email = entity.usu_email;
                    if (live.VerificarContaEmailExistente(entityUserLive))
                    {
                        entity.usu_login = entityUserLive.login;
                        entity.usu_email = entityUserLive.email;
                        if (criptografarSenha)
                            entity.usu_criptografia = Convert.ToByte(eCriptografa.MD5);
                    }
                }

                // Verifica se senha criptografada
                if (criptografarSenha)
                {
                    // Configura criptografia da senha
                    eCriptografa criptografia = eCriptografa.SHA512;
                    entity.usu_senha = UtilBO.CriptografarSenha(entity.usu_senha, criptografia);
                    entity.usu_criptografia = Convert.ToByte(criptografia);
                }
                else
                {
                    if (!Enum.IsDefined(typeof(eCriptografa), Enum.Parse(typeof(eCriptografa), Convert.ToString(entity.usu_criptografia), true)))
                        throw new ArgumentException("Criptografia é obrigatório.");

                    // Configura criptografia da senha
                    eCriptografa criptografia = (eCriptografa)Enum.Parse(typeof(eCriptografa), Convert.ToString(entity.usu_criptografia), true);

                    if (entity.usu_integracaoAD == (short)SYS_UsuarioBO.eIntegracaoAD.IntegradoADReplicacaoSenha && criptografia != eCriptografa.TripleDES)
                        throw new ArgumentException("Criptografia de usuários integrados com AD e replicação de senha deve ser do tipo TripleDES.");

                    if (entity.usu_integracaoAD == (short)SYS_UsuarioBO.eIntegracaoAD.IntegradoADReplicacaoSenha)
                        senhaDescriptografada = new SymmetricAlgorithm(SymmetricAlgorithm.Tipo.TripleDES).Decrypt(entity.usu_senha);
                }

                if (salvarHistoricoSenha)
                {
                    List<SYS_UsuarioSenhaHistorico> listaHistoricoSenhas = SYS_UsuarioSenhaHistoricoBO.SelecionaUltimasSenhas(entity.usu_id, dao._Banco);

                    if (listaHistoricoSenhas.Any(p => p.ush_senha == entity.usu_senha && p.ush_criptografia == entity.usu_criptografia))
                    {
                        throw new ArgumentException(String.Format(SYS_MensagemSistemaBO.RetornaValor(SYS_MensagemSistemaChave.MeusDadosMensagemValidacaoHistoricoSenha),
                                                                  SYS_ParametroBO.ParametroValorInt32(SYS_ParametroBO.eChave.QUANTIDADE_ULTIMAS_SENHAS_VALIDACAO).ToString()));
                    }
                }

                // Atualiza senha no bd
                dao.Update_Senha(entity.usu_id, entity.usu_senha, entity.usu_criptografia);

                if (entity.usu_integracaoAD == (short)SYS_UsuarioBO.eIntegracaoAD.IntegradoADReplicacaoSenha) LOG_UsuarioADBO.Insert(entity, LOG_UsuarioAD.eAcao.AlterarSenha, dao._Banco, senhaDescriptografada);

                if (salvarHistoricoSenha)
                {
                    SYS_UsuarioSenhaHistoricoBO.Salvar(entity, banco);
                }

                // Atualiza senha do usuário no live, caso necessário
                AlterarSenhaUsuarioLive(entity, live, false);

                return true;
            }
            catch (Exception ex)
            {
                if (banco == null)
                {
                    dao._Banco.Close(ex);
                }
                throw;
            }
            finally
            {
                if (dao._Banco.ConnectionIsOpen && banco == null)
                    dao._Banco.Close();
            }
        }

        /// <summary>
        /// Utilizado para atualizar o usuário e alterar senha
        /// </summary>
        /// <param name="entity">
        /// Entidade SYS_Usuario
        /// </param>
        /// <returns>
        /// True = sucesso | False = fracasso
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static bool AlterarSenhaAtualizarUsuario(SYS_Usuario entity, bool salvarHistoricoSenha = false, bool alterarSituacao = false)
        {
            return AlterarSenhaAtualizarUsuario(entity, true, true, salvarHistoricoSenha, alterarSituacao);
        }

        /// <summary>
        /// Utilizado para atualizar usuario e alterar senha (criptografada ou não)
        /// </summary>
        /// <param name="entity">
        /// Entidade SYS_Usuario
        /// </param>
        /// <param name="criptografarSenha">
        /// Flag para senha criptografada (True - criptografada | False - não criptografada)
        /// </param>
        /// <param name="alterarSenhaLive">
        /// </param>
        /// <returns>
        /// True = sucesso | False = fracasso
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static bool AlterarSenhaAtualizarUsuario(SYS_Usuario entity, bool criptografarSenha, bool alterarSenhaLive, bool salvarHistoricoSenha = false, bool alterarSituacao = false)
        {
            SYS_UsuarioDAO dao = new SYS_UsuarioDAO();
            dao._Banco.Open(IsolationLevel.ReadCommitted);

            try
            {
                // Configura usuário live, caso exista integração externa e conta live
                ManageUserLive live = new ManageUserLive();
                if ((live.ExistsIntegracaoExterna()) && (live.IsContaEmail(entity.usu_email)))
                {
                    // Verifica se existe conta de email
                    UserLive entityUserLive = new UserLive();
                    entityUserLive.email = entity.usu_email;
                    if (live.VerificarContaEmailExistente(entityUserLive))
                    {
                        entity.usu_login = entityUserLive.login;
                        entity.usu_email = entityUserLive.email;
                        if (criptografarSenha)
                            entity.usu_criptografia = Convert.ToByte(eCriptografa.MD5);
                    }
                }

                string senha = entity.usu_senha;

                // Salva dados na tabela SYS_Usuario
                if (entity.Validate())
                {
                    if (VerificaLoginExistente(entity))
                        throw new DuplicateNameException("Já existe um usuário cadastrado com este login.");

                    if (SYS_ParametroBO.Parametro_ValidarUnicidadeEmailUsuario() && !string.IsNullOrEmpty(entity.usu_email) &&
                        VerificaEmailExistente(entity))
                        throw new DuplicateNameException("Já existe um usuário cadastrado com este e-mail.");

                    dao.Salvar(entity);
                }
                else
                {
                    throw new CoreLibrary.Validation.Exceptions.ValidationException(entity.PropertiesErrorList[0].Message);
                }

                // Verifica se senha criptografada
                if (criptografarSenha)
                {
                    // Configura criptografia da senha
                    eCriptografa criptografia = (eCriptografa)Enum.Parse(typeof(eCriptografa), Convert.ToString(entity.usu_criptografia), true);
                    if (!Enum.IsDefined(typeof(eCriptografa), criptografia))
                        criptografia = eCriptografa.SHA512;
                    entity.usu_senha = UtilBO.CriptografarSenha(entity.usu_senha, criptografia);
                    entity.usu_criptografia = Convert.ToByte(criptografia);
                }
                else if (!Enum.IsDefined(typeof(eCriptografa), Enum.Parse(typeof(eCriptografa), Convert.ToString(entity.usu_criptografia), true)))
                    throw new ArgumentException("Criptografia é obrigatório.");

                bool verificarHistoricoSenha = SYS_ParametroBO.ParametroValorBooleano(SYS_ParametroBO.eChave.SALVAR_HISTORICO_SENHA_USUARIO);

                if (verificarHistoricoSenha)
                {
                    List<SYS_UsuarioSenhaHistorico> listaHistoricoSenhas = SYS_UsuarioSenhaHistoricoBO.SelecionaUltimasSenhas(entity.usu_id, dao._Banco);

                    if (listaHistoricoSenhas.Any(p => p.ush_senha == entity.usu_senha && p.ush_criptografia == entity.usu_criptografia))
                    {
                        throw new ArgumentException(String.Format(SYS_MensagemSistemaBO.RetornaValor(SYS_MensagemSistemaChave.MeusDadosMensagemValidacaoHistoricoSenha),
                                                                  SYS_ParametroBO.ParametroValorInt32(SYS_ParametroBO.eChave.QUANTIDADE_ULTIMAS_SENHAS_VALIDACAO).ToString()));
                    }
                }

                // Atualiza senha
                dao.Update_Senha(entity.usu_id, entity.usu_senha, entity.usu_criptografia);

                if (alterarSituacao && entity.usu_situacao == (byte)eSituacao.Senha_Expirada)
                {
                    dao.AtualizarSituacao(entity.usu_id, (byte)eSituacao.Ativo);
                }

                // Atualiza senha do usuário no live, caso necessário
                if (alterarSenhaLive) AlterarSenhaUsuarioLive(entity, live, false);

                if (salvarHistoricoSenha) LOG_UsuarioADBO.Insert(entity, LOG_UsuarioAD.eAcao.AlterarSenha, dao._Banco, senha);

                if (verificarHistoricoSenha)
                {
                    SYS_UsuarioSenhaHistoricoBO.Salvar(entity, dao._Banco);
                }

                return true;
            }
            catch (Exception err)
            {
                dao._Banco.Close(err);
                throw;
            }
            finally
            {
                dao._Banco.Close();
            }
        }

        /// <summary>
        /// Altera os dados do usuário.
        /// </summary>
        /// <param name="entity">
        /// Entidade com os dados do usuário.
        /// </param>
        /// <param name="alterarSenha">
        /// Flag que indica se a senha será alterada.
        /// </param>
        /// <returns>
        /// </returns>
        public static bool AlterarDadosUsuario(SYS_Usuario entity, bool alterarSenha)
        {
            bool retorno = false;
            TalkDBTransaction banco = new SYS_UsuarioDAO()._Banco;
            banco.Open(IsolationLevel.ReadCommitted);

            try
            {
                if (alterarSenha)
                {
                    retorno |= AlterarSenhaUsuario(entity, banco);
                }

                if (SYS_ParametroBO.ParametroValorBooleano(SYS_ParametroBO.eChave.PERMITIR_ALTERAR_EMAIL_MEUSDADOS))
                {
                    retorno |= AlterarEmailUsuario(entity, banco);
                }

                return retorno;
            }
            catch (Exception ex)
            {
                banco.Close(ex);
                throw;
            }
            finally
            {
                if (banco.ConnectionIsOpen)
                {
                    banco.Close();
                }
            }
        }

        /// <summary>
        /// Incrementa 1 no campo integridade do usuario
        /// </summary>
        /// <param name="usu_id">
        /// ID do usuario - obrigatório
        /// </param>
        /// <param name="banco">
        /// Transação - obrigatório
        /// </param>
        /// <returns>
        /// Sucesso na operação
        /// </returns>
        public static bool IncrementaIntegridade(Guid usu_id, TalkDBTransaction banco)
        {
            SYS_UsuarioDAO dao = new SYS_UsuarioDAO()
            {
                _Banco = banco
            };
            return dao.Update_IncrementaIntegridade(usu_id);
        }

        /// <summary>
        /// Decrementa 1 do campo integridade do usuario.
        /// </summary>
        /// <param name="usu_id">
        /// ID do usuario - obrigatório
        /// </param>
        /// <param name="banco">
        /// Transação - obrigatório
        /// </param>
        /// <returns>
        /// Sucesso na operação
        /// </returns>
        public static bool DecrementaIntegridade(Guid usu_id, TalkDBTransaction banco)
        {
            SYS_UsuarioDAO dao = new SYS_UsuarioDAO()
            {
                _Banco = banco
            };
            return dao.Update_DecrementaIntegridade(usu_id);
        }

        /// <summary>
        /// Atualiza a situação dos usuários para senha expirada.
        /// </summary>
        /// <param name="usuIds">
        /// String concatenada com os ids dos usuários
        /// </param>
        public static void ExpiraUsuariosSenhaPadrao(string usuIds)
        {
            new SYS_UsuarioDAO().ExpiraUsuariosSenhaPadrao(usuIds);
        }

        [Obsolete("Utilizar o método ValidarLogin para cenários com o AspNet Identity", false)]
        public static LoginStatus LoginWEB(SYS_Usuario entityUsuarioLogin, bool autenticarNoSucesso = true)
        {
            int indexAD = entityUsuarioLogin.usu_login.IndexOf('\\');
            SYS_UsuarioDAO dao = new SYS_UsuarioDAO();
            LoginStatus credential = LoginStatus.Sucesso;

            if (indexAD <= 0)
            {
                string senha = entityUsuarioLogin.usu_senha;
                if (dao.CarregarBy_ent_id_usu_login(entityUsuarioLogin))
                {
                    // Configura criptografia da senha
                    eCriptografa criptografia = (eCriptografa)Enum.Parse(typeof(eCriptografa), Convert.ToString(entityUsuarioLogin.usu_criptografia), true);
                    if (!Enum.IsDefined(typeof(eCriptografa), criptografia))
                        criptografia = eCriptografa.SHA512;
                    senha = UtilBO.CriptografarSenha(senha, criptografia);

                    // Valida login do usuário
                    if (!UtilBO.EqualsSenha(entityUsuarioLogin.usu_senha, senha, criptografia))
                        credential = LoginStatus.SenhaInvalida;
                    else if ((entityUsuarioLogin.usu_situacao == 2) || (entityUsuarioLogin.usu_situacao == 3))
                        credential = LoginStatus.Bloqueado;
                    else if (entityUsuarioLogin.usu_situacao == 5)
                        credential = LoginStatus.Expirado;
                    else
                    {
                        credential = LoginStatus.Sucesso;

                        if (autenticarNoSucesso)
                        {
                            // Autenticação SAML
                            AutenticarUsuario(entityUsuarioLogin);
                        }
                    }
                }
                else
                {
                    credential = LoginStatus.NaoEncontrado;
                }

                return credential;
            }

            entityUsuarioLogin.usu_dominio = entityUsuarioLogin.usu_login.Split('\\')[0];
            entityUsuarioLogin.usu_login = entityUsuarioLogin.usu_login.Split('\\')[1];
            if (dao.CarregarBy_ent_id_usu_dominio_usu_login(entityUsuarioLogin))
            {
                if (!CoreLibrary.LDAP.LdapUtil.Exists(entityUsuarioLogin.usu_dominio))
                    throw new ArgumentException("Domínio não encontrado.");

                entityUsuarioLogin.usu_dominio = CoreLibrary.LDAP.LdapUtil.CheckPath(entityUsuarioLogin.usu_dominio);
                CoreLibrary.LDAP.LdapUsers userAD = new CoreLibrary.LDAP.LdapUsers(entityUsuarioLogin.usu_dominio);

                if (!userAD.UserExists(entityUsuarioLogin.usu_login))
                    throw new ArgumentException("Usuário não encontrado.");

                userAD.Authenticate(entityUsuarioLogin.usu_login, entityUsuarioLogin.usu_senha);

                if (autenticarNoSucesso)
                {
                    // Autenticação SAML
                    AutenticarUsuario(entityUsuarioLogin);
                }
            }
            else
            {
                credential = LoginStatus.NaoEncontrado;
            }

            return credential;
        }

        /// <summary>
        /// Verifica as credencias do usuário no sistema
        /// </summary>
        /// <param name="entityUsuarioLogin">
        /// Entidade SYS_Usuario com login e senha (sem criptografada)
        /// </param>
        /// <param name="autenticarNoSucesso">
        /// Indica se deve ser autenticado o usuário no FormsAuthentication, caso haja sucesso no login
        /// </param>
        /// <returns>
        /// Credenciais (status do usuário)
        /// </returns>
        public static LoginStatus ValidarLogin(SYS_Usuario entityUsuarioLogin, bool ProviderExterno = false)
        {
            int indexAD = entityUsuarioLogin.usu_login.IndexOf('\\');
            SYS_UsuarioDAO dao = new SYS_UsuarioDAO();
            LoginStatus credential = LoginStatus.Sucesso;

            if (indexAD <= 0)
            {
                string senhaDigitada = entityUsuarioLogin.usu_senha;

                if (dao.CarregarBy_ent_id_usu_login(entityUsuarioLogin))
                {
                    if (!ProviderExterno)
                    {
                        // Configura criptografia da senha
                        eCriptografa criptografia = (eCriptografa)Enum.Parse(typeof(eCriptografa), Convert.ToString(entityUsuarioLogin.usu_criptografia), true);
                        if (!Enum.IsDefined(typeof(eCriptografa), criptografia))
                            criptografia = eCriptografa.SHA512;
                        senhaDigitada = UtilBO.CriptografarSenha(senhaDigitada, criptografia);

                        // Valida login do usuário
                        if (!UtilBO.EqualsSenha(entityUsuarioLogin.usu_senha, senhaDigitada, criptografia))
                        {
                            credential = LoginStatus.SenhaInvalida;
                            return credential;
                        }
                    }

                    //continua a verificação da situação do usuário.
                    if ((entityUsuarioLogin.usu_situacao == 2) || (entityUsuarioLogin.usu_situacao == 3))
                        credential = LoginStatus.Bloqueado;
                    else if (entityUsuarioLogin.usu_situacao == 5)
                        credential = LoginStatus.Expirado;
                    else
                    {
                        credential = LoginStatus.Sucesso;
                    }
                }
                else
                {
                    credential = LoginStatus.NaoEncontrado;
                }

                return credential;
            }

            entityUsuarioLogin.usu_dominio = entityUsuarioLogin.usu_login.Split('\\')[0];
            entityUsuarioLogin.usu_login = entityUsuarioLogin.usu_login.Split('\\')[1];
            if (dao.CarregarBy_ent_id_usu_dominio_usu_login(entityUsuarioLogin))
            {
                if (!CoreLibrary.LDAP.LdapUtil.Exists(entityUsuarioLogin.usu_dominio))
                    throw new ArgumentException("Domínio não encontrado.");

                entityUsuarioLogin.usu_dominio = CoreLibrary.LDAP.LdapUtil.CheckPath(entityUsuarioLogin.usu_dominio);
                CoreLibrary.LDAP.LdapUsers userAD = new CoreLibrary.LDAP.LdapUsers(entityUsuarioLogin.usu_dominio);

                if (!userAD.UserExists(entityUsuarioLogin.usu_login))
                    throw new ArgumentException("Usuário não encontrado.");

                userAD.Authenticate(entityUsuarioLogin.usu_login, entityUsuarioLogin.usu_senha);
            }
            else
            {
                credential = LoginStatus.NaoEncontrado;
            }

            return credential;
        }

        /// <summary>
        /// Autentica usuário através da autenticação SAML
        /// </summary>
        /// <param name="entity">
        /// Usuário da session
        /// </param>
        [Obsolete()]
        public static void AutenticarUsuario(SYS_Usuario entity)
        {
            try
            {
                // Grava Ticket de autenticação no Cookie
                FormsAuthentication.Initialize();
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                    1
                    , UtilBO.FormatNameFormsAuthentication(entity)
                    , DateTime.UtcNow
                    // Hours (para carregar a Session quando ela expirar)
                    , DateTime.UtcNow.AddHours(10)
                    , false
                    , String.Empty
                    , FormsAuthentication.FormsCookiePath);
                string hash = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                if (ticket.IsPersistent)
                    cookie.Expires = ticket.Expiration;
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            catch
            {
                throw;
            }
        }

        [Obsolete("Utilizar o método para autenticar com o AspNet Identity", false)]
        public static void AutenticarUsuario(SYS_Usuario entityUsuario, SYS_Grupo entityGrupo)
        {
            SYS_Sistema entitySistema = new SYS_Sistema
            {
                sis_id = entityGrupo.sis_id
            };

            try
            {
                // Verifica tipo de autenticação
                SYS_SistemaBO.GetEntity(entitySistema);
                if (entitySistema.sis_tipoAutenticacao == 1)
                {
                    // Carrega visão para utilizar como dado do usuário na autenticação
                    SYS_Visao entityVisao = new SYS_Visao
                    {
                        vis_id = entityGrupo.vis_id
                    };
                    SYS_VisaoBO.GetEntity(entityVisao);

                    // Grava Ticket de autenticação no Cookie
                    FormsAuthentication.Initialize();
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                        1
                        , UtilBO.FormatNameFormsAuthentication(entityUsuario, entityGrupo)
                        , DateTime.UtcNow
                        // Hours (para carregar a Session quando ela expirar)
                        , DateTime.UtcNow.AddHours(10)
                        , false
                        , entityVisao.vis_nome
                        , FormsAuthentication.FormsCookiePath);
                    string hash = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                    if (ticket.IsPersistent)
                        cookie.Expires = ticket.Expiration;
                    HttpContext.Current.Response.Cookies.Add(cookie);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Autentica o usuário de acordo com o tipo de autenticação
        /// </summary>
        /// <param name="entityUsuario">
        /// Grupo do usuário na session
        /// </param>
        /// <param name="entityGrupo">
        /// Usuário da session
        /// </param>
        /// <summary>
        /// Utilizado para salvar o usuario e os grupos selecionados
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static bool Save(SYS_Usuario entity, SortedDictionary<Guid, TmpGrupos> grupos, SortedDictionary<Guid, List<TmpEntidadeUA>> entidades, bool enviaemail, string nome, string nome_portal, string host, string emailsuporte, CoreLibrary.Data.Common.TalkDBTransaction banco, bool salvarHistoricoSenha = false, string emailRemetente = null)
        {
            return Save(entity, true, grupos, entidades, enviaemail, nome, nome_portal, host, emailsuporte, banco, salvarHistoricoSenha, emailRemetente);
        }

        /// <summary>
        /// Utilizado para salvar o usuário e definir automaticamente os grupos
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static bool Save(SYS_Usuario entity, string padrao, bool enviaemail, string nome, string nome_portal, string host, string emailsuporte, Guid ent_idUsuarioCadastro, CoreLibrary.Data.Common.TalkDBTransaction banco)
        {
            //Chama método padrão para salvar o usuário e os grupos definidos automaticamente pelo tipo de usuário
            return Save(entity, true, Guid.Empty, padrao, enviaemail, nome, nome_portal, host, emailsuporte, ent_idUsuarioCadastro, banco);
        }

        /// <summary>
        /// Utilizado para salvar o usuario e os grupos definidos automaticamente
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static bool Save(SYS_Usuario entity, Guid uad_id, string padrao, bool enviaemail, string nome, string nome_portal, string host, string emailsuporte, Guid ent_idUsuarioCadastro, CoreLibrary.Data.Common.TalkDBTransaction banco)
        {
            return Save(entity, true, uad_id, padrao, enviaemail, nome, nome_portal, host, emailsuporte, ent_idUsuarioCadastro, banco);
        }

        /// <summary>
        /// Utilizado para salvar o usuario e recuperar sua senha pelo e-mail
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static bool Save(SYS_Usuario entity, string nome, string nome_portal, string host, string emailsuporte, string emailRemetente = null)
        {
            SYS_UsuarioDAO usuario = new SYS_UsuarioDAO();
            usuario._Banco.Open(IsolationLevel.ReadCommitted);

            try
            {
                string tamanhoSenha = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.TAMANHO_SENHA_USUARIO);
                Regex intRegex = new Regex(@"\d+");
                int tamanho = 6;

                if (intRegex.IsMatch(tamanhoSenha) && SYS_ParametroBO.ParametroValorBooleano(SYS_ParametroBO.eChave.GERAR_SENHA_FORMATO_PARAMETRIZADO))
                {
                    tamanho = Convert.ToInt32(intRegex.Matches(tamanhoSenha)[0].Value);
                }

                //Gera uma senha automaticamente para enviar por email
                string novasenha = UtilBO._CriaSenha(tamanho);

                // Configura usuário live, caso exista integração externa e conta live
                ManageUserLive live = new ManageUserLive();
                if ((live.ExistsIntegracaoExterna()) && (live.IsContaEmail(entity.usu_email)))
                {
                    // Verifica se existe conta de email
                    UserLive entityUserLive = new UserLive();
                    entityUserLive.email = entity.usu_email;
                    if (live.VerificarContaEmailExistente(entityUserLive))
                    {
                        entity.usu_login = entityUserLive.login;
                        entity.usu_email = entityUserLive.email;
                        entity.usu_criptografia = Convert.ToByte(eCriptografa.MD5);
                    }
                }

                // Configura criptografia da senha
                eCriptografa criptografia = eCriptografa.SHA512;
                entity.usu_senha = UtilBO.CriptografarSenha(novasenha, criptografia);
                entity.usu_criptografia = Convert.ToByte(criptografia);

                bool verificarHistoricoSenha = SYS_ParametroBO.ParametroValorBooleano(SYS_ParametroBO.eChave.SALVAR_HISTORICO_SENHA_USUARIO);

                //Salva dados na tabela SYS_Usuario
                if (entity.Validate())
                {
                    if (VerificaLoginExistente(entity))
                        throw new DuplicateNameException("Já existe um usuário cadastrado com este login.");

                    if (SYS_ParametroBO.Parametro_ValidarUnicidadeEmailUsuario() && !string.IsNullOrEmpty(entity.usu_email) &&
                        VerificaEmailExistente(entity))
                        throw new DuplicateNameException("Já existe um usuário cadastrado com este e-mail.");

                    if (verificarHistoricoSenha)
                    {
                        List<SYS_UsuarioSenhaHistorico> listaHistoricoSenhas = SYS_UsuarioSenhaHistoricoBO.SelecionaUltimasSenhas(entity.usu_id, usuario._Banco);

                        if (listaHistoricoSenhas.Any(p => p.ush_senha == entity.usu_senha && p.ush_criptografia == entity.usu_criptografia))
                        {
                            throw new ArgumentException(String.Format(SYS_MensagemSistemaBO.RetornaValor(SYS_MensagemSistemaChave.MeusDadosMensagemValidacaoHistoricoSenha),
                                                                      SYS_ParametroBO.ParametroValorInt32(SYS_ParametroBO.eChave.QUANTIDADE_ULTIMAS_SENHAS_VALIDACAO).ToString()));
                        }
                    }

                    usuario.Salvar(entity);
                }
                else
                {
                    throw new CoreLibrary.Validation.Exceptions.ValidationException(entity.PropertiesErrorList[0].Message);
                }

                usuario.Update_Senha(entity.usu_id, entity.usu_senha, entity.usu_criptografia);

                if (verificarHistoricoSenha)
                {
                    SYS_UsuarioSenhaHistoricoBO.Salvar(entity, usuario._Banco);
                }

                SYS_Entidade entidade = new SYS_Entidade { ent_id = entity.ent_id };
                SYS_EntidadeBO.GetEntity(entidade);
                _EnviarEmailEsqueciSenha(nome_portal, host, emailsuporte, nome, entity.usu_login, entity.usu_email, novasenha, entidade.ent_razaoSocial, emailRemetente);

                // Atualiza senha do usuário no live, caso necessário
                AlterarSenhaUsuarioLive(entity, live, false);

                return true;
            }
            catch (Exception err)
            {
                usuario._Banco.Close(err);
                throw;
            }
            finally
            {
                usuario._Banco.Close();
            }
        }

        /// <summary>
        /// Utilizado para salvar o usuário, definir automaticamente os grupos e salvar usuário live
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static bool Save(SYS_Usuario entity, UserLive entityUserLive, bool integrarLive, string padrao, bool enviarEmail, string nome, string nome_portal, string host, string emailsuporte, Guid ent_idUsuarioCadastro, CoreLibrary.Data.Common.TalkDBTransaction banco)
        {
            bool criptografarSenha = true;

            // Verifica se existe integração externa
            ManageUserLive live = new ManageUserLive();
            if (integrarLive && live.ExistsIntegracaoExterna())
            {
                // Verifica se existe conta de email
                bool existeContaEmail = (!string.IsNullOrEmpty(entity.usu_email));
                if (existeContaEmail)
                {
                    entityUserLive.email = entity.usu_email;
                    existeContaEmail = (live.VerificarContaEmailExistente(entityUserLive));
                }
                // Cria conta de email para o usuário no live
                if (!existeContaEmail)
                {
                    if (live.CriarContaEmail(entityUserLive))
                    {
                        entity.usu_login = entityUserLive.login;
                        entity.usu_email = entityUserLive.email;
                        entity.usu_situacao = entityUserLive.situacao;
                        entity.usu_senha = entityUserLive.senha;
                        entity.usu_criptografia = Convert.ToByte(eCriptografa.MD5);
                        criptografarSenha = false;
                        enviarEmail = false;
                    }
                }
            }

            // Chamada ao método padrão para salvar o usuário e definir grupos automaticamente
            return Save(entity, criptografarSenha, Guid.Empty, padrao, enviarEmail, nome, nome_portal, host, emailsuporte, ent_idUsuarioCadastro, banco);
        }

        /// <summary>
        /// Deleta logicamente uma Entidade
        /// </summary>
        /// <param name="entity">
        /// Entidade SYS_Entidade
        /// </param>
        /// <param name="banco">
        /// Conexão aberta com o banco de dados ou null para uma nova conexão
        /// </param>
        /// <returns>
        /// True = deletado/alterado | False = não deletado/alterado
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public new static bool Delete
        (
            SYS_Usuario entity
            , CoreLibrary.Data.Common.TalkDBTransaction banco
        )
        {
            SYS_UsuarioDAO dao = new SYS_UsuarioDAO();

            if (banco == null)
                dao._Banco.Open(IsolationLevel.ReadCommitted);
            else
                dao._Banco = banco;

            try
            {
                //Verifica se o usuário pode ser deletado
                if (dao.Select_Integridade(entity.usu_id) > 0)
                {
                    throw new Exception("Não é possível excluir o usuário pois possui outros registros ligados a ele.");
                }

                DataTable dt = SYS_UsuarioGrupoUABO.GetSelect(entity.usu_id, Guid.Empty);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Guid ent_id = new Guid(dt.Rows[i]["ent_id"].ToString());
                    Guid uad_id = new Guid(dt.Rows[i]["uad_id"].ToString());
                    SYS_UnidadeAdministrativaDAO uadDAO = new SYS_UnidadeAdministrativaDAO { _Banco = dao._Banco };
                    uadDAO.Update_DecrementaIntegridade(ent_id, uad_id);
                }

                if (entity.pes_id != Guid.Empty)
                {
                    //Decrementa um na integridade da pessoa
                    PES_PessoaDAO pesDAO = new PES_PessoaDAO { _Banco = dao._Banco };
                    pesDAO.Update_DecrementaIntegridade(entity.pes_id);
                }

                //Decrementa um na integridade da entidade
                SYS_EntidadeDAO entDAO = new SYS_EntidadeDAO { _Banco = dao._Banco };
                entDAO.Update_DecrementaIntegridade(entity.ent_id);

                //Deleta logicamente o usuário
                dao.Delete(entity);

                string sExcluiUsuarioAD = CFG_ConfiguracaoBO.SelecionaValorPorChave("AppExcluiUsuarioADSincronizacaoCoreSSO");
                bool excluiUsuario = false;

                if (entity.usu_integracaoAD == (byte)eIntegracaoAD.IntegradoADReplicacaoSenha && Boolean.TryParse(sExcluiUsuarioAD, out excluiUsuario) && excluiUsuario)
                    LOG_UsuarioADBO.Insert(entity, LOG_UsuarioAD.eAcao.ExcluirUsuario, dao._Banco);

                return true;
            }
            catch (Exception err)
            {
                if (banco == null)
                    dao._Banco.Close(err);
                throw;
            }
            finally
            {
                if (banco == null)
                    dao._Banco.Close();
            }
        }

        public static void _EnviarEmail(string nome_portal, string host, string emailSuporte, string nome, string login, string email, string senha, string entidade, string remetenteEmail, bool novoUsuario = true)
        {
            try
            {
                string corpoEmail = novoUsuario ?
                    String.Format(SYS_MensagemSistemaBO.RetornaValor(SYS_MensagemSistemaChave.CriacaoUsuarioCorpoEmailNovaSenha), entidade, login, senha) :
                    String.Format(SYS_MensagemSistemaBO.RetornaValor(SYS_MensagemSistemaChave.AlteracaoUsuarioCorpoEmailNovaSenha), entidade, login, senha);

                if (string.IsNullOrEmpty(corpoEmail))
                {
                    corpoEmail = "Olá <b>" + nome + "</b>!" +
                                 "<br /><br />Foi criado um novo usuário para você no " +
                                  nome_portal + "." +
                                 "<br /><br />Entidade: <b>" + entidade +
                                 "</b><br />Login: <b>" + login +
                                 "</b><BR />Senha: <b>" + senha + "</b>" +
                                 "<br /><br />Atenciosamente," +
                                 "<br /><b>" + nome_portal + "</b>";
                }

                string assuntoEmail = novoUsuario ?
                    SYS_MensagemSistemaBO.RetornaValor(SYS_MensagemSistemaChave.CriacaoUsuarioAssuntoEmailNovaSenha) :
                    SYS_MensagemSistemaBO.RetornaValor(SYS_MensagemSistemaChave.AlteracaoUsuarioAssuntoEmailNovaSenha);

                if (string.IsNullOrEmpty(assuntoEmail))
                {
                    assuntoEmail = nome_portal + " - Cadastro de novo usuário no sistema.";
                }

                string remetente = string.IsNullOrEmpty(remetenteEmail) ?
                    "\"" + nome_portal + "\"<" + emailSuporte + ">" :
                    "\"" + remetenteEmail + "\"<" + emailSuporte + ">";

                CoreLibrary.Web.Mail.Mail mail = new CoreLibrary.Web.Mail.Mail(host, true, System.Net.Mail.MailPriority.Normal)
                {
                    _From = remetente,
                    _Subject = assuntoEmail,
                    _To = email,
                    _Body = corpoEmail
                };
                mail.SendMail();
            }
            catch
            {
                throw new ArgumentException("Erro ao tentar enviar o e-mail.");
            }
        }

        /// <summary>
        /// Utilizado para salvar o usuário e alterar sua senha
        /// </summary>
        [Obsolete("Utilizar o método AlterarSenhaAtualizarUsuario, passando por parâmetro se a senha já está criptografada ou não", false)]
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static bool Save(SYS_Usuario entity, string usu_senha)
        {
            SYS_UsuarioDAO usuario = new SYS_UsuarioDAO();
            usuario._Banco.Open(IsolationLevel.ReadCommitted);

            try
            {
                //Salva dados na tabela SYS_Usuario
                if (entity.Validate())
                {
                    if (VerificaLoginExistente(entity))
                        throw new DuplicateNameException("Já existe um usuário cadastrado com este login.");

                    if (SYS_ParametroBO.Parametro_ValidarUnicidadeEmailUsuario() && !string.IsNullOrEmpty(entity.usu_email) &&
                        VerificaEmailExistente(entity))
                        throw new DuplicateNameException("Já existe um usuário cadastrado com este e-mail.");

                    usuario.Salvar(entity);
                }
                else
                {
                    throw new CoreLibrary.Validation.Exceptions.ValidationException(entity.PropertiesErrorList[0].Message);
                }

                usuario.Update_Senha(entity.usu_id, usu_senha, entity.usu_criptografia);

                return true;
            }
            catch (Exception err)
            {
                usuario._Banco.Close(err);
                throw;
            }
            finally
            {
                usuario._Banco.Close();
            }
        }

        /// <summary>
        /// Utilizado para alterar apenas a senha do usuário
        /// </summary>
        /// <param name="entity">
        /// Entidade SYS_Usuario
        /// </param>
        /// <returns>
        /// True = sucesso | False = fracasso
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        [Obsolete("Utilizar o método AlterarSenhaUsuario(), passando por parâmetro se a senha já está criptografada ou não", false)]
        public static bool AlterarSenhaUsuarioAlterarSenha(SYS_Usuario entity)
        {
            SYS_UsuarioDAO dal = new SYS_UsuarioDAO();
            return dal.Update_Senha(entity.usu_id, entity.usu_senha, entity.usu_criptografia);
        }

        /// <summary>
        /// Verifica se o login que está sendo cadastrado já existe na tabela SYS_Usuario
        /// </summary>
        /// <param name="entity">
        /// Entidade SYS_Usuario
        /// </param>
        /// <returns>
        /// true = o Login já existe na tabela SYS_Usuario / false = o Login não existe na tabela SYS_Usuario
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        protected static bool VerificaLoginExistente
        (
            SYS_Usuario entity
        )
        {
            SYS_UsuarioDAO dal = new SYS_UsuarioDAO();
            try
            {
                return dal.SelectBy__ent_id_usu_login_usu_email_pes_id(entity.ent_id, entity.usu_id, entity.usu_dominio, entity.usu_login, string.Empty, Guid.Empty, 0);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Utilizado para alterar apenas a senha do usuário no live
        /// </summary>
        /// <param name="entity">
        /// Entidade SYS_Usuario
        /// </param>
        /// <param name="live">
        /// </param>
        /// <param name="criptografarSenha">
        /// Flag para senha criptografada (True - criptografada | False - não criptografada)
        /// </param>
        private static void AlterarSenhaUsuarioLive(SYS_Usuario entity, ManageUserLive live, bool criptografarSenha)
        {
            // Verifica se existe integração externa
            if ((live.ExistsIntegracaoExterna()) && (live.IsContaEmail(entity.usu_email)))
            {
                // Verifica se existe conta de email
                UserLive entityUserLive = new UserLive();
                entityUserLive.email = entity.usu_email;
                if (live.VerificarContaEmailExistente(entityUserLive))
                {
                    // Verifica se senha alterada, se necessário altera a senha no live
                    if (!string.IsNullOrEmpty(entity.usu_senha))
                    {
                        string senha = (criptografarSenha ? UtilBO.CriptografarSenha(entity.usu_senha, eCriptografa.MD5) : entity.usu_senha);
                        if (!entityUserLive.senha.Equals(senha, StringComparison.OrdinalIgnoreCase))
                        {
                            entityUserLive.senha = senha;
                            entityUserLive.situacao = entity.usu_situacao;
                            live.AlterarContaEmailSenha(entityUserLive);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Atualiza o email de um usuário.
        /// </summary>
        /// <param name="entity">
        /// Entidade de usuário com os dados do usuário.
        /// </param>
        /// <param name="banco">
        /// </param>
        /// <returns>
        /// </returns>
        private static bool AlterarEmailUsuario(SYS_Usuario entity, TalkDBTransaction banco = null)
        {
            SYS_UsuarioDAO dao = banco == null ? new SYS_UsuarioDAO() : new SYS_UsuarioDAO { _Banco = banco };
            if (banco == null)
            {
                dao._Banco.Open(IsolationLevel.ReadCommitted);
            }

            try
            {
                if (!string.IsNullOrEmpty(entity.usu_email))
                {
                    if (SYS_ParametroBO.Parametro_ValidarUnicidadeEmailUsuario() && VerificaEmailExistente(entity))
                        throw new DuplicateNameException(SYS_MensagemSistemaBO.RetornaValor(SYS_MensagemSistemaChave.MeusDadosMensagemValidacaoEmailExistente));
                }

                return dao.AtualizaEmail(entity.usu_id, entity.usu_email);
            }
            catch (Exception ex)
            {
                if (banco == null)
                {
                    dao._Banco.Close(ex);
                }
                throw;
            }
            finally
            {
                if (dao._Banco.ConnectionIsOpen && banco == null)
                    dao._Banco.Close();
            }
        }

        /// <summary>
        /// Utilizado para salvar o usuario e os grupos selecionados
        /// </summary>
        /// <param name="entity">
        /// Entidade SYS_Usuario
        /// </param>
        /// <param name="criptografarSenha">
        /// Flag para senha criptografada(True - criptografada | False - não criptografada)
        /// </param>
        /// <param name="grupos">
        /// </param>
        /// <param name="entidades">
        /// </param>
        /// <param name="enviaemail">
        /// </param>
        /// <param name="nome">
        /// </param>
        /// <param name="nome_portal">
        /// </param>
        /// <param name="host">
        /// </param>
        /// <param name="emailsuporte">
        /// </param>
        /// <param name="banco">
        /// </param>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        private static bool Save(SYS_Usuario entity, bool criptografarSenha, SortedDictionary<Guid, TmpGrupos> grupos, SortedDictionary<Guid, List<TmpEntidadeUA>> entidades, bool enviaemail, string nome, string nome_portal, string host, string emailsuporte, CoreLibrary.Data.Common.TalkDBTransaction banco, bool salvarHistoricoSenha = false, string emailRemetente = null)
        {
            bool result;
            string novasenha = string.Empty;
            SYS_UsuarioDAO usuario = new SYS_UsuarioDAO();

            if (banco == null)
                usuario._Banco.Open(IsolationLevel.ReadCommitted);
            else
                usuario._Banco = banco;
            try
            {
                //Validação de domínio
                if (!ValidaString(entity.usu_dominio, false))
                    throw new ArgumentException("Domínio não pode conter caracteres especiais, espaço, acentos e cedilha.");

                //Validação de login
                if (!ValidaString(entity.usu_login, true))
                    throw new ArgumentException("Login não pode conter caracteres especiais, espaço, acentos ou cedilha.");

                //Se o e-mail tiver sido preenchido
                if (!string.IsNullOrEmpty(entity.usu_email))
                {
                    if (!ValidaString(entity.usu_email, true))
                        throw new ArgumentException("E-mail incorreto.");
                    //Gera uma senha automaticamente caso tenha sido escolhida essa opção
                    if (enviaemail)
                    {
                        string tamanhoSenha = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.TAMANHO_SENHA_USUARIO);
                        Regex intRegex = new Regex(@"\d+");
                        int tamanho = 6;

                        if (intRegex.IsMatch(tamanhoSenha) && SYS_ParametroBO.ParametroValorBooleano(SYS_ParametroBO.eChave.GERAR_SENHA_FORMATO_PARAMETRIZADO))
                        {
                            tamanho = Convert.ToInt32(intRegex.Matches(tamanhoSenha)[0].Value);
                        }

                        //Gera uma senha automaticamente para enviar por email
                        novasenha = UtilBO._CriaSenha(tamanho);
                        entity.usu_senha = novasenha;
                        entity.usu_situacao = 5;
                    }
                }
                else
                {
                    if (enviaemail)
                        throw new ArgumentException("Para gerar senha o e-mail é obrigatório.");
                }

                // Configura usuário live, caso exista integração externa e conta live
                ManageUserLive live = new ManageUserLive();
                if ((live.ExistsIntegracaoExterna()) && (live.IsContaEmail(entity.usu_email)))
                {
                    // Verifica se existe conta de email
                    UserLive entityUserLive = new UserLive();
                    entityUserLive.email = entity.usu_email;
                    if (live.VerificarContaEmailExistente(entityUserLive))
                    {
                        entity.usu_login = entityUserLive.login;
                        entity.usu_email = entityUserLive.email;
                        if ((string.IsNullOrEmpty(entity.usu_senha)) && (entity.IsNew))
                        {
                            entity.usu_senha = entityUserLive.senha;
                            entity.usu_criptografia = Convert.ToByte(eCriptografa.MD5);
                            criptografarSenha = false;
                        }
                        if ((!string.IsNullOrEmpty(entity.usu_senha)) && (criptografarSenha))
                        {
                            entity.usu_criptografia = Convert.ToByte(eCriptografa.MD5);
                        }
                    }
                }

                //Caso não seja usuário AD
                // NOVO: verifica se a propriedade usu_integracaoExterna é maior que zero. se for ignora a senha.
                if (entity.usu_integracaoAD != (byte)eIntegracaoAD.IntegradoAD && entity.usu_integracaoExterna < 1)
                {
                    //Verifica se a senha foi preenchida na inclusão
                    //Não foi utilizado o "MSNotNullOrEmpty" por que na alteração a senha estará vazia
                    if (entity.IsNew && string.IsNullOrEmpty(entity.usu_senha))
                        throw new ArgumentException("Senha é obrigatório.");

                    //A validação de senha só ocorre quando é o próprio usuário que vai alterá-la
                    //if (!string.IsNullOrEmpty(entity.usu_senha) && (entity.usu_senha.Length < 6 || entity.usu_senha.Length > 100))
                    //    throw new ArgumentException("Senha deve conter no mínimo 6 e no máximo 100 caracteres.");

                    //if (!string.IsNullOrEmpty(entity.usu_senha) && !enviaemail)
                    //{
                    //    Regex senha = new Regex("((([0-9]+[a-z]+)|([0-9]+[A-Z]+)|([0-9]+[!@#$%&amp;]+)).*)|((([a-z]+[0-9]+)|([a-z]+[A-Z]+)|([a-z]+[!@#$%&amp;]+)).*)|((([A-Z]+[0-9]+)|([A-Z]+[a-z]+)|([A-Z]+[!@#$%&amp;]+)).*)|((([!@#$%&amp;]+[0-9]+)|([!@#$%&amp;]+[a-z]+)|([!@#$%&amp;]+[A-Z]+)).*)");

                    //    if (!senha.IsMatch(entity.usu_senha))
                    //    {
                    //        throw new ArgumentException("A senha deve conter pelo menos uma combinação de letras e números ou letras maiúsculas e minúsculas ou algum caracter especial (!, @, #, $, %, &amp;) somados a letras e/ou números.");
                    //    }
                    //}
                }
                else
                {
                    entity.usu_senha = string.Empty;
                }

                if (entity.Validate())
                {
                    if (VerificaLoginExistente(entity))
                        throw new DuplicateNameException("Já existe um usuário cadastrado com este login.");

                    if (!string.IsNullOrEmpty(entity.usu_email))
                    {
                        if (SYS_ParametroBO.Parametro_ValidarUnicidadeEmailUsuario() && !string.IsNullOrEmpty(entity.usu_email) &&
                            VerificaEmailExistente(entity))
                            throw new DuplicateNameException("Já existe um usuário cadastrado com este e-mail.");
                    }
                    else if (SYS_ParametroBO.Parametro_ValidarObrigatoriedadeEmailUsuario())
                    {
                        throw new ArgumentException("E-mail é obrigatório.");
                    }

                    string senha = entity.usu_senha;

                    // Configura senha e criptografia, caso inclusão VERIFICA SE É
                    // usu_integracaoExterna SE FOR NÃO CRIPTOGRAFA SENHA
                    if (entity.IsNew && entity.usu_integracaoAD != (byte)eIntegracaoAD.IntegradoAD && entity.usu_integracaoExterna < 1)
                    {
                        if (criptografarSenha)
                        {
                            // Configura criptografia da senha
                            eCriptografa criptografia = eCriptografa.TripleDES;
                            entity.usu_senha = UtilBO.CriptografarSenha(entity.usu_senha, criptografia);
                            entity.usu_criptografia = Convert.ToByte(criptografia);
                        }
                        else if (!Enum.IsDefined(typeof(eCriptografa), Enum.Parse(typeof(eCriptografa), Convert.ToString(entity.usu_criptografia), true)))
                            throw new ArgumentException("Criptografia é obrigatório.");
                    }

                    result = usuario.Salvar(entity);

                    if (salvarHistoricoSenha && (!enviaemail || SYS_ParametroBO.ParametroValorBooleano(SYS_ParametroBO.eChave.PERMITIR_INTEGRACAO_SENHA_EXPIRADA_AD)) && !string.IsNullOrEmpty(entity.usu_senha))
                    {
                        LOG_UsuarioAD.eAcao acao = entity.IsNew ? LOG_UsuarioAD.eAcao.IncluirUsuario : LOG_UsuarioAD.eAcao.AlterarSenha;
                        result &= LOG_UsuarioADBO.Insert(entity, acao, usuario._Banco, senha);
                    }
                }
                else
                {
                    throw new CoreLibrary.Validation.Exceptions.ValidationException(entity.PropertiesErrorList[0].Message);
                }

                if (result && !(string.IsNullOrEmpty(entity.usu_email)) && entity.pes_id != Guid.Empty)
                {
                    string tipoemailpadrao = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.TIPO_MEIOCONTATO_EMAIL);
                    DataTable dt = PES_PessoaContatoBO.GetSelect(entity.pes_id, false, 1, 1);
                    bool contato = true;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (tipoemailpadrao == dt.Rows[i]["tmc_id"].ToString())
                        {
                            if (dt.Rows[i]["contato"].ToString().ToLower().Trim() == entity.usu_email.ToLower().Trim())
                            {
                                contato = false;
                                break;
                            }
                        }
                    }

                    if (contato && !string.IsNullOrEmpty(tipoemailpadrao))
                    {
                        PES_PessoaContato entityContato = new PES_PessoaContato
                        {
                            pes_id = entity.pes_id
                            ,
                            tmc_id = new Guid(tipoemailpadrao)
                            ,
                            psc_contato = entity.usu_email
                            ,
                            psc_situacao = 1
                            ,
                            IsNew = true
                        };

                        PES_PessoaContatoBO.Save(entityContato, usuario._Banco);
                    }
                }

                if (result)//&& grupos.Count > 0)
                {
                    XmlDocument xDoc = new XmlDocument();
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (XmlTextWriter xtw = new XmlTextWriter(ms, Encoding.UTF8))
                        {
                            xtw.Formatting = Formatting.Indented;
                            XmlSerializer ser = new XmlSerializer(typeof(List<TmpGrupos>));
                            ser.Serialize(xtw, grupos.Values.ToList());
                            ms.Seek(0, SeekOrigin.Begin);
                            xDoc.Load(ms);
                        }
                    }
                    SYS_UsuarioGrupoDAO usuarioGrupo = new SYS_UsuarioGrupoDAO { _Banco = usuario._Banco };
                    XmlNode node = xDoc.SelectSingleNode("/ArrayOfTmpGrupos");
                    try
                    {
                        result = usuarioGrupo._SalvarXml(node, entity.usu_id);
                    }
                    catch (System.Data.SqlClient.SqlException err)
                    {
                        //Erro diferente do de duplicidade de primary key (2627). Caso já tenha o mesmo grupo com o
                        //mesmo usuário a situação é alterada e a inserção desnecessária.
                        if (err.Number != 2627)
                            throw;
                    }
                    if (result)
                    {
                        DataTable dt = SYS_UsuarioGrupoUABO.GetSelect(entity.usu_id, Guid.Empty);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Guid ent_id = new Guid(dt.Rows[i]["ent_id"].ToString());
                            Guid uad_id = new Guid(dt.Rows[i]["uad_id"].ToString());
                            SYS_UnidadeAdministrativaDAO uadDAO = new SYS_UnidadeAdministrativaDAO { _Banco = usuario._Banco };
                            uadDAO.Update_DecrementaIntegridade(ent_id, uad_id);
                        }

                        using (MemoryStream ms = new MemoryStream())
                        {
                            using (XmlTextWriter xtw = new XmlTextWriter(ms, Encoding.UTF8))
                            {
                                xtw.Formatting = Formatting.Indented;
                                XmlSerializer ser = new XmlSerializer(typeof(List<TmpEntidadeUA>));
                                List<TmpEntidadeUA> lt = new List<TmpEntidadeUA>();
                                foreach (KeyValuePair<Guid, List<TmpEntidadeUA>> kv in entidades)
                                {
                                    foreach (TmpEntidadeUA aux in kv.Value.ToList())
                                        lt.Add(aux);
                                }
                                ser.Serialize(xtw, lt);
                                ms.Seek(0, SeekOrigin.Begin);
                                xDoc.Load(ms);
                            }
                        }
                        SYS_UsuarioGrupoUADAO usuarioGrupoUA = new SYS_UsuarioGrupoUADAO { _Banco = usuario._Banco };
                        node = xDoc.SelectSingleNode("/ArrayOfTmpEntidadeUA");
                        result = usuarioGrupoUA._SalvarXml(node, entity.usu_id);

                        dt = SYS_UsuarioGrupoUABO.GetSelect(entity.usu_id, Guid.Empty);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Guid ent_id = new Guid(dt.Rows[i]["ent_id"].ToString());
                            Guid uad_id = new Guid(dt.Rows[i]["uad_id"].ToString());
                            SYS_UnidadeAdministrativaDAO uadDAO = new SYS_UnidadeAdministrativaDAO { _Banco = usuario._Banco };
                            uadDAO.Update_IncrementaIntegridade(ent_id, uad_id);
                        }
                    }
                }

                if (entity.IsNew)
                {
                    if (entity.pes_id != Guid.Empty)
                    {
                        //Incrementa um na integridade da pessoa
                        PES_PessoaDAO pesDAL = new PES_PessoaDAO { _Banco = usuario._Banco };
                        pesDAL.Update_IncrementaIntegridade(entity.pes_id);
                    }

                    //Incrementa um na integridade da entidade
                    SYS_EntidadeDAO entDAO = new SYS_EntidadeDAO { _Banco = usuario._Banco };
                    entDAO.Update_IncrementaIntegridade(entity.ent_id);
                }
                else
                {
                    // Configura senha e criptografia, caso necessário e alteração
                    if (!string.IsNullOrEmpty(entity.usu_senha))
                    {
                        if (criptografarSenha)
                        {
                            // Configura criptografia da senha
                            eCriptografa criptografia = eCriptografa.TripleDES;
                            entity.usu_senha = UtilBO.CriptografarSenha(entity.usu_senha, criptografia);
                            entity.usu_criptografia = Convert.ToByte(criptografia);
                        }
                        else if (!Enum.IsDefined(typeof(eCriptografa), Enum.Parse(typeof(eCriptografa), Convert.ToString(entity.usu_criptografia), true)))
                            throw new ArgumentException("Criptografia é obrigatório.");

                        bool verificarHistoricoSenha = SYS_ParametroBO.ParametroValorBooleano(SYS_ParametroBO.eChave.SALVAR_HISTORICO_SENHA_USUARIO);

                        if (verificarHistoricoSenha)
                        {
                            List<SYS_UsuarioSenhaHistorico> listaHistoricoSenhas = new List<SYS_UsuarioSenhaHistorico>();

                            listaHistoricoSenhas = SYS_UsuarioSenhaHistoricoBO.SelecionaUltimasSenhas(entity.usu_id, usuario._Banco);

                            if (listaHistoricoSenhas.Any(p => p.ush_senha == entity.usu_senha && p.ush_criptografia == entity.usu_criptografia))
                            {
                                throw new ArgumentException(String.Format(SYS_MensagemSistemaBO.RetornaValor(SYS_MensagemSistemaChave.MeusDadosMensagemValidacaoHistoricoSenha),
                                                                          SYS_ParametroBO.ParametroValorInt32(SYS_ParametroBO.eChave.QUANTIDADE_ULTIMAS_SENHAS_VALIDACAO).ToString()));
                            }
                        }

                        // Atualiza senha
                        usuario.Update_Senha(entity.usu_id, entity.usu_senha, entity.usu_criptografia);

                        if (verificarHistoricoSenha)
                        {
                            result &= SYS_UsuarioSenhaHistoricoBO.Salvar(entity, usuario._Banco);
                        }
                    }
                }

                if (result && enviaemail)
                {
                    SYS_Entidade entidade = new SYS_Entidade { ent_id = entity.ent_id };
                    SYS_EntidadeBO.GetEntity(entidade);
                    _EnviarEmail(nome_portal, host, emailsuporte, nome, entity.usu_login, entity.usu_email, novasenha, entidade.ent_razaoSocial, emailRemetente, entity.IsNew);
                }
                // Atualiza senha do usuário no live, caso necessário
                AlterarSenhaUsuarioLive(entity, live, false);

                return result;
            }
            catch (Exception err)
            {
                if (banco == null)
                    usuario._Banco.Close(err);
                throw;
            }
            finally
            {
                if (banco == null)
                    usuario._Banco.Close();
            }
        }

        /// <summary>
        /// Utilizado para salvar o usuario e os grupos definidos automaticamente
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "ent_idUsuarioCadastro"), DataObjectMethod(DataObjectMethodType.Update, false)]
        private static bool Save(SYS_Usuario entity, bool criptografarSenha, Guid uad_id, string padrao, bool enviaemail, string nome, string nome_portal, string host, string emailsuporte, Guid ent_idUsuarioCadastro, CoreLibrary.Data.Common.TalkDBTransaction banco)
        {
            SYS_UsuarioDAO usuario = new SYS_UsuarioDAO();

            if (banco == null)
                usuario._Banco.Open(IsolationLevel.ReadCommitted);
            else
                usuario._Banco = banco;

            try
            {
                //Adiciona os grupos no Dictionary de acordo com o tipo de usuário
                //se for inclusão
                SortedDictionary<Guid, TmpGrupos> grupos = new SortedDictionary<Guid, TmpGrupos>();
                SortedDictionary<Guid, List<TmpEntidadeUA>> entidadeUA = new SortedDictionary<Guid, List<TmpEntidadeUA>>();

                if (entity.IsNew)
                {
                    DataTable dtGrupos = SYS_ParametroGrupoPerfilBO.GetSelect_gru_idBy_pgs_chave(padrao, false, 1, 1);

                    for (int i = 0; i < dtGrupos.Rows.Count; i++)
                    {
                        Guid gru_id = new Guid(dtGrupos.Rows[i]["gru_id"].ToString());

                        if (uad_id != Guid.Empty)
                        {
                            List<TmpEntidadeUA> lt = new List<TmpEntidadeUA>();
                            AddTmpEntidadeUA(new Guid(dtGrupos.Rows[i]["gru_id"].ToString()), entity.ent_id, uad_id, lt);
                            entidadeUA.Add(gru_id, lt);
                        }

                        AddTmpGrupo(gru_id, grupos, 1);
                    }
                }
                else
                {
                    //recupera os grupos já cadastrados se for alteração
                    GetGruposUsuario(entity.usu_id, grupos, entidadeUA);
                }

                //Chama método padrão para salvar o usuário
                Save(entity, criptografarSenha, grupos, entidadeUA, enviaemail, nome, nome_portal, host, emailsuporte, banco);

                return true;
            }
            catch (Exception err)
            {
                if (banco == null)
                    usuario._Banco.Close(err);
                throw;
            }
            finally
            {
                if (banco == null)
                    usuario._Banco.Close();
            }
        }

        private static void _EnviarEmailEsqueciSenha(string nome_portal, string host, string emailSuporte, string nome, string login, string email, string senha, string entidade, string remetenteEmail)
        {
            try
            {
                string corpoEmail = String.Format(SYS_MensagemSistemaBO.RetornaValor(SYS_MensagemSistemaChave.EsqueciSenhaCorpoEmailRecuperacaoSenha), entidade, login, senha);

                if (string.IsNullOrEmpty(corpoEmail))
                {
                    corpoEmail = "Olá <b>" + nome + "</b>!" +
                                "<br /><br />Conforme solicitado, foi criada uma nova senha para você no " +
                                nome_portal + "." +
                                "<br /><br />Entidade: <b>" + entidade +
                                "</b><br />Login: <b>" + login +
                                "</b><br />Senha: <b>" + senha + "</b>" +
                                "<br /><br />Atenciosamente," +
                                "<br /><b>" + nome_portal + "</b>";
                }

                string assuntoEmail = SYS_MensagemSistemaBO.RetornaValor(SYS_MensagemSistemaChave.EsqueciSenhaAssuntoEmailRecuperacaoSenha);

                if (string.IsNullOrEmpty(assuntoEmail))
                {
                    assuntoEmail = nome_portal + " - Recuperação de senha";
                }

                string remetente = string.IsNullOrEmpty(remetenteEmail) ?
                    "\"" + nome_portal + "\"<" + emailSuporte + ">" :
                    "\"" + remetenteEmail + "\"<" + emailSuporte + ">";

                CoreLibrary.Web.Mail.Mail mail = new CoreLibrary.Web.Mail.Mail(host, true, System.Net.Mail.MailPriority.Normal)
                {
                    _From = remetente,
                    _Subject = assuntoEmail,
                    _To = email,
                    _Body = corpoEmail
                };
                mail.SendMail();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Estrutura para apresentar dados no grid de grupos do usuário e armazenar id que serão
        /// usuado para inserir dados na tabela SYS_UsuarioGrupo.
        /// </summary>
        [Serializable]
        public struct TmpGrupos
        {
            [XmlAttribute]
            public Guid gru_id { get; set; }

            [XmlIgnore]
            public string sistema { get; set; }

            [XmlIgnore]
            public string grupo { get; set; }

            [XmlAttribute]
            public int usg_situacao { get; set; }
        }

        /// <summary>
        /// Estrututa para listar as entidade e unidade administrativas do usuário, e armazenar id
        /// que serão usuado para inserir dados na tabela SYS_UsarioGrupoUA
        /// </summary>
        [Serializable]
        public struct TmpEntidadeUA
        {
            [XmlAttribute]
            public Guid gru_id { get; set; }

            [XmlAttribute]
            public Guid ent_id { get; set; }

            [XmlAttribute]
            public Guid uad_id { get; set; }

            [XmlIgnore]
            public string EntidadeOrUA { get; set; }

            [XmlIgnore]
            public bool Entidade { get; set; }
        }
    }
}