using CoreLibrary.Business.Common;
using Autenticador.DAL;
using Autenticador.Entities;
using System.ComponentModel;
using System.Data;

namespace Autenticador.BLL
{
    public class SYS_TipoDocumentacaoAtributoBO : BusinessBase<SYS_TipoDocumentacaoAtributoDAO, SYS_TipoDocumentacaoAtributo>
    {
        #region Enumerador

        /// <summary>
        /// Enumerador para listar os atributos (campos) de um tipo de documento
        /// </summary>
        public enum eAtributos
        {
            Categoria = 1,
            Classificacao = 2,
            CSM = 3,
            DataEmissao = 4,
            DataEntrada = 5,
            DataExpedicao = 6,
            DataValidade = 7,
            EstadoEmissor = 8,
            InfoComplementares = 9,
            Numero = 10,
            OrgaoExpedidor = 11,
            PaisOrigem = 12,
            RA = 13,
            RegiaoMilitar = 14,
            Secao = 15,
            Serie = 16,
            TipoGuarda = 17,
            Via = 18,
            Zona = 19
        }

        #endregion Enumerador

        /// <summary>
        /// Listagem de todos os atributos dos tipos de documentos
        /// </summary>
        /// <param name="tdo_id">Id do tipo de documento</param>
        /// <returns>Datatable com os atributos</returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static DataTable SelecionarAtributos()
        {
            SYS_TipoDocumentacaoAtributoDAO dao = new SYS_TipoDocumentacaoAtributoDAO();
            return dao.SelecionarAtributos();
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static string SelecionarStringAtributosDefault()
        {
            SYS_TipoDocumentacaoAtributoDAO dao = new SYS_TipoDocumentacaoAtributoDAO();
            DataTable dt = dao.SelecionarStringAtributosDefault(true);

            return dt.Rows[0][0].ToString();
        }
    }
}