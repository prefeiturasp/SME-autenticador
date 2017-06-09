using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autenticador.DAL.Abstracts;
using System.Data;
using CoreLibrary.Data.Common;

namespace Autenticador.DAL
{
    public class SYS_VisaoDAO : Abstract_SYS_VisaoDAO
    {
        /// <summary>
        /// Parâmetros para efetuar a inclusão sem o ID da PK gerado automaticamente
        /// </summary>
        protected override void ParamAlterar(QueryStoredProcedure qs, Autenticador.Entities.SYS_Visao entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@vis_nome";
            Param.Size = 50;
            Param.Value = entity.vis_nome;
            qs.Parameters.Add(Param);
        }

        public DataTable GetSelectAll()
        {
            //DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("STP_SYS_Visao_SELECT", this._Banco);
            try
            {
                qs.Execute();
                return qs.Return;
                //if (qs.Return.Rows.Count > 0)
                //    dt = qs.Return;

                //return dt;
                
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

        public int GetSelect_vis_id(string vis_nome)
        {            
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Visao_SelectBy_vis_nome", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@vis_nome";
                Param.Size = 50;
                if (!string.IsNullOrEmpty(vis_nome))
                    Param.Value = vis_nome;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                    return Convert.ToInt32(qs.Return.Rows[0][0]);

                return -1;                
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
    }
}
