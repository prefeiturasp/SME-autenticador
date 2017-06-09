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
    public abstract class Abstract_PES_Pessoa : Abstract_Entity
    {

		/// <summary>
		/// 
		/// </summary>
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual Guid pes_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[MSValidRange(200)]
		[MSNotNullOrEmpty()]
		public virtual string pes_nome { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[MSValidRange(50)]
		public virtual string pes_nome_abreviado { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public virtual Guid pai_idNacionalidade { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public virtual bool pes_naturalizado { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public virtual Guid cid_idNaturalidade { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public virtual DateTime pes_dataNascimento { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public virtual byte pes_estadoCivil { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public virtual byte pes_racaCor { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public virtual byte pes_sexo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public virtual Guid pes_idFiliacaoPai { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public virtual Guid pes_idFiliacaoMae { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public virtual Guid tes_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Obsolete("Utilizar o campo arq_idFoto, salvando na tabela CFG_Arquivo.", true)]
        public virtual byte[] pes_foto { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[MSNotNullOrEmpty()]
		public virtual byte pes_situacao { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[MSNotNullOrEmpty()]
		public virtual DateTime pes_dataCriacao { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[MSNotNullOrEmpty()]
		public virtual DateTime pes_dataAlteracao { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[MSNotNullOrEmpty()]
		public virtual int pes_integridade { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public virtual Int64 arq_idFoto { get; set; }

        /// <summary>
        /// Nome Social da pessoa.
        /// </summary>
        [MSValidRange(200)]
        public virtual string pes_nomeSocial { get; set; }

    }
}