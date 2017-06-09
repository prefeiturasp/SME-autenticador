/*
	Classe gerada automaticamente pelo Code Creator
*/

using System;
using System.ComponentModel;
using CoreLibrary.Data.Common.Abstracts;
using CoreLibrary.Validation;

namespace Autenticador.Entities.Abstracts
{	
	/// <summary>
	/// 
	/// </summary>
	[Serializable()]
    public abstract class Abstract_SYS_UsuarioFalhaAutenticacao : Abstract_Entity
    {

		/// <summary>
		/// ID do usuário que tentou a autenticação.
		/// </summary>
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual Guid usu_id { get; set; }

		/// <summary>
		/// Quantidade de falhas de autenticação do usuário.
		/// </summary>
		[MSNotNullOrEmpty()]
		public virtual int ufl_qtdeFalhas { get; set; }

		/// <summary>
		/// Data e hora da última tentativa de login do usuário com falha.
		/// </summary>
		[MSNotNullOrEmpty()]
		public virtual DateTime ufl_dataUltimaTentativa { get; set; }

    }
}