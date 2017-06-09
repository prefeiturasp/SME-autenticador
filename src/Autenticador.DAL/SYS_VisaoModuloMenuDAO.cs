using System;
using Autenticador.DAL.Abstracts;
using CoreLibrary.Data.Common;
using System.Data;
using Autenticador.Entities;

namespace Autenticador.DAL
{
    public class SYS_VisaoModuloMenuDAO : Abstract_SYS_VisaoModuloMenuDAO
    {       
        /// <summary>
        /// Retorna a próxima ordem para inserção
        /// </summary>
        /// <param name="sis_id">Id do sistema</param>        
        /// <param name="mod_idPai">Id do sistema</param>
        /// <param name="vis_id">Id do módulo</param>        
        /// <returns>Int com a ordem para o novo registro</returns>
        public int Gerar_vmm_ordem
        (
            int sis_id
            , int mod_idPai
            , int vis_id
        )
        {
            QuerySelectStoredProcedure qu = new QuerySelectStoredProcedure("NEW_SYS_VisaoModuloMenu_SelectBy_GerarOrdem", _Banco);

            try
            {
                #region PARAMETROS

                Param = qu.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@sis_id";
                Param.Size = 8;
                Param.Value = sis_id;
                qu.Parameters.Add(Param);

                Param = qu.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@mod_idPai";
                Param.Size = 8;
                if (mod_idPai > 0)
                    Param.Value = mod_idPai;
                else
                    Param.Value = DBNull.Value;
                qu.Parameters.Add(Param);

                Param = qu.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@vis_id";
                Param.Size = 8;
                Param.Value = vis_id;
                qu.Parameters.Add(Param);

                #endregion

                qu.Execute();

                return Convert.ToInt32(qu.Return.Rows[0][0]);
            }
            catch
            {
                throw;
            }
            finally
            {
                qu.Parameters.Clear();
            }
        }

        /// <summary>
        /// Retorna o sitemap setado como "PRINCIPAL", ou seja, inserido na SYS_VisaoModuloMenu
        /// </summary>
        /// <param name="sis_id">Id do sistema</param>
        /// <param name="mod_id">Id do módulo</param>
        public int GetSelect_SiteMapMenu
        (
            int sis_id
            , int mod_id
        )
        {
            QuerySelectStoredProcedure qu = new QuerySelectStoredProcedure("NEW_SYS_VisaoModuloMenu_SelectBy_SiteMapMenu", _Banco);

            try
            {
                #region PARAMETROS

                Param = qu.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@sis_id";
                Param.Size = 8;
                if (sis_id > 0)
                    Param.Value = sis_id;
                else
                    Param.Value = DBNull.Value;
                qu.Parameters.Add(Param);

                Param = qu.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@mod_id";
                Param.Size = 8;
                if (mod_id > 0)
                    Param.Value = mod_id;
                else
                    Param.Value = DBNull.Value;
                qu.Parameters.Add(Param);

                #endregion

                qu.Execute();

                if (qu.Return.Rows.Count == 1)
                    return Convert.ToInt32(qu.Return.Rows[0][0]);
                else
                    return 0;
            }
            catch
            {
                throw;
            }
            finally
            {
                qu.Parameters.Clear();
            }
        }

        /// <summary>
        /// Recebe o valor do auto incremento e coloca na propriedade 
        /// </summary>        
        protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, SYS_VisaoModuloMenu entity)
        {
            return true;
        }	
    }
}
