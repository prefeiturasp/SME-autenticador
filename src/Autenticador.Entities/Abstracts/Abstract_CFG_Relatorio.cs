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
    /// Tabela responsável pelo gerenciamento dos relatórios criados para os sistema ligados ao sistema. Essa tabela gerenciará os relatórios dando a ele um número fixo que será usando para renderezar os relatório do sistema. Também poderá bloquear um relatório caso necessário.
    /// </summary>
    [Serializable()]
    public abstract class Abstract_CFG_Relatorio : Abstract_Entity
    {

        /// <summary>
        /// id dos rdl (valor definido e inalteravel)
        /// </summary>
        [MSNotNullOrEmpty()]
        [DataObjectField(true, false, false)]
        public virtual int rlt_id { get; set; }

        /// <summary>
        /// Nome do arquivo rdl
        /// </summary>
        [MSValidRange(100)]
        [MSNotNullOrEmpty()]
        public virtual string rlt_nome { get; set; }

        /// <summary>
        /// 1-Ativo; 2-Bloqueado; 3-Excluido
        /// </summary>
        [MSNotNullOrEmpty()]
        public virtual byte rlt_situacao { get; set; }

        /// <summary>
        /// Data da criação do registro.
        /// </summary>
        [MSNotNullOrEmpty()]
        public virtual DateTime rlt_dataCriacao { get; set; }

        /// <summary>
        /// Data da alteração do registro.
        /// </summary>
        [MSNotNullOrEmpty()]
        public virtual DateTime rlt_dataAlteracao { get; set; }

        /// <summary>
        /// Número de entidade de banco de dados externos relacionados com o registro. Esse registro só poderá ser excluído caso o valor do campo seja igual à zero.
        /// </summary>
        [MSNotNullOrEmpty()]
        public virtual int rlt_integridade { get; set; }

    }
}