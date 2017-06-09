/*
	Classe gerada automaticamente pelo Code Creator
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using CoreLibrary.Data.Common.Abstracts;
using CoreLibrary.Validation;

namespace Autenticador.Entities.Abstracts
{	
	/// <summary>
	/// Responsável pela associação do arquivo do relatório com as configurações do servidor de relatório.
	/// </summary>
	[Serializable()]
    public abstract class Abstract_CFG_RelatorioServidorRelatorio : Abstract_Entity
    {

		/// <summary>
		/// Id do sistema cadastrado na tabela CFG_ServidorRelatorio
		/// </summary>
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual int sis_id { get; set; }

		/// <summary>
		/// Id da entidade cadastrado na tabela CFG_ServidorRelatorio
		/// </summary>
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual Guid ent_id { get; set; }

		/// <summary>
		/// Id da configuração do servidor de relatório cadastrado na tabela CFG_ServidorRelatorio
		/// </summary>
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual int srr_id { get; set; }

		/// <summary>
		/// Id do relatório cadastrado na tabela CFG_Relatorio
		/// </summary>
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual int rlt_id { get; set; }

    }
}