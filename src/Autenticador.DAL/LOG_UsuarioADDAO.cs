/*
	Classe gerada automaticamente pelo Code Creator
*/

namespace Autenticador.DAL
{
	using System.Data;
    using Autenticador.DAL.Abstracts;
    using CoreLibrary.Data.Common;
	
	/// <summary>
	/// Description: .
	/// </summary>
	public class LOG_UsuarioADDAO : AbstractLOG_UsuarioADDAO
    {
        #region M�todos

        /// <summary>
        /// Seleciona todos os hist�ricos de altera��o de senha n�o processados.
        /// </summary>
        /// <returns></returns>
        public DataTable SelecionaNaoProcessados()
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_LOG_UsuarioAD_SelecionaNaoProcessados", _Banco);

            try
            {
                qs.Execute();

                return qs.Return;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        #endregion
    }
}