using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using CoreLibrary.Data.Common;
using Autenticador.Entities;
using Autenticador.DAL.Abstracts;

namespace Autenticador.DAL
{
	
	/// <summary>
	/// 
	/// </summary>
	public class SYS_MensagemSistemaDAO : Abstract_SYS_MensagemSistemaDAO
	{
        #region Métodos

        /// <summary>
        /// Retorna todos as mensagens cadastrados.
        /// </summary>
        /// <returns></returns>
        public DataTable Seleciona()
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_MensagemSistema_Select", _Banco);

            qs.Execute();

            return qs.Return;
        }

        #endregion

        #region Sobrescritos

        /// <summary>
        /// Retorna todos as mensagens cadastradas.
        /// </summary>
        /// <returns></returns>
        public override IList<SYS_MensagemSistema> Select()
        {
            __STP_SELECT = "NEW_SYS_MensagemSistema_Select";
            return base.Select();
        }

        /// <summary>
        /// Configura os parâmetros de inclusão.
        /// </summary>
        /// <param name="qs">Stored procedure</param>
        /// <param name="entity">entidade com os dados a serem passados para a procedure</param>
        protected override void ParamInserir(QuerySelectStoredProcedure qs, SYS_MensagemSistema entity)
        {
            base.ParamInserir(qs, entity);

            qs.Parameters["@mss_dataCriacao"].Value = DateTime.Now;
            qs.Parameters["@mss_dataAlteracao"].Value = DateTime.Now;
        }

        /// <summary>
        /// Configura os parâmetros de alteração.
        /// </summary>
        /// <param name="qs">Stored procedure</param>
        /// <param name="entity">entidade com os dados a serem passados para a procedure</param>
        protected override void ParamAlterar(QueryStoredProcedure qs, SYS_MensagemSistema entity)
        {
            base.ParamAlterar(qs, entity);

            qs.Parameters.RemoveAt("@mss_dataCriacao");
            qs.Parameters["@mss_dataAlteracao"].Value = DateTime.Now;
        }

        /// <summary>
        /// Método de alteração.
        /// </summary>
        /// <param name="entity">Entidade a ser alterada</param>
        /// <returns></returns>
        protected override bool Alterar(SYS_MensagemSistema entity)
        {
            __STP_UPDATE = "NEW_SYS_MensagemSistema_Update";
            return base.Alterar(entity);
        }

        /// <summary>
        /// Configura os parâmetros de exclusão.
        /// </summary>
        /// <param name="qs">Stored procedure</param>
        /// <param name="entity">entidade com os dados a serem passados para a procedure</param>
        protected override void ParamDeletar(QueryStoredProcedure qs, SYS_MensagemSistema entity)
        {
            base.ParamDeletar(qs, entity);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@mss_situacao";
            Param.Size = 1;
            Param.Value = 3;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@mss_dataAlteracao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Método de alteração.
        /// </summary>
        /// <param name="entity">Entidade a ser excçuída</param>
        /// <returns></returns>
        public override bool Delete(SYS_MensagemSistema entity)
        {
            __STP_DELETE = "NEW_SYS_MensagemSistema_UpdateSituacao";
            return base.Delete(entity);
        }

        #endregion
	}
}