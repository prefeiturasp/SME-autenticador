using System;
using System.Data;
using CoreLibrary.Business.Common;
using Autenticador.DAL;
using Autenticador.Entities;
using System.ComponentModel;
using CoreLibrary.Data.Common;
using CoreLibrary.Validation.Exceptions;
using System.Collections.Generic;

namespace Autenticador.BLL
{
    public class PES_PessoaEnderecoBO : BusinessBase<PES_PessoaEnderecoDAO, PES_PessoaEndereco>
    {
        /// <summary>
        /// Retorna um datatable contendo todos os endereços da pessoa
        /// que não foram excluídos logicamente, filtrados por 
        /// id da pessoa
        /// </summary>
        /// <param name="pes_id">Id da tabela PES_Pessoa do bd</param>
        /// <param name="paginado">Indica se o datatable será paginado ou não</param>
        /// <param name="currentPage">Página atual do gridview</param>
        /// <param name="pageSize">Total de registros por página</param> 
        /// <returns>DataTable com os endereços da pessoa</returns>
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
            PES_PessoaEnderecoDAO dal = new PES_PessoaEnderecoDAO();
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
        ///  Carrega Pes_PessoaEndereco 
        /// </summary>
        /// <param name="pes_id"></param>
        /// <returns>DataTable</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable CarregaEnderecos_Pessoa(Guid pes_id)
        {

            PES_PessoaEnderecoDAO dal = new PES_PessoaEnderecoDAO();
            try
            {
                return dal.CarregaEnderecos_Pessoa(pes_id);
            }
            catch
            {
                throw;
            }
        }

