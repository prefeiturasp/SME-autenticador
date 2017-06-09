/*
	Classe gerada automaticamente pelo Code Creator
*/

namespace Autenticador.DAL
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Autenticador.DAL.Abstracts;
    using Autenticador.Entities;
    using CoreLibrary.Data.Common;
    using System.Data;
	
	/// <summary>
	/// Description: .
	/// </summary>
	public class SYS_ServicoDAO : Abstract_SYS_ServicoDAO
    {
        #region M�todos de consulta

        /// <summary>
        /// Seleciona os servi�os cadastrados.
        /// </summary>
        /// <returns></returns>
        public List<SYS_Servico> SelecionaServicos()
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Servico_SelecionaServicos", _Banco);

            try
            {
                qs.Execute();

                return qs.Return.Rows.Cast<DataRow>()
                                     .Select(p => DataRowToEntity(p, new SYS_Servico()))
                                     .ToList();
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Seleciona nome do job pelo ID do servi�o
        /// </summary>
        /// <param name="ser_id">ID do servi�o</param>
        /// <returns></returns>
        public string SelectNomeProcedimento(Int16 ser_id)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Servico_SelecionaNomeProcedimentoPorServico", _Banco);

            try
            {
                #region Par�metro

                Param = qs.NewParameter();
                Param.DbType = DbType.Int16;
                Param.ParameterName = "@ser_id";
                Param.Size = 2;
                Param.Value = ser_id;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                return qs.Return.Rows.Count > 0 ? 
                    qs.Return.Rows[0]["ser_nomeProcedimento"].ToString() : 
                    string.Empty;
            }
            catch
            {
                throw;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        #endregion

        #region M�todos sobrescritos

        protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, SYS_Servico entity)
        {
            entity.ser_id = Convert.ToInt16(qs.Return.Rows[0][0]);
            return (entity.ser_id > 0);
        }

        #endregion
    }
}