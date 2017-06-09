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
	/// 
	/// </summary>
	[Serializable()]
    public abstract class Abstract_SYS_Sistema : Abstract_Entity
    {
		[MSNotNullOrEmpty()]
		[DataObjectField(true, true, false)]
		public virtual int sis_id { get; set; }
		[MSValidRange(100)]
		[MSNotNullOrEmpty()]
		public virtual string sis_nome { get; set; }
		public virtual string sis_descricao { get; set; }
		[MSValidRange(2000)]
		public virtual string sis_caminho { get; set; }
		[MSValidRange(2000)]
		public virtual string sis_caminhoLogout { get; set; }
		[MSValidRange(2000)]
		public virtual string sis_urlImagem { get; set; }
		[MSValidRange(2000)]
		public virtual string sis_urlLogoCabecalho { get; set; }
		public virtual byte sis_tipoAutenticacao { get; set; }
		[MSValidRange(200)]
		public virtual string sis_urlIntegracao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual byte sis_situacao { get; set; }
        [MSNotNullOrEmpty]
        public virtual bool sis_ocultarLogo { get; set; }

    }
}