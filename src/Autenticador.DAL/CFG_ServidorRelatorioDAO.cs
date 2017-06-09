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

namespace Autenticador.DAL
{

    /// <summary>
    /// Responsável em guardar as configurações do(s) servidor(s) de relatórios do sistema agregado ao sistema separados por sistema e entidade. Assim podemos guardar mais de um configuração para as várias entidade que o sistema permite e evitando a necessidade de um novo ambiente para cada entidade e sistema.
    /// </summary>
    [Obsolete("Esse recurso está obsoleto. A implementação deve ser realizada no próprio sistema.", false)]
    public class CFG_ServidorRelatorioDAO : Abstract_CFG_ServidorRelatorioDAO
    {
        /// <summary>
        /// Retorna um objeto da classe CFG_ServidorRelatorio carregado com os dados do servidor
        /// de relatório conforme o id da entidade, id do sistema e id do relatório. Caso o relatório
        /// esteja ligado a mais de um servidor de relatório para a entidade do sistema a query 
        /// seleciona randomicamente qual servidor será preenchido na instância da classe.
        /// MÉTODO(S) DEPENDENTE(S):
        /// 1 - Classe: CFG_ServidorRelatorioBO; Método: CarregarCrendencialServidorPorRelatorio
        /// </summary>
        /// <param name="ent_id">id da entidade.</param>
        /// <param name="sis_id">id do sistema.</param>
        /// <param name="rlt_id">id do relatório.</param>
        /// <returns>Retorna o objeto da classe preenchido com os dados do servidor de relatórios.</returns>
        public CFG_ServidorRelatorio CarregarPorIdRelatorioSistema(Guid ent_id, int sis_id, int rlt_id)
        {
            CFG_ServidorRelatorio entity = new CFG_ServidorRelatorio();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_CFG_ServidorRelatorio_LOADBY_idRelatorioSistema", this._Banco);
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
                Param.ParameterName = "@rlt_id";
                Param.Size = 4;
                Param.Value = rlt_id;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();
                if (qs.Return.Rows.Count > 0)
                    entity = this.DataRowToEntity(qs.Return.Rows[0], entity, false);
                return entity;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }
        /// <summary>
        /// Retorno o número de servidores de relatório que estão cadastrados com o mesmo sistema, entidade, 
        /// url e pasta.
        /// MÉTODO(S) DEPENDENTE(S):
        /// 1 - Classe: CFG_RelatorioServidorRelatorioBO; Método: VerificarExisteServidor
        /// </summary>
        /// <param name="ent_id">id da entidade(obrigatório).</param>
        /// <param name="sis_id">id do sistema(obrigatório).</param>
        /// <param name="srr_id">id do servidor de relatório(obrigatório).</param>
        /// <param name="srr_diretorioRelatorios">url do servidor de relatório(obrigatório).</param>
        /// <param name="srr_pastaRelatorios">pasta dos relatórios(obrigatório).</param>
        /// <returns>Números de servidores de relatório que atendem o filtro.</returns>
        public int ContaUrlPorEntidadeDoSistema(Guid ent_id, int sis_id, int srr_id, string srr_diretorioRelatorios, string srr_pastaRelatorios)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_CFG_ServidorRelatorio_ContaUrlPorEntidadeDoSistema", this._Banco);
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

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@srr_diretorioRelatorios";
                Param.Size = 1000;
                Param.Value = srr_diretorioRelatorios;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@srr_pastaRelatorios";
                Param.Size = 1000;
                Param.Value = srr_pastaRelatorios;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                return Convert.ToInt32(qs.Return.Rows[0][0]);
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }
        /// <summary>
        /// Metódo usado na página de busca do módulo de configurações do servidor de relatório 
        /// que retorna todos os servidores que não tenham sido excluídos lógicamente fitrado por
        /// id da entidade, id do sistema e nome do servidor de relatório no sistema administrativo(Autenticador).
        /// MÉTODO(S) DEPENDENTE(S):
        /// 1 - Classe: CFG_RelatorioServidorRelatorioBO; Método: BuscaServidorRelatorio
        /// </summary>
        /// <param name="ent_id">id do entidade do usuário logado(obrigatório)</param>
        /// <param name="sis_id">id do sistema selecionado na busca(opcional, para retornar todos -1)</param>
        /// <param name="srr_nome">nome do servidor de relatório no sistema 
        /// Autenticador(opcional, para todos String.Empty, aceita parte do nome pois usa LIKE na query)</param>
        /// <param name="currentPage">página atual do gridview.</param>
        /// <param name="pageSize">número de registros por página.</param>
        /// <param name="totalRecord">retorna o total de registros da query.</param>
        /// <returns>DataTable com os campos retornados da procededure [Banco de dados]..[NEW_CFG_RelatorioServidorRelatorio_SelectBy_BuscaConfigRelatorio]</returns>
        public DataTable SelectBy_BuscaConfigRelatorio_Paginado(Guid ent_id, int sis_id, string srr_nome, int currentPage, int pageSize, out int totalRecord)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_CFG_ServidorRelatorio_SelectBy_BuscaConfigRelatorio", this._Banco);
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
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@srr_nome";
                Param.Size = 100;
                if (!String.IsNullOrEmpty(srr_nome))
                    Param.Value = srr_nome;
                else
                    Param.Value = String.Empty;
                qs.Parameters.Add(Param);

