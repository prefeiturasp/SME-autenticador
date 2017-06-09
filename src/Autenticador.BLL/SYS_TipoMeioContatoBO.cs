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
    public class SYS_TipoMeioContatoBO : BusinessBase<SYS_TipoMeioContatoDAO, SYS_TipoMeioContato>
    {
        #region Métodos

        /// <summary>
        /// Retorna um datatable contendo todos os tipos de meio de contato
        /// que não foram excluídos logicamente. Filtrado por pes_id.
        /// </summary>
        /// <param name="pes_id">Id da pessoa.</param>
        /// <returns>DataTable com tipos de meio de contato.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable SelecionaContatosDaPessoa
        (
            Guid pes_id
        )
        {
            SYS_TipoMeioContatoDAO dao = new SYS_TipoMeioContatoDAO();
            try
            {
                return dao.SelecionaContatosDaPessoa(pes_id);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna um datatable contendo todas os tipos de meio de contato
        /// que não foram excluídas logicamente, filtradas por 
        ///	tmc_id, tmc_nome, tmc_situacao
        /// </summary>
        /// <param name="tmc_id">ID do tipo de meio de contato</param>
        /// <param name="tmc_nome">Nome do tipo de meio de contato</param>        
        /// <param name="tmc_situacao">Situacao do tipo de meio de contato</param>
        /// <param name="paginado">Indica se vai exibir os registros paginados ou não.</param>
        /// <param name="currentPage">Página atual do gridview</param>
        /// <param name="pageSize">Total de registros por página</param>
        /// <returns>DataTable com os tipos de meio de contato</returns> 
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelect
        (
            Guid tmc_id
            , string tmc_nome
            , byte tmc_situacao
            , bool paginado
            , int currentPage
            , int pageSize
        )
        {
            totalRecords = 0;

            if (pageSize == 0)
                pageSize = 1;

            SYS_TipoMeioContatoDAO dal = new SYS_TipoMeioContatoDAO();
            try
            {
                return dal.SelectBy_All(tmc_id, tmc_nome, tmc_situacao, paginado, currentPage / pageSize, pageSize, out totalRecords);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorno booleano na qual verifica se existe nome de Tipo de Meio de Contato 
        /// cadastrado no banco com situação Ativo ou Bloqueado.
        /// </summary>
        /// <param name="entity">Entidade do tipo de meio de contato</param>
        /// <returns>True - caso encontre algum registro no select/False - caso não encontre nada no select</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static bool VerificaNomeExistente(Autenticador.Entities.SYS_TipoMeioContato entity)
        {
            try
            {
                SYS_TipoMeioContatoDAO dal = new SYS_TipoMeioContatoDAO();
                return dal.SelectBy_Nome(entity.tmc_id, entity.tmc_nome);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Método para consultar se já existe um tipo de meio de contatao através do nome
        /// e carregar a entidade.
        /// </summary>
        /// <param name="entity">Entidade do tipo de meio de contato.</param>
        /// <returns>True - Existe o tipo de contato. | False - Não existe.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static bool ConsultaNomeExistente(Autenticador.Entities.SYS_TipoMeioContato entity)
        {
            try
            {
                SYS_TipoMeioContatoDAO dal = new SYS_TipoMeioContatoDAO();
                return dal.SelectBy_Nome(entity);
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
        /// <param name="entity">Entidade do tipo de meio de contato</param>
        /// <returns>True/False</returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public new static bool Save(Autenticador.Entities.SYS_TipoMeioContato entity)
        {
            SYS_TipoMeioContatoDAO dal = new SYS_TipoMeioContatoDAO();
            dal._Banco.Open(IsolationLevel.ReadCommitted);

            try
            {
                if (entity.Validate())
                {
                    if (VerificaNomeExistente(entity))
                    {
                        throw new DuplicateNameException("Já existe um tipo de meio contato cadastrado com este nome.");
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

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public new static bool Delete(Autenticador.Entities.SYS_TipoMeioContato entity)
        {
            SYS_TipoMeioContatoDAO dal = new SYS_TipoMeioContatoDAO();
            dal._Banco.Open(IsolationLevel.ReadCommitted);

            try
            {
                if (dal.Select_Integridade(entity.tmc_id) > 0)
                {
                    throw new Exception("Não é possível excluir o tipo de meio de contato pois possui outros registros ligados a ele.");
                }
                else
                {
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
