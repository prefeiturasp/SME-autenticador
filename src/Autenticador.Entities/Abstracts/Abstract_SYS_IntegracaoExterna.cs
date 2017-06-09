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
    public abstract class Abstract_SYS_IntegracaoExterna : Abstract_Entity
    {
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual Guid ine_id { get; set; }
		[MSValidRange(200)]
		public virtual string ine_descricao { get; set; }
		[MSValidRange(200)]
		[MSNotNullOrEmpty()]
		public virtual string ine_urlInterna { get; set; }
		[MSValidRange(200)]
		[MSNotNullOrEmpty()]
		public virtual string ine_urlExterna { get; set; }
		[MSValidRange(100)]
		[MSNotNullOrEmpty()]
		public virtual string ine_dominio { get; set; }
		
		public virtual byte ine_tipo { get; set; }
		[MSValidRange(10)]
		[MSNotNullOrEmpty()]
		public virtual string ine_chave { get; set; }
		[MSValidRange(50)]
		public virtual string ine_tokenInterno { get; set; }
		[MSValidRange(50)]
		public virtual string ine_tokenExterno { get; set; }
		[MSNotNullOrEmpty()]
		public virtual bool ine_proxy { get; set; }
		[MSValidRange(15)]
		public virtual string ine_proxyIP { get; set; }
		[MSValidRange(10)]
		public virtual string ine_proxyPorta { get; set; }
		[MSNotNullOrEmpty()]
		public virtual bool ine_proxyAutenticacao { get; set; }
		[MSValidRange(100)]
		public virtual string ine_proxyAutenticacaoUsuario { get; set; }
		[MSValidRange(100)]
		public virtual string ine_proxyAutenticacaoSenha { get; set; }
		[MSNotNullOrEmpty()]
		public virtual byte ine_situacao { get; set; }

       
        public virtual short iet_id { get; set; }

    }
}