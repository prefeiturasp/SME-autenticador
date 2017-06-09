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
    public class SYS_TipoEntidadeBO : BusinessBase<SYS_TipoEntidadeDAO, SYS_TipoEntidade>
    {
        #region METODOS

        /// <summary>
        /// Retorna um datatable contendo todas s os tipos de entidades
        /// que não foram excluídas logicamente, filtradas por 
        ///	ten_id, ten_nome, ten_situacao             
        /// </summary>
        /// <param name="ten_id">ID do tipo de entidade</param>
        /// <param name="ten_nome">Nome do tipo de entidade</param>        
        /// <param name="ten_situacao">Situacao do tipo da entidade</param>
        /// <param name="paginado">Indica se vai exibir os registros paginados ou não.</param>
        /// <param name="currentPage">Página atual do gridview</param>
        /// <param name="pageSize">Total de registros por página</param>
        /// <returns>DataTable com os tipos de entidades</returns>  
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelect
        (
            Guid ten_id
            , string ten_nome
            , byte ten_situacao
            , bool paginado
            , int currentPage
            , int pageSize          
        )
        {
            totalRecords = 0;

            if (pageSize == 0)
                pageSize = 1;

            SYS_TipoEntidadeDAO dal = new SYS_TipoEntidadeDAO();
            try
            {
                return dal.SelectBy_All(ten_id, ten_nome, ten_situacao, paginado, currentPage / pageSize, pageSize, out totalRecords);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Retorno booleano na qual verifica se existe nome de Tipo de Entidade 
        /// cadastrado no banco com situação Ativo ou Bloqueado.
        /// </summary>
        /// <param name="entity">Entidade do tipo de entidade</param>
        /// <returns>True - caso encontre algum registro no select/False - caso não encontre nada no select</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static bool VerificaNomeExistente(Autenticador.Entities.SYS_TipoEntidade entity)
        {
            SYS_TipoEntidadeDAO dal = new SYS_TipoEntidadeDAO();
            try
            {
                return dal.SelectBy_Nome(entity.ten_id, entity.ten_nome);
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
        /// <param name="entity">Entidade do tipo de entidade</param>
        /// <returns>True/False</returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public new static bool Save(Autenticador.Entities.SYS_TipoEntidade entity)
        {
            SYS_TipoEntidadeDAO dal = new SYS_TipoEntidadeDAO();
            dal._Banco.Open(IsolationLevel.ReadCommitted);

            try
            {
                if (entity.Validate())
                {
                    if (VerificaNomeExistente(entity))
                    {
                        throw new DuplicateNameException("Já existe um tipo de entidade cadastrado com este nome.");
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
        /// Deleta logicamente um Tipo de Entidade
        /// </summary>
        /// <param name="entity">Entidade SYS_TipoEntidade</param>        
        /// <returns>True = deletado/alterado | False = não deletado/alterado</returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public new static bool Delete
        (
            Autenticador.Entities.SYS_TipoEntidade entity
        )
        {
            SYS_TipoEntidadeDAO dal = new SYS_TipoEntidadeDAO();
            dal._Banco.Open(IsolationLevel.ReadCommitted);

            try
            {
                //Verifica se o tipo de entidade pode ser deletado
                if (dal.Select_Integridade(entity.ten_id) > 0)
                {
                    throw new Exception("Não é possível excluir o tipo de entidade pois possui outros registros ligados a ele.");
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
