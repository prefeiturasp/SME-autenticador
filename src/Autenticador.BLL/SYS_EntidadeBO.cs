using System;
using System.Data;
using CoreLibrary.Business.Common;
using Autenticador.DAL;
using Autenticador.Entities;
using System.ComponentModel;
using System.Web;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Autenticador.BLL
{
    public class SYS_EntidadeBO : BusinessBase<SYS_EntidadeDAO, SYS_Entidade>
    {
        /// <summary>
        /// Retorna um datatable contendo todos as entidades
        /// que não foram excluídas logicamente, filtradas por 
        /// tipo de entidade, razao social, nome fantasia, CNPJ, situacao e paginado.
        /// </summary>
        /// <param name="ent_id">ID da entidade</param>
        /// <param name="ten_id">ID do tipo de entidade</param>
        /// <param name="ent_razaoSocial">Razao social da Entidade</param>
        /// <param name="ent_nomeFantasia">Nome fantasia Entidade</param>
        /// <param name="ent_cnpj">CNPJ Entidade</param>
        /// <param name="ent_codigo">Código da Entidade</param>
        /// <param name="ent_situacao">Situacao da Entidade</param>
        /// <param name="paginado">Indica se vai exibir os registros paginados ou não.</param>
        /// <param name="currentPage">Página atual do gridview</param>
        /// <param name="pageSize">Total de registros por página</param>
        /// <returns>DataTable com as entidades</returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static DataTable GetSelect
        (
            Guid ent_id
            , Guid ten_id
            , string ent_razaoSocial
            , string ent_nomeFantasia
            , string ent_cnpj
            , string ent_codigo
            , byte ent_situacao
            , bool paginado
            , int currentPage
            , int pageSize
        )
        {
            if (pageSize == 0)
                pageSize = 1;

            totalRecords = 0;
            SYS_EntidadeDAO dal = new SYS_EntidadeDAO();
            try
            {
                return dal.SelectBy_All(ent_id, ten_id, ent_razaoSocial, ent_nomeFantasia, ent_cnpj, ent_codigo, ent_situacao, paginado, currentPage / pageSize, pageSize, out totalRecords);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Método na qual retorna datatable com regsitros de entidade filtrado por usuario e grupo usuario
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static DataTable GetSelectBy_UsuarioGrupoUA
        (
            Guid ent_id
            , Guid gru_id
            , Guid usu_id
            , byte ent_situacao
            , bool paginado
            , int currentPage
            , int pageSize
        )
        {
            if (pageSize == 0)
                pageSize = 1;

            totalRecords = 0;
            SYS_EntidadeDAO dal = new SYS_EntidadeDAO();
            try
            {
                return dal.SelectBy_UsuarioGrupoUA(ent_id, gru_id, usu_id, ent_situacao, paginado, currentPage / pageSize, pageSize, out totalRecords);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna um datatable contendo todos as entidades
        /// que não foram excluídas logicamente
        /// e que estão ligadas a pelo menos um sistema, filtradas por 
        /// tipo de entidade, razao social, nome fantasia, CNPJ, situacao e paginado.
        /// </summary>
        /// <param name="ent_id">ID da entidade</param>
        /// <param name="ten_id">ID do tipo de entidade</param>
        /// <param name="ent_razaoSocial">Razao social da Entidade</param>
        /// <param name="ent_nomeFantasia">Nome fantasia Entidade</param>
        /// <param name="ent_cnpj">CNPJ Entidade</param>
        /// <param name="ent_situacao">Situacao da Entidade</param>
        /// <param name="paginado">Indica se vai exibir os registros paginados ou não.</param>
        /// <param name="currentPage">Página atual do gridview</param>
        /// <param name="pageSize">Total de registros por página</param>
        /// <returns>DataTable com as entidades</returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static DataTable GetSelectBy_SistemaEntidade
        (
            Guid ent_id
            , Guid ten_id
            , string ent_razaoSocial
            , string ent_nomeFantasia
            , string ent_cnpj
            , byte ent_situacao
            , bool paginado
            , int currentPage
            , int pageSize
        )
        {
            if (pageSize == 0)
                pageSize = 1;

            totalRecords = 0;
            SYS_EntidadeDAO dal = new SYS_EntidadeDAO();
            try
            {
                return dal.SelectBy_SistemaEntidade(ent_id, ten_id, ent_razaoSocial, ent_nomeFantasia, ent_cnpj, ent_situacao, paginado, currentPage / pageSize, pageSize, out totalRecords);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Verifica se o CNPJ que está sendo cadastrado já existe na tabela SYS_Entidade
        /// </summary>
        /// <param name="entity">Entidade SYS_Entidade</param>        
        /// <returns>true = o CNPJ já existe na tabela SYS_Entidade / false = o CNPJ não existe na tabela SYS_Entidade</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static bool VerificaCNPJExistente
        (
            SYS_Entidade entity
        )
        {
            SYS_EntidadeDAO dal = new SYS_EntidadeDAO();
            try
            {
                return dal.SelectBy_ent_razaoSocial_ent_CNPJ(entity.ent_id, "", entity.ent_cnpj, 0);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Verifica se a Razão Social que está sendo cadastrada já existe na tabela SYS_Entidade
        /// </summary>
        /// <param name="entity">Entidade SYS_Entidade</param>        
        /// <returns>true = a Razão Social já existe na tabela SYS_Entidade / false = a Razão Social não existe na tabela SYS_Entidade</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static bool VerificaRazaoSocialExistente
        (
            SYS_Entidade entity
        )
        {
            SYS_EntidadeDAO dal = new SYS_EntidadeDAO();
            try
            {
                return dal.SelectBy_ent_razaoSocial_ent_CNPJ(entity.ent_id, entity.ent_razaoSocial, "", 0);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Inclui ou Altera a Entidade, EntidadeEndereço e EntidadeContato
        /// Inclui um novo endereço se necessário
        /// </summary>
        /// <param name="entityEntidade">Entidade SYS_Entidade</param>        
        /// <param name="entityEndereco">Entidade END_Endereco</param>    
        /// <param name="entityEntidadeEndereco">Entidade SYS_EntidadeEndereco</param>    
        /// <param name="dtContatos">Datatable Contatos</param>    
        /// <param name="ent_idSuperiorAntigo">Campo ent_idSuperior antigo</param>    
        /// <param name="end_idAntigo">Campo end_id antigo</param>               
        /// <param name="banco"></param>
        /// <returns>True = incluído/alterado | False = não incluído/alterado</returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static bool Save
        (
            SYS_Entidade entityEntidade
            , DataTable entityEntidadeEndereco
            , DataTable dtContatos
            , Guid ent_idSuperiorAntigo
            , Guid end_idAntigo
            , string caminho
            , string nomeLogoCliente
            , HttpPostedFile logoCliente
            ,CoreLibrary.Data.Common.TalkDBTransaction banco
        )

        /* public static bool Save
     (
         SYS_Entidade entityEntidade
         , END_Endereco entityEndereco
         , SYS_EntidadeEndereco entityEntidadeEndereco
         , DataTable dtContatos
         , Guid ent_idSuperiorAntigo
         , Guid end_idAntigo
         , string caminho
         , string nomeLogoCliente
         , HttpPostedFile logoCliente 
         ,CoreLibrary.Data.Common.TalkDBTransaction banco
     )*/
        {
            SYS_EntidadeDAO entDAL = new SYS_EntidadeDAO();

            if (banco == null)
                entDAL._Banco.Open(IsolationLevel.ReadCommitted);
            else
                entDAL._Banco = banco;

            try
            {

                //Salva dados na tabela SYS_Entidade
                if (entityEntidade.Validate())
                {
                    if (VerificaRazaoSocialExistente(entityEntidade))
                        throw new DuplicateNameException("Já existe uma entidade cadastrada com esta razão social.");

                    if (!string.IsNullOrEmpty(entityEntidade.ent_cnpj.Trim()))
                    {
                        if (!UtilBO._ValidaCNPJ(entityEntidade.ent_cnpj))
                            throw new ArgumentException("CNPJ inválido.");

                        if (VerificaCNPJExistente(entityEntidade))
                            throw new DuplicateNameException("Já existe uma entidade cadastrada com este CNPJ.");
                    }

                    if (!string.IsNullOrEmpty(entityEntidade.ent_urlAcesso))
                    {
                        Regex reg = new Regex(@"^(http|https):\/\/([a-z]+)(\.[a-z]+)+$");

                        if (!reg.IsMatch(entityEntidade.ent_urlAcesso))
                        {
                            throw new ArgumentException("URL de acesso inválido.");
                        }
                    }

                    if (entDAL.Salvar(entityEntidade) && !string.IsNullOrEmpty(nomeLogoCliente) && logoCliente != null)
                    {
                        nomeLogoCliente = entityEntidade.ent_id + "_" + nomeLogoCliente;

                        UtilBO.SaveThumbnailImage
                            (
                            1000
                            , caminho
                            , nomeLogoCliente
                            , logoCliente
                            , 84
                            , 60
                            );
                    }
                }
                else
                {
                    throw new CoreLibrary.Validation.Exceptions.ValidationException(entityEntidade.PropertiesErrorList[0].Message);
                }


                //TODO:[Gabriel] Multiplos endereços
                /*

                if (entityEntidadeEndereco != null)
                {
                    //Salva dados na tabela SYS_EntidadeEndereco
                    if (entityEntidadeEndereco.Validate())
                    {
                        SYS_EntidadeEnderecoDAO entendDAL = new SYS_EntidadeEnderecoDAO { _Banco = entDAL._Banco };
                        entityEntidadeEndereco.ent_id = entityEntidade.ent_id;
                        entendDAL.Salvar(entityEntidadeEndereco);
                    }
                    else
                    {
                        throw new CoreLibrary.Validation.Exceptions.ValidationException(entityEntidadeEndereco.PropertiesErrorList[0].Message);
                    }

                    if (entityEntidadeEndereco.IsNew)
                    {
                        if (entityEntidadeEndereco.end_id != Guid.Empty)
                        {
                            //Incrementa um na integridade do endereço
                            END_EnderecoDAO endDAL = new END_EnderecoDAO { _Banco = entDAL._Banco };
                            endDAL.Update_IncrementaIntegridade(entityEntidadeEndereco.end_id);
                        }
                    }
                    else
                    {
                        if (end_idAntigo != entityEntidadeEndereco.end_id)
                        {
                            END_EnderecoDAO endDAL = new END_EnderecoDAO { _Banco = entDAL._Banco };

                            if (entityEntidadeEndereco.ene_situacao != 3)
                            {
                                //Decrementa um na integridade do endereço antigo (se existia)
                                if (end_idAntigo != Guid.Empty)
                                    endDAL.Update_DecrementaIntegridade(end_idAntigo);

                                //Incrementa um na integridade do endereço atual (se existir)
                                if (entityEntidadeEndereco.end_id != Guid.Empty)
                                    endDAL.Update_IncrementaIntegridade(entityEntidadeEndereco.end_id);
                            }
                        }
                        else
                        {
                            if (entityEntidadeEndereco.ene_situacao == 3)
                            {
                                //Decrementa um na integridade do endereço atual
                                if (end_idAntigo != Guid.Empty)
                                {
                                    END_EnderecoDAO endDAL = new END_EnderecoDAO { _Banco = entDAL._Banco };
                                    endDAL.Update_DecrementaIntegridade(entityEntidadeEndereco.end_id);
                                }
                            }
                        }
                    }
                }
                */

                //Salva dados na tabela SYS_EntidadeContato
                SYS_EntidadeContato entityContato = new SYS_EntidadeContato
                {
                    ent_id = entityEntidade.ent_id
                };

                for (int i = 0; i < dtContatos.Rows.Count; i++)
                {
                    if (dtContatos.Rows[i].RowState != DataRowState.Deleted)
                    {
                        if (dtContatos.Rows[i].RowState == DataRowState.Added)
                        {
                            entityContato.tmc_id = new Guid(dtContatos.Rows[i]["tmc_id"].ToString());
                            entityContato.enc_contato = dtContatos.Rows[i]["contato"].ToString();
                            entityContato.enc_situacao = Convert.ToByte(1);
                            entityContato.enc_id = new Guid(dtContatos.Rows[i]["id"].ToString());
                            entityContato.IsNew = true;
                            SYS_EntidadeContatoBO.Save(entityContato, entDAL._Banco);

                            //Incrementa um na integridade do tipo de contato
                            SYS_TipoMeioContatoDAO tipoDAL = new SYS_TipoMeioContatoDAO { _Banco = entDAL._Banco };
                            tipoDAL.Update_IncrementaIntegridade(entityContato.tmc_id);
                        }
                        else if (dtContatos.Rows[i].RowState == DataRowState.Modified)
                        {
                            entityContato.tmc_id = new Guid(dtContatos.Rows[i]["tmc_id"].ToString());
                            entityContato.enc_contato = dtContatos.Rows[i]["contato"].ToString();
                            entityContato.enc_situacao = Convert.ToByte(1);
                            entityContato.enc_id = new Guid(dtContatos.Rows[i]["id"].ToString());
                            entityContato.IsNew = false;
                            SYS_EntidadeContatoBO.Save(entityContato, entDAL._Banco);
                        }
                    }
                    else
                    {
                        entityContato.enc_id = (Guid)dtContatos.Rows[i]["id", DataRowVersion.Original];
                        entityContato.tmc_id = (Guid)dtContatos.Rows[i]["tmc_id", DataRowVersion.Original];
                        SYS_EntidadeContatoDAO entconDAL = new SYS_EntidadeContatoDAO { _Banco = entDAL._Banco };
                        entconDAL.Delete(entityContato);

                        //Decrementa um na integridade do tipo de contato
                        SYS_TipoMeioContatoDAO tipoDAL = new SYS_TipoMeioContatoDAO { _Banco = entDAL._Banco };
                        tipoDAL.Update_DecrementaIntegridade(entityContato.tmc_id);
                    }
                }

                if (entityEntidade.IsNew)
                {
                    //Incrementa um na integridade do tipo de entidade
                    SYS_TipoEntidadeDAO tipoDAL = new SYS_TipoEntidadeDAO { _Banco = entDAL._Banco };
                    tipoDAL.Update_IncrementaIntegridade(entityEntidade.ten_id);

                    //Incrementa um na integridade da entidade superior (se existir)
                    if (entityEntidade.ent_idSuperior != Guid.Empty)
                        entDAL.Update_IncrementaIntegridade(entityEntidade.ent_idSuperior);
                }
                else
                {
                    if (ent_idSuperiorAntigo != entityEntidade.ent_idSuperior)
                    {
                        //Decrementa um na integridade da entidade superior anterior (se existia)
                        if (ent_idSuperiorAntigo != Guid.Empty)
                            entDAL.Update_DecrementaIntegridade(ent_idSuperiorAntigo);

                        //Incrementa um na integridade da entidade superior atual (se existir)
                        if (entityEntidade.ent_idSuperior != Guid.Empty)
                            entDAL.Update_IncrementaIntegridade(entityEntidade.ent_idSuperior);
                    }
                }


                // endereço

                SYS_EntidadeEndereco entityEndereco = new SYS_EntidadeEndereco
                {
                    ent_id = entityEntidade.ent_id
                };

                // ABRIR CONEXÃO
                SYS_EntidadeEnderecoDAO entidadeEnderecoDAO = new SYS_EntidadeEnderecoDAO();
                entidadeEnderecoDAO._Banco = entDAL._Banco;

                for (int i = 0; i < entityEntidadeEndereco.Rows.Count; i++)
                {
                    //if (entityEntidadeEndereco.Rows[i].RowState != DataRowState.Deleted)
                    if (!Convert.ToBoolean(entityEntidadeEndereco.Rows[i]["excluido"].ToString()))
                    {
                        string end_id = entityEntidadeEndereco.Rows[i]["end_id"].ToString();

                        if ((String.IsNullOrEmpty(end_id)) || (end_id.Equals(Guid.Empty.ToString())))
                        {
                            END_Endereco entityNovoEndereco = new END_Endereco
                            {
                                //[OLD]end_id = new Guid(dtEndereco.Rows[i]["end_id"].ToString())
                                end_cep = entityEntidadeEndereco.Rows[i]["end_cep"].ToString()
                               ,
                                end_logradouro = entityEntidadeEndereco.Rows[i]["end_logradouro"].ToString()
                               ,
                                end_distrito = entityEntidadeEndereco.Rows[i]["end_distrito"].ToString()
                               ,
                                end_zona = entityEntidadeEndereco.Rows[i]["end_zona"].ToString() == "0" || string.IsNullOrEmpty(entityEntidadeEndereco.Rows[i]["end_zona"].ToString()) ? Convert.ToByte(0) : Convert.ToByte(entityEntidadeEndereco.Rows[i]["end_zona"].ToString())
                               ,
                                end_bairro = entityEntidadeEndereco.Rows[i]["end_bairro"].ToString()
                               ,
                                cid_id = new Guid(entityEntidadeEndereco.Rows[i]["cid_id"].ToString())
                               ,
                                end_situacao = Convert.ToByte(1)
                            };
                            //Inclui dados na tabela END_Endereco (se necessário)
                            if (entityNovoEndereco.end_id == Guid.Empty)
                            {
                                entityEndereco.end_id = END_EnderecoBO.Save(entityNovoEndereco, Guid.Empty, entDAL._Banco);
                                entityNovoEndereco.end_id = entityEndereco.end_id;
                            }
                            //
                            entityEntidadeEndereco.Rows[i]["end_id"] = entityNovoEndereco.end_id;
                        }
                        string endRel_id = entityEntidadeEndereco.Rows[i]["endRel_id"].ToString();

                        if (entityEntidadeEndereco.Rows[i].RowState == DataRowState.Added || string.IsNullOrEmpty(endRel_id))
                        {
                            //TRATA DECIMAL
                            decimal latitude = string.IsNullOrEmpty(entityEntidadeEndereco.Rows[i]["latitude"].ToString()) ? 0 : decimal.Parse(entityEntidadeEndereco.Rows[i]["latitude"].ToString());
                            decimal longitude = string.IsNullOrEmpty(entityEntidadeEndereco.Rows[i]["longitude"].ToString()) ? 0 : decimal.Parse(entityEntidadeEndereco.Rows[i]["longitude"].ToString());
                            //ATRIBUI VALORES
                            entityEndereco.ent_id = entityEntidade.ent_id;
                            entityEndereco.end_id = new Guid(entityEntidadeEndereco.Rows[i]["end_id"].ToString());
                            entityEndereco.ene_numero = entityEntidadeEndereco.Rows[i]["numero"].ToString();
                            entityEndereco.ene_complemento = entityEntidadeEndereco.Rows[i]["complemento"].ToString();
                            entityEndereco.ene_situacao = Convert.ToByte(1);
                            entityEndereco.ene_id = new Guid(entityEntidadeEndereco.Rows[i]["id"].ToString());
                            entityEndereco.IsNew = true;
                            //
                            entityEndereco.ene_enderecoPrincipal = string.IsNullOrEmpty(entityEntidadeEndereco.Rows[i]["enderecoprincipal"].ToString()) ? false : Convert.ToBoolean(entityEntidadeEndereco.Rows[i]["enderecoprincipal"]);
                            entityEndereco.ene_latitude = latitude;
                            entityEndereco.ene_longitude = longitude;
                            //
                            entidadeEnderecoDAO.Salvar(entityEndereco);
                        }
                        else if (entityEntidadeEndereco.Rows[i].RowState == DataRowState.Modified && !string.IsNullOrEmpty(endRel_id))
                        {
                            //TRATA DECIMAL
                            decimal latitude = string.IsNullOrEmpty(entityEntidadeEndereco.Rows[i]["latitude"].ToString()) ? 0 : decimal.Parse(entityEntidadeEndereco.Rows[i]["latitude"].ToString());
                            decimal longitude = string.IsNullOrEmpty(entityEntidadeEndereco.Rows[i]["longitude"].ToString()) ? 0 : decimal.Parse(entityEntidadeEndereco.Rows[i]["longitude"].ToString());
                            //ATRIBUI VALORES
                            entityEndereco.ene_id = new Guid(entityEntidadeEndereco.Rows[i]["endRel_id"].ToString());
                            entityEndereco.ent_id = entityEntidade.ent_id;
                            entityEndereco.end_id = new Guid(entityEntidadeEndereco.Rows[i]["end_id"].ToString());
                            //
                            entityEndereco.ene_numero = entityEntidadeEndereco.Rows[i]["numero"].ToString();
                            entityEndereco.ene_complemento = entityEntidadeEndereco.Rows[i]["complemento"].ToString();
                            bool excluido = Convert.ToBoolean(entityEntidadeEndereco.Rows[i]["excluido"]);
                            if (excluido)
                                entityEndereco.ene_situacao = Convert.ToByte(3);
                            else
                                entityEndereco.ene_situacao = Convert.ToByte(1);
                            entityEndereco.IsNew = false;
                            //
                            entityEndereco.ene_enderecoPrincipal = string.IsNullOrEmpty(entityEntidadeEndereco.Rows[i]["enderecoprincipal"].ToString()) ? false : Convert.ToBoolean(entityEntidadeEndereco.Rows[i]["enderecoprincipal"]);
                            entityEndereco.ene_latitude = latitude;
                            entityEndereco.ene_longitude = longitude;
                            //
                            entidadeEnderecoDAO.Salvar(entityEndereco);
                        }
                    }
                    else
                    {

                        entityEndereco.ene_id = new Guid(entityEntidadeEndereco.Rows[i]["endRel_id", DataRowVersion.Original].ToString());
                        entityEndereco.end_id = new Guid(entityEntidadeEndereco.Rows[i]["end_id", DataRowVersion.Original].ToString());
                        entidadeEnderecoDAO.Delete(entityEndereco);
                    }
                }

                return true;
            }
            catch (Exception err)
            {
                if (banco == null)
                    entDAL._Banco.Close(err);

                throw;
            }
            finally
            {
                if (banco == null)
                    entDAL._Banco.Close();
            }
        }

        /// <summary>
        /// Deleta logicamente uma Entidade
        /// </summary>
        /// <param name="entity">Entidade SYS_Entidade</param>        
        /// <returns>True = deletado/alterado | False = não deletado/alterado</returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public new static bool Delete
        (
            SYS_Entidade entity
        )
        {
            //Chama a função principal de Delete de entidade passando conexão com banco de dados nula
            return Delete(entity, null);
        }

        /// <summary>
        /// Deleta logicamente uma Entidade
        /// </summary>
        /// <param name="entity">Entidade SYS_Entidade</param>        
        /// <param name="banco"></param>
        /// <returns>True = deletado/alterado | False = não deletado/alterado</returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public new static bool Delete
        (
            SYS_Entidade entity
            ,CoreLibrary.Data.Common.TalkDBTransaction banco
        )
        {
            SYS_EntidadeDAO dal = new SYS_EntidadeDAO();

            if (banco == null)
                dal._Banco.Open(IsolationLevel.ReadCommitted);
            else
                dal._Banco = banco;

            try
            {
                //Verifica se a entidade pode ser deletada
                if (dal.Select_Integridade(entity.ent_id) > 0)
                    throw new Exception("Não é possível excluir a entidade pois possui outros registros ligados a ela.");

                //Decrementa um na integridade do endereço (se necessário)
                SYS_EntidadeEnderecoDAO entendDal = new SYS_EntidadeEnderecoDAO { _Banco = dal._Banco };
                SYS_EntidadeEndereco entityEntidadeEndereco = new SYS_EntidadeEndereco { ent_id = entity.ent_id, ene_id = entendDal.SelectBy_ent_id_top_one(entity.ent_id) };

                entendDal.Carregar(entityEntidadeEndereco);

                if (entityEntidadeEndereco.ene_situacao != 3)
                {
                    END_EnderecoDAO endDal = new END_EnderecoDAO { _Banco = dal._Banco };
                    endDal.Update_DecrementaIntegridade(entityEntidadeEndereco.end_id);
                }

                //Decrementa um na integridade de cada tipo de contato da entidade                 
                SYS_EntidadeContatoDAO entconDal = new SYS_EntidadeContatoDAO { _Banco = dal._Banco };
                SYS_TipoMeioContatoDAO conDal = new SYS_TipoMeioContatoDAO { _Banco = dal._Banco };

                DataTable dt = entconDal.SelectBy_ent_id(entity.ent_id, false, 1, 1, out totalRecords);
                for (int i = 0; i < dt.Rows.Count; i++)
                    conDal.Update_DecrementaIntegridade(new Guid(dt.Rows[i]["tmc_id"].ToString()));

                //Decrementa um na integridade do tipo de entidade
                SYS_TipoEntidadeDAO tipoDAL = new SYS_TipoEntidadeDAO { _Banco = dal._Banco };
                tipoDAL.Update_DecrementaIntegridade(entity.ten_id);

                //Decrementa um na integridade da entidade superior (se existir)
                if (entity.ent_idSuperior != Guid.Empty)
                    dal.Update_DecrementaIntegridade(entity.ent_idSuperior);

                //Deleta logicamente a entidade
                dal.Delete(entity);

                return true;
            }
            catch (Exception err)
            {
                if (banco == null)
                    dal._Banco.Close(err);

                throw;
            }
            finally
            {
                if (banco == null)
                    dal._Banco.Close();
            }
        }

        public static SYS_Entidade GetEntity(Guid id)
        {
            DAL.SYS_EntidadeDAO dal = new SYS_EntidadeDAO();
            var entidade = new SYS_Entidade { ent_id = id };

            if (dal.Carregar(entidade))
            {
                return entidade;
            }
            else
            {
                return new SYS_Entidade();
            }
        }
    }
}
