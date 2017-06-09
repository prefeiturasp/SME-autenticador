/*
	Classe gerada automaticamente pelo Code Creator
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autenticador.Entities.Abstracts;
using CoreLibrary.Validation;
using System.ComponentModel;

namespace Autenticador.Entities
{
	
	/// <summary>
	/// 
	/// </summary>
    [Serializable]
	public class SYS_SistemaEntidade : Abstract_SYS_SistemaEntidade
	{
        [MSValidRange(100, "Chave K1 pode conter at� 100 caracteres.")]
        public override string sen_chaveK1 { get; set; }
        [MSValidRange(200, "URL de acesso pode conter at� 200 caracteres.")]
        public override string sen_urlAcesso { get; set; }
        [MSValidRange(2000, "Logo do cliente pode conter at� 2000 caracteres.")]
        public override string sen_logoCliente { get; set; }
        [MSValidRange(1000, "Url do cliente pode conter at� 1000 caracteres.")]
        public override string sen_urlCliente { get; set; }
        [MSDefaultValue(1)]
        public override byte sen_situacao { get; set; }        
	}
}