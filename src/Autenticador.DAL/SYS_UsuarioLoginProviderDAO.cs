/*
    Classe gerada automaticamente pelo Code Creator
*/
using Autenticador.DAL.Abstracts;
using Autenticador.Entities;
using CoreLibrary.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Autenticador.DAL
{

    /// <summary>
    /// Description: .
    /// </summary>
    public class SYS_UsuarioLoginProviderDAO : Abstract_SYS_UsuarioLoginProviderDAO
    {

        public SYS_UsuarioLoginProvider SelectBy_LoginProvider_ProviderKey(string providerName, string providerKey)
        {
            List<SYS_UsuarioLoginProvider> lt = new List<SYS_UsuarioLoginProvider>();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_STP_SYS_UsuarioLoginProvider_LOADBy_LoginProvider_ProviderKey", _Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.String;
                Param.ParameterName = "@LoginProvider";
                Param.Size = 128;
                Param.Value = providerName;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.String;
                Param.ParameterName = "@ProviderKey";
                Param.Size = 512;
                Param.Value = providerKey;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                {
                    foreach (DataRow dr in qs.Return.Rows)
                    {
                        SYS_UsuarioLoginProvider entity = new SYS_UsuarioLoginProvider();
                        lt.Add(this.DataRowToEntity(dr, entity));
                    }
                }

                return lt.FirstOrDefault();

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

        /// <summary>
        /// Retorna a lista de contas externas vinculadas ao usuário.
        /// </summary>
        /// <param name="usu_id">Id do usuário</param>
        /// <returns>DataTable de UsuarioLoginProvider</returns>
        public List<SYS_UsuarioLoginProvider> SelectBy_usu_id(Guid usu_id)
        {
            var lstUsuarioLoginProvider = new List<SYS_UsuarioLoginProvider>();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_STP_SYS_UsuarioLoginProvider_SelectBy_usu_id", _Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@usu_id";
                Param.Size = 16;
                if (usu_id != Guid.Empty)
                    Param.Value = usu_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);
                

                #endregion PARAMETROS

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                {
                    foreach (DataRow dr in qs.Return.Rows)
                    {
                        SYS_UsuarioLoginProvider entity = new SYS_UsuarioLoginProvider();
                        lstUsuarioLoginProvider.Add(DataRowToEntity(dr, entity));
                    }
                }

                return lstUsuarioLoginProvider;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }



        /// <summary>
        /// Busca UsuarioLoginProvider por id do usuário
        /// </summary>
        /// <param name="usu_id">Id do usuário</param>
        /// <returns>DataTable de UsuarioLoginProvider</returns>
        public SYS_UsuarioLoginProvider SelectBy_usu_id_LoginProvider(Guid usu_id, string loginProvider)
        {
            var lstUsuarioLoginProvider = new List<SYS_UsuarioLoginProvider>();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_STP_SYS_UsuarioLoginProvider_SelectBy_usu_id_loginProvider", _Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@usu_id";
                Param.Size = 16;
                if (usu_id != Guid.Empty)
                    Param.Value = usu_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);


                Param = qs.NewParameter();
                Param.DbType = DbType.String;
                Param.ParameterName = "@LoginProvider";
                Param.Size = 128;
                Param.Value = loginProvider;
                qs.Parameters.Add(Param);


                #endregion PARAMETROS

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                {
                    foreach (DataRow dr in qs.Return.Rows)
                    {
                        SYS_UsuarioLoginProvider entity = new SYS_UsuarioLoginProvider();
                        lstUsuarioLoginProvider.Add(DataRowToEntity(dr, entity));
                    }
                }

                return lstUsuarioLoginProvider.FirstOrDefault();
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

        /// <summary>
        /// Delete o UsuarioLoginProvider pelo Id do usuário
        /// </summary>
        /// <param name="usu_id">Id do usuário</param>
        /// <returns>true = deletado / false = não deletado</returns>
        public bool DeleteBy_usu_id(Guid usu_id)
        {
            var lstUsuarioLoginProvider = new List<SYS_UsuarioLoginProvider>();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("STP_SYS_UsuarioLoginProvider_DELETE_By_usu_id", _Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@usu_id";
                Param.Size = 16;
                if (usu_id != Guid.Empty)
                    Param.Value = usu_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                {
                    return true;
                }

                return false;
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
        // protected override bool Alterar(SYS_UsuarioLoginProvider entity)
        // {
        //    return base.Alterar(entity);
        // }
        ///// <summary>
        ///// Inseri os valores da classe em um novo registro.
        ///// </summary>
        ///// <param name="entity">Entidade com os dados a serem inseridos.</param>
        ///// <returns>True - Operacao bem sucedida.</returns>
        // protected override bool Inserir(SYS_UsuarioLoginProvider entity)
        // {
        //    return base.Inserir(entity);
        // }
        ///// <summary>
        ///// Carrega um registro da tabela usando os valores nas chaves.
        ///// </summary>
        ///// <param name="entity">Entidade com os dados a serem carregados.</param>
        ///// <returns>True - Operacao bem sucedida.</returns>
        // public override bool Carregar(SYS_UsuarioLoginProvider entity)
        // {
        //    return base.Carregar(entity);
        // }
        ///// <summary>
        ///// Exclui um registro do banco.
        ///// </summary>
        ///// <param name="entity">Entidade com os dados a serem apagados.</param>
        ///// <returns>True - Operacao bem sucedida.</returns>
        // public override bool Delete(SYS_UsuarioLoginProvider entity)
        // {
        //    return base.Delete(entity);
        // }
        ///// <summary>
        ///// Configura os parametros do metodo de Alterar.
        ///// </summary>
        ///// <param name="qs">Objeto da Store Procedure.</param>
        ///// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
        // protected override void ParamAlterar(QueryStoredProcedure qs, SYS_UsuarioLoginProvider entity)
        // {
        //    base.ParamAlterar(qs, entity);
        // }
        ///// <summary>
        ///// Configura os parametros do metodo de Carregar.
        ///// </summary>
        ///// <param name="qs">Objeto da Store Procedure.</param>
        ///// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
        // protected override void ParamCarregar(QuerySelectStoredProcedure qs, SYS_UsuarioLoginProvider entity)
        // {
        //    base.ParamCarregar(qs, entity);
        // }
        ///// <summary>
        ///// Configura os parametros do metodo de Deletar.
        ///// </summary>
        ///// <param name="qs">Objeto da Store Procedure.</param>
        ///// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
        // protected override void ParamDeletar(QueryStoredProcedure qs, SYS_UsuarioLoginProvider entity)
        // {
        //    base.ParamDeletar(qs, entity);
        // }
        ///// <summary>
        ///// Configura os parametros do metodo de Inserir.
        ///// </summary>
        ///// <param name="qs">Objeto da Store Procedure.</param>
        ///// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
        // protected override void ParamInserir(QuerySelectStoredProcedure qs, SYS_UsuarioLoginProvider entity)
        // {
        //    base.ParamInserir(qs, entity);
        // }
        ///// <summary>
        ///// Salva o registro no banco de dados.
        ///// </summary>
        ///// <param name="entity">Entidade com os dados para preenchimento para inserir ou alterar.</param>
        ///// <returns>True - Operacao bem sucedida.</returns>
        // public override bool Salvar(SYS_UsuarioLoginProvider entity)
        // {
        //    return base.Salvar(entity);
        // }
        ///// <summary>
        ///// Realiza o select da tabela.
        ///// </summary>
        ///// <returns>Lista com todos os registros da tabela.</returns>
        // public override IList<SYS_UsuarioLoginProvider> Select()
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
        // public override IList<SYS_UsuarioLoginProvider> Select_Paginado(int currentPage, int pageSize, out int totalRecord)
        // {
        //    return base.Select_Paginado(currentPage, pageSize, out totalRecord);
        // }
        ///// <summary>
        ///// Recebe o valor do auto incremento e coloca na propriedade. 
        ///// </summary>
        ///// <param name="qs">Objeto da Store Procedure.</param>
        ///// <param name="entity">Entidade com os dados.</param>
        ///// <returns>True - Operacao bem sucedida.</returns>
        // protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, SYS_UsuarioLoginProvider entity)
        // {
        //    return base.ReceberAutoIncremento(qs, entity);
        // }
        ///// <summary>
        ///// Passa os dados de um datatable para uma entidade.
        ///// </summary>
        ///// <param name="dr">DataRow do datatable preenchido.</param>
        ///// <param name="entity">Entidade onde ser�o transferidos os dados.</param>
        ///// <returns>Entidade preenchida.</returns>
        // public override SYS_UsuarioLoginProvider DataRowToEntity(DataRow dr, SYS_UsuarioLoginProvider entity)
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
        // public override SYS_UsuarioLoginProvider DataRowToEntity(DataRow dr, SYS_UsuarioLoginProvider entity, bool limparEntity)
        // {
        //    return base.DataRowToEntity(dr, entity, limparEntity);
        // }
    }
}