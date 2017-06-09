

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreLibrary.Business.Common;
using Autenticador.DAL;
using Autenticador.Entities;
using System.ComponentModel;

namespace Autenticador.BLL
{
    public class SYS_TipoUnidadeAdministrativaBO : BusinessBase<SYS_TipoUnidadeAdministrativaDAO, SYS_TipoUnidadeAdministrativa>
    {
        #region METODOS

        /// <summary>
        /// Retorna um datatable contendo todas s os tipos de unidade administrativa
        /// que não foram excluídas logicamente, filtradas por 
        ///	tua_id, tua_nome, tua_situacao
        /// </summary>
        /// <param name="tua_id">ID do tipo de unidade administrativa</param>
        /// <param name="tua_nome">Nome do tipo de unidade administrativa</param>        
        /// <param name="tua_situacao">Situacao do tipo de unidade administrativa</param>
        /// <param name="paginado">Indica se vai exibir os registros paginados ou não.</param>
        /// <param name="currentPage">Página atual do gridview</param>
        /// <param name="pageSize">Total de registros por página</param>
        /// <returns>DataTable com os tipos de meio de contato</returns> 
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelect
        (
            Guid tua_id
            , string tua_nome
            , byte tua_situacao
            , bool paginado
            , int currentPage
            , int pageSize
        )
        {
            totalRecords = 0;

            if (pageSize == 0)
                pageSize = 1;

            SYS_TipoUnidadeAdministrativaDAO dal = new SYS_TipoUnidadeAdministrativaDAO();
            try
            {
                return dal.SelectBy_All(tua_id, tua_nome, tua_situacao, paginado, currentPage / pageSize, pageSize, out totalRecords);
            }
            catch
            {
                throw;
            }
        }

        
        /// <summary>
        /// recriado para evitar erro TEMPORARIO
        /// Retorna um datatable contendo todas s os tipos de unidade administrativa
        /// que não foram excluídas logicamente, filtradas por 
        ///	tua_id, tua_nome, tua_situacao
        /// </summary>
        /// <param name="tua_id">ID do tipo de unidade administrativa</param>
        /// <param name="tua_nome">Nome do tipo de unidade administrativa</param>        
        /// <param name="tua_situacao">Situacao do tipo de unidade administrativa</param>
        /// <param name="paginado">Indica se vai exibir os registros paginados ou não.</param>
        /// <param name="currentPage">Página atual do gridview</param>
        /// <param name="pageSize">Total de registros por página</param>
        /// <returns>DataTable com os tipos de meio de contato</returns> 
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelect
        (
            Guid tua_id
            , string tua_nome
            , byte tua_situacao
            , bool paginado
        )
        {
            totalRecords = 0;            

            SYS_TipoUnidadeAdministrativaDAO dal = new SYS_TipoUnidadeAdministrativaDAO();
            try
            {
                return dal.SelectBy_All(tua_id, tua_nome, tua_situacao, paginado, 1 / 1, 1, out totalRecords);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Vaerifica se o nome está cadastrado retornando o respectivo tipo de unidade administrativa
        /// </summary>
        /// <param name="entity">Entidade SYS_TipoUnidadeAdministrativa(contendo tua_nome)</param>
        /// <returns></returns>
        public static bool ConsultarNomeExistente(SYS_TipoUnidadeAdministrativa entity)
        {
            SYS_TipoUnidadeAdministrativaDAO dao = new SYS_TipoUnidadeAdministrativaDAO();
            return dao.SelectBy_Nome(entity);
        }

        /// <summary>
        /// Retorno booleano na qual verifica se existe nome de Tipo de Unidade Administrativa 
        /// cadastrado no banco com situação Ativo ou Bloqueado.
        /// </summary>
        /// <param name="entity">Entidade do tipo de unidade administrativa</param>
        /// <returns>True - caso encontre algum registro no select/False - caso não encontre nada no select</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static bool VerificaNomeExistente(Autenticador.Entities.SYS_TipoUnidadeAdministrativa entity)
        {
            try
            {
                SYS_TipoUnidadeAdministrativaDAO dal = new SYS_TipoUnidadeAdministrativaDAO();
                return dal.SelectBy_Nome(entity.tua_id, entity.tua_nome);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Retorno booleano, após verificação caso não exista registro
        /// executa metodo Salvar para inserção do novo registro.
        /// </summary>
        /// <param name="entity">Entidade do tipo de unidade administrativa</param>
        /// <returns>True/False</returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public new static bool Save(Autenticador.Entities.SYS_TipoUnidadeAdministrativa entity)
        {
            SYS_TipoUnidadeAdministrativaDAO dal = new SYS_TipoUnidadeAdministrativaDAO();
            dal._Banco.Open(IsolationLevel.ReadCommitted);

            try
            {                
                if (entity.Validate())
                {
                    if (VerificaNomeExistente(entity))
                    {
                        throw new DuplicateNameException("Já existe um tipo de unidade administrativa cadastrado com este nome.");
                    }
                    else
                    {                        
                        dal.Salvar(entity);
                    }
                }
                else
                {
                    throw new CoreLibrary.Validation.Exceptions.ValidationException(entity.PropertiesErrorList[0].Message);
                }

                return true;
            }
            catch (Exception err)
            {
                dal._Banco.Close(err);
                throw;
            }
            finally
            {
                dal._Banco.Close();
            }
        }

        /// <summary>
        /// Deleta logicamente um Tipo de Unidade Administrativa
        /// </summary>
        /// <param name="entity">Entidade SYS_TipoEntidade</param>        
        /// <returns>True = deletado/alterado | False = não deletado/alterado</returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public new static bool Delete
        (
            Autenticador.Entities.SYS_TipoUnidadeAdministrativa entity
        )
        {
            SYS_TipoUnidadeAdministrativaDAO dal = new SYS_TipoUnidadeAdministrativaDAO();
            dal._Banco.Open(IsolationLevel.ReadCommitted);

            try
            {
                //Verifica se o tipo de entidade pode ser deletado
                if (dal.Select_Integridade(entity.tua_id) > 0)
                {
                    throw new Exception("Não é possível excluir o tipo de unidade administrativa pois possui outros registros ligados a ele.");
                }
                else
                {
                    //Deleta logicamente o tipo de entidade
                    dal.Delete(entity);                    
                }

                return true;
            }
            catch (Exception err)
            {
                dal._Banco.Close(err);
                throw;
            }
            finally
            {
                dal._Banco.Close();
            }
        }

        #endregion
    }
}
