/*
	Classe gerada automaticamente pelo Code Creator
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreLibrary.Data.Common;
using Autenticador.Entities;
using Autenticador.DAL.Abstracts;
using System.Xml;

namespace Autenticador.DAL
{

    /// <summary>
    /// Responsável pela associação do arquivo do relatório com as configurações do servidor de relatório.
    /// </summary>
    public class CFG_RelatorioServidorRelatorioDAO : Abstract_CFG_RelatorioServidorRelatorioDAO
    {
        /// <summary>
        /// Apaga todos os registros da tabela CFG_RelatorioServidorRelatorio relacionados 
        /// com a tabela CFG_ServidorRelatorio da base de dados do sistema.
        /// MÉTODO(S) DEPENDENTE(S):
        /// 1 - Classe: CFG_ServidorRelatorioBO; Método: ApagarTodosRelatoriosServidor
        /// </summary>
        /// <param name="entity">Instância do objeto CFG_RelatorioServidorRelatorio carregados com os dados ent_id, sis_id e srr_id</param>
        /// <returns>True se algum registro for excluído.</returns>
        public bool DeleteAll(CFG_RelatorioServidorRelatorio entity)
        {
            QueryStoredProcedure qs = new QueryStoredProcedure("NEW_CFG_RelatorioServidorRelatorio_DELETE_ALL", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@sis_id";
                Param.Size = 4;
                Param.Value = entity.sis_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ent_id";
                Param.Size = 16;
                Param.Value = entity.ent_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@srr_id";
                Param.Size = 4;
                Param.Value = entity.srr_id;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                return (qs.Return > 0);
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }
        /// <summary>
        /// Lista todos os dados da tabela CFG_Relatorio que estão associado a tabela CFG_RelatorioServidorRelatorio
        /// através da tabela de de/para CFG_RelatorioServidorRelatorio.
        /// MÉTODO(S) DEPENDENTE(S):
        /// 1 - Classe: CFG_ServidorRelatorioBO; Método: ListarRelatorioDoServidor
        /// </summary>
        /// <param name="ent_id">id da entidade que o servidor de relatório pertence</param>
        /// <param name="sis_id">id do sistema que o servidor de relatório pertence</param>
        /// <param name="srr_id">id do servidor de relatório</param>
        /// <returns>DataTable com os dados dos relatórios do servidor</returns>        
        public IList<CFG_RelatorioServidorRelatorio> SelectBy_ServidorRelatorio(Guid ent_id, int sis_id, int srr_id)
        {
            IList<CFG_RelatorioServidorRelatorio> lt = new List<CFG_RelatorioServidorRelatorio>();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_CFG_RelatorioServidorRelatorio_SelectByServidorRelatorio", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ent_id";
                Param.Size = 16;
                Param.Value = ent_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@sis_id";
                Param.Size = 4;
                Param.Value = sis_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@srr_id";
                Param.Size = 4;
                Param.Value = srr_id;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();
                foreach (DataRow dr in qs.Return.Rows)
                {
                    CFG_RelatorioServidorRelatorio entity = new CFG_RelatorioServidorRelatorio();
                    lt.Add(this.DataRowToEntity(dr, entity));
                }
                return lt;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }
        /// <summary>
        /// Insere os dados na tabela conforme o arquivo XML recebido no parametro de entrada e 
        /// delete todos os registros relacionado ao servidor de relatório informado que não esteja
        /// contido no arquivo.
        /// </summary>
        /// <param name="xml">Arquivo xml com os dados dos relatórios usados no servidor</param>
        /// <returns>True se os registro foram salvos com sucesso.</returns>
        public bool InserirPorXML(XmlNode xml)
        {
            QueryStoredProcedure qs = new QueryStoredProcedure("NEW_CFG_RelatorioServidorRelatorio_INSERTBY_XML", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Xml;
                Param.ParameterName = "@xmlRelatorios";
                Param.Value = xml.OuterXml;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                if (qs.Return == 0)
                {
                    return false;
                }
                return true;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }
        ///// <summary>
        ///// Inseri os valores da classe em um registro ja existente
        ///// </summary>
        ///// <param name="entity">Entidade com os dados a serem modificados</param>
        ///// <returns>True - Operacao bem sucedida</returns>
        //protected override bool Alterar(CFG_RelatorioServidorRelatorio entity)
        //{
        //    return base.Alterar(entity);
        //}
        ///// <summary>
        ///// Inseri os valores da classe em um novo registro
        ///// </summary>
        ///// <param name="entity">Entidade com os dados a serem inseridos</param>
        ///// <returns>True - Operacao bem sucedida</returns>
        //protected override bool Inserir(CFG_RelatorioServidorRelatorio entity)
        //{
        //    return base.Inserir(entity);
        //}
        ///// <summary>
        ///// Carrega um registro da tabela usando os valores nas chaves
        ///// </summary>
        ///// <param name="entity">Entidade com os dados a serem carregados</param>
        ///// <returns>True - Operacao bem sucedida</returns>
        //public override bool Carregar(CFG_RelatorioServidorRelatorio entity)
        //{
        //    return base.Carregar(entity);
        //}
        ///// <summary>
        ///// Exclui um registro do banco
        ///// </summary>
        ///// <param name="entity">Entidade com os dados a serem apagados</param>
        ///// <returns>True - Operacao bem sucedida</returns>
        //public override bool Delete(CFG_RelatorioServidorRelatorio entity)
        //{
        //    return base.Delete(entity);
        //}
        ///// <summary>
        ///// Configura os parametros do metodo de Alterar
        ///// </summary>
        ///// <param name="qs">Objeto da Store Procedure</param>
        ///// <param name="entity">Entidade com os dados para preenchimento dos parametros</param>
        //protected override void ParamAlterar(QueryStoredProcedure qs, CFG_RelatorioServidorRelatorio entity)
        //{
        //    base.ParamAlterar(qs, entity);
        //}
        ///// <summary>
        ///// Configura os parametros do metodo de Carregar
        ///// </summary>
        ///// <param name="qs">Objeto da Store Procedure</param>
        ///// <param name="entity">Entidade com os dados para preenchimento dos parametros</param>
        //protected override void ParamCarregar(QuerySelectStoredProcedure qs, CFG_RelatorioServidorRelatorio entity)
        //{
        //    base.ParamCarregar(qs, entity);
        //}
        ///// <summary>
        ///// Configura os parametros do metodo de Deletar
        ///// </summary>
        ///// <param name="qs">Objeto da Store Procedure</param>
        ///// <param name="entity">Entidade com os dados para preenchimento dos parametros</param>
        //protected override void ParamDeletar(QueryStoredProcedure qs, CFG_RelatorioServidorRelatorio entity)
        //{
        //    base.ParamDeletar(qs, entity);
        //}
        ///// <summary>
        ///// Configura os parametros do metodo de Inserir
        ///// </summary>
        ///// <param name="qs">Objeto da Store Procedure</param>
        ///// <param name="entity">Entidade com os dados para preenchimento dos parametros</param>
        //protected override void ParamInserir(QuerySelectStoredProcedure qs, CFG_RelatorioServidorRelatorio entity)
        //{
        //    base.ParamInserir(qs, entity);
        //}
        ///// <summary>
        ///// Salva o registro no banco de dados
        ///// </summary>
        ///// <param name="entity">Entidade com os dados para preenchimento para inserir ou alterar</param>
        ///// <returns>True - Operacao bem sucedida</returns>
        //public override bool Salvar(CFG_RelatorioServidorRelatorio entity)
        //{
        //    return base.Salvar(entity);
        //}
        ///// <summary>
        ///// Realiza o select da tabela
        ///// </summary>
        ///// <returns>Lista com todos os registros da tabela</returns>
        //public override IList<CFG_RelatorioServidorRelatorio> Select()
        //{
        //    return base.Select();
        //}
        ///// <summary>
        ///// Realiza o select da tabela com paginacao
        ///// </summary>
        ///// <param name="currentPage">Pagina atual</param>
        ///// <param name="pageSize">Tamanho da pagina</param>
        ///// <param name="totalRecord">Total de registros na tabela original</param>
        ///// <returns>Lista com todos os registros da p�gina</returns>
        //public override IList<CFG_RelatorioServidorRelatorio> Select_Paginado(int currentPage, int pageSize, out int totalRecord)
        //{
        //    return base.Select_Paginado(currentPage, pageSize, out totalRecord);
        //}
        ///// <summary>
        ///// Recebe o valor do auto incremento e coloca na propriedade 
        ///// </summary>
        ///// <param name="qs">Objeto da Store Procedure</param>
        ///// <param name="entity">Entidade com os dados</param>
        ///// <returns>True - Operacao bem sucedida</returns>
        //protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, CFG_RelatorioServidorRelatorio entity)
        //{
        //    return base.ReceberAutoIncremento(qs, entity);
        //}
        ///// <summary>
        ///// Passa os dados de um datatable para uma entidade
        ///// </summary>
        ///// <param name="dr">DataRow do datatable preenchido</param>
        ///// <param name="entity">Entidade onde ser�o transferidos os dados</param>
        ///// <returns>Entidade preenchida</returns>
        //public override CFG_RelatorioServidorRelatorio DataRowToEntity(DataRow dr, CFG_RelatorioServidorRelatorio entity)
        //{
        //    return base.DataRowToEntity(dr, entity);
        //}
        ///// <summary>
        ///// Passa os dados de um datatable para uma entidade
        ///// </summary>
        ///// <param name="dr">DataRow do datatable preenchido</param>
        ///// <param name="entity">Entidade onde ser�o transferidos os dados</param>
        ///// <param name="limparEntity">Indica se a entidade deve ser limpada antes da transferencia</param>
        ///// <returns>Entidade preenchida</returns>
        //public override CFG_RelatorioServidorRelatorio DataRowToEntity(DataRow dr, CFG_RelatorioServidorRelatorio entity, bool limparEntity)
        //{
        //    return base.DataRowToEntity(dr, entity, limparEntity);
        //}
    }
}