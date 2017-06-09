using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreLibrary.Data.Common;
using Autenticador.DAL.Abstracts;
using System.Xml;
using Autenticador.Entities;

namespace Autenticador.DAL
{
    public class END_EnderecoDAO : Abstract_END_EnderecoDAO
    {
        /// <summary>
        /// Retorna um datatable contendo todos os endereços
        /// que não foram excluídos logicamente, filtrados por 
		///	end_id, cid_id, unf_id, pai_id, cep, logradouro, bairro,
	    ///	cidade, estado, sigla do estado, pais e situação        
        /// </summary>
        /// <param name="end_id">Id da tabela END_Endereco do bd</param>
        /// <param name="cid_id">Id da tabela END_Cidade do bd</param>
        /// <param name="unf_id">Id da tabela END_UnidadeFederativa do bd</param>
        /// <param name="pai_id">Id da tabela END_Pais do bd</param>
        /// <param name="end_cep">Campo end_cep da tabela END_Endereco do bd</param>
        /// <param name="end_logradouro">Campo end_logradouro da tabela END_Endereco do bd</param>
        /// <param name="end_bairro">Campo end_bairro da tabela END_Endereco do bd</param>
        /// <param name="cid_nome">Campo cid_nome da tabela END_Cidade do bd</param>
        /// <param name="unf_nome">Campo unf_nome da tabela END_UnidadeFederativa do bd</param>
        /// <param name="unf_sigla">Campo unf_sigla da tabela END_UnidadeFederativa do bd</param>        
        /// <param name="pai_nome">Campo pai_nome da tabela END_Pais do bd</param>        
        /// <param name="end_situacao">Campo end_situcao da tabela END_Endereco do bd</param>
        /// <param name="paginado">Indica se o datatable será paginado ou não</param> 
        /// <returns>DataTable com os endereço</returns>
        public DataTable SelectBy_All
        (
            Guid end_id
            , Guid cid_id
            , Guid pai_id
            , Guid unf_id
            , string end_cep
            , string end_logradouro
            , string end_bairro
            , string cid_nome
            , string unf_nome
            , string unf_sigla
            , string pai_nome            
            , byte end_situacao
            , bool paginado
            , int currentPage
            , int pageSize
            , out int totalRecords
        )
        {
            DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_END_Endereco_SelectBy_All", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@end_id";
                Param.Size = 16;
                if (end_id != Guid.Empty)
                    Param.Value = end_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

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
                Param.ParameterName = "@end_cep";
                Param.Size = 8;
                if (!string.IsNullOrEmpty(end_cep))
                    Param.Value = end_cep;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@end_logradouro";
                Param.Size = 200;
                if (!string.IsNullOrEmpty(end_logradouro))
                    Param.Value = end_logradouro;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@end_bairro";
                Param.Size = 100;
                if (!string.IsNullOrEmpty(end_bairro))
                    Param.Value = end_bairro;
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
                Param.ParameterName = "@end_situacao";
                Param.Size = 1;
                if (end_situacao > 0)
                    Param.Value = end_situacao;
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

                if (qs.Return.Rows.Count > 0)
                    dt = qs.Return;

                return dt;
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
        /// Retorna um datatable contendo todos os endereços
        /// que não foram excluídos logicamente, filtrados por 
        /// end_cep, end_logradouro      
        /// </summary>
        /// <param name="end_cep">Campo end_cep da tabela END_Endereco do bd</param>
        /// <param name="end_logradouro">Campo end_logradouro da tabela END_Endereco do bd</param>
        /// <returns>Lista com os endereço</returns>
        public List<END_Endereco> SelectBy_end_id_end_logradouro
        (
            string end_cep
            , string end_logradouro
        )
        {
            List<END_Endereco> lt = new List<END_Endereco>();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_END_Endereco_SelectBy_end_cep_end_logradouro", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@end_cep";
                Param.Size = 8;
                if (!string.IsNullOrEmpty(end_cep))
                    Param.Value = end_cep;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@end_logradouro";
                Param.Size = 200;
                if (!string.IsNullOrEmpty(end_logradouro))
                    Param.Value = end_logradouro;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);                

                #endregion

                qs.Execute();

                foreach (DataRow dr in qs.Return.Rows)
                {
                    END_Endereco entity = new END_Endereco();
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

        /// <summary>
        /// Parâmetros para efetuar a inclusão com data de criação e de alteração fixas
        /// </summary>
        protected override void ParamInserir(QuerySelectStoredProcedure qs, Autenticador.Entities.END_Endereco entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@end_cep";
            Param.Size = 8;
            Param.Value = entity.end_cep;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@end_logradouro";
            Param.Size = 200;
            Param.Value = entity.end_logradouro;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@end_bairro";
            Param.Size = 100;
            if (!string.IsNullOrEmpty(entity.end_bairro))
                Param.Value = entity.end_bairro;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@end_distrito";
            Param.Size = 100;
            if (!string.IsNullOrEmpty(entity.end_distrito))
                Param.Value = entity.end_distrito;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@end_zona";
            Param.Size = 1;
            if (entity.end_zona > 0)
                Param.Value = entity.end_zona;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@cid_id";
            Param.Size = 16;
            Param.Value = entity.cid_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@end_situacao";
            Param.Size = 1;
            Param.Value = entity.end_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@end_dataCriacao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@end_dataAlteracao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@end_integridade";
            Param.Size = 4;
            Param.Value = entity.end_integridade;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Parâmetros para efetuar a alteração preservando a data de criação e a integridade
        /// </summary>
        protected override void ParamAlterar(QueryStoredProcedure qs, Autenticador.Entities.END_Endereco entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@end_id";
            Param.Size = 16;
            Param.Value = entity.end_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@end_cep";
            Param.Size = 8;
            Param.Value = entity.end_cep;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@end_logradouro";
            Param.Size = 200;
            Param.Value = entity.end_logradouro;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@end_distrito";
            Param.Size = 100;
            if (!string.IsNullOrEmpty(entity.end_distrito))
                Param.Value = entity.end_distrito;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@end_zona";
            Param.Size = 1;
            if (entity.end_zona > 0)
                Param.Value = entity.end_zona;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@end_bairro";
            Param.Size = 100;
            if (!string.IsNullOrEmpty(entity.end_bairro))
                Param.Value = entity.end_bairro;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@cid_id";
            Param.Size = 16;
            Param.Value = entity.cid_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@end_integridade";
            Param.Size = 4;
            Param.Value = entity.end_integridade;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@end_situacao";
            Param.Size = 1;
            Param.Value = entity.end_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@end_dataAlteracao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Método alterado para que o update não faça a alteração da data de criação e da integridade
        /// </summary>
        /// <param name="entity"> Entidade END_Endereco</param>
        /// <returns>true = sucesso | false = fracasso</returns>  
        protected override bool Alterar(Autenticador.Entities.END_Endereco entity)
        {
            this.__STP_UPDATE = "NEW_END_Endereco_UPDATE";
            return base.Alterar(entity);
        }

        /// <summary>
        /// Parâmetros para efetuar a exclusão lógica.
        /// </summary>
        protected override void ParamDeletar(QueryStoredProcedure qs, Autenticador.Entities.END_Endereco entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@end_id";
            Param.Size = 16;
            Param.Value = entity.end_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@end_situacao";
            Param.Size = 1;
            Param.Value = 3;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@end_dataAlteracao";
            Param.Size = 8;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Método alterado para que o delete não faça exclusão física e sim lógica (update).
        /// </summary>
        /// <param name="entity"> Entidade END_Endereco</param>
        /// <returns>true = sucesso | false = fracasso</returns>        
        public override bool Delete(Autenticador.Entities.END_Endereco entity)
        {
            this.__STP_DELETE = "NEW_END_Endereco_Update_Situacao";
            return base.Delete(entity);            
        }

        /// <summary>
        /// Seleciona campo integridade do endereço
        /// </summary>
        /// <param name="ent_id">Id da tabela END_Endereco do bd</param>
        /// <returns>true = sucesso | false = fracasso</returns>    
        public int Select_Integridade
        (
            Guid end_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_END_Endereco_Select_Integridade", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@end_id";
                Param.Size = 16;
                if (end_id != Guid.Empty)
                    Param.Value = end_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                    return Convert.ToInt32(qs.Return.Rows[0]["end_integridade"].ToString());

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
        /// Incrementa uma unidade no campo integridade do endereço
        /// </summary>
        /// <param name="end_id">Id da tabela END_Endereco do bd</param>
        /// <returns>true = sucesso | false = fracasso</returns>    
        public bool Update_IncrementaIntegridade
        (
            Guid end_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_END_Endereco_INCREMENTA_INTEGRIDADE", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@end_id";
                Param.Size = 16;
                if (end_id != Guid.Empty)
                    Param.Value = end_id;
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
        /// Decrementa uma unidade no campo integridade do endereço
        /// </summary>
        /// <param name="ent_id">Id da tabela END_Endereco do bd</param>
        /// <returns>true = sucesso | false = fracasso</returns>    
        public bool Update_DecrementaIntegridade
        (
            Guid end_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_END_Endereco_DECREMENTA_INTEGRIDADE", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@end_id";
                Param.Size = 16;
                if (end_id != Guid.Empty)
                    Param.Value = end_id;
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
        /// Retorna true/false
        /// para saber se o endereço (valor) está sendo usada em alguma tabela no banco GestaoCore e nos bancos
        /// relacionados ao GestaoCore
        /// </summary>
        /// <param name="coluna">nome da coluna referente ao id do endereco no bd</param>
        /// <param name="coluna_valor">valor da coluna referente ao id do endereco no bd</param>        
        /// <returns>True - Caso esteja sendo usada/False - caso não exista registro com uso para este valor</returns>
        public bool Select_ColunaValor_BD
        (
            string coluna
            , long coluna_valor
            , string tabela_raiz
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
                Param.DbType = DbType.Int64;
                Param.ParameterName = "@valor";
                Param.Size = 8;
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
        /// para saber se o endereço (valor) está sendo usada em alguma tabela no banco GestaoCore e nos bancos
        /// relacionados ao GestaoCore
        /// </summary>
        /// <param name="coluna">nome da coluna referente ao id do endereco no bd</param>
        /// <param name="coluna_valor">valor da coluna referente ao id do endereco no bd</param>        
        /// <returns>True - Caso esteja sendo usada/False - caso não exista registro com uso para este valor</returns>
        public bool AssociarEnderecos
        (
            Guid end_id
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
                Param.Value = end_id;
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
    }
}
