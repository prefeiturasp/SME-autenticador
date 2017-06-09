using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Transactions;
using System.ComponentModel;
using CoreLibrary.Business.Common;
using Autenticador.DAL;
using Autenticador.Entities;

namespace Autenticador.BLL
{
    public class SYS_DiaNaoUtilBO : BusinessBase<SYS_DiaNaoUtilDAO,SYS_DiaNaoUtil>
    {
        /// <summary>
        /// Retorna uma list contendo todos os Dias Não Úteis
        /// que não foram excluídos logicamente, filtrados por cidade.
        /// </summary>
        /// <param name="cid_id">Id da cidade</param>
        /// <returns>List com as entidades</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<SYS_DiaNaoUtil> SelecionaTodosPorCidade
        (
            Guid cid_id
        )
        {
            try
            {
                SYS_DiaNaoUtilDAO dal = new SYS_DiaNaoUtilDAO();
                return dal.SelecionaTodosPorCidade(cid_id);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna um datatable contendo todos os Dias Não Uteis
        /// que não foram excluídas logicamente, filtradas por 
        /// Nome, Abrangencia, Data, paginado.
        /// </summary>
        /// <param name="dnu_nome">Nome do Dia Não Util</param>
        /// <param name="dnu_abrangencia">Abrangência do Dia Não Util</param>
        /// <param name="dnu_data">Data do Dia Não Util</param>
        /// <param name="dnu_recorrencia"></param>
        /// <param name="paginado">Indica se vai exibir os registros paginados ou não.</param>
        /// <param name="currentPage">Página atual do gridview</param>
        /// <param name="pageSize">Total de registros por página</param>
        /// <returns>DataTable com as entidades</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelect
        (
            string dnu_nome
            , DateTime dnu_data
            , byte dnu_abrangencia
            , byte dnu_recorrencia
            , bool paginado
            , int currentPage
            , int pageSize
        )
        {
            if (pageSize == 0)
                pageSize = 1;

            totalRecords = 0;
            try
            {
                SYS_DiaNaoUtilDAO dal = new SYS_DiaNaoUtilDAO();                
                return dal.Select(dnu_nome, dnu_data, dnu_abrangencia, dnu_recorrencia, paginado, currentPage / pageSize, pageSize, out totalRecords);
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
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public static bool Save(Autenticador.Entities.SYS_DiaNaoUtil entity, bool _verificaVigenciaInicio)
        {
            try
            {
                if (entity.Validate())
                {
                    var data = "";
                    if (entity.dnu_recorrencia)
                        data = entity.dnu_data.Date.ToString().Split(' ')[0].Remove(entity.dnu_data.Date.ToString().Split(' ')[0].Length - 5);
                    else
                        data = entity.dnu_data.Date.ToString();

                    if (UtilBO.VerificaDataIgual(entity.dnu_data, DateTime.Now) && !entity.dnu_recorrencia)
                    {
                        throw new ArgumentException("Data do dia não útil não pode ser igual à data atual.");
                    }
                    else if (UtilBO.VerificaDataMaior(DateTime.Now, Convert.ToDateTime(data)) && !entity.dnu_recorrencia)
                    {
                        throw new ArgumentException("Data do dia não útil não pode ser anterior à data atual.");
                    }
                    else if (UtilBO.VerificaDataIgual(entity.dnu_vigenciaInicio, DateTime.Now) && _verificaVigenciaInicio)
                    {
                        throw new ArgumentException("Data de início de vigência não pode ser igual à data atual.");
                    }
                    else if (UtilBO.VerificaDataMaior(DateTime.Now, entity.dnu_vigenciaInicio) && _verificaVigenciaInicio)
                    {
                        throw new ArgumentException("Data de início de vigência não pode ser anterior à data atual.");
                    }
                    else if (UtilBO.VerificaDataMaior(DateTime.Now, entity.dnu_vigenciaFim) && entity.dnu_vigenciaFim != new DateTime())
                    {
                        throw new ArgumentException("Data de fim de vigência não pode ser anterior à data atual.");
                    }
                    else if (UtilBO.VerificaDataMaior(entity.dnu_vigenciaInicio, entity.dnu_vigenciaFim) && entity.dnu_vigenciaFim != new DateTime())
                    {
                        throw new ArgumentException("Data de fim de vigência não pode ser anterior à data de início de vigência.");
                    }
                    else
                    {
                        SYS_DiaNaoUtilDAO dal = new SYS_DiaNaoUtilDAO();
                        dal.Salvar(entity);
                    }
                }
                else
                    throw new CoreLibrary.Validation.Exceptions.ValidationException(entity.PropertiesErrorList[0].Message);
                return true;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Deleta logicamente um Dia não Útil
        /// </summary>
        /// <param name="entity">Entidade SYS_DiaNaoUtil</param>        
        /// <returns>True = deletado/alterado | False = não deletado/alterado</returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public new static bool Delete
        (
            Autenticador.Entities.SYS_DiaNaoUtil entity
        )
        {
            SYS_DiaNaoUtilDAO dao = new SYS_DiaNaoUtilDAO();
            dao._Banco.Open(System.Data.IsolationLevel.ReadCommitted);

            try
            {
                return dao.Delete(entity);
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
    }
}
