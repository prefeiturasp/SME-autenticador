using System;
using System.Data;
using CoreLibrary.Business.Common;
using Autenticador.DAL;
using Autenticador.Entities;
using System.ComponentModel;
using CoreLibrary.Data.Common;
using CoreLibrary.Validation.Exceptions;

namespace Autenticador.BLL
{
    public class PES_PessoaContatoBO : BusinessBase<PES_PessoaContatoDAO, PES_PessoaContato>    
    {
        /// <summary>
        /// Retorna um datatable contendo todos os contatos da pessoa
        /// que não foram excluídos logicamente, filtrados por 
        /// id da pessoa
        /// </summary>
        /// <param name="pes_id">Id da tabela PES_Pessoa do bd</param>
        /// <param name="paginado">Indica se o datatable será paginado ou não</param>
        /// <param name="currentPage">Página atual do gridview</param>
        /// <param name="pageSize">Total de registros por página</param> 
        /// <returns>DataTable com os contatos da pessoa</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelect
        (
            Guid pes_id
            , bool paginado
            , int currentPage
            , int pageSize
        )
        {
            if (pageSize == 0)
                pageSize = 1;

            totalRecords = 0;
            PES_PessoaContatoDAO dal = new PES_PessoaContatoDAO();
            try
            {
                return dal.SelectBy_pes_id(pes_id, paginado, currentPage / pageSize, pageSize, out totalRecords);
            }
            catch
            {
                throw;
            }
        }
        
        /// <summary>
        /// Retorna um datatable de contatos da pessoa de acordo com o tipo_meio_contato e o pes_id
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelectBy_pes_id_tmc_id
        (
            Guid pes_id
            , Guid tmc_id
        )
        {
            PES_PessoaContatoDAO dal = new PES_PessoaContatoDAO();
            try
            {
                return dal.SelectBy_pes_id_tmc_id(pes_id, tmc_id);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Inclui um novo contato para a pessoa
        /// </summary>
        /// <param name="entity">Entidade PES_PessoaContato</param>        
        /// <param name="banco"></param>
        /// <returns>True = incluído/alterado | False = não incluído/alterado</returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public new static bool Save
        (
            PES_PessoaContato entity
            , TalkDBTransaction banco
        )
        {
            PES_PessoaContatoDAO dal = new PES_PessoaContatoDAO { _Banco = banco };

            try
            {
                if (entity.Validate())
                {
                    dal.Salvar(entity);
                }
                else
                {
                    throw new ValidationException(entity.PropertiesErrorList[0].Message);
                }

                return true;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Salva os registros de contato da tabela passada, para a pessoa passada
        /// </summary>
        /// <param name="banco"></param>
        /// <param name="entity"></param>
        /// <param name="dtContato"></param>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void SaveContatosPessoa(TalkDBTransaction banco, PES_Pessoa entity, DataTable dtContato)
        {
            PES_PessoaContato entityContato = new PES_PessoaContato
            {
                pes_id = entity.pes_id
            };

            if (banco == null)
            {
                PES_PessoaContatoDAO dao = new PES_PessoaContatoDAO();
                banco = dao._Banco.CopyThisInstance();
                banco.Open(IsolationLevel.ReadCommitted);
            }

            for (int i = 0; i < dtContato.Rows.Count; i++)
            {
                if (dtContato.Rows[i].RowState != DataRowState.Deleted)
                {
                    if (dtContato.Rows[i].RowState == DataRowState.Added)
                    {
                        entityContato.tmc_id = new Guid(dtContato.Rows[i]["tmc_id"].ToString());
                        entityContato.psc_contato = dtContato.Rows[i]["contato"].ToString();
                        entityContato.psc_situacao = Convert.ToByte(1);
                        entityContato.psc_id = new Guid(dtContato.Rows[i]["id"].ToString());
                        entityContato.IsNew = true;
                        Save(entityContato, banco);

                        //Incrementa um na integridade do tipo de contato
                        SYS_TipoMeioContatoDAO tipoDAL = new SYS_TipoMeioContatoDAO { _Banco = banco };
                        tipoDAL.Update_IncrementaIntegridade(entityContato.tmc_id);
                    }
                    else if (dtContato.Rows[i].RowState == DataRowState.Modified)
                    {
                        entityContato.tmc_id = new Guid(dtContato.Rows[i]["tmc_id"].ToString());
                        entityContato.psc_contato = dtContato.Rows[i]["contato"].ToString();
                        entityContato.psc_situacao = Convert.ToByte(1);
                        entityContato.psc_id = new Guid(dtContato.Rows[i]["id"].ToString());
                        entityContato.IsNew = false;
                        Save(entityContato, banco);
                    }
                }
                else
                {
                    entityContato.psc_id = new Guid(dtContato.Rows[i]["id", DataRowVersion.Original].ToString());
                    entityContato.tmc_id = new Guid(dtContato.Rows[i]["tmc_id", DataRowVersion.Original].ToString());
                    PES_PessoaContatoDAO pesconDAL = new PES_PessoaContatoDAO { _Banco = banco };
                    pesconDAL.Delete(entityContato);

                    //Decrementa um na integridade do tipo de contato
                    SYS_TipoMeioContatoDAO tipoDAL = new SYS_TipoMeioContatoDAO { _Banco = banco };
                    tipoDAL.Update_DecrementaIntegridade(entityContato.tmc_id);
                }
            }
        }
    }
}
