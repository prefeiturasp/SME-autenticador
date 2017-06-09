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
    public class SYS_Usuario : Abstract_SYS_Usuario
    {
        [MSValidRange(500, "Login pode conter até 500 caracteres.")]
        [MSNotNullOrEmpty("Login é obrigatório.")]
        public override string usu_login { get; set; }
        [MSValidRange(100, "Domínio pode conter até 100 caracteres.")]
        public override string usu_dominio { get; set; }
        [MSValidRange(500, "E-mail pode conter até 500 caracteres.")]
        //[MSNotNullOrEmpty("E-mail é obrigatório.")]
        public override string usu_email { get; set; }
        public override string usu_senha { get; set; }
        //[MSNotNullOrEmpty("Criptografia é obrigatório.")]
        [MSDefaultValue(1)]
        public override byte usu_criptografia { get; set; }
        [MSDefaultValue(1)]
        public override byte usu_situacao { get; set; }
        public override DateTime usu_dataCriacao { get; set; }
        public override DateTime usu_dataAlteracao { get; set; }
        public override DateTime usu_dataAlteracaoSenha { get; set; }
        public override int usu_integridade { get; set; }
        [MSNotNullOrEmpty("Entidade é obrigatório.")]
        public override Guid ent_id { get; set; }

        /// <summary>
        /// 0 - Usuário não integrado; 1 - Usuário integrado com AD; 2 - Usuário integrado com AD/Replicação de senha.
        /// </summary>
        [MSDefaultValue(0)]
        public override byte usu_integracaoAD { get; set; }
       
        public override short usu_integracaoExterna { get; set; }

        #region Enumerador

        public enum eIntegracaoAD
        {
            NaoIntegrado = 0
            , IntegradoAD = 1
            , IntegradoAD_ReplicacaoSenha = 2
        }

        #endregion
    }
}
