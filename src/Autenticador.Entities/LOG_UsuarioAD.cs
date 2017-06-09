/*
	Classe gerada automaticamente pelo Code Creator
*/

namespace Autenticador.Entities
{
    using Autenticador.Entities.Abstracts;
    using System;

    /// <summary>
    /// Description: .
    /// </summary>
    [Serializable]
    public class LOG_UsuarioAD : AbstractLOG_UsuarioAD
    {
        #region Enumerador

        public enum eAcao : short
        {
            AlterarSenha = 1
            , IncluirUsuario = 2
            , ExcluirUsuario = 3
        }

        public enum eStatus : short
        {
            Pendente = 1
            , EmProcessamento = 2
            , Processado = 3
            , Falha = 4
        }

        public enum eOrigem : short
        {
            Autenticador = 1
            , AD = 2
        }

        #endregion 

        public override long usa_id { get; set; }
    }
}