                #endregion

                totalRecord = qs.Execute(currentPage, pageSize);
                return qs.Return;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }
        /// <summary>
        /// Exclui um registro do banco
        /// </summary>
        /// <param name="entity">Entidade com os dados a serem apagados</param>
        /// <returns>True - Operacao bem sucedida</returns>
        public override bool Delete(CFG_ServidorRelatorio entity)
        {
            this.__STP_DELETE = "NEW_CFG_ServidorRelatorio_DELETE";
            return base.Delete(entity);
        }
        /// <summary>
        /// Inseri os valores da classe em um registro ja existente
        /// </summary>
        /// <param name="entity">Entidade com os dados a serem modificados</param>
        /// <returns>True - Operacao bem sucedida</returns>
        protected override bool Alterar(CFG_ServidorRelatorio entity)
        {
            this.__STP_UPDATE = "NEW_CFG_ServidorRelatorio_UPDATE";
            return base.Alterar(entity);
        }
        /// <summary>
        /// Configura os parametros do metodo de Alterar
        /// </summary>
        /// <param name="qs">Objeto da Store Procedure</param>
        protected override void ParamAlterar(QueryStoredProcedure qs, CFG_ServidorRelatorio entity)
        {
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

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@srr_nome";
            Param.Size = 100;
            Param.Value = entity.srr_nome;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@srr_descricao";
            Param.Size = 1000;
            if (!string.IsNullOrEmpty(entity.srr_descricao))
                Param.Value = entity.srr_descricao;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Boolean;
            Param.ParameterName = "@srr_remoteServer";
            Param.Size = 1;
            Param.Value = entity.srr_remoteServer;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@srr_usuario";
            Param.Size = 512;
            if (!string.IsNullOrEmpty(entity.srr_usuario))
                Param.Value = entity.srr_usuario;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@srr_senha";
            Param.Size = 512;
            if (!string.IsNullOrEmpty(entity.srr_senha))
                Param.Value = entity.srr_senha;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@srr_dominio";
            Param.Size = 512;
            if (!string.IsNullOrEmpty(entity.srr_dominio))
                Param.Value = entity.srr_dominio;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@srr_diretorioRelatorios";
            Param.Size = 1000;
            if (!string.IsNullOrEmpty(entity.srr_diretorioRelatorios))
                Param.Value = entity.srr_diretorioRelatorios;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@srr_pastaRelatorios";
            Param.Size = 1000;
            Param.Value = entity.srr_pastaRelatorios;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@srr_situacao";
            Param.Size = 1;
            Param.Value = entity.srr_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@srr_dataAlteracao";
            Param.Size = 16;
            Param.Value = entity.srr_dataAlteracao;
            qs.Parameters.Add(Param);
        }


        ///// <summary>
        ///// Inseri os valores da classe em um novo registro
        ///// </summary>
        ///// <param name="entity">Entidade com os dados a serem inseridos</param>
        ///// <returns>True - Operacao bem sucedida</returns>
        //protected override bool Inserir(CFG_ServidorRelatorio entity)
        //{
        //    return base.Inserir(entity);
        //}
        ///// <summary>
        ///// Carrega um registro da tabela usando os valores nas chaves
        ///// </summary>
        ///// <param name="entity">Entidade com os dados a serem carregados</param>
        ///// <returns>True - Operacao bem sucedida</returns>
        //public override bool Carregar(CFG_ServidorRelatorio entity)
        //{
        //    return base.Carregar(entity);
        //}
        ///// <summary>
        ///// Configura os parametros do metodo de Alterar
        ///// </summary>
        ///// <param name="qs">Objeto da Store Procedure</param>
        ///// <param name="entity">Entidade com os dados para preenchimento dos parametros</param>
        //protected override void ParamAlterar(QueryStoredProcedure qs, CFG_ServidorRelatorio entity)
        //{
        //    base.ParamAlterar(qs, entity);
        //}
        ///// <summary>
        ///// Configura os parametros do metodo de Carregar
        ///// </summary>
        ///// <param name="qs">Objeto da Store Procedure</param>
        ///// <param name="entity">Entidade com os dados para preenchimento dos parametros</param>
        //protected override void ParamCarregar(QuerySelectStoredProcedure qs, CFG_ServidorRelatorio entity)
        //{
        //    base.ParamCarregar(qs, entity);
        //}
        ///// <summary>
        ///// Configura os parametros do metodo de Deletar
        ///// </summary>
        ///// <param name="qs">Objeto da Store Procedure</param>
        ///// <param name="entity">Entidade com os dados para preenchimento dos parametros</param>
        //protected override void ParamDeletar(QueryStoredProcedure qs, CFG_ServidorRelatorio entity)
        //{
        //    base.ParamDeletar(qs, entity);
        //}
        ///// <summary>
        ///// Configura os parametros do metodo de Inserir
        ///// </summary>
        ///// <param name="qs">Objeto da Store Procedure</param>
        ///// <param name="entity">Entidade com os dados para preenchimento dos parametros</param>
        //protected override void ParamInserir(QuerySelectStoredProcedure qs, CFG_ServidorRelatorio entity)
        //{
        //    base.ParamInserir(qs, entity);
        //}
        ///// <summary>
        ///// Salva o registro no banco de dados
        ///// </summary>
        ///// <param name="entity">Entidade com os dados para preenchimento para inserir ou alterar</param>
        ///// <returns>True - Operacao bem sucedida</returns>
        //public override bool Salvar(CFG_ServidorRelatorio entity)
        //{
        //    return base.Salvar(entity);
        //}
        ///// <summary>
        ///// Realiza o select da tabela
        ///// </summary>
        ///// <returns>Lista com todos os registros da tabela</returns>
        //public override IList<CFG_ServidorRelatorio> Select()
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
        //public override IList<CFG_ServidorRelatorio> Select_Paginado(int currentPage, int pageSize, out int totalRecord)
        //{
        //    return base.Select_Paginado(currentPage, pageSize, out totalRecord);
        //}
        ///// <summary>
        ///// Recebe o valor do auto incremento e coloca na propriedade 
        ///// </summary>
        ///// <param name="qs">Objeto da Store Procedure</param>
        ///// <param name="entity">Entidade com os dados</param>
        ///// <returns>True - Operacao bem sucedida</returns>
        //protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, CFG_ServidorRelatorio entity)
        //{
        //    return base.ReceberAutoIncremento(qs, entity);
        //}
        ///// <summary>
        ///// Passa os dados de um datatable para uma entidade
        ///// </summary>
        ///// <param name="dr">DataRow do datatable preenchido</param>
        ///// <param name="entity">Entidade onde ser�o transferidos os dados</param>
        ///// <returns>Entidade preenchida</returns>
        //public override CFG_ServidorRelatorio DataRowToEntity(DataRow dr, CFG_ServidorRelatorio entity)
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
        //public override CFG_ServidorRelatorio DataRowToEntity(DataRow dr, CFG_ServidorRelatorio entity, bool limparEntity)
        //{
        //    return base.DataRowToEntity(dr, entity, limparEntity);
        //}
    }
}