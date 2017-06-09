using System;
using CoreLibrary.Business.Common;
using Autenticador.DAL;
using Autenticador.Entities;
using System.ComponentModel;
using System.Data;
using CoreLibrary.Data.Common;
using CoreLibrary.Validation.Exceptions;
using System.Collections.Generic;

namespace Autenticador.BLL
{
    public class PES_CertidaoCivilBO : BusinessBase<PES_CertidaoCivilDAO, PES_CertidaoCivil>
    {
        #region Enumerador

        public enum TipoCertidaoCivil
        {
            Nascimento = 1
            , Casamento
        }

        #endregion

        /// <summary>
        /// Retorna a entidade PES_CertidaoCivil  
        /// filtrado por pessoa e tipo da certidão
        /// </summary>
        /// <param name="pes_id">Id da pessoa</param>
        /// <param name="ctc_tipo">Tipo da certidão</param>
        /// <returns></returns>
        public static PES_CertidaoCivil SelecionaPorTipoCertidao(Guid pes_id, byte ctc_tipo, TalkDBTransaction banco)
        {
            PES_CertidaoCivilDAO dao = new PES_CertidaoCivilDAO();

            if (banco != null)
            {
                dao._Banco = banco;
            }

            return dao.SelecionaPorTipoCertidao(pes_id, ctc_tipo);
        }

        /// <summary>
        /// Retorna um datatable contendo todas as certidoes da pessoa
        /// que não foram excluídos logicamente, filtrados por 
        /// id da pessoa
        /// </summary>
        /// <param name="pes_id">Id da tabela PES_Pessoa do bd</param>
        /// <param name="paginado">Indica se o datatable será paginado ou não</param>
        /// <param name="currentPage">Página atual do gridview</param>
        /// <param name="pageSize">Total de registros por página</param> 
        /// <returns>DataTable com as certidoes da pessoa da pessoa</returns>
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
            PES_CertidaoCivilDAO dal = new PES_CertidaoCivilDAO();
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
        /// Retorna um list de entidade PES_CertidaoCivil contendo 
        /// todas as certidoes da pessoa que não foram excluídos 
        /// logicamente, filtrados pelo id da pessoa
        /// </summary>
        /// <param name="pes_id">Id da pessoa</param>
        /// <returns></returns>
        public static List<PES_CertidaoCivil> SelecionaPorPessoa(Guid pes_id)
        {
            PES_CertidaoCivilDAO dao = new PES_CertidaoCivilDAO();
            return dao.SelectBy_pes_id(pes_id);
        }

