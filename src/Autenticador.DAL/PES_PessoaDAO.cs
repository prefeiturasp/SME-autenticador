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
    public class PES_PessoaDAO : Abstract_PES_PessoaDAO
    {
        public DataTable SelectBy_Nome
        (
            string pes_nome
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_PES_Pessoa_SelectBy_Nome", this._Banco);
            try
            {
                #region Parâmetros

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@pes_nome";
                Param.Size = 200;
                Param.Value = pes_nome;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                return qs.Return;
            }
            catch
            {
                throw;
            }
        }

        public DataTable SelectBy_Busca(
            string pes_nome
            , DateTime pes_dataNascimento
            , string TIPO_DOCUMENTACAO_CPF
            , string TIPO_DOCUMENTACAO_RG
            , int currentPage
            , int pageSize
            , out int totalRecord)
        {
            DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_PES_Pessoa_SelectBy_Busca", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@pes_nome";
                Param.Size = 200;
                if (!String.IsNullOrEmpty(pes_nome))
                    Param.Value = pes_nome;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.DateTime;
                Param.ParameterName = "@pes_dataNascimento";
                Param.Size = 16;
                if (pes_dataNascimento != new DateTime())
                    Param.Value = pes_dataNascimento;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@TIPO_DOCUMENTACAO_RG";
                Param.Size = 50;
                if (!String.IsNullOrEmpty(TIPO_DOCUMENTACAO_RG))
                    Param.Value = TIPO_DOCUMENTACAO_RG;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@TIPO_DOCUMENTACAO_CPF";
                Param.Size = 50;
                if (!String.IsNullOrEmpty(TIPO_DOCUMENTACAO_CPF))
                    Param.Value = TIPO_DOCUMENTACAO_CPF;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion

                totalRecord = qs.Execute(currentPage / pageSize, pageSize);

                if (qs.Return.Rows.Count > 0)
                    dt = qs.Return;

                return dt;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna true/false
        /// para saber se a pessoa (valor) está sendo usada em alguma tabela no banco GestaoCore e nos bancos
        /// relacionados ao GestaoCore
        /// </summary>
        /// <param name="coluna">nome da coluna referente ao id da pessoa no bd</param>
        /// <param name="coluna_valor">valor da coluna referente ao id da pessoa no bd</param>        
        /// <returns>True - Caso esteja sendo usada/False - caso não exista registro com uso para este valor</returns>
        public bool AssociarPessoas
        (
            Guid pes_id
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
                Param.Value = pes_id;
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
        /// Parâmetros para efetuar a inclusão com data de criação e de alteração fixas
        /// </summary>
        protected override void ParamInserir(QuerySelectStoredProcedure qs, PES_Pessoa entity)
        {
            entity.pes_dataCriacao = DateTime.Now;
            entity.pes_dataAlteracao = DateTime.Now;
            entity.pes_id = Guid.NewGuid();

            base.ParamInserir(qs, entity);

            if (entity.pai_idNacionalidade == Guid.Empty)
                qs.Parameters["@pai_idNacionalidade"].Value = DBNull.Value;

            if (entity.cid_idNaturalidade == Guid.Empty)
                qs.Parameters["@cid_idNaturalidade"].Value = DBNull.Value;

            if (entity.pes_idFiliacaoPai == Guid.Empty)
                qs.Parameters["@pes_idFiliacaoPai"].Value = DBNull.Value;

            if (entity.pes_idFiliacaoMae == Guid.Empty)
                qs.Parameters["@pes_idFiliacaoMae"].Value = DBNull.Value;

            if (entity.tes_id == Guid.Empty)
                qs.Parameters["@tes_id"].Value = DBNull.Value;

            qs.Parameters["@pes_dataNascimento"].DbType = DbType.Date;
        }

        protected override bool Inserir(Autenticador.Entities.PES_Pessoa entity)
        {
            __STP_INSERT = "NEW_PES_Pessoa_INSERT";
            return base.Inserir(entity);
        }

        /// <summary>
        /// Override do método Carregar - Colocado WITH(NOLOCK) na procedure.
        /// </summary>
        public override bool Carregar(PES_Pessoa entity)
        {
            __STP_LOAD = "NEW_PES_Pessoa_LOAD";
            return base.Carregar(entity);
        }

        /// <summary>
        /// Parâmetros para efetuar a alteração preservando a data de criação e a integridade
        /// </summary>
        protected override void ParamAlterar(QueryStoredProcedure qs, PES_Pessoa entity)
        {
            entity.pes_dataAlteracao = DateTime.Now;

            base.ParamAlterar(qs, entity);

            qs.Parameters.RemoveAt("@pes_dataCriacao");
            qs.Parameters.RemoveAt("@pes_integridade");

            if (entity.pai_idNacionalidade == Guid.Empty)
                qs.Parameters["@pai_idNacionalidade"].Value = DBNull.Value;

            if (entity.cid_idNaturalidade == Guid.Empty)
                qs.Parameters["@cid_idNaturalidade"].Value = DBNull.Value;

            if (entity.pes_idFiliacaoPai == Guid.Empty)
                qs.Parameters["@pes_idFiliacaoPai"].Value = DBNull.Value;

            if (entity.pes_idFiliacaoMae == Guid.Empty)
                qs.Parameters["@pes_idFiliacaoMae"].Value = DBNull.Value;

            if (entity.tes_id == Guid.Empty)
                qs.Parameters["@tes_id"].Value = DBNull.Value;

            qs.Parameters["@pes_dataNascimento"].DbType = DbType.Date;
        }

        /// <summary>
        /// Método alterado para que o update não faça a alteração da data de criação e da integridade
        /// </summary>
        /// <param name="entity"> Entidade PES_Pessoa</param>
        /// <returns>true = sucesso | false = fracasso</returns>  
        protected override bool Alterar(PES_Pessoa entity)
        {
            this.__STP_UPDATE = "NEW_PES_Pessoa_UPDATE";
            return base.Alterar(entity);
        }

        /// <summary>
        /// Parâmetros para efetuar a exclusão lógica.
        /// </summary>
        protected override void ParamDeletar(QueryStoredProcedure qs, PES_Pessoa entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@pes_id";
            Param.Size = 16;
            Param.Value = entity.pes_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@pes_situacao";
            Param.Size = 1;
            Param.Value = 3;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@pes_dataAlteracao";
            Param.Size = 8;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Retorna o valor do campo pes_integridade para a pessoa informada.
        /// </summary>
        /// <param name="pes_id"></param>
        /// <returns></returns>
        public long Select_Integridade
        (
             Guid pes_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_PES_Pessoa_Select_Integridade", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@pes_id";
                Param.Size = 16;
                if (pes_id != Guid.Empty)
                    Param.Value = pes_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                    return Convert.ToInt64(qs.Return.Rows[0]["pes_integridade"].ToString());

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
        /// Incrementa uma unidade no campo integridade da pessoa
        /// </summary>
        /// <param name="pes_id">Id da tabela PES_Pessoa do bd</param>
        /// <returns>true = sucesso | false = fracasso</returns>    
        public bool Update_IncrementaIntegridade
        (
            Guid pes_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_PES_Pessoa_INCREMENTA_INTEGRIDADE", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@pes_id";
                Param.Size = 16;
                if (pes_id != Guid.Empty)
                    Param.Value = pes_id;
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
        /// Decrementa uma unidade no campo integridade da pessoa
        /// </summary>
        /// <param name="pes_id">Id da tabela PES_Pessoa do bd</param>
        /// <returns>true = sucesso | false = fracasso</returns>    
        public bool Update_DecrementaIntegridade
        (
            Guid pes_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_PES_Pessoa_DECREMENTA_INTEGRIDADE", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@pes_id";
                Param.Size = 16;
                if (pes_id != Guid.Empty)
                    Param.Value = pes_id;
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
        /// Método alterado para que o delete não faça exclusão física e sim lógica (update).
        /// </summary>
        /// <param name="entity"> Entidade PES_Pessoa</param>
        /// <returns>true = sucesso | false = fracasso</returns>        
        public override bool Delete(PES_Pessoa entity)
        {
            this.__STP_DELETE = "NEW_PES_Pessoa_Update_Situacao";
            return base.Delete(entity);
        }

        /// <summary>
        /// Retorna true/false
        /// para saber se a pessoa (valor) está sendo usada em alguma tabela no banco GestaoCore e nos bancos
        /// relacionados ao GestaoCore
        /// </summary>
        /// <param name="coluna">nome da coluna referente ao id da cidade no bd</param>
        /// <param name="coluna_valor">valor da coluna referente ao id da cidade no bd</param>        
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

        public PES_Pessoa SelectBy_Nome_Nascimento_Documento(string pes_nome, DateTime pes_dataNascimento, Guid? tdo_id = null, string psd_numero = null)
        {
            PES_Pessoa entity = new PES_Pessoa();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_PES_Pessoa_SelectBy_Nome_Nascimento_CPF", this._Banco);

            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@pes_nome";
                Param.Size = 200;
                Param.Value = pes_nome;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.DateTime;
                Param.ParameterName = "@pes_dataNascimento";
                Param.Value = pes_dataNascimento;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tdo_id";
                Param.Size = 16;
                if (tdo_id != null)
                    Param.Value = tdo_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@psd_numero";
                Param.Size = 50;
                if (!string.IsNullOrEmpty(psd_numero))
                    Param.Value = psd_numero;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                {
                    entity = this.DataRowToEntity(qs.Return.Rows[0], entity);
                }

                return entity;
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
