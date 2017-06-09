using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using CoreLibrary.Data.Common;

namespace Autenticador.DAL
{
    public class CoreServicoDAO : Persistent
    {
        protected override string ConnectionStringName
        {
            get
            {
                return "AutenticadorDB";
            }
        }

        /// <summary>
        /// Retorna a expressão de configuração de acordo com o nome do trigger.
        /// </summary>
        /// <param name="trigger">Nome do trigger.</param>
        /// <returns>Expressão de configuração</returns>
        public string SelecionaExpressaoPorTrigger(string trigger)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("MS_QTZ_Cron_Triggers_SelecionaExpressaoPorTrigger", _Banco);

            #region Parâmetros

            DbParameter Param = qs.NewParameter();
            Param.DbType = DbType.String;
            Param.ParameterName = "@trigger";
            Param.Value = trigger;
            qs.Parameters.Add(Param);

            #endregion

            qs.Execute();

            return qs.Return.Rows.Count > 0 ?
                qs.Return.Rows[0]["CRON_EXPRESSION"].ToString() :
                string.Empty;
        }

    }
}
