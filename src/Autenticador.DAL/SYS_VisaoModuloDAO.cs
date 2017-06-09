using Autenticador.DAL.Abstracts;
using CoreLibrary.Data.Common;
using Autenticador.Entities;

namespace Autenticador.DAL
{
    public class SYS_VisaoModuloDAO : Abstract_SYS_VisaoModuloDAO
    {
        /// <summary>
        /// Recebe o valor do auto incremento e coloca na propriedade 
        /// </summary>
        protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, SYS_VisaoModulo entity)
        {
            return true;
        }	
    }
}
