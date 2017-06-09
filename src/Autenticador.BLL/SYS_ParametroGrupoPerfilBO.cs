using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreLibrary.Business.Common;
using Autenticador.DAL;
using Autenticador.Entities;
using System.Data;
using System.ComponentModel;
using CoreLibrary.Data.Common;

namespace Autenticador.BLL
{
    public class SYS_ParametroGrupoPerfilBO : BusinessBase<SYS_ParametroGrupoPerfilDAO, SYS_ParametroGrupoPerfil>
    {
        #region METODOS

        /// <summary>
        /// Retorna um datatable contendo todas s os tipos de atendimento da turma
        /// que não foram excluídas logicamente, filtradas por 
        ///	tat_id, tat_nome, tat_situacao             
        /// </summary>
        /// <param name="tat_id">ID do tipo de atendimento turma</param>
        /// <param name="tat_nome">Nome do tipo de atendimento turma</param>        
        /// <param name="tat_situacao">Situacao do tipo de atendimento turma</param>
        /// <param name="paginado">Indica se vai exibir os registros paginados ou não.</param>
        /// <param name="currentPage">Página atual do gridview</param>
        /// <param name="pageSize">Total de registros por página</param>
        /// <returns>DataTable com os tipos de atendimento turma</returns>  
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelect
        (
            Guid pgs_id
            , byte pgs_situacao
            , bool paginado
            , int currentPage
            , int pageSize
        )
        {
            totalRecords = 0;

            if (pageSize == 0)
                pageSize = 1;

            SYS_ParametroGrupoPerfilDAO dal = new SYS_ParametroGrupoPerfilDAO();
            try
            {
                return dal.SelectBy_All(pgs_id, pgs_situacao, paginado, currentPage / pageSize, pageSize, out totalRecords);
            }
            catch
            {
                throw;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelect2
        (
            Guid pgs_id
            , bool paginado
            , int currentPage
            , int pageSize
        )
        {
            totalRecords = 0;

            if (pageSize == 0)
                pageSize = 1;

            SYS_ParametroGrupoPerfilDAO dal = new SYS_ParametroGrupoPerfilDAO();
            try
            {
                return dal.Select(pgs_id, paginado, currentPage / pageSize, pageSize, out totalRecords);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna um Datatable com todos os grupos cadastrados para a chave informada
        /// que não foram excluidos logicamente.
        /// filtrados por pgs_chave           
        /// </summary>
        /// <param name="pgs_chave">Campo pgs_chave da tabela SYS_ParametroGrupoPerfil</param>
        /// <param name="paginado">Indica se vai exibir os registros paginados ou não.</param>
        /// <param name="currentPage">Página atual do gridview</param>
        /// <param name="pageSize">Total de registros por página</param>
        /// <param name="banco">Transação com banco</param>
        /// <returns>DataTable com os Grupos</returns>  
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelect_gru_idBy_pgs_chave
        (
            string pgs_chave
            , bool paginado
            , int currentPage
            , int pageSize
            , TalkDBTransaction banco
        )
        {
            totalRecords = 0;

            if (pageSize == 0)
                pageSize = 1;

            SYS_ParametroGrupoPerfilDAO dal = new SYS_ParametroGrupoPerfilDAO {_Banco = banco};
            return dal.Select_gru_id_By_pgs_chave(pgs_chave, paginado, currentPage / pageSize, pageSize, out totalRecords);
        }

        /// <summary>
        /// Retorna um Datatable com todos os grupos cadastrados para a chave informada
        /// que não foram excluidos logicamente.
        /// filtrados por pgs_chave           
        /// </summary>
        /// <param name="pgs_chave">Campo pgs_chave da tabela SYS_ParametroGrupoPerfil</param>
        /// <param name="paginado">Indica se vai exibir os registros paginados ou não.</param>
        /// <param name="currentPage">Página atual do gridview</param>
        /// <param name="pageSize">Total de registros por página</param>
        /// <returns>DataTable com os Grupos</returns>  
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelect_gru_idBy_pgs_chave
        (
            string pgs_chave            
            , bool paginado
            , int currentPage
            , int pageSize
        )
        {
            totalRecords = 0;

            if (pageSize == 0)
                pageSize = 1;

            SYS_ParametroGrupoPerfilDAO dal = new SYS_ParametroGrupoPerfilDAO();
            try
            {
                return dal.Select_gru_id_By_pgs_chave(pgs_chave, paginado, currentPage / pageSize, pageSize, out totalRecords);
            }
            catch
            {
                throw;
            }
        }

        public static bool VerificaNomeExistente(Autenticador.Entities.SYS_ParametroGrupoPerfil entity)
        {
            try
            {
                SYS_ParametroGrupoPerfilDAO dal = new SYS_ParametroGrupoPerfilDAO();
                return dal.SelectBy_Nome(entity.pgs_chave, entity.gru_id, entity.pgs_id);
            }
            catch
            {
                throw;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public new static bool Save(Autenticador.Entities.SYS_ParametroGrupoPerfil entity)
        {
            try
            {
                if (entity.Validate())
                    if (VerificaNomeExistente(entity))
                    {
                        throw new DuplicateNameException("Já existe um grupo padrão cadastrado com o mesmo nome e grupo.");
                    }
                    else
                    {
                        SYS_ParametroGrupoPerfilDAO dal = new SYS_ParametroGrupoPerfilDAO();
                        return dal.Salvar(entity);
                    }
                else
                    return false;
            }
            catch
            {
                throw;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static bool Deletar(Autenticador.Entities.SYS_ParametroGrupoPerfil entity)
        {
            try
            {
                return SYS_ParametroGrupoPerfilBO.Delete(entity);
            }

            catch
            {
                throw;
            }
        }

        #endregion
    }
}
