using System;
using System.Data;
using CoreLibrary.Business.Common;
using Autenticador.DAL;
using Autenticador.Entities;
using System.ComponentModel;
using CoreLibrary.Data.Common;
using CoreLibrary.Validation.Exceptions;

namespace Autenticador.BLL
{
    public class PES_PessoaDocumentoBO : BusinessBase<PES_PessoaDocumentoDAO, PES_PessoaDocumento>        
    {
        /// <summary>
        /// Retorna um datatable contendo todos os documentos da pessoa
        /// que não foram excluídos logicamente, filtrados por 
        /// id da pessoa
        /// </summary>
        /// <param name="pes_id">Id da tabela PES_Pessoa do bd</param>
        /// <param name="paginado">Indica se o datatable será paginado ou não</param>
        /// <param name="currentPage">Página atual do gridview</param>
        /// <param name="pageSize">Total de registros por página</param> 
        /// <returns>DataTable com os documentos da pessoa</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelect
        (
            Guid pes_id
            , bool paginado
            , int currentPage
            , int pageSize
        )
        {
            if (pageSize == 0)
                pageSize = 1;

            totalRecords = 0;
            PES_PessoaDocumentoDAO dal = new PES_PessoaDocumentoDAO();
            try
            {
                return dal.SelectBy_pes_id(pes_id, paginado, currentPage / pageSize, pageSize, out totalRecords);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Verifica se já existe o numero de documento do tipo TIPO_DOCUMENTACAO_CPF cadastrado para outra pessoa        
        /// filtrados por psd_numero, pes_id (diferente do informado)
        /// </summary>
        /// <param name="psd_numero">Campo psd_numero da tabela PES_PessoaDocumento do bd</param>                
        /// <param name="pes_id">Campo pes_id da tabela PES_PessoaDocumento do bd</param>                
        /// <returns>true ou false</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static bool VerificaDocumentoExistente
        (
            string psd_numero
            , Guid pes_id
        )
        {
            PES_PessoaDocumentoDAO dal = new PES_PessoaDocumentoDAO();
            try
            {
                return dal.SelectBy_psd_numero(psd_numero, pes_id);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna as pessoas encontradas que tenham o mesmo documento
        /// do tipo informado.
        /// </summary>
        /// <param name="psd_numero">Número do documento</param>
        /// <param name="tdo_id">Tipo de documento</param>
        /// <returns>DataTable com resultados</returns>
        public static PES_PessoaDocumento GetEntityBy_Documento
        (
            string psd_numero
            , Guid tdo_id
        )
        {
            PES_PessoaDocumento entity = new PES_PessoaDocumento();

            PES_PessoaDocumentoDAO dao = new PES_PessoaDocumentoDAO();
            DataTable dt = dao.SelectBy_Documento(psd_numero, tdo_id);

            if (dt.Rows.Count > 0)
            {
                // Carregar entidade com a primeira linha do DataTable.
                entity = dao.DataRowToEntity(dt.Rows[0], entity);
            }

            return entity;
        }

        /// <summary>
        /// Retorna a pessoa cadastrada para um determinado documento.            
        /// </summary>
        /// <param name="psd_numero">Campo psd_numero da tabela PES_PessoaDocumento do bd</param>                        
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Guid VerificaPessoaDocumentoExistente
        (
            string psd_numero            
        )
        {
            PES_PessoaDocumentoDAO dal = new PES_PessoaDocumentoDAO();
            try
            {
                return dal.SelectBy_psd_numero(psd_numero);
            }
            catch
            {
                throw;
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
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static bool VerificaTipoDocumentoExistente
        (
            Guid pes_id
            , Guid tdo_id
        )
        {
            PES_PessoaDocumentoDAO dal = new PES_PessoaDocumentoDAO();
            try
            {
                return dal.SelectBy_pes_id_tdo_id_excluido(pes_id, tdo_id);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Inclui um novo documento para a pessoa
        /// </summary>
        /// <param name="entity">Entidade PES_PessoaDocumento</param>        
        /// <param name="banco"></param>
        /// <returns>True = incluído/alterado | False = não incluído/alterado</returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public new static bool Save
        (
            PES_PessoaDocumento entity
            , TalkDBTransaction banco
        )
        {
            PES_PessoaDocumentoDAO dal = new PES_PessoaDocumentoDAO { _Banco = banco };

            try
            {
                if (entity.Validate())
                {
                    dal.Salvar(entity);
                }
                else
                {
                    throw new ValidationException(entity.PropertiesErrorList[0].Message);
                }

                return true;
            }
            catch
            {
                throw;
            }         
        }

        /// <summary>
        /// Salva os registros de documento do DataTable passado para a pessoa passada.
        /// </summary>
        /// <param name="banco"></param>
        /// <param name="entity"></param>
        /// <param name="dtDocumento"></param>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void SaveDocumentosPessoa(TalkDBTransaction banco, PES_Pessoa entity, DataTable dtDocumento)
        {
            //Verifica se os dados da pessoa serão sempre salvos em maiúsculo.
            string sSalvarMaiusculo = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.SALVAR_SEMPRE_MAIUSCULO);
            bool Salvar_Sempre_Maiusculo = !string.IsNullOrEmpty(sSalvarMaiusculo) && Convert.ToBoolean(sSalvarMaiusculo);

            PES_PessoaDocumento entityDocumento = new PES_PessoaDocumento
            {
                pes_id = entity.pes_id
            };

            if (banco == null)
            {
                PES_PessoaDocumentoDAO dao = new PES_PessoaDocumentoDAO();
                banco = dao._Banco.CopyThisInstance();
                banco.Open(IsolationLevel.ReadCommitted);
            }

            for (int i = 0; i < dtDocumento.Rows.Count; i++)
            {
                if (dtDocumento.Rows[i].RowState != DataRowState.Deleted)
                {
                    if (dtDocumento.Rows[i].RowState == DataRowState.Added)
                    {
                        entityDocumento.tdo_id = new Guid(dtDocumento.Rows[i]["tdo_id"].ToString());
                        entityDocumento.unf_idEmissao = new Guid(dtDocumento.Rows[i]["unf_idEmissao"].ToString());
                        entityDocumento.psd_numero = Salvar_Sempre_Maiusculo ? dtDocumento.Rows[i]["numero"].ToString().ToUpper() : dtDocumento.Rows[i]["numero"].ToString();
                        entityDocumento.psd_dataEmissao = string.IsNullOrEmpty(dtDocumento.Rows[i]["dataemissao"].ToString()) ? new DateTime() : Convert.ToDateTime(dtDocumento.Rows[i]["dataemissao"].ToString());
                        entityDocumento.psd_orgaoEmissao = Salvar_Sempre_Maiusculo ? dtDocumento.Rows[i]["orgaoemissao"].ToString().ToUpper() : dtDocumento.Rows[i]["orgaoemissao"].ToString();
                        entityDocumento.psd_infoComplementares = Salvar_Sempre_Maiusculo ? dtDocumento.Rows[i]["info"].ToString().ToUpper() : dtDocumento.Rows[i]["info"].ToString();
                        entityDocumento.psd_situacao = Convert.ToByte(1);

                        //NOVOS CAMPOS
                        entityDocumento.psd_categoria = Salvar_Sempre_Maiusculo ? dtDocumento.Rows[i]["categoria"].ToString().ToUpper() : dtDocumento.Rows[i]["categoria"].ToString();
                        entityDocumento.psd_classificacao = Salvar_Sempre_Maiusculo ? dtDocumento.Rows[i]["classificacao"].ToString().ToUpper() : dtDocumento.Rows[i]["classificacao"].ToString();
                        entityDocumento.psd_csm = Salvar_Sempre_Maiusculo ? dtDocumento.Rows[i]["csm"].ToString().ToUpper() : dtDocumento.Rows[i]["csm"].ToString();
                        entityDocumento.psd_dataEntrada = string.IsNullOrEmpty(dtDocumento.Rows[i]["dataEntrada"].ToString()) ? new DateTime() : Convert.ToDateTime(dtDocumento.Rows[i]["dataEntrada"].ToString());
                        entityDocumento.psd_dataValidade = string.IsNullOrEmpty(dtDocumento.Rows[i]["dataValidade"].ToString()) ? new DateTime() : Convert.ToDateTime(dtDocumento.Rows[i]["dataValidade"].ToString());
                        entityDocumento.pai_idOrigem = string.IsNullOrEmpty(dtDocumento.Rows[i]["pai_idOrigem"].ToString()) ? Guid.Empty : new Guid(dtDocumento.Rows[i]["pai_idOrigem"].ToString());
                        entityDocumento.psd_serie = Salvar_Sempre_Maiusculo ? dtDocumento.Rows[i]["serie"].ToString().ToUpper() : dtDocumento.Rows[i]["serie"].ToString();
                        entityDocumento.psd_tipoGuarda = Salvar_Sempre_Maiusculo ? dtDocumento.Rows[i]["tipoGuarda"].ToString().ToUpper() : dtDocumento.Rows[i]["tipoGuarda"].ToString();
                        entityDocumento.psd_via = Salvar_Sempre_Maiusculo ? dtDocumento.Rows[i]["via"].ToString().ToUpper() : dtDocumento.Rows[i]["via"].ToString();
                        entityDocumento.psd_secao = Salvar_Sempre_Maiusculo ? dtDocumento.Rows[i]["secao"].ToString().ToUpper() : dtDocumento.Rows[i]["secao"].ToString();
                        entityDocumento.psd_zona = Salvar_Sempre_Maiusculo ? dtDocumento.Rows[i]["zona"].ToString().ToUpper() : dtDocumento.Rows[i]["zona"].ToString();
                        entityDocumento.psd_dataExpedicao = string.IsNullOrEmpty(dtDocumento.Rows[i]["dataexpedicao"].ToString()) ? new DateTime() : Convert.ToDateTime(dtDocumento.Rows[i]["dataexpedicao"].ToString());
                        entityDocumento.psd_regiaoMilitar = Salvar_Sempre_Maiusculo ? dtDocumento.Rows[i]["regiaoMilitar"].ToString().ToUpper() : dtDocumento.Rows[i]["regiaoMilitar"].ToString();
                        entityDocumento.psd_numeroRA = Salvar_Sempre_Maiusculo ? dtDocumento.Rows[i]["numeroRA"].ToString().ToUpper() : dtDocumento.Rows[i]["numeroRA"].ToString();

                        entityDocumento.IsNew = !VerificaTipoDocumentoExistente(entityDocumento.pes_id, entityDocumento.tdo_id);

                        Save(entityDocumento, banco);

                        //Incrementa um na integridade do tipo de documento
                        SYS_TipoDocumentacaoDAO tipoDAL = new SYS_TipoDocumentacaoDAO { _Banco = banco };
                        tipoDAL.Update_IncrementaIntegridade(entityDocumento.tdo_id);

                        //Incrementa um na integridade da Unidade Federativa (se existir)
                        if (entityDocumento.unf_idEmissao != Guid.Empty)
                        {
                            END_UnidadeFederativaDAO unfDAL = new END_UnidadeFederativaDAO { _Banco = banco };
                            unfDAL.Update_IncrementaIntegridade(entityDocumento.unf_idEmissao);
                        }
                    }
                    else if (dtDocumento.Rows[i].RowState == DataRowState.Modified)
                    {
                        entityDocumento.tdo_id = new Guid(dtDocumento.Rows[i]["tdo_id"].ToString());
                        entityDocumento.unf_idEmissao = new Guid(dtDocumento.Rows[i]["unf_idEmissao"].ToString());
                        entityDocumento.psd_numero = Salvar_Sempre_Maiusculo ? dtDocumento.Rows[i]["numero"].ToString().ToUpper() : dtDocumento.Rows[i]["numero"].ToString();
                        entityDocumento.psd_dataEmissao = string.IsNullOrEmpty(dtDocumento.Rows[i]["dataemissao"].ToString()) ? new DateTime() : Convert.ToDateTime(dtDocumento.Rows[i]["dataemissao"].ToString());
                        entityDocumento.psd_orgaoEmissao = Salvar_Sempre_Maiusculo ? dtDocumento.Rows[i]["orgaoemissao"].ToString().ToUpper() : dtDocumento.Rows[i]["orgaoemissao"].ToString();
                        entityDocumento.psd_infoComplementares = Salvar_Sempre_Maiusculo ? dtDocumento.Rows[i]["info"].ToString().ToUpper() : dtDocumento.Rows[i]["info"].ToString();
                        entityDocumento.psd_situacao = Convert.ToByte(1);

                        //NOVOS CAMPOS
                        entityDocumento.psd_categoria = Salvar_Sempre_Maiusculo ? dtDocumento.Rows[i]["categoria"].ToString().ToUpper() : dtDocumento.Rows[i]["categoria"].ToString();
                        entityDocumento.psd_classificacao = Salvar_Sempre_Maiusculo ? dtDocumento.Rows[i]["classificacao"].ToString().ToUpper() : dtDocumento.Rows[i]["classificacao"].ToString();
                        entityDocumento.psd_csm = Salvar_Sempre_Maiusculo ? dtDocumento.Rows[i]["csm"].ToString().ToUpper() : dtDocumento.Rows[i]["csm"].ToString();
                        entityDocumento.psd_dataEntrada = string.IsNullOrEmpty(dtDocumento.Rows[i]["dataEntrada"].ToString()) ? new DateTime() : Convert.ToDateTime(dtDocumento.Rows[i]["dataEntrada"].ToString());
                        entityDocumento.psd_dataValidade = string.IsNullOrEmpty(dtDocumento.Rows[i]["dataValidade"].ToString()) ? new DateTime() : Convert.ToDateTime(dtDocumento.Rows[i]["dataValidade"].ToString());
                        entityDocumento.pai_idOrigem = string.IsNullOrEmpty(dtDocumento.Rows[i]["pai_idOrigem"].ToString()) ? Guid.Empty : new Guid(dtDocumento.Rows[i]["pai_idOrigem"].ToString());
                        entityDocumento.psd_serie = Salvar_Sempre_Maiusculo ? dtDocumento.Rows[i]["serie"].ToString().ToUpper() : dtDocumento.Rows[i]["serie"].ToString();
                        entityDocumento.psd_tipoGuarda = Salvar_Sempre_Maiusculo ? dtDocumento.Rows[i]["tipoGuarda"].ToString().ToUpper() : dtDocumento.Rows[i]["tipoGuarda"].ToString();
                        entityDocumento.psd_via = Salvar_Sempre_Maiusculo ? dtDocumento.Rows[i]["via"].ToString().ToUpper() : dtDocumento.Rows[i]["via"].ToString();
                        entityDocumento.psd_secao = Salvar_Sempre_Maiusculo ? dtDocumento.Rows[i]["secao"].ToString().ToUpper() : dtDocumento.Rows[i]["secao"].ToString();
                        entityDocumento.psd_zona = Salvar_Sempre_Maiusculo ? dtDocumento.Rows[i]["zona"].ToString().ToUpper() : dtDocumento.Rows[i]["zona"].ToString();
                        entityDocumento.psd_dataExpedicao = string.IsNullOrEmpty(dtDocumento.Rows[i]["dataexpedicao"].ToString()) ? new DateTime() : Convert.ToDateTime(dtDocumento.Rows[i]["dataexpedicao"].ToString());
                        entityDocumento.psd_regiaoMilitar = Salvar_Sempre_Maiusculo ? dtDocumento.Rows[i]["regiaoMilitar"].ToString().ToUpper() : dtDocumento.Rows[i]["regiaoMilitar"].ToString();
                        entityDocumento.psd_numeroRA = Salvar_Sempre_Maiusculo ? dtDocumento.Rows[i]["numeroRA"].ToString().ToUpper() : dtDocumento.Rows[i]["numeroRA"].ToString();

                        entityDocumento.tdo_id = new Guid(dtDocumento.Rows[i]["tdo_id"].ToString());
                        entityDocumento.IsNew = false;
                        Save(entityDocumento, banco);

                        if (new Guid(dtDocumento.Rows[i]["unf_idEmissao"].ToString()) != new Guid(dtDocumento.Rows[i]["unf_idAntigo"].ToString()))
                        {
                            END_UnidadeFederativaDAO unfDAL = new END_UnidadeFederativaDAO { _Banco = banco };

                            //Decrementa um na integridade da Unidade Federativa anterior (se existia)
                            if (new Guid(dtDocumento.Rows[i]["unf_idAntigo"].ToString()) != Guid.Empty)
                                unfDAL.Update_DecrementaIntegridade(new Guid(dtDocumento.Rows[i]["unf_idAntigo"].ToString()));

                            //Incrementa um na integridade da Unidade Federetiva atual (se existir)
                            if (new Guid(dtDocumento.Rows[i]["unf_idEmissao"].ToString()) != Guid.Empty)
                                unfDAL.Update_IncrementaIntegridade(new Guid(dtDocumento.Rows[i]["unf_idEmissao"].ToString()));
                        }
                    }
                }
                else
                {
                    entityDocumento.tdo_id = new Guid(dtDocumento.Rows[i]["tdo_id", DataRowVersion.Original].ToString());
                    entityDocumento.unf_idEmissao = new Guid(dtDocumento.Rows[i]["unf_idEmissao", DataRowVersion.Original].ToString());
                    PES_PessoaDocumentoDAO pesdocDAL = new PES_PessoaDocumentoDAO { _Banco = banco };
                    pesdocDAL.Delete(entityDocumento);

                    //Decrementa um na integridade do tipo de documento
                    SYS_TipoDocumentacaoDAO tipoDAL = new SYS_TipoDocumentacaoDAO { _Banco = banco };
                    tipoDAL.Update_DecrementaIntegridade(entityDocumento.tdo_id);

                    //Decrementa um na integridade da Unidade Federativa (se existia)
                    if (entityDocumento.unf_idEmissao != Guid.Empty)
                    {
                        END_UnidadeFederativaDAO unfDAL = new END_UnidadeFederativaDAO { _Banco = banco };
                        unfDAL.Update_DecrementaIntegridade(entityDocumento.unf_idEmissao);
                    }
                }
            }
        }
    }
}
