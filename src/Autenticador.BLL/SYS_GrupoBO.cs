using CoreLibrary.Business.Common;
using Autenticador.DAL;
using Autenticador.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace Autenticador.BLL
{
    public class SYS_GrupoBO : BusinessBase<SYS_GrupoDAO, SYS_Grupo>
    {
        #region Estruturas

        public struct Permissoes
        {
            [XmlAttribute]
            public bool grp_alterar { get; set; }

            [XmlAttribute]
            public bool grp_consultar { get; set; }

            [XmlAttribute]
            public bool grp_excluir { get; set; }

            [XmlAttribute]
            public bool grp_inserir { get; set; }

            [XmlAttribute]
            public Guid gru_id { get; set; }

            [XmlAttribute]
            public int mod_id { get; set; }

            [XmlAttribute]
            public int sis_id { get; set; }
        }

        #endregion Estruturas

        #region Propriedades

        [XmlElement]
        public Permissoes permissao { get; set; }

        #endregion Propriedades

        #region Métodos

        /// <summary>
        /// Retorna DataTable com os grupos de usuário ativos filtrados por sistema.
        /// </summary>
        /// <param name="sis_id">Id do sistema</param>
        /// <returns>DataTable com os grupos de usuário</returns>
        public static DataTable ConsultarPorSistema(int sis_id)
        {
            SYS_GrupoDAO dao = new SYS_GrupoDAO();
            return dao.SelectBy_Sistema(sis_id);
        }

        public new static bool Delete(SYS_Grupo entity)
        {
            SYS_GrupoDAO dal = new SYS_GrupoDAO();
            dal._Banco.Open(IsolationLevel.ReadCommitted);

            try
            {
                if (dal.Select_Integridade(entity.gru_id) > 0)
                {
                    throw new Exception("Não é possível excluir o grupo pois possui outros registros ligados a ela.");
                }

                dal.Delete(entity);
                return true;
            }
            catch
            {
                throw;
            }
            finally
            {
                dal._Banco.Close();
            }
        }

        /// <summary>
        /// Carrega as permissões no módulo para o grupo do usuário e url que ele está acessando.
        /// </summary>
        /// <param name="sis_id">ID do sistema ao qual o grupo pertence</param>
        /// <param name="gru_id">ID do grupo</param>
        /// <param name="url">
        /// Informar url nos formatos ~/myapp/pagina.aspx, ~/pagina.aspx, /myapp/pagina.asx ou /pagina.aspx.
        /// </param>
        /// <param name="entModulo">
        /// Entidade de Módulo carregada, quando a entidade GrupoPermissao estiver preenchida ela
        /// também estará.
        /// </param>
        /// <returns></returns>
        public static SYS_GrupoPermissao GetGrupoPermissao_Grupo_By_Url(int sis_id, Guid gru_id, string url, out SYS_Modulo entModulo)
        {
            entModulo = new SYS_Modulo();
            SYS_GrupoPermissao entGrupoPermissao = new SYS_GrupoPermissao();

            url = ResolveUrl(url);

            if (sis_id <= 0)
                throw new ArgumentException("Sistema não informado/encontrado.");

            DataTable dtGrupoPermissao = new SYS_GrupoPermissaoDAO().CarregarGrupos_Urls_PorSistema(gru_id, url, sis_id);

            if (dtGrupoPermissao.Rows.Count > 0)
            {
                // Busca os valores pra carregar as entidades.
                entGrupoPermissao = new SYS_GrupoPermissaoDAO().DataRowToEntity(dtGrupoPermissao.Rows[0], entGrupoPermissao);
                entModulo = new SYS_ModuloDAO().DataRowToEntity(dtGrupoPermissao.Rows[0], entModulo);
            }

            return entGrupoPermissao;
        }

        /// <summary>
        /// Carrega as permissões do sistema pelo grupo do usuário e url que ele está acessando.
        /// </summary>
        /// <param name="gru_id">ID do grupo</param>
        /// <param name="url">
        /// Informar url nos formatos ~/myapp/pagina.aspx, ~/pagina.aspx, /myapp/pagina.asx ou /pagina.aspx.
        /// </param>
        /// <returns></returns>
        public static SYS_GrupoPermissao GetGrupoPermissaoBy_url(Guid gru_id, string url)
        {
            SYS_GrupoPermissaoDAO dal = new SYS_GrupoPermissaoDAO();
            try
            {
                url = ResolveUrl(url);

                return dal.CarregarBy_url(gru_id, url);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Carrega as permissões do sistema pelo grupo do usuário e url que ele está acessando.
        /// </summary>
        /// <param name="gru_id">ID do grupo</param>
        /// <param name="url">Informar url no formato "~/myapp/pagina.aspx"</param>
        /// <returns></returns>
        public static SYS_GrupoPermissao GetGrupoPermissaoBy_UrlNaoAbsoluta(Guid gru_id, string url)
        {
            SYS_GrupoPermissaoDAO dal = new SYS_GrupoPermissaoDAO();
            return dal.CarregarBy_url(gru_id, url);
        }

        /// <summary>
        /// Retorna um data table com os grupos de usuário filtrados por sistema e paginado.
        /// </summary>
        /// <param name="sis_id">ID do sistema</param>
        /// <param name="paginado"></param>
        /// <param name="currentPage">Página atual do gridview</param>
        /// <param name="pageSize">Total de registros por página</param>
        /// <returns>DataTable com grupos</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelect(int sis_id, bool paginado, int currentPage, int pageSize)
        {
            if (pageSize == 0)
                pageSize = 1;
            totalRecords = 0;
            SYS_GrupoDAO dal = new SYS_GrupoDAO();
            try
            {
                return dal.SelectBy_sis_id(sis_id, paginado, currentPage / pageSize, pageSize, out totalRecords);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna os grupos do usuário não paginados.
        /// </summary>
        /// <param name="usu_id">ID do usuário</param>
        /// <returns>Lista de grupos do usuário</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelect(Guid usu_id)
        {
            SYS_GrupoDAO dal = new SYS_GrupoDAO();
            try
            {
                return dal.SelectBy_usu_id(usu_id);
            }
            catch
            {
                throw;
            }
        }

        public static DataTable GetSelect_sis_id_vis_id
        (
            int sis_id
            , int vis_id
            , bool paginado
            , int currentPage
            , int pageSize
        )
        {
            if (pageSize == 0)
                pageSize = 1;
            totalRecords = 0;
            SYS_GrupoDAO dal = new SYS_GrupoDAO();
            try
            {
                return dal.SelectBy_sis_id_vis_id(sis_id, vis_id, paginado, currentPage / pageSize, pageSize, out totalRecords);
            }
            catch
            {
                throw;
            }
        }







        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelectBy_MeusDados
        (
            Guid usu_id
            , bool paginado
            , int currentPage
            , int pageSize
        )
        {
            if (pageSize == 0)
                pageSize = 1;
            totalRecords = 0;
            SYS_GrupoDAO dal = new SYS_GrupoDAO();
            try
            {
                return dal.SelectBy_MeusDados(usu_id, paginado, currentPage / pageSize, pageSize, out totalRecords);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Selecionar os grupos do usuário em um determinado sistema.
        /// </summary>
        /// <param name="usu_id">ID do usuário logado</param>
        /// <param name="sis_id">ID do sistema</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static IList<SYS_Grupo> GetSelectBySis_idAndUsu_id(Guid usu_id, int sis_id)
        {
            SYS_GrupoDAO dal = new SYS_GrupoDAO();
            try
            {
                return dal.SelectBy_Sis_id_And_Usu_id(usu_id, sis_id);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna uma lista de objetos SYS_Modulos filtrado por grupo
        /// </summary>
        /// <param name="gru_id">ID do grupo</param>
        /// <returns>Lista de objetos SYS_Modulo</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static IEnumerable<SYS_Modulo> GetSelectModulosPai(Guid gru_id)
        {
            SYS_ModuloDAO dal = new SYS_ModuloDAO();
            try
            {
                IList<SYS_Modulo> lt = dal.SelectBy_gru_id(gru_id);
                //Retorna apenas os nós pai
                var ltModPai = from modulo in lt
                               where modulo.mod_idPai <= 0 &&
                                    (from m in lt
                                     where modulo.mod_idPai >= 0
                                     select m.mod_idPai).Contains(modulo.mod_id)
                               select modulo;
                return ltModPai;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna um datatable com dados das permissões do grupo.
        /// </summary>
        /// <param name="mod_id">ID do módulo</param>
        /// <param name="gru_id"></param>
        /// <returns>DataTable</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelectPermissoes(int mod_id, Guid gru_id)
        {
            SYS_GrupoPermissaoDAO dal = new SYS_GrupoPermissaoDAO();
            try
            {
                return dal.SelectBy_mod_id(mod_id, gru_id);
            }
            catch
            {
                throw;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<SYS_GrupoPermissao> GetSelectPermissoesBy_Estado(List<SYS_GrupoPermissao> lt)
        {
            try
            {
                var permissoes =
                   from n in lt
                   where n.grp_alterar ||
                   n.grp_consultar ||
                   n.grp_excluir ||
                   n.grp_inserir
                   select n;

                return permissoes.ToList();
            }
            catch
            {
                throw;
            }
        }

        public static IEnumerable<int> GetSelectPermissoesBy_ModuloFilho(int mod_idPai)
        {
            try
            {
                var mod_id =
                   from n in SYS_ModuloBO.GetSelect()
                   where n.mod_idPai == mod_idPai
                   select n.mod_idPai;

                return mod_id;
            }
            catch
            {
                throw;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static IEnumerable<int> GetSelectPermissoesBy_ModuloPai(int mod_id, int sis_id)
        {
            try
            {
                var mod_paiID =
                   from n in SYS_ModuloBO.GetSelect()
                   where n.mod_id == mod_id && n.sis_id == sis_id
                   select n.mod_idPai;

                return mod_paiID;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Salva as permissões do grupo
        /// </summary>
        /// <param name="lt"></param>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static bool SalvarPermissoes(List<SYS_GrupoPermissao> lt)
        {
            List<Permissoes> xml = new List<Permissoes>();
            try
            {
                foreach (SYS_GrupoPermissao grp in lt)
                {
                    Permissoes per = new Permissoes
                    {
                        gru_id = grp.gru_id
                        ,
                        sis_id = grp.sis_id
                        ,
                        mod_id = grp.mod_id
                        ,
                        grp_inserir = grp.grp_inserir
                        ,
                        grp_alterar = grp.grp_alterar
                        ,
                        grp_excluir = grp.grp_excluir
                        ,
                        grp_consultar = grp.grp_consultar
                    };
                    xml.Add(per);
                }
                XmlDocument xDoc = new XmlDocument();
                using (MemoryStream ms = new MemoryStream())
                {
                    using (XmlTextWriter xtw = new XmlTextWriter(ms, Encoding.UTF8))
                    {
                        xtw.Formatting = Formatting.Indented;
                        XmlSerializer ser = new XmlSerializer(typeof(List<Permissoes>));
                        ser.Serialize(xtw, xml);
                        ms.Seek(0, SeekOrigin.Begin);
                        xDoc.Load(ms);
                    }
                }
                SYS_GrupoPermissaoDAO dal = new SYS_GrupoPermissaoDAO();
                XmlNode node = xDoc.SelectSingleNode("/ArrayOfPermissoes");
                return dal._AlterarByXML(node);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Inclui ou Altera o Grupo
        /// </summary>
        /// <param name="entityGrupo">Entidade SYS_Grupo</param>
        /// <returns>True = incluído/alterado | False = não incluído/alterado</returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public new static bool Save
        (
            SYS_Grupo entityGrupo
        )
        {
            try
            {
                //Chama método save padrão.
                Save(entityGrupo, Guid.Empty, Guid.Empty, null);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Inclui ou Altera o Grupo
        /// </summary>
        /// <param name="entity">Entidade SYS_Grupo</param>
        /// <param name="gru_idPermissao">Grupo para copiar permissões</param>
        /// <param name="gru_idUsuario">Grupo para copiar usuários</param>
        /// <param name="banco"></param>
        /// <returns>True = incluído/alterado | False = não incluído/alterado</returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static bool Save
        (
            SYS_Grupo entity
            , Guid gru_idPermissao
            , Guid gru_idUsuario
            , CoreLibrary.Data.Common.TalkDBTransaction banco
        )
        {
            SYS_GrupoDAO dao = new SYS_GrupoDAO();
            try
            {
                if (banco == null)
                    dao._Banco.Open(IsolationLevel.ReadCommitted);
                else
                    dao._Banco = banco;

                //Salva dados na tabela SYS_Grupo
                if (entity.Validate())
                {
                    if (VerificaGrupoExistente(entity))
                        throw new DuplicateNameException("Já existe um grupo cadastrado com este nome.");

                    dao.Salvar(entity);
                }
                else
                {
                    throw new CoreLibrary.Validation.Exceptions.ValidationException(entity.PropertiesErrorList[0].Message);
                }

                if (entity.IsNew && gru_idPermissao != Guid.Empty)
                {
                    SYS_GrupoPermissaoDAO grp = new SYS_GrupoPermissaoDAO { _Banco = dao._Banco };
                    grp.CopiaPermissao(entity.gru_id, entity.sis_id, gru_idPermissao);
                }

                if (entity.IsNew && gru_idUsuario != Guid.Empty)
                {
                    SYS_GrupoPermissaoDAO grp = new SYS_GrupoPermissaoDAO { _Banco = dao._Banco };
                    grp.CopiaUsuario(entity.gru_id, gru_idUsuario);
                }

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

        /// <summary>
        /// Ajusta a Url informada para realizar a busca por módulos, colocando "~/" quando
        /// necessário no começo da url.
        /// </summary>
        /// <param name="url">Url Informada</param>
        /// <returns></returns>
        private static string ResolveUrl(string url)
        {
            if (!url.StartsWith("~"))
            {
                if (!url.StartsWith("/"))
                    url = url.Insert(0, "/");
                if (!VirtualPathUtility.IsAppRelative(url))
                {
                    if (VirtualPathUtility.IsAbsolute(url))
                        url = VirtualPathUtility.ToAppRelative(url);
                    else
                        throw new ArgumentOutOfRangeException("url", "url inválida!");
                }
            }
            else
            {
                if (!VirtualPathUtility.IsAppRelative(url.Substring(1)))
                {
                    if (VirtualPathUtility.IsAbsolute(url.Substring(1)))
                        url = VirtualPathUtility.ToAppRelative(url.Substring(1));
                    else
                        throw new ArgumentOutOfRangeException("url", "url inválida!");
                }
            }
            return url;
        }

        /// <summary>
        /// Verifica se o grupo que está sendo cadastrado já existe na tabela SYS_Grupo (para o
        /// sistema selecionado)
        /// </summary>
        /// <param name="entity">Entidade SYS_Grupo</param>
        /// <returns>
        /// true = o Grupo já existe na tabela SYS_Grupo / false = o Grupo não existe na tabela SYS_Grupo
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        private static bool VerificaGrupoExistente
        (
            SYS_Grupo entity
        )
        {
            SYS_GrupoDAO dal = new SYS_GrupoDAO();
            try
            {
                return dal.SelectBy_gru_nome(entity.gru_id, entity.sis_id, entity.gru_nome, 0);
            }
            catch
            {
                throw;
            }
        }



        #endregion Métodos
    }
}