        /// <summary>
        /// Inclui uma nova certidão para a pessoa
        /// </summary>
        /// <param name="entity">Entidade PES_CertidaoCivil</param>        
        /// <param name="banco"></param>
        /// <returns>True = incluído/alterado | False = não incluído/alterado</returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public new static bool Save
        (
            PES_CertidaoCivil entity
            , TalkDBTransaction banco
        )
        {
            PES_CertidaoCivilDAO dal = new PES_CertidaoCivilDAO { _Banco = banco };

            try
            {
                if (entity.Validate())                
                    return dal.Salvar(entity);
                

                throw new ValidationException(entity.PropertiesErrorList[0].Message);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Salva os registros de certidão civil do DataTable para a pessoa passada.
        /// </summary>
        /// <param name="banco"></param>
        /// <param name="entity"></param>
        /// <param name="dtCertidao"></param>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void SaveCertidoesPessoa(TalkDBTransaction banco, PES_Pessoa entity, DataTable dtCertidao)
        {
            //Verifica se os dados da pessoa serão sempre salvos em maiúsculo.
            string sSalvarMaiusculo = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.SALVAR_SEMPRE_MAIUSCULO);
            bool Salvar_Sempre_Maiusculo = !string.IsNullOrEmpty(sSalvarMaiusculo) && Convert.ToBoolean(sSalvarMaiusculo);

            PES_CertidaoCivil entityCertidaoCivil = new PES_CertidaoCivil
            {
                pes_id = entity.pes_id
            };

            if (banco == null)
            {
                PES_CertidaoCivilDAO dao = new PES_CertidaoCivilDAO();
                banco = dao._Banco.CopyThisInstance();
                banco.Open(IsolationLevel.ReadCommitted);
            }

            for (int i = 0; i < dtCertidao.Rows.Count; i++)
            {
                if (dtCertidao.Rows[i].RowState != DataRowState.Deleted)
                {
                    if (dtCertidao.Rows[i].RowState == DataRowState.Added)
                    {
                        entityCertidaoCivil.ctc_id = new Guid(dtCertidao.Rows[i]["ctc_id"].ToString());
                        entityCertidaoCivil.ctc_tipo = Convert.ToByte(dtCertidao.Rows[i]["ctc_tipo"].ToString());
                        entityCertidaoCivil.ctc_numeroTermo = Salvar_Sempre_Maiusculo ? dtCertidao.Rows[i]["ctc_numeroTermo"].ToString().ToUpper() : dtCertidao.Rows[i]["ctc_numeroTermo"].ToString();
                        entityCertidaoCivil.ctc_folha = Salvar_Sempre_Maiusculo ? dtCertidao.Rows[i]["ctc_folha"].ToString().ToUpper() : dtCertidao.Rows[i]["ctc_folha"].ToString();
                        entityCertidaoCivil.ctc_livro = Salvar_Sempre_Maiusculo ? dtCertidao.Rows[i]["ctc_livro"].ToString().ToUpper() : dtCertidao.Rows[i]["ctc_livro"].ToString();
                        entityCertidaoCivil.ctc_nomeCartorio = Salvar_Sempre_Maiusculo ? dtCertidao.Rows[i]["ctc_nomeCartorio"].ToString().ToUpper() : dtCertidao.Rows[i]["ctc_nomeCartorio"].ToString();
                        entityCertidaoCivil.ctc_distritoCartorio = Salvar_Sempre_Maiusculo ? dtCertidao.Rows[i]["ctc_distritoCartorio"].ToString().ToUpper() : dtCertidao.Rows[i]["ctc_distritoCartorio"].ToString();
                        entityCertidaoCivil.cid_idCartorio = new Guid(dtCertidao.Rows[i]["cid_idCartorio"].ToString());
                        entityCertidaoCivil.unf_idCartorio = new Guid(dtCertidao.Rows[i]["unf_idCartorio"].ToString());
                        entityCertidaoCivil.ctc_dataEmissao = !string.IsNullOrEmpty(dtCertidao.Rows[i]["ctc_dataEmissao"].ToString()) ? Convert.ToDateTime(dtCertidao.Rows[i]["ctc_dataEmissao"].ToString()) : new DateTime();                        
                        entityCertidaoCivil.ctc_situacao = Convert.ToByte(1);
                        entityCertidaoCivil.ctc_matricula = Salvar_Sempre_Maiusculo ? dtCertidao.Rows[i]["ctc_matricula"].ToString().ToUpper() : dtCertidao.Rows[i]["ctc_matricula"].ToString();
                        entityCertidaoCivil.ctc_gemeo = Convert.ToBoolean(dtCertidao.Rows[i]["ctc_gemeo"]);
                        entityCertidaoCivil.ctc_modeloNovo = Convert.ToBoolean(dtCertidao.Rows[i]["ctc_modeloNovo"]);
                        entityCertidaoCivil.IsNew = true;
                        Save(entityCertidaoCivil, banco);

                        //Incrementa um na integridade da Cidade (se existir)
                        if (entityCertidaoCivil.cid_idCartorio != Guid.Empty)
                        {
                            END_CidadeDAO cidDao = new END_CidadeDAO { _Banco = banco };
                            cidDao.Update_IncrementaIntegridade(entityCertidaoCivil.cid_idCartorio);
                        }

                        //Incrementa um na integridade da Unidade Federativa (se existir)
                        if (entityCertidaoCivil.unf_idCartorio != Guid.Empty)
                        {
                            END_UnidadeFederativaDAO unfDAL = new END_UnidadeFederativaDAO { _Banco = banco };
                            unfDAL.Update_IncrementaIntegridade(entityCertidaoCivil.unf_idCartorio);
                        }
                    }
                    else if (dtCertidao.Rows[i].RowState == DataRowState.Modified)
                    {
                        entityCertidaoCivil.ctc_id = new Guid(dtCertidao.Rows[i]["ctc_id"].ToString());
                        entityCertidaoCivil.ctc_tipo = Convert.ToByte(dtCertidao.Rows[i]["ctc_tipo"].ToString());
                        entityCertidaoCivil.ctc_numeroTermo = Salvar_Sempre_Maiusculo ? dtCertidao.Rows[i]["ctc_numeroTermo"].ToString().ToUpper() : dtCertidao.Rows[i]["ctc_numeroTermo"].ToString();
                        entityCertidaoCivil.ctc_folha = Salvar_Sempre_Maiusculo ? dtCertidao.Rows[i]["ctc_folha"].ToString().ToUpper() : dtCertidao.Rows[i]["ctc_folha"].ToString();
                        entityCertidaoCivil.ctc_livro = Salvar_Sempre_Maiusculo ? dtCertidao.Rows[i]["ctc_livro"].ToString().ToUpper() : dtCertidao.Rows[i]["ctc_livro"].ToString();
                        entityCertidaoCivil.ctc_nomeCartorio = Salvar_Sempre_Maiusculo ? dtCertidao.Rows[i]["ctc_nomeCartorio"].ToString().ToUpper() : dtCertidao.Rows[i]["ctc_nomeCartorio"].ToString();
                        entityCertidaoCivil.ctc_distritoCartorio = Salvar_Sempre_Maiusculo ? dtCertidao.Rows[i]["ctc_distritoCartorio"].ToString().ToUpper() : dtCertidao.Rows[i]["ctc_distritoCartorio"].ToString();
                        entityCertidaoCivil.cid_idCartorio = new Guid(dtCertidao.Rows[i]["cid_idCartorio"].ToString());
                        entityCertidaoCivil.unf_idCartorio = new Guid(dtCertidao.Rows[i]["unf_idCartorio"].ToString());
                        entityCertidaoCivil.ctc_dataEmissao = !string.IsNullOrEmpty(dtCertidao.Rows[i]["ctc_dataEmissao"].ToString()) ? Convert.ToDateTime(dtCertidao.Rows[i]["ctc_dataEmissao"].ToString()) : new DateTime();                        
                        entityCertidaoCivil.ctc_situacao = Convert.ToByte(1);
                        entityCertidaoCivil.ctc_matricula = Salvar_Sempre_Maiusculo ? dtCertidao.Rows[i]["ctc_matricula"].ToString().ToUpper() : dtCertidao.Rows[i]["ctc_matricula"].ToString();
                        entityCertidaoCivil.ctc_gemeo = Convert.ToBoolean(dtCertidao.Rows[i]["ctc_gemeo"]);
                        entityCertidaoCivil.ctc_modeloNovo = Convert.ToBoolean(dtCertidao.Rows[i]["ctc_modeloNovo"]);
                        entityCertidaoCivil.IsNew = false;
                        Save(entityCertidaoCivil, banco);

                        if (new Guid(dtCertidao.Rows[i]["cid_idCartorio"].ToString()) != new Guid(dtCertidao.Rows[i]["cid_idAntigo"].ToString()))
                        {
                            END_CidadeDAO cidDao = new END_CidadeDAO { _Banco = banco };

                            //Decrementa um na integridade da Unidade Federativa anterior (se existia)
                            if (new Guid(dtCertidao.Rows[i]["cid_idAntigo"].ToString()) != Guid.Empty)
                                cidDao.Update_DecrementaIntegridade(new Guid(dtCertidao.Rows[i]["cid_idAntigo"].ToString()));

                            //Incrementa um na integridade da Unidade Federetiva atual (se existir)
                            if (new Guid(dtCertidao.Rows[i]["cid_idCartorio"].ToString()) != Guid.Empty)
                                cidDao.Update_IncrementaIntegridade(new Guid(dtCertidao.Rows[i]["cid_idCartorio"].ToString()));
                        }

                        if (new Guid(dtCertidao.Rows[i]["unf_idCartorio"].ToString()) != new Guid(dtCertidao.Rows[i]["unf_idAntigo"].ToString()))
                        {
                            END_UnidadeFederativaDAO unfDAL = new END_UnidadeFederativaDAO { _Banco = banco };

                            //Decrementa um na integridade da Unidade Federativa anterior (se existia)
                            if (new Guid(dtCertidao.Rows[i]["unf_idAntigo"].ToString()) != Guid.Empty)
                                unfDAL.Update_DecrementaIntegridade(new Guid(dtCertidao.Rows[i]["unf_idAntigo"].ToString()));

                            //Incrementa um na integridade da Unidade Federetiva atual (se existir)
                            if (new Guid(dtCertidao.Rows[i]["unf_idCartorio"].ToString()) != Guid.Empty)
                                unfDAL.Update_IncrementaIntegridade(new Guid(dtCertidao.Rows[i]["unf_idCartorio"].ToString()));
                        }
                    }
                }
                else
                {
                    entityCertidaoCivil.ctc_id = new Guid(dtCertidao.Rows[i]["ctc_id", DataRowVersion.Original].ToString());
                    entityCertidaoCivil.cid_idCartorio = new Guid(dtCertidao.Rows[i]["cid_idCartorio", DataRowVersion.Original].ToString());
                    entityCertidaoCivil.unf_idCartorio = new Guid(dtCertidao.Rows[i]["unf_idCartorio", DataRowVersion.Original].ToString());                    
                    PES_CertidaoCivilDAO pescerDAL = new PES_CertidaoCivilDAO { _Banco = banco };
                    pescerDAL.Delete(entityCertidaoCivil);

                    //Decrementa um na integridade da Cidade (se existia)
                    if (entityCertidaoCivil.cid_idCartorio != Guid.Empty)
                    {
                        END_CidadeDAO cidDao = new END_CidadeDAO { _Banco = banco };
                        cidDao.Update_DecrementaIntegridade(entityCertidaoCivil.cid_idCartorio);
                    }

                    //Decrementa um na integridade da Unidade Federativa (se existia)
                    if (entityCertidaoCivil.unf_idCartorio != Guid.Empty)
                    {
                        END_UnidadeFederativaDAO unfDAL = new END_UnidadeFederativaDAO { _Banco = banco };
                        unfDAL.Update_DecrementaIntegridade(entityCertidaoCivil.unf_idCartorio);
                    }
                }
            }
        }
    }
}
