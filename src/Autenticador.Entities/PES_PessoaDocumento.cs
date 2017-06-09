using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autenticador.Entities.Abstracts;
using CoreLibrary.Validation;
using System.ComponentModel;

namespace Autenticador.Entities
{
    [Serializable]
    public class PES_PessoaDocumento : Abstract_PES_PessoaDocumento
    {
        [MSNotNullOrEmpty("Tipo de documento é obrigatório.")]
        [DataObjectField(true, false, false)]
        public override Guid tdo_id { get; set; }
        [MSValidRange(50, "Número pode conter até 50 caracteres.")]
        public override string psd_numero { get; set; }
        [MSValidRange(200, "Orgão emissor pode conter até 200 caracteres.")]
        public override string psd_orgaoEmissao { get; set; }
        public override Guid unf_idEmissao { get; set; }
        [MSValidRange(1000, "Informações complementares pode conter até 1000 caracteres.")]
        public override string psd_infoComplementares { get; set; }
        [MSDefaultValue(1)]
        public override byte psd_situacao { get; set; }
        public override DateTime psd_dataCriacao { get; set; }
        public override DateTime psd_dataAlteracao { get; set; }

        //NOVOS CAMPOS
        /// <summary>
        /// Propriedade psd_categoria.
        /// </summary>
        [MSValidRange(64, "Categoria pode conter até 64 caracteres.")]
        public override string psd_categoria { get; set; }

        /// <summary>
        /// Propriedade psd_classificacao.
        /// </summary>
        [MSValidRange(64, "Classificação pode conter até 64 caracteres.")]
        public override string psd_classificacao { get; set; }

        /// <summary>
        /// Propriedade psd_csm.
        /// </summary>
        [MSValidRange(32, "Csm pode conter até 32 caracteres.")]
        public override string psd_csm { get; set; }

        /// <summary>
        /// Propriedade psd_dataEntrada.
        /// </summary>
        public override DateTime psd_dataEntrada { get; set; }

        /// <summary>
        /// Propriedade psd_dataValidade.
        /// </summary>
        public override DateTime psd_dataValidade { get; set; }

        /// <summary>
        /// Propriedade pai_idOrigem.
        /// </summary>
        public override Guid pai_idOrigem { get; set; }

        /// <summary>
        /// Propriedade psd_serie.
        /// </summary>
        [MSValidRange(32, "Série pode conter até 32 caracteres.")]
        public override string psd_serie { get; set; }

        /// <summary>
        /// Propriedade psd_tipoGuarda.
        /// </summary>
        [MSValidRange(128, "Tipo de guarda pode conter até 128 caracteres.")]
        public override string psd_tipoGuarda { get; set; }

        /// <summary>
        /// Propriedade psd_via.
        /// </summary>
        [MSValidRange(16, "Via pode conter até 16 caracteres.")]
        public override string psd_via { get; set; }

        /// <summary>
        /// Propriedade psd_secao.
        /// </summary>
        [MSValidRange(32, "Seção pode conter até 32 caracteres.")]
        public override string psd_secao { get; set; }

        /// <summary>
        /// Propriedade psd_zona.
        /// </summary>
        [MSValidRange(16, "Zona pode conter até 16 caracteres.")]
        public override string psd_zona { get; set; }

        /// <summary>
        /// Propriedade psd_regiaoMilitar.
        /// </summary>
        [MSValidRange(16, "Região militar pode conter até 16 caracteres.")]
        public override string psd_regiaoMilitar { get; set; }

        /// <summary>
        /// Propriedade psd_numeroRA.
        /// </summary>
        [MSValidRange(64, "Registro de alistamento pode conter até 64 caracteres.")]
        public override string psd_numeroRA { get; set; }

        /// <summary>
        /// Propriedade psd_dataExpedicao.
        /// </summary>
        public override DateTime psd_dataExpedicao { get; set; }
    }
}
