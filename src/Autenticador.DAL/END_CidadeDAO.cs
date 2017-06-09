using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Autenticador.Entities;
using CoreLibrary.Data.Common;
using Autenticador.DAL.Abstracts;
using System.Xml;

namespace Autenticador.DAL
{
    public class END_CidadeDAO : Abstract_END_CidadeDAO
    {
        /// <summary>
        /// Retorna um datatable contendo todas as cidades
        /// que não foram excluídas logicamente, filtradas por 
        ///	cid_id, unf_id, pai_id, cidade, estado, sigla do estado,
	    ///	pais e situação        
        /// </summary>        
        /// <param name="cid_id">ID da cidade</param>
        /// <param name="pai_id">ID do pais</param>
        /// <param name="unf_id">ID do estado</param>
        /// <param name="cid_nome">Nome da cidade</param>
        /// <param name="unf_nome">Nome do estado</param>
        /// <param name="unf_sigla">Sigla do estado</param>
        /// <param name="pai_nome">Nome do pais</param>
        /// <param name="cid_situacao">Situacao da cidade</param>
        /// <param name="paginado">Indica se o datatable será paginado ou não</param>
        /// <returns>DataTable com as cidades</returns>
        public DataTable SelectBy_All
        (
            Guid cid_id
            , Guid pai_id
            , Guid unf_id
            , string cid_nome
            , string unf_nome
            , string unf_sigla
            , string pai_nome            
            , byte cid_situacao
            , bool paginado
            , int currentPage
            , int pageSize
            , out int totalRecords
        )
        {
            //DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_END_Cidade_SelectBy_All", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@cid_id";
                Param.Size = 16;
                if (cid_id != Guid.Empty)
                    Param.Value = cid_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@unf_id";
                Param.Size = 16;
                if (unf_id != Guid.Empty)
                    Param.Value = unf_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@pai_id";
                Param.Size = 16;
                if (pai_id != Guid.Empty)
                    Param.Value = pai_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@cid_nome";
                Param.Size = 200;
                if (!string.IsNullOrEmpty(cid_nome))
                    Param.Value = cid_nome;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@unf_nome";
                Param.Size = 100;
                if (!string.IsNullOrEmpty(unf_nome))
                    Param.Value = unf_nome;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@unf_sigla";
                Param.Size = 2;
                if (!string.IsNullOrEmpty(unf_sigla))
                    Param.Value = unf_sigla;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@pai_nome";
                Param.Size = 100;
                if (!string.IsNullOrEmpty(pai_nome))
                    Param.Value = pai_nome;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@cid_situacao";
                Param.Size = 1;
                if (cid_situacao > 0)
                    Param.Value = cid_situacao;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion

                if (paginado)
                    totalRecords = qs.Execute(currentPage, pageSize);
                else
                {
                    qs.Execute();
                    totalRecords = qs.Return.Rows.Count;
                }

                //if (qs.Return.Rows.Count > 0)
                    //dt = qs.Return;

                //return dt;
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

        /// <summary>
        /// Retorna um datatable contendo todas as cidades
        /// que não foram excluídas logicamente d eum determinado
        /// estado, filtradas por unf_id
        /// </summary>        
        /// <param name="unf_id">ID do estado</param>
        /// <param name="paginado">Indica se o datatable será paginado ou não</param>
        /// <returns>DataTable com as cidades</returns>
        public DataTable SelectBy_unf_id
        (
            Guid unf_id
            , bool paginado
            , int currentPage
            , int pageSize
            , out int totalRecords
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_END_Cidade_SelectBy_unf_id", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@unf_id";
                Param.Size = 16;
                if (unf_id != Guid.Empty)
                    Param.Value = unf_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion

                if (paginado)
                    totalRecords = qs.Execute(currentPage, pageSize);
                else
                {
                    qs.Execute();
                    totalRecords = qs.Return.Rows.Count;
                }

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

        /// <summary>
        /// Carrega a Entidade END_Cidade
        /// apartir dos parâmetros
        /// </summary>
        /// <param name="cid_nome">Nome da cidade</param>
        /// <param name="unf_sigla">Sigla da unidade federativa</param>
        /// <param name="entityCidade">Entidade END_Cidade de retorno que será carregada</param>
        /// <returns></returns>
        public bool SelectBy_cid_nome_unf_sigla
            (
                 string cid_nome
                , string unf_sigla
                , out END_Cidade entityCidade
            )
        {
            entityCidade = new END_Cidade();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_END_Cidade_SelectBy_Nome_UF", this._Banco);
            try
            {
                #region Parâmetros

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@cid_nome";
                Param.Size = 200;
                Param.Value = cid_nome;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@unf_sigla";
                Param.Size = 2;
                if (!string.IsNullOrEmpty(unf_sigla))
                    Param.Value = unf_sigla;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                {
                    entityCidade = DataRowToEntity(qs.Return.Rows[0], entityCidade, false);
                    return true;
                }
                return false;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Retorna true/false
        /// para saber se a cidade já está cadastrada
        /// filtradas por cid_id (diferente do parametro informado), pai_id, unf_id, cid_nome, cid_situacao
        /// </summary>
        /// <param name="cid_id">Id da tabela END_Cidade do bd</param>
        /// <param name="pai_id">Id da tabela END_Pais do bd</param>        
        /// <param name="unf_id">Id da tabela END_UnidadeFederativa do bd</param>
        /// <param name="cid_nome">Campo cid_nome da da tabela END_Cidade do bd</param>
        /// <param name="ent_situacao">Campo cid_situcao da tabela END_Cidade do bd</param>        
        /// <returns>DataTable com as entidades</returns>
        public bool SelectBy_cid_nome
        (
            Guid cid_id
            , Guid pai_id
            , Guid unf_id
            , string cid_nome
            , byte cid_situacao
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_END_Cidade_SelectBy_cid_nome", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@cid_id";
                Param.Size = 16;
                if (cid_id != Guid.Empty)
                    Param.Value = cid_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@pai_id";
                Param.Size = 16;
                if (pai_id != Guid.Empty)
                    Param.Value = pai_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@unf_id";
                Param.Size = 16;
                if (unf_id != Guid.Empty)
                    Param.Value = unf_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@cid_nome";
                Param.Size = 200;
                if (!string.IsNullOrEmpty(cid_nome))
                    Param.Value = cid_nome;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@cid_situacao";
                Param.Size = 1;
                if (cid_situacao > 0)
                    Param.Value = cid_situacao;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                    return true;

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

        /// <summary>
        /// Retorna um List contendo as 10 primeiras cidades
        /// que não foram excluídas logicamente              
        /// </summary>        
        /// <returns>Lista com as cidades</returns>
        public List<END_Cidade> SelectBy_PesquisaIncremental
        (            
            string cid_nome
        )
        {
            List<END_Cidade> lt = new List<END_Cidade>();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_END_Cidade_SelectBy_PesquisaIncremental", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@cid_nome";
                Param.Size = 200;
                if (!string.IsNullOrEmpty(cid_nome))
                    Param.Value = cid_nome;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                foreach (DataRow dr in qs.Return.Rows)
                {
                    END_Cidade entity = new END_Cidade();
                    lt.Add(DataRowToEntity(dr, entity));
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

        /// <summary>
        /// Retorna true/false
        /// para saber se a cidade (valor) está sendo usada em alguma tabela no banco GestaoCore e nos bancos
        /// relacionados ao GestaoCore
        /// </summary>
        /// <param name="coluna">nome da coluna referente ao id da cidade no bd</param>
        /// <param name="coluna_valor">valor da coluna referente ao id da cidade no bd</param>        
        /// <returns>True - Caso esteja sendo usada/False - caso não exista registro com uso para este valor</returns>
        public bool Select_ColunaValor_BD
        (
            string coluna
            ,int coluna_valor
            ,string tabela_raiz
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_Select_ColunaValor_BD", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@coluna";
                Param.Size = 100;
                if (!string.IsNullOrEmpty(coluna))
                    Param.Value = coluna;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@valor";
                Param.Size = 4;
                if (coluna_valor > 0)
                    Param.Value = coluna_valor;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@tabela_raiz";
                Param.Size = 100;
                if (!string.IsNullOrEmpty(tabela_raiz))
                    Param.Value = tabela_raiz;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                if (Convert.ToInt32(qs.Return.Rows[0][0]) > 0)
                    return true;

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

        /// <summary>
        /// Retorna true/false
        /// para saber se a cidade (valor) está sendo usada em alguma tabela no banco GestaoCore e nos bancos
        /// relacionados ao GestaoCore
        /// </summary>
        /// <param name="coluna">nome da coluna referente ao id da cidade no bd</param>
        /// <param name="coluna_valor">valor da coluna referente ao id da cidade no bd</param>        
        /// <returns>True - Caso esteja sendo usada/False - caso não exista registro com uso para este valor</returns>
        public bool AssociarCidades
        (
            Guid cid_id
            , XmlDocument xDoc
            , string coluna
            , string tabela_raiz
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_Update_ColunaValor_BD", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@valor";
                Param.Size = 16;
                Param.Value = cid_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = System.Data.DbType.Xml;
                Param.ParameterName = "@xml";
                Param.Value = xDoc.InnerXml;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@coluna";
                Param.Size = 100;
                Param.Value = coluna;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@tabela_raiz";
                Param.Size = 100;
                Param.Value = tabela_raiz;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                return true;
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
        /// Parâmetros para efetuar a inclusão sem o ID da PK gerado automaticamente
        /// </summary>
        protected override void ParamInserir(QuerySelectStoredProcedure qs, Autenticador.Entities.END_Cidade entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@pai_id";
            Param.Size = 16;
            Param.Value = entity.pai_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@unf_id";
            Param.Size = 16;
            if (entity.unf_id != Guid.Empty)
                Param.Value = entity.unf_id;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@cid_nome";
            Param.Size = 200;
            Param.Value = entity.cid_nome;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@cid_ddd";
            Param.Size = 3;
            if (!string.IsNullOrEmpty(entity.cid_ddd))
                Param.Value = entity.cid_ddd;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@cid_situacao";
            Param.Size = 1;
            Param.Value = entity.cid_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@cid_integridade";
            Param.Size = 4;
            Param.Value = entity.cid_integridade;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Parâmetros para efetuar a alteração preservando a integridade
        /// </summary>
        protected override void ParamAlterar(QueryStoredProcedure qs, Autenticador.Entities.END_Cidade entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@cid_id";
            Param.Size = 16;
            Param.Value = entity.cid_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@pai_id";
            Param.Size = 16;
            Param.Value = entity.pai_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@unf_id";
            Param.Size = 16;
            if (entity.unf_id != Guid.Empty)
                Param.Value = entity.unf_id;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@cid_nome";
            Param.Size = 200;
            Param.Value = entity.cid_nome;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@cid_ddd";
            Param.Size = 3;
            if (!string.IsNullOrEmpty(entity.cid_ddd))
                Param.Value = entity.cid_ddd;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@cid_situacao";
            Param.Size = 1;
            Param.Value = entity.cid_situacao;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Método alterado para que o update não faça a alteração da integridade
        /// </summary>
        /// <param name="entity"> Entidade END_Cidade</param>
        /// <returns>true = sucesso | false = fracasso</returns>  
        protected override bool Alterar(Autenticador.Entities.END_Cidade entity)
        {
            this.__STP_UPDATE = "NEW_END_Cidade_UPDATE";
            return base.Alterar(entity);
        }

        /// <summary>
        /// Parâmetros para efetuar a exclusão lógica.
        /// </summary>
        protected override void ParamDeletar(QueryStoredProcedure qs, Autenticador.Entities.END_Cidade entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@cid_id";
            Param.Size = 16;
            Param.Value = entity.cid_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@cid_situacao";
            Param.Size = 1;
            Param.Value = 3;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Método alterado para que o delete não faça exclusão física e sim lógica (update).
        /// </summary>
        /// <param name="entity"> Entidade END_Cidade</param>
        /// <returns>true = sucesso | false = fracasso</returns>  
        public override bool Delete(Autenticador.Entities.END_Cidade entity)
        {
            this.__STP_DELETE = "NEW_END_Cidade_UPDATE_Situacao";
            return base.Delete(entity);
        }

        public int Select_Integridade
        (
         Guid cid_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_END_Cidade_Select_Integridade", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@cid_id";
                Param.Size = 16;
                if (cid_id != Guid.Empty)
                    Param.Value = cid_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                    return Convert.ToInt32(qs.Return.Rows[0]["cid_integridade"].ToString());

                return -1;
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
        /// Incrementa uma unidade no campo integridade da cidade
        /// </summary>
        /// <param name="cid_id">Id da tabela end_cidade do bd</param>
        /// <returns>true = sucesso | false = fracasso</returns>    
        public bool Update_IncrementaIntegridade
        (
            Guid cid_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_END_Cidade_INCREMENTA_INTEGRIDADE", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@cid_id";
                Param.Size = 16;
                if (cid_id != Guid.Empty)
                    Param.Value = cid_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                return true;
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
        /// Decrementa uma unidade no campo integridade da cidade
        /// </summary>
        /// <param name="cid_id">Id da tabela end_cidade do bd</param>
        /// <returns>true = sucesso | false = fracasso</returns>    
        public bool Update_DecrementaIntegridade
        (
            Guid cid_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_END_Cidade_DECREMENTA_INTEGRIDADE", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@cid_id";
                Param.Size = 16;
                if (cid_id != Guid.Empty)
                    Param.Value = cid_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                return true;
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
    }
}
