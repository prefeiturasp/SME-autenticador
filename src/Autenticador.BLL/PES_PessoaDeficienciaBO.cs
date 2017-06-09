using System;
using System.Collections.Generic;
using System.Data;
using CoreLibrary.Business.Common;
using Autenticador.DAL;
using Autenticador.Entities;
using System.ComponentModel;
using CoreLibrary.Data.Common;
using CoreLibrary.Validation.Exceptions;

namespace Autenticador.BLL
{
    public class PES_PessoaDeficienciaBO : BusinessBase<PES_PessoaDeficienciaDAO, PES_PessoaDeficiencia>
    {
        /// <summary>
        /// Retorna um datatable contendo todas as deficiencias da pessoa
        /// que não foram excluídos logicamente, filtrados por 
        /// id da pessoa
        /// </summary>
        /// <param name="pes_id">Id da tabela PES_Pessoa do bd</param>
        /// <param name="paginado">Indica se o datatable será paginado ou não</param>
        /// <param name="currentPage">Página atual do gridview</param>
        /// <param name="pageSize">Total de registros por página</param> 
        /// <returns>DataTable com as deficiencias da pessoa</returns>
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
            PES_PessoaDeficienciaDAO dal = new PES_PessoaDeficienciaDAO();
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
        /// Retorna um list de entidade contendo todas as deficiencias da pessoa
        /// que não foram excluídos logicamente, filtrados pelo
        /// id da pessoa
        /// </summary>
        /// <param name="pes_id">Id da pessoa</param>
        /// <returns></returns>
        public static List<PES_PessoaDeficiencia> SelecionaPorPessoa(Guid pes_id)
        {
            PES_PessoaDeficienciaDAO dao = new PES_PessoaDeficienciaDAO();
            DataTable dt = GetSelect(pes_id, false, 0, 0);

            List<PES_PessoaDeficiencia> lt = new List<PES_PessoaDeficiencia>();
            foreach (DataRow dr in dt.Rows)
            {
                PES_PessoaDeficiencia entity = new PES_PessoaDeficiencia();
                lt.Add(dao.DataRowToEntity(dr, entity));
            }
            return lt;
        }

        /// <summary>
        /// Inclui uma nova deficiencia para a pessoa
        /// </summary>
        /// <param name="entity">Entidade PES_PessoaDeficiencia</param>        
        /// <param name="banco"></param>
        /// <returns>True = incluído/alterado | False = não incluído/alterado</returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public new static bool Save
        (
            PES_PessoaDeficiencia entity
            , TalkDBTransaction banco
        )
        {
            PES_PessoaDeficienciaDAO dao = new PES_PessoaDeficienciaDAO();
            dao._Banco = banco;

            try
            {
                if (entity.Validate())
                {
                    dao.Salvar(entity);
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
    }
}