        public static DataTable GetEntity_ByPessoa
        (
            PES_PessoaEndereco entity
        )
        {
            totalRecords = 0;
            PES_PessoaEnderecoDAO dal = new PES_PessoaEnderecoDAO();
            try
            {
                return dal.SelectBy_pes_id(entity.pes_id, false, 0 / 1, 1, out totalRecords);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Inclui um novo endereço para a pessoa
        /// </summary>
        /// <param name="entity">Entidade PES_PessoaEndereco</param>        
        /// <param name="banco"></param>
        /// <returns>True = incluído/alterado | False = não incluído/alterado</returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public new static bool Save
        (
            PES_PessoaEndereco entity
            , TalkDBTransaction banco
        )
        {
            PES_PessoaEnderecoDAO dal = new PES_PessoaEnderecoDAO { _Banco = banco };

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
        /// Salva os registros de endereço na tabela passada, para a entidade Pessoa.
        /// </summary>
        /// <param name="banco"></param>
        /// <param name="entity"></param>
        /// <param name="dtEndereco"></param>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void SaveEnderecosPessoa(TalkDBTransaction banco, PES_Pessoa entity, DataTable dtEndereco)
        {
            //Verifica se os dados da pessoa serão sempre salvos em maiúsculo.
            string sSalvarMaiusculo = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.SALVAR_SEMPRE_MAIUSCULO);
            bool Salvar_Sempre_Maiusculo = !string.IsNullOrEmpty(sSalvarMaiusculo) && Convert.ToBoolean(sSalvarMaiusculo);

            PES_PessoaEndereco entityEndereco = new PES_PessoaEndereco
            {
                pes_id = entity.pes_id
            };

            if (banco == null)
            {
                PES_PessoaEnderecoDAO dao = new PES_PessoaEnderecoDAO();
                banco = dao._Banco.CopyThisInstance();
                banco.Open(IsolationLevel.ReadCommitted);
            }


            for (int i = 0; i < dtEndereco.Rows.Count; i++)
            {

                // if (dtEndereco.Rows[i].RowState != DataRowState.Deleted)
                if (!Convert.ToBoolean(dtEndereco.Rows[i]["excluido"].ToString()))
                {

                    string end_id = dtEndereco.Rows[i]["end_id"].ToString();

                    if((String.IsNullOrEmpty(end_id)) || (end_id.Equals(Guid.Empty.ToString())))
                    {
                        END_Endereco entityNovoEndereco = new END_Endereco
                        {
                            //[OLD]end_id = new Guid(dtEndereco.Rows[i]["end_id"].ToString())
                            end_cep = dtEndereco.Rows[i]["end_cep"].ToString()
                                ,
                            end_logradouro = Salvar_Sempre_Maiusculo ? dtEndereco.Rows[i]["end_logradouro"].ToString().ToUpper() : dtEndereco.Rows[i]["end_logradouro"].ToString()
                                ,
                            end_distrito = Salvar_Sempre_Maiusculo ? dtEndereco.Rows[i]["end_distrito"].ToString().ToUpper() : dtEndereco.Rows[i]["end_distrito"].ToString()
                                ,
                            end_zona = dtEndereco.Rows[i]["end_zona"].ToString() == "0" || string.IsNullOrEmpty(dtEndereco.Rows[i]["end_zona"].ToString()) ? Convert.ToByte(0) : Convert.ToByte(dtEndereco.Rows[i]["end_zona"].ToString())
                                ,
                            end_bairro = Salvar_Sempre_Maiusculo ? dtEndereco.Rows[i]["end_bairro"].ToString().ToUpper() : dtEndereco.Rows[i]["end_bairro"].ToString()
                                ,
                            cid_id = new Guid(dtEndereco.Rows[i]["cid_id"].ToString())
                                ,
                            end_situacao = Convert.ToByte(1)
                        };

                        //Inclui dados na tabela END_Endereco (se necessário)
                        if (entityNovoEndereco.end_id == Guid.Empty)
                        {
                            entityEndereco.end_id = END_EnderecoBO.Save(entityNovoEndereco, Guid.Empty, banco);
                            entityNovoEndereco.end_id = entityEndereco.end_id;
                        }

                        dtEndereco.Rows[i]["end_id"] = entityNovoEndereco.end_id;
                    }
                    string endRel_id = dtEndereco.Rows[i]["endRel_id"].ToString();

                    if (dtEndereco.Rows[i].RowState == DataRowState.Added || string.IsNullOrEmpty(endRel_id))
                    {
                        //TRATA DECIMAL
                        decimal latitude = string.IsNullOrEmpty(dtEndereco.Rows[i]["latitude"].ToString()) ? 0 : decimal.Parse(dtEndereco.Rows[i]["latitude"].ToString());
                        decimal longitude = string.IsNullOrEmpty(dtEndereco.Rows[i]["longitude"].ToString()) ? 0 : decimal.Parse(dtEndereco.Rows[i]["longitude"].ToString());

                        //ATRIBUI VALORES
                        entityEndereco.end_id = new Guid(dtEndereco.Rows[i]["end_id"].ToString());
                        entityEndereco.pse_numero = Salvar_Sempre_Maiusculo ? dtEndereco.Rows[i]["numero"].ToString().ToUpper() : dtEndereco.Rows[i]["numero"].ToString();
                        entityEndereco.pse_complemento = Salvar_Sempre_Maiusculo ? dtEndereco.Rows[i]["complemento"].ToString().ToUpper() : dtEndereco.Rows[i]["complemento"].ToString();
                        entityEndereco.pse_situacao = Convert.ToByte(1);
                        entityEndereco.pse_id = new Guid(dtEndereco.Rows[i]["id"].ToString());
                        entityEndereco.IsNew = true;

                        entityEndereco.pse_enderecoPrincipal = string.IsNullOrEmpty(dtEndereco.Rows[i]["enderecoprincipal"].ToString()) ? false : Convert.ToBoolean(dtEndereco.Rows[i]["enderecoprincipal"]);
                        entityEndereco.pse_latitude = latitude;
                        entityEndereco.pse_longitude = longitude;

                        Save(entityEndereco, banco);

                        //Incrementa um na integridade do endereço
                        END_EnderecoDAO endDAL = new END_EnderecoDAO { _Banco = banco };
                        endDAL.Update_IncrementaIntegridade(entityEndereco.end_id);
                    }
                    else if (dtEndereco.Rows[i].RowState == DataRowState.Modified && !string.IsNullOrEmpty(endRel_id))
                    {
                        //TRATA DECIMAL
                        decimal latitude = string.IsNullOrEmpty(dtEndereco.Rows[i]["latitude"].ToString()) ? 0 : decimal.Parse(dtEndereco.Rows[i]["latitude"].ToString());
                        decimal longitude = string.IsNullOrEmpty(dtEndereco.Rows[i]["longitude"].ToString()) ? 0 : decimal.Parse(dtEndereco.Rows[i]["longitude"].ToString());
                        //ATRIBUI VALORES
                        entityEndereco.end_id = new Guid(dtEndereco.Rows[i]["end_id"].ToString());
                        entityEndereco.pse_numero = Salvar_Sempre_Maiusculo ? dtEndereco.Rows[i]["numero"].ToString().ToUpper() : dtEndereco.Rows[i]["numero"].ToString();
                        entityEndereco.pse_complemento = Salvar_Sempre_Maiusculo ? dtEndereco.Rows[i]["complemento"].ToString().ToUpper() : dtEndereco.Rows[i]["complemento"].ToString();

                        bool excluido = Convert.ToBoolean(dtEndereco.Rows[i]["excluido"]);
                        if (excluido)
                            entityEndereco.pse_situacao = Convert.ToByte(3);
                        else
                            entityEndereco.pse_situacao = Convert.ToByte(1);

                        entityEndereco.pse_id = new Guid(dtEndereco.Rows[i]["endRel_id"].ToString());
                        entityEndereco.IsNew = false;

                        entityEndereco.pse_enderecoPrincipal = string.IsNullOrEmpty(dtEndereco.Rows[i]["enderecoprincipal"].ToString()) ? false : Convert.ToBoolean(dtEndereco.Rows[i]["enderecoprincipal"]);
                        entityEndereco.pse_latitude = latitude;
                        entityEndereco.pse_longitude = longitude;

                        Save(entityEndereco, banco);
                    }
                }
                else
                {
                    entityEndereco.pse_id = new Guid(dtEndereco.Rows[i]["endRel_id", DataRowVersion.Original].ToString());
                    entityEndereco.end_id = new Guid(dtEndereco.Rows[i]["end_id", DataRowVersion.Original].ToString());
                    PES_PessoaEnderecoDAO pesendDAL = new PES_PessoaEnderecoDAO { _Banco = banco };
                    pesendDAL.Delete(entityEndereco);

                    //Decrementa um na integridade do endereço                        
                    END_EnderecoDAO endDAL = new END_EnderecoDAO { _Banco = banco };
                    endDAL.Update_DecrementaIntegridade(entityEndereco.end_id);
                }
            }
        }
    }
}
