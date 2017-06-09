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
    /// Responsável em guardar as configurações do(s) servidor(s) de relatórios do sistema agregado ao sistema separados por sistema e entidade. Assim podemos guardar mais de um configuração para as várias entidade que o sistema permite e evitando a necessidade de um novo ambiente para cada entidade e sistema.
    /// </summary>
    [Serializable()]
    public abstract class Abstract_CFG_ServidorRelatorio : Abstract_Entity
    {

        /// <summary>
        /// Id do sistema na tabele SYS_SistemaEntidade
        /// </summary>
        [MSNotNullOrEmpty()]
        [DataObjectField(true, false, false)]
        public virtual int sis_id { get; set; }

        /// <summary>
        /// Id da entidade na tabela SYS_SistemaEntidade
        /// </summary>
        [MSNotNullOrEmpty()]
        [DataObjectField(true, false, false)]
        public virtual Guid ent_id { get; set; }

        /// <summary>
        /// Id seqüêncial de acordo com o sis_id e ent_id. Responsável pelas multiplas configurações de servidores de relatórios. 
        /// </summary>
        [MSNotNullOrEmpty()]
        [DataObjectField(true, false, false)]
        public virtual int srr_id { get; set; }

        /// <summary>
        /// Nome do servidor de relatórios (Apelido)
        /// </summary>
        [MSValidRange(100)]
        [MSNotNullOrEmpty()]
        public virtual string srr_nome { get; set; }

        /// <summary>
        /// Descrição para ajudar na identificação do servidor de relatórios.
        /// </summary>
        [MSValidRange(1000)]
        public virtual string srr_descricao { get; set; }

        /// <summary>
        /// Informa se o relatório está em um servidor de relatórios 
        /// ou se é executado localmente pelo site e gerenciado pelo reportviewer.
        /// </summary>
        [MSNotNullOrEmpty()]
        public virtual bool srr_remoteServer { get; set; }

        /// <summary>
        /// Nome do usuário do servidor de relatórios (Normalmente um usuário do Windows).
        /// </summary>
        [MSValidRange(512)]
        public virtual string srr_usuario { get; set; }

        /// <summary>
        /// Senha do usuário do servidor de relatórios.
        /// </summary>
        [MSValidRange(512)]
        public virtual string srr_senha { get; set; }

        /// <summary>
        /// Nome do domínio do servidor de relatórios.
        /// </summary>
        [MSValidRange(512)]
        public virtual string srr_dominio { get; set; }

        /// <summary>
        /// Pasta onde estão os arquivos de relatório no Reporting Services.
        /// </summary>
        [MSValidRange(1000)]
        public virtual string srr_diretorioRelatorios { get; set; }

        /// <summary>
        /// Informa em qual pasta está os arquivos físicos (.rdl) dos relatórios.
        /// </summary>
        [MSValidRange(1000)]
        [MSNotNullOrEmpty()]
        public virtual string srr_pastaRelatorios { get; set; }

        /// <summary>
        /// 1-Ativo;2-Bloqueado;3-Excluído
        /// </summary>
        [MSNotNullOrEmpty()]
        public virtual byte srr_situacao { get; set; }

        /// <summary>
        /// Data da criação do registro no banco de dados.
        /// </summary>
        [MSNotNullOrEmpty()]
        public virtual DateTime srr_dataCriacao { get; set; }

        /// <summary>
        /// Data da alteração do registro no banco de dados.
        /// </summary>
        [MSNotNullOrEmpty()]
        public virtual DateTime srr_dataAlteracao { get; set; }

    }
}