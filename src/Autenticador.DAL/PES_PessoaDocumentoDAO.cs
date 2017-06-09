using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreLibrary.Data.Common;
using Autenticador.DAL.Abstracts;

namespace Autenticador.DAL
{
    public class PES_PessoaDocumentoDAO : Abstract_PES_PessoaDocumentoDAO
    {
        /// <summary>
        /// Retorna um datatable contendo todos os documentos da pessoa
        /// que não foram excluídos logicamente, filtrados por 
        /// id da pessoa
        /// </summary>
        /// <param name="pes_id">Id da tabela PES_Pessoa do bd</param>        
        /// <param name="paginado">Indica se o datatable será paginado ou não</param>
        /// <param name="currentPage">Página atual do grid</param>
        /// <param name="pageSize">Total de registros por página do grid</param>
        /// <param name="totalRecord">Total de registros retornado na busca</param>
        /// <returns>DataTable com os documentos da pessoa</returns>
        public DataTable SelectBy_pes_id
        (
            Guid pes_id            
            , bool paginado
            , int currentPage
            , int pageSize
            , out int totalRecords
        )
        {
            DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_PES_PessoaDocumento_SelectBy_pes_id", this._Banco);
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
        /// Verifica se já existe o numero de documento do tipo TIPO_DOCUMENTACAO_CPF cadastrado para outra pessoa        
        /// filtrados por psd_numero, pes_id (diferente do informado)
        /// </summary>
        /// <param name="psd_numero">Campo psd_numero da tabela PES_PessoaDocumento do bd</param>                
        /// <param name="pes_id">Campo pes_id da tabela PES_PessoaDocumento do bd</param>                
        /// <returns>true ou false</returns>
        public bool SelectBy_psd_numero
        (
            string psd_numero
            , Guid pes_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_PES_PessoaDocumento_SelectBy_psd_numero", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@psd_numero";
                Param.Size = 200;
                if (!string.IsNullOrEmpty(psd_numero))
                    Param.Value = psd_numero;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

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
                    return true;
                else
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
        /// Retorna as pessoas encontradas que tenham o mesmo documento
        /// do tipo informado.
        /// </summary>
        /// <param name="psd_numero">Número do documento</param>
        /// <param name="tdo_id">Tipo de documento</param>
        /// <returns>DataTable com resultados</returns>
        public DataTable SelectBy_Documento
        (
            string psd_numero
            , Guid tdo_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_PES_PessoaDocumento_SelectBy_Documento", _Banco);

            #region PARAMETROS

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_numero";
            Param.Size = 50;
            Param.Value = psd_numero;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@tdo_id";
            Param.Size = 16;
            Param.Value = tdo_id;
            qs.Parameters.Add(Param);

            #endregion

            qs.Execute();

            return qs.Return;
        }

        /// <summary>
        /// Retorna a pessoa cadastrada para um determinado documento.        
        /// </summary>
        /// <param name="psd_numero">Campo psd_numero da tabela PES_PessoaDocumento do bd</param>                        
        /// <returns></returns>
        public Guid SelectBy_psd_numero
        (
            string psd_numero            
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_PES_PessoaDocumento_SelectBy_psd_numero", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@psd_numero";
                Param.Size = 200;
                if (!string.IsNullOrEmpty(psd_numero))
                    Param.Value = psd_numero;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@pes_id";
                Param.Size = 16;
                Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                return qs.Return.Rows.Count > 0 ? new Guid(qs.Return.Rows[0]["pes_id"].ToString()) : Guid.Empty;
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
        /// Verifica se já existe o tipo de documento cadastrado para a pessoa
        /// e excluido logicamente
        /// filtrados por pes_id, tdo_id
        /// </summary>
        /// <param name="pes_id">Campo pes_id da tabela PES_PessoaDocumento do bd</param>        
        /// <param name="tdo_id">Campo tdo_id da tabela PES_PessoaDocumento do bd</param>        
        /// <returns>true ou false</returns>
        public bool SelectBy_pes_id_tdo_id_excluido
        (
            Guid pes_id
            , Guid tdo_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_PES_PessoaDocumento_SelectBy_pes_id_tdo_id_excluido", this._Banco);
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

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tdo_id";
                Param.Size = 16;
                if (tdo_id != Guid.Empty)
                    Param.Value = tdo_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                    return true;
                else
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
        /// Parâmetros para efetuar a inclusão com data de criação e de alteração fixas
        /// </summary>
        protected override void ParamInserir(QuerySelectStoredProcedure qs, Autenticador.Entities.PES_PessoaDocumento entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@pes_id";
            Param.Size = 16;
            Param.Value = entity.pes_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@tdo_id";
            Param.Size = 16;
            Param.Value = entity.tdo_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_numero";
            Param.Size = 50;
            Param.Value = entity.psd_numero;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Date;
            Param.ParameterName = "@psd_dataEmissao";
            Param.Size = 20;
            if (entity.psd_dataEmissao != new DateTime())
                Param.Value = entity.psd_dataEmissao;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_orgaoEmissao";
            Param.Size = 200;
            if (!string.IsNullOrEmpty(entity.psd_orgaoEmissao))
                Param.Value = entity.psd_orgaoEmissao;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@unf_idEmissao";
            Param.Size = 16;
            if (entity.unf_idEmissao != Guid.Empty)
                Param.Value = entity.unf_idEmissao;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_infoComplementares";
            Param.Size = 1000;
            if (!string.IsNullOrEmpty(entity.psd_infoComplementares))
                Param.Value = entity.psd_infoComplementares;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@psd_situacao";
            Param.Size = 1;
            Param.Value = entity.psd_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@psd_dataCriacao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@psd_dataAlteracao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            //NOVOS CAMPOS
            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_categoria";
            Param.Size = 64;
            if (!string.IsNullOrEmpty(entity.psd_categoria))
            {
                Param.Value = entity.psd_categoria;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_classificacao";
            Param.Size = 64;
            if (!string.IsNullOrEmpty(entity.psd_classificacao))
            {
                Param.Value = entity.psd_classificacao;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_csm";
            Param.Size = 32;
            if (!string.IsNullOrEmpty(entity.psd_csm))
            {
                Param.Value = entity.psd_csm;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime2;
            Param.ParameterName = "@psd_dataEntrada";
            Param.Size = 20;
            if (entity.psd_dataEntrada != new DateTime())
            {
                Param.Value = entity.psd_dataEntrada;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime2;
            Param.ParameterName = "@psd_dataValidade";
            Param.Size = 20;
            if (entity.psd_dataValidade != new DateTime())
            {
                Param.Value = entity.psd_dataValidade;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@pai_idOrigem";
            Param.Size = 16;
            if (entity.pai_idOrigem != Guid.Empty)
                Param.Value = entity.pai_idOrigem;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_serie";
            Param.Size = 32;
            if (!string.IsNullOrEmpty(entity.psd_serie))
            {
                Param.Value = entity.psd_serie;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_tipoGuarda";
            Param.Size = 128;
            if (!string.IsNullOrEmpty(entity.psd_tipoGuarda))
            {
                Param.Value = entity.psd_tipoGuarda;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_via";
            Param.Size = 16;
            if (!string.IsNullOrEmpty(entity.psd_via))
            {
                Param.Value = entity.psd_via;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_secao";
            Param.Size = 32;
            if (!string.IsNullOrEmpty(entity.psd_secao))
            {
                Param.Value = entity.psd_secao;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_zona";
            Param.Size = 16;
            if (!string.IsNullOrEmpty(entity.psd_zona))
            {
                Param.Value = entity.psd_zona;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_regiaoMilitar";
            Param.Size = 16;
            if (!string.IsNullOrEmpty(entity.psd_regiaoMilitar))
            {
                Param.Value = entity.psd_regiaoMilitar;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_numeroRA";
            Param.Size = 64;
            if (!string.IsNullOrEmpty(entity.psd_numeroRA))
            {
                Param.Value = entity.psd_numeroRA;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Date;
            Param.ParameterName = "@psd_dataExpedicao";
            Param.Size = 20;
            if (entity.psd_dataExpedicao != new DateTime())
                Param.Value = entity.psd_dataExpedicao;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Parâmetros para efetuar a alteração preservando a data de criação
        /// </summary>
        protected override void ParamAlterar(QueryStoredProcedure qs, Autenticador.Entities.PES_PessoaDocumento entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@pes_id";
            Param.Size = 16;
            Param.Value = entity.pes_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@tdo_id";
            Param.Size = 16;
            Param.Value = entity.tdo_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_numero";
            Param.Size = 50;
            Param.Value = entity.psd_numero;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@psd_dataEmissao";
            Param.Size = 20;
            if (entity.psd_dataEmissao != new DateTime())
                Param.Value = entity.psd_dataEmissao;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_orgaoEmissao";
            Param.Size = 200;
            if (!string.IsNullOrEmpty(entity.psd_orgaoEmissao))
                Param.Value = entity.psd_orgaoEmissao;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@unf_idEmissao";
            Param.Size = 16;
            if (entity.unf_idEmissao != Guid.Empty)
                Param.Value = entity.unf_idEmissao;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_infoComplementares";
            Param.Size = 1000;
            if (!string.IsNullOrEmpty(entity.psd_infoComplementares))
                Param.Value = entity.psd_infoComplementares;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@psd_situacao";
            Param.Size = 1;
            Param.Value = entity.psd_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@psd_dataAlteracao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            //NOVOS CAMPOS
            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_categoria";
            Param.Size = 64;
            if (!string.IsNullOrEmpty(entity.psd_categoria))
            {
                Param.Value = entity.psd_categoria;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_classificacao";
            Param.Size = 64;
            if (!string.IsNullOrEmpty(entity.psd_classificacao))
            {
                Param.Value = entity.psd_classificacao;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_csm";
            Param.Size = 32;
            if (!string.IsNullOrEmpty(entity.psd_csm))
            {
                Param.Value = entity.psd_csm;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime2;
            Param.ParameterName = "@psd_dataEntrada";
            Param.Size = 20;
            if (entity.psd_dataEntrada != new DateTime())
            {
                Param.Value = entity.psd_dataEntrada;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime2;
            Param.ParameterName = "@psd_dataValidade";
            Param.Size = 20;
            if (entity.psd_dataValidade != new DateTime())
            {
                Param.Value = entity.psd_dataValidade;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@pai_idOrigem";
            Param.Size = 16;
            Param.Value = entity.pai_idOrigem;
            if (entity.pai_idOrigem != Guid.Empty)
                Param.Value = entity.pai_idOrigem;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_serie";
            Param.Size = 32;
            if (!string.IsNullOrEmpty(entity.psd_serie))
            {
                Param.Value = entity.psd_serie;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_tipoGuarda";
            Param.Size = 128;
            if (!string.IsNullOrEmpty(entity.psd_tipoGuarda))
            {
                Param.Value = entity.psd_tipoGuarda;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_via";
            Param.Size = 16;
            if (!string.IsNullOrEmpty(entity.psd_via))
            {
                Param.Value = entity.psd_via;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_secao";
            Param.Size = 32;
            if (!string.IsNullOrEmpty(entity.psd_secao))
            {
                Param.Value = entity.psd_secao;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_zona";
            Param.Size = 16;
            if (!string.IsNullOrEmpty(entity.psd_zona))
            {
                Param.Value = entity.psd_zona;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_regiaoMilitar";
            Param.Size = 16;
            if (!string.IsNullOrEmpty(entity.psd_regiaoMilitar))
            {
                Param.Value = entity.psd_regiaoMilitar;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_numeroRA";
            Param.Size = 64;
            if (!string.IsNullOrEmpty(entity.psd_numeroRA))
            {
                Param.Value = entity.psd_numeroRA;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Date;
            Param.ParameterName = "@psd_dataExpedicao";
            Param.Size = 20;
            if (entity.psd_dataExpedicao != new DateTime())
                Param.Value = entity.psd_dataExpedicao;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Método alterado para que o update não faça a alteração da data de criação
        /// </summary>
        /// <param name="entity"> Entidade PES_PessoaDocumento</param>
        /// <returns>true = sucesso | false = fracasso</returns>  
        protected override bool Alterar(Autenticador.Entities.PES_PessoaDocumento entity)
        {
            this.__STP_UPDATE = "NEW_PES_PessoaDocumento_UPDATE";
            return base.Alterar(entity);
        }

        /// <summary>
        /// Parâmetros para efetuar a exclusão lógica.
        /// </summary>
        protected override void ParamDeletar(QueryStoredProcedure qs, Autenticador.Entities.PES_PessoaDocumento entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@pes_id";
            Param.Size = 16;
            Param.Value = entity.pes_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@tdo_id";
            Param.Size = 16;
            Param.Value = entity.tdo_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@psd_situacao";
            Param.Size = 1;
            Param.Value = 3;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@psd_dataAlteracao";
            Param.Size = 8;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Método alterado para que o delete não faça exclusão física e sim lógica (update).
        /// </summary>
        /// <param name="entity"> Entidade PES_PessoaDocumento</param>
        /// <returns>true = sucesso | false = fracasso</returns>        
        public override bool Delete(Autenticador.Entities.PES_PessoaDocumento entity)
        {
            this.__STP_DELETE = "NEW_PES_PessoaDocumento_Update_Situacao";
            return base.Delete(entity);
        }

        /// <summary>
        /// Recebe o valor do auto incremento e coloca na propriedade 
        /// </summary>
        /// <param name="qs">Objeto da Store Procedure</param>
        protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, Autenticador.Entities.PES_PessoaDocumento entity)
        {
            return true;
        }	
    }
}
