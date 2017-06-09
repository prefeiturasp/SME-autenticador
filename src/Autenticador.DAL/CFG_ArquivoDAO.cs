/*
	Classe gerada automaticamente pelo Code Creator
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreLibrary.Data.Common;
using Autenticador.Entities;
using Autenticador.DAL.Abstracts;

namespace Autenticador.DAL
{
	
	/// <summary>
	/// 
	/// </summary>
	public class CFG_ArquivoDAO : Abstract_CFG_ArquivoDAO
	{
        protected override void ParamInserir(QuerySelectStoredProcedure qs, CFG_Arquivo entity)
        {
            entity.arq_dataCriacao = DateTime.Now;
            entity.arq_dataAlteracao = DateTime.Now;

            base.ParamInserir(qs, entity);

            qs.Parameters["@arq_data"].DbType = DbType.Binary;
            qs.Parameters["@arq_data"].Size = int.MaxValue;
        }

        protected override void ParamAlterar(QueryStoredProcedure qs, CFG_Arquivo entity)
        {
            entity.arq_dataAlteracao = DateTime.Now;

            base.ParamAlterar(qs, entity);

            qs.Parameters.RemoveAt("@arq_dataCriacao");

            qs.Parameters["@arq_data"].DbType = DbType.Binary;
            qs.Parameters["@arq_data"].Size = int.MaxValue;
        }

        protected override bool Alterar(CFG_Arquivo entity)
        {
            __STP_UPDATE = "NEW_CFG_Arquivo_UPDATE";
            return base.Alterar(entity);
        }
	}
}