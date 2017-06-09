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
    public class PES_TipoEscolaridadeBO : BusinessBase<PES_TipoEscolaridadeDAO, PES_TipoEscolaridade>
    {
        /// <summary>
        /// Retorna o id do tipo de escolaridade.               
        /// </summary>
        /// <param name="entity">Entidade TipoEscolaridade.</param>                
        /// <returns>Id do tipo de escolaridade.</returns> 
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Guid SelecionaPorNomeEscolaridade
        (
            Autenticador.Entities.PES_TipoEscolaridade entity
        )
        {
            PES_TipoEscolaridadeDAO dal = new PES_TipoEscolaridadeDAO();
            try
            {
                return dal.SelectBy_NomeEscolaridade(entity.tes_id, entity.tes_nome, 0);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna um datatable contendo todos os tipos de escolaridade
        /// que não foram excluídos logicamente, filtradas por 
        /// id do tipo de escolaridade, nome e situação
        /// </summary>
        /// <param name="tes_id">ID da tipo de escolaridade</param>
        /// <param name="tes_nome">Nome do tipo de entidade</param>        
        /// <param name="tes_situacao">Situacao do tipo de escolaridade</param>
        /// <param name="paginado">Indica se vai exibir os registros paginados ou não.</param>
        /// <param name="currentPage">Página atual do gridview</param>
        /// <param name="pageSize">Total de registros por página</param>
        /// <returns>DataTable com os tipos de escolaridade</returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static DataTable GetSelect
        (
            Guid tes_id            
            , string tes_nome            
            , byte tes_situacao
            , bool paginado
            , int currentPage
            , int pageSize
        )
        {
            if (pageSize == 0)
                pageSize = 1;

            totalRecords = 0;
            PES_TipoEscolaridadeDAO dal = new PES_TipoEscolaridadeDAO();
            try
            {
                return dal.SelectBy_All(tes_id, tes_nome, tes_situacao, paginado, currentPage / pageSize, pageSize, out totalRecords);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Verifica se o nome que está sendo cadastrado já existe na tabela PES_TipoEscolaridade
        /// </summary>
        /// <param name="entity">Entidade PES_TipoEscolaridade</param>        
        /// <returns>true = o nome já existe na tabela PES_TipoEscolaridade / false = o nome não existe na tabela PES_TipoEscolaridade</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        private static bool VerificaNomeExistente
        (
            Autenticador.Entities.PES_TipoEscolaridade entity
        )
        {
            PES_TipoEscolaridadeDAO dal = new PES_TipoEscolaridadeDAO();
            try
            {
                return dal.SelectBy_tes_nome(entity.tes_id, entity.tes_nome, 0);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Inclui ou Altera o Tipo de Escolaridade
        /// </summary>
        /// <param name="entity">Entidade do tipo de escolaridade</param>
        /// <returns>True/False</returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public new static bool Save(Autenticador.Entities.PES_TipoEscolaridade entity)
        {
            PES_TipoEscolaridadeDAO dal = new PES_TipoEscolaridadeDAO();
            dal._Banco.Open(IsolationLevel.ReadCommitted);
        
            try
            {
                if (entity.Validate())
                {
                    if (VerificaNomeExistente(entity))
                    {
                        throw new DuplicateNameException("Já existe um tipo de escolaridade cadastrado com este nome.");
                    }
                    else
                    {                                                
                        dal.Salvar(entity);

                        if (entity.IsNew)
                        {
                            entity.tes_ordem = dal.SelectMAX_tes_ordem();
                            entity.IsNew = false;
                            dal.Salvar(entity);
                        }
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
        /// Altera a ordem do tipo de escolaridade
        /// </summary>
        /// <param name="entitySubir">Entidade do tipo de escolaridade</param>
        /// <param name="entityDescer">Entidade do tipo de escolaridade</param>
        /// <returns>True/False</returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static bool SaveOrdem
        (
            Autenticador.Entities.PES_TipoEscolaridade entityDescer
            , Autenticador.Entities.PES_TipoEscolaridade entitySubir            
        )        
        {
            PES_TipoEscolaridadeDAO dal = new PES_TipoEscolaridadeDAO();
            dal._Banco.Open(IsolationLevel.ReadCommitted);

            try
            {
                if (entityDescer.Validate())
                {
                    dal.Salvar(entityDescer);
                }
                else
                {
                    throw new CoreLibrary.Validation.Exceptions.ValidationException(entityDescer.PropertiesErrorList[0].Message);
                }                
                
                if (entitySubir.Validate())
                {
                    dal.Salvar(entitySubir);                    
                }
                else
                {
                    throw new CoreLibrary.Validation.Exceptions.ValidationException(entitySubir.PropertiesErrorList[0].Message);
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
        /// Deleta logicamente um Tipo de Escolaridade
        /// </summary>
        /// <param name="entity">Entidade PES_TipoEscolaridade</param>        
        /// <returns>True = deletado/alterado | False = não deletado/alterado</returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public new static bool Delete
        (
            Autenticador.Entities.PES_TipoEscolaridade entity
        )
        {
            PES_TipoEscolaridadeDAO entDal = new PES_TipoEscolaridadeDAO();
            entDal._Banco.Open(IsolationLevel.ReadCommitted);

            try
            {
                //Verifica se o tipo de escolaridade pode ser deletado
                if (entDal.Select_Integridade(entity.tes_id) > 0)
                {
                    throw new Exception("Não é possível excluir o tipo de escolaridade pois possui outros registros ligados a ele.");
                }
                else
                {
                    //Deleta logicamente o tipo de escolaridade
                    entDal.Delete(entity);                    
                }

                return true;
            }
            catch (Exception err)
            {
                entDal._Banco.Close(err);
                throw;
            }
            finally
            {
                entDal._Banco.Close();
            }
        }
    }
}
