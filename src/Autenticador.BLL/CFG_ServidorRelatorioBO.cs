using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using CoreLibrary.Data.Common;
using CoreLibrary.Business.Common;
using Autenticador.DAL;
using Autenticador.Entities;
using CoreLibrary.Validation.Exceptions;

namespace Autenticador.BLL
{
    [DataObjectAttribute()]
    [Obsolete("Esse recurso está obsoleto. A implementação deve ser realizada no próprio sistema.", false)]
    public class CFG_ServidorRelatorioBO : BusinessBase<CFG_ServidorRelatorioDAO, CFG_ServidorRelatorio>
    {
        /// <summary>
        /// Retorna a lista de relatórios associados ao servidor de relatórios definidos nos 
        /// parâmetros de entrada.
        /// </summary>
        /// <param name="idEntidade">id da entidade do servidor de relatório(obrigatório).</param>
        /// <param name="idSistema">id da sistema do servidor de relatório(obrigatório).</param>
        /// <param name="idServidor">id do servidor de relatório(obrigatório).</param>
        /// <returns>DataTable de relatórios associados ao servidor(obrigatório).</returns>
        public static IList<CFG_RelatorioServidorRelatorio> ListarRelatorioDoServidor(Guid idEntidade, int idSistema, int idServidor)
        {
            #region VALIDA PARÂMETROS DE ENTRADA

            if (idEntidade.Equals(Guid.Empty))
                throw new ValidationException("Parâmetro idEntidade é obrigatório.");
            if (idSistema <= 0)
                throw new ValidationException("Parâmetro idSistema é obrigatório.");
            if (idServidor <= 0)
                throw new ValidationException("Parâmetro idServidor é obrigatório.");

            #endregion

            CFG_RelatorioServidorRelatorioDAO dal = new CFG_RelatorioServidorRelatorioDAO();
            return dal.SelectBy_ServidorRelatorio(idEntidade, idSistema, idServidor);
        }
        /// <summary>
        /// Retorna lista de todos os relatórios ativos.
        /// </summary>
        /// <returns>List de relatórios ativos.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static IList<CFG_Relatorio> ListarRelatoriosAtivos()
        {
            CFG_RelatorioDAO dal = new CFG_RelatorioDAO();
            return dal.SelectBy_RelatoriosAtivos();
        }
        /// <summary>
        /// Metódo usado na página de busca do módulo de configurações do servidor de relatório 
        /// que retorna todos os servidores que não tenham sido excluídos lógicamente fitrado por</summary>
        /// id da entidade, id do sistema e nome do servidor de relatório no sistema administrativo(Autenticador).
        /// <param name="idEntidade">id do entidade do usuário logado(obrigatório)</param>
        /// <param name="idSistema">id do sistema selecionado na busca(opcional, para retornar todos -1)</param>
        /// <param name="nomeServidorRelatorio">nome do servidor de relatório no sistema 
        /// Autenticador(opcional, para todos String.Empty, aceita parte do nome pois usa LIKE na query)</param>
        /// <param name="currentPage">página atual do gridview.</param>
        /// <param name="pageSize">número de registros por página.</param>
        /// <returns>DataTable</returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static DataTable BuscaServidorRelatorio(Guid idEntidade, int idSistema, string nomeServidorRelatorio, int currentPage, int pageSize)
        {
            #region VALIDA PARÂMETROS DE ENTRADA

            if (idEntidade.Equals(Guid.Empty))
                throw new ValidationException("Parâmetro idEntidade é obrigatório.");
            if (pageSize.Equals(0))
                pageSize = 1;

            #endregion

            totalRecords = 0;
            CFG_ServidorRelatorioDAO dal = new CFG_ServidorRelatorioDAO();
            return dal.SelectBy_BuscaConfigRelatorio_Paginado(idEntidade, idSistema, nomeServidorRelatorio, currentPage / pageSize, pageSize, out totalRecords);
        }
        /// <summary>
        /// Recebe um lista preenchido com objetos da classe CFG_RelatorioServidorRelatorio
        /// contendo os relatórios que serão usados em um determinado servidor de relatórios.
        /// essa lista é convertida para um arquivo xml e enviado para o banco de dados para
        /// que os dados sejam salvos em lote.
        /// </summary>
        /// <param name="lt">List</param>
        /// <returns>True se os dados forem salvos com sucesso.</returns>
        public static bool SalvarRelatoriosDoServidor(IList<CFG_RelatorioServidorRelatorio> lt, TalkDBTransaction banco)
        {
            XmlDocument xDoc = new XmlDocument();
            using (MemoryStream ms = new MemoryStream())
            {
                using (XmlTextWriter xtw = new XmlTextWriter(ms, Encoding.UTF8))
                {
                    xtw.Formatting = Formatting.Indented;
                    XmlSerializer ser = new XmlSerializer(typeof(List<CFG_RelatorioServidorRelatorio>));
                    ser.Serialize(xtw, lt);
                    ms.Seek(0, SeekOrigin.Begin);
                    xDoc.Load(ms);
                }
            }
            CFG_RelatorioServidorRelatorioDAO dal = new CFG_RelatorioServidorRelatorioDAO();
            dal._Banco = banco;
            XmlNode node = xDoc.SelectSingleNode("/ArrayOfCFG_RelatorioServidorRelatorio");
            return dal.InserirPorXML(node);
        }
        /// <summary>
        /// Verifica se já existe algum servidor que possui as mesmas configurações na entidade do usuário logado.
        /// </summary>
        /// <param name="idEntidade">id da entidade do usuário logado(obrigatório).</param>
        /// <param name="idSistema">id do sistema selecionado pelo usuário(obrigatório).</param>
        /// <param name="idServidor">id do servidor de relatório(obrigatório).</param>
        /// <param name="urlServidorRelatorio">url do servidor de relatório(obrigatório).</param>
        /// <param name="pastaRelatorios">diretório onde estão depositados os arquivos dos relatórios(obrigatório).</param>
        /// <returns>True caso encontre um servidor com as mesmas configurações</returns>
        public static bool VerificarExisteServidor(Guid idEntidade, int idSistema, int idServidor, string urlServidorRelatorio, string pastaRelatorios)
        {
            #region VALIDA PARAMETROS DE ENTRADAS

            if (idEntidade.Equals(Guid.Empty))
                throw new ValidationException("Parâmetro idEntidade é obrigatório.");
            if (idSistema <= 0)
                throw new ValidationException("Parâmetro idSistema é obrigatório.");
            if (idServidor <= 0 && (!idServidor.Equals(-1)))
                throw new ValidationException("Parâmetro idServidor é obrigatório.");
            if (String.IsNullOrEmpty(urlServidorRelatorio))
                throw new ValidationException("Parâmetro urlServidorRelatorio é obrigatório.");
            if (String.IsNullOrEmpty(pastaRelatorios))
                throw new ValidationException("Parâmetro pastaRelatorios é obrigatório.");

            #endregion

            CFG_ServidorRelatorioDAO dal = new CFG_ServidorRelatorioDAO();
            int contador = dal.ContaUrlPorEntidadeDoSistema(idEntidade, idSistema, idServidor, urlServidorRelatorio.Trim(), pastaRelatorios.Trim());
            return contador > 0;
        }
        /// <summary>
        /// Retorna as configurações do servidor de relatório conforme o id do relatório da entidade do sistema.
        /// </summary>
        /// <param name="idEntidade">id da entidade do usuário logado.</param>
        /// <param name="idSistema">sistema que o usuário está logado.</param>
        /// <param name="idRelatorio"></param>
        /// <returns></returns>
        public static CFG_ServidorRelatorio CarregarCredencialServidorPorRelatorio(Guid idEntidade, int idSistema, int idRelatorio)
        {
            #region VALIDA PARAMETROS DE ENTRADA

            if (idEntidade.Equals(Guid.Empty))
                throw new ValidationException("Parâmetro idEntidade é obrigatório.");
            if (idSistema <= 0)
                throw new ValidationException("Parâmetro idSistema é obrigatório.");
            if (idRelatorio <= 0)
                throw new ValidationException("Parâmetro idRelatorio é obrigatório.");

            #endregion

            CFG_ServidorRelatorioDAO dal = new CFG_ServidorRelatorioDAO();
            return dal.CarregarPorIdRelatorioSistema(idEntidade, idSistema, idRelatorio);
        }
        /// <summary>
        /// Apaga todos os relatórios relacionado ao servidor de relatório informado nos parâmetros de entrada.
        /// </summary>
        /// <param name="idEntidade">id da entidade do servidor de relatório.</param>
        /// <param name="idSistema">id do sistema do servidor de relatório.</param>
        /// <param name="idServidor">id do servidor de relatório.</param>
        /// <param name="banco">Instância do banco de dados do servidor de relatório</param>
        public static void ApagarTodosRelatoriosServidor(Guid idEntidade, int idSistema, int idServidor, TalkDBTransaction banco)
        {
            #region VALIDA PARAMETROS DE ENTRADA

            if (idEntidade.Equals(Guid.Empty))
                throw new ValidationException("Parâmetro idEntidade é obrigatório.");
            if (idSistema <= 0)
                throw new ValidationException("Parâmetro idSistema é obrigatório.");
            if (idServidor <= 0)
                throw new ValidationException("Parâmetro idServidor é obrigatório.");

            #endregion

            CFG_RelatorioServidorRelatorio entity = new CFG_RelatorioServidorRelatorio()
            {
                ent_id = idEntidade,
                sis_id = idSistema,
                srr_id = idServidor
            };
            CFG_RelatorioServidorRelatorioDAO dal = new CFG_RelatorioServidorRelatorioDAO();
            dal._Banco = banco;
            dal.DeleteAll(entity);
        }
    }
}
