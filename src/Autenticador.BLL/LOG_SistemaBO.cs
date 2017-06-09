using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.ComponentModel;
using System.Reflection;
using CoreLibrary.Business.Common;
using Autenticador.DAL;
using Autenticador.Entities;
using CoreLibrary.Data.Common.Abstracts;
using System.Data;

namespace Autenticador.BLL
{
    #region ENUM

    /// <summary>
    /// Ação realizada pelo usuário
    /// </summary>
    public enum LOG_SistemaTipo
    {
        Insert
        , Update
        , Delete
        , Query
        , Login
    }

    #endregion

    public class LOG_SistemaBO : BusinessBase<LOG_SistemaDAO, LOG_Sistema>, IRequiresSessionState
    {
        #region DELEGATES

        public delegate Guid DelSalvarLog(LOG_SistemaTipo acao, string descricao);

        #endregion

        #region CONSTANTES

        public const string messageInsert = "Inserido um novo registro no sistema. Entidade: {0} - ID: {1}";
        public const string messageUpdate = "Dados do sistema foram modificados. Entidade: {0} - ID: {1}";
        public const string messageDelete = "Dados do sistema foram apagados. Entidade: {0} - ID: {1}";

        #endregion

        #region PROPRIEDADES

        /// <summary>
        /// Evento ligado ao delegate. Usado para salvar o log de sistema.
        /// </summary>
        public static DelSalvarLog SalvarLog { get; set; }

        #endregion

        #region METODOS
        
        /// <summary>
        /// Retorna uma lista de objetos LOG_Sistema filtrados por intervalo de data, sistema e tipo do log.
        /// </summary>
        /// <param name="dataInicio">Data fncial</param>
        /// <param name="dataTermino">Data final</param>
        /// <param name="sis_id">ID do sistema</param>
        /// <param name="log_acao">Tipo de log de sistema</param>
        /// <param name="currentPage">Página atual do grid</param>
        /// <param name="pageSize">Total de registros por página no grid</param>
        /// <returns>Lista de objetos LOG_Sistema</returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static DataTable GetSelect(DateTime dataInicio, DateTime dataTermino, int sistema, string acao, string login, int currentPage, int pageSize)
        {
             if (pageSize == 0)
                pageSize = 1;
            totalRecords = 0;
            LOG_SistemaDAO dal = new LOG_SistemaDAO();
            try
            {
                return dal.SelectBy_Busca(dataInicio, dataTermino, sistema, acao, login, currentPage / pageSize, pageSize, out totalRecords);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna uma lista de objetos LOG_Sistema filtrados pelo id do usuário
        /// </summary>
        /// <param name="usu_id">ID do Usuário</param>
        /// <param name="sis_id">ID do sistema</param>
        /// <returns>Lista de objetos LOG_Sistema</returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static DataTable GetSelect_byUsu_id(Guid usu_id, int sistema)
        {
            LOG_SistemaDAO dal = new LOG_SistemaDAO();
            try
            {
                return dal.SelectBy_usu_id(usu_id,sistema);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Configura e retorna a mensagem padrão do log de sistema para as ações de insert, update e delete.
        /// </summary>
        /// <typeparam name="T">Classe referente a entidade principal.</typeparam>
        /// <param name="acao">Tipo do log de sistema.</param>
        /// <param name="entity">Entidade principal do módulo.</param>
        /// <returns>Mensagem padronizada formatada conforme a ação(insert, update ou delete).</returns>
        public static string GetDescricaoDIU<T>(LOG_SistemaTipo acao, T entity)
        {
            Type t = typeof(T);
            string ids = String.Empty;
            PropertyInfo[] properties = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo p in properties)
            {
                DataObjectFieldAttribute[] attrs = (DataObjectFieldAttribute[])p.GetCustomAttributes(typeof(DataObjectFieldAttribute), true);
                foreach (DataObjectFieldAttribute a in attrs)
                {
                    if (a.PrimaryKey)
                    {
                        if (!String.IsNullOrEmpty(ids))
                            ids += String.Format("\"{0}\" : \"{1}\"", p.Name, p.GetValue(entity, null));
                        else
                            ids += String.Format("\"{0}\" : \"{1}\"", p.Name, p.GetValue(entity, null));
                    }
                }
            }
            ids = String.Concat("{", ids, "}");
            return GetDescricaoDIU(acao, t.Name, ids);
        }
        /// <summary>
        /// Configura e retorna a mensagem padrão do log de sistema para as ações de insert, update e delete.
        /// </summary>
        /// <param name="acao">Tipo do log de sistema.</param>
        /// <param name="entidade">Nome da entidade principal do módulo.</param>
        /// <param name="id">ids da entidade principal.</param>
        /// <returns>Mensagem padronizada formatada conforme a ação(insert, update ou delete).</returns>
        public static string GetDescricaoDIU(LOG_SistemaTipo acao, string entidade, string id)
        {
            switch (acao)
            {
                case LOG_SistemaTipo.Insert:
                    return String.Format(messageInsert, entidade, id);
                case LOG_SistemaTipo.Update:
                    return String.Format(messageUpdate, entidade, id);
                case LOG_SistemaTipo.Delete:
                    return String.Format(messageDelete, entidade, id);
                default:
                    throw new ArgumentOutOfRangeException("acao","Somente são permitidos os tipos de ação Insert, Update e Delete.");
            }
        }
        /// <summary>
        /// Gerar a chave para o log de sistema e armazena na session
        /// para que seja compartilhado com a auditoria.
        /// </summary>
        public static void GenerateLogID()
        {
            Guid id = Guid.NewGuid();
            HttpContext.Current.Session[LOG_Sistema.SessionName] = id;
        }

        #endregion
    }
}
