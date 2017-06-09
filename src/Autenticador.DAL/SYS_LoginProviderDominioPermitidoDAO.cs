/*
	Classe gerada automaticamente pelo Code Creator
*/
using Autenticador.DAL.Abstracts;
using Autenticador.Entities;
using CoreLibrary.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace Autenticador.DAL
{
	
	
	/// <summary>
	/// Description: .
	/// </summary>
	public class SYS_LoginProviderDominioPermitidoDAO : Abstract_SYS_LoginProviderDominioPermitidoDAO
	{
        /*
        public List<SYS_LoginProviderDominioPermitido> ObterDominiosPermitidos(Guid entId, string providerName)
        {
            List<SYS_LoginProviderDominioPermitido> lt = new List<SYS_LoginProviderDominioPermitido>();

            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("", _Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@entId";
                Param.Size = 16;
                Param.Value = entId;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.String;
                Param.ParameterName = "@providerName";
                Param.Size = 128;
                Param.Value = providerName;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

                qs.Execute();

                foreach (DataRow dr in qs.Return.Rows)
                {
                    SYS_LoginProviderDominioPermitido entity = new SYS_LoginProviderDominioPermitido();
                    lt.Add(this.DataRowToEntity(dr, entity));
                }

                return lt;
            
            }
            catch
            {
                throw;
            }
            finally
            {
                qs.Parameters.Clear();
            }

        }

        */
 
        public DataTable DeletarDominios(SYS_LoginProviderDominioPermitido entity)
        {
           
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_LoginProviderDominioPermitido_DELETE", _Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ent_id";
                Param.Size = 16;
                Param.Value = entity.ent_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@LoginProvider";
                Param.Size = 128;
                Param.Value = entity.LoginProvider;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

                qs.Execute();

                return qs.Return;

            }
            catch
            {
                throw;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }


        ///// <summary>
        ///// Inseri os valores da classe em um registro ja existente.
        ///// </summary>
        ///// <param name="entity">Entidade com os dados a serem modificados.</param>
        ///// <returns>True - Operacao bem sucedida.</returns>
        // protected override bool Alterar(SYS_LoginProviderDominioPermitido entity)
        // {
        //    return base.Alterar(entity);
        // }
        ///// <summary>
        ///// Inseri os valores da classe em um novo registro.
        ///// </summary>
        ///// <param name="entity">Entidade com os dados a serem inseridos.</param>
        ///// <returns>True - Operacao bem sucedida.</returns>
        // protected override bool Inserir(SYS_LoginProviderDominioPermitido entity)
        // {
        //    return base.Inserir(entity);
        // }
        ///// <summary>
        ///// Carrega um registro da tabela usando os valores nas chaves.
        ///// </summary>
        ///// <param name="entity">Entidade com os dados a serem carregados.</param>
        ///// <returns>True - Operacao bem sucedida.</returns>
        // public override bool Carregar(SYS_LoginProviderDominioPermitido entity)
        // {
        //    return base.Carregar(entity);
        // }
        ///// <summary>
        ///// Exclui um registro do banco.
        ///// </summary>
        ///// <param name="entity">Entidade com os dados a serem apagados.</param>
        ///// <returns>True - Operacao bem sucedida.</returns>
        // public override bool Delete(SYS_LoginProviderDominioPermitido entity)
        // {
        //    return base.Delete(entity);
        // }
        ///// <summary>
        ///// Configura os parametros do metodo de Alterar.
        ///// </summary>
        ///// <param name="qs">Objeto da Store Procedure.</param>
        ///// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
        // protected override void ParamAlterar(QueryStoredProcedure qs, SYS_LoginProviderDominioPermitido entity)
        // {
        //    base.ParamAlterar(qs, entity);
        // }
        ///// <summary>
        ///// Configura os parametros do metodo de Carregar.
        ///// </summary>
        ///// <param name="qs">Objeto da Store Procedure.</param>
        ///// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
        // protected override void ParamCarregar(QuerySelectStoredProcedure qs, SYS_LoginProviderDominioPermitido entity)
        // {
        //    base.ParamCarregar(qs, entity);
        // }
        ///// <summary>
        ///// Configura os parametros do metodo de Deletar.
        ///// </summary>
        ///// <param name="qs">Objeto da Store Procedure.</param>
        ///// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
        // protected override void ParamDeletar(QueryStoredProcedure qs, SYS_LoginProviderDominioPermitido entity)
        // {
        //    base.ParamDeletar(qs, entity);
        // }
        ///// <summary>
        ///// Configura os parametros do metodo de Inserir.
        ///// </summary>
        ///// <param name="qs">Objeto da Store Procedure.</param>
        ///// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
        // protected override void ParamInserir(QuerySelectStoredProcedure qs, SYS_LoginProviderDominioPermitido entity)
        // {
        //    base.ParamInserir(qs, entity);
        // }
        ///// <summary>
        ///// Salva o registro no banco de dados.
        ///// </summary>
        ///// <param name="entity">Entidade com os dados para preenchimento para inserir ou alterar.</param>
        ///// <returns>True - Operacao bem sucedida.</returns>
        // public override bool Salvar(SYS_LoginProviderDominioPermitido entity)
        // {
        //    return base.Salvar(entity);
        // }
        ///// <summary>
        ///// Realiza o select da tabela.
        ///// </summary>
        ///// <returns>Lista com todos os registros da tabela.</returns>
        // public override IList<SYS_LoginProviderDominioPermitido> Select()
        // {
        //    return base.Select();
        // }
        ///// <summary>
        ///// Realiza o select da tabela com paginacao.
        ///// </summary>
        ///// <param name="currentPage">Pagina atual.</param>
        ///// <param name="pageSize">Tamanho da pagina.</param>
        ///// <param name="totalRecord">Total de registros na tabela original.</param>
        ///// <returns>Lista com todos os registros da p�gina.</returns>
        // public override IList<SYS_LoginProviderDominioPermitido> Select_Paginado(int currentPage, int pageSize, out int totalRecord)
        // {
        //    return base.Select_Paginado(currentPage, pageSize, out totalRecord);
        // }
        ///// <summary>
        ///// Recebe o valor do auto incremento e coloca na propriedade. 
        ///// </summary>
        ///// <param name="qs">Objeto da Store Procedure.</param>
        ///// <param name="entity">Entidade com os dados.</param>
        ///// <returns>True - Operacao bem sucedida.</returns>
        // protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, SYS_LoginProviderDominioPermitido entity)
        // {
        //    return base.ReceberAutoIncremento(qs, entity);
        // }
        ///// <summary>
        ///// Passa os dados de um datatable para uma entidade.
        ///// </summary>
        ///// <param name="dr">DataRow do datatable preenchido.</param>
        ///// <param name="entity">Entidade onde ser�o transferidos os dados.</param>
        ///// <returns>Entidade preenchida.</returns>
        // public override SYS_LoginProviderDominioPermitido DataRowToEntity(DataRow dr, SYS_LoginProviderDominioPermitido entity)
        // {
        //    return base.DataRowToEntity(dr, entity);
        // }
        ///// <summary>
        ///// Passa os dados de um datatable para uma entidade.
        ///// </summary>
        ///// <param name="dr">DataRow do datatable preenchido.</param>
        ///// <param name="entity">Entidade onde ser�o transferidos os dados.</param>
        ///// <param name="limparEntity">Indica se a entidade deve ser limpada antes da transferencia.</param>
        ///// <returns>Entidade preenchida.</returns>
        // public override SYS_LoginProviderDominioPermitido DataRowToEntity(DataRow dr, SYS_LoginProviderDominioPermitido entity, bool limparEntity)
        // {
        //    return base.DataRowToEntity(dr, entity, limparEntity);
        // }
    }
}