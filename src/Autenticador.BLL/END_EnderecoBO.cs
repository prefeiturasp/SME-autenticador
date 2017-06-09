using System;
using System.Collections.Generic;
using System.Data;
using CoreLibrary.Business.Common;
using Autenticador.DAL;
using Autenticador.Entities;
using System.ComponentModel;
using System.Xml;
using System.Text.RegularExpressions;
using CoreLibrary.Validation.Exceptions;

namespace Autenticador.BLL
{
    public class END_EnderecoBO : BusinessBase<END_EnderecoDAO, END_Endereco>
    {
        /// <summary>
        /// Retorna um datatable contendo todos os endereços que não foram excluídos logicamente,
        /// filtrados por end_id, cid_id, unf_id, pai_id, cep, logradouro, bairro, cidade, estado,
        /// sigla do estado, pais e situação
        /// </summary>
        /// <param name="end_id">
        /// Id da tabela END_Endereco do bd
        /// </param>
        /// <param name="cid_id">
        /// Id da tabela END_Cidade do bd
        /// </param>
        /// <param name="unf_id">
        /// Id da tabela END_Cidade do bd
        /// </param>
        /// <param name="pai_id">
        /// Id da tabela END_Pais do bd
        /// </param>
        /// <param name="end_cep">
        /// Campo end_cep da tabela END_Endereco do bd
        /// </param>
        /// <param name="end_logradouro">
        /// Campo end_logradouro da tabela END_Endereco do bd
        /// </param>
        /// <param name="end_bairro">
        /// Campo end_bairro da tabela END_Endereco do bd
        /// </param>
        /// <param name="cid_nome">
        /// Campo cid_nome da tabela END_Cidade do bd
        /// </param>
        /// <param name="unf_nome">
        /// Campo unf_nome da tabela END_UnidadeFederativa do bd
        /// </param>
        /// <param name="unf_sigla">
        /// Campo unf_sigla da tabela END_UnidadeFederativa do bd
        /// </param>
        /// <param name="pai_nome">
        /// Campo pai_nome da tabela END_Pais do bd
        /// </param>
        /// <param name="end_situacao">
        /// Campo end_situcao da tabela END_Endereco do bd
        /// </param>
        /// <param name="paginado">
        /// Indica se o datatable será paginado ou não
        /// </param>
        /// <param name="currentPage">
        /// Página atual do gridview
        /// </param>
        /// <param name="pageSize">
        /// Total de registros por página
        /// </param>
        /// <returns>
        /// DataTable com os endereços
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelect
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
        )
        {
            totalRecords = 0;

            if (pageSize == 0)
                pageSize = 1;

            END_EnderecoDAO dao = new END_EnderecoDAO();
            return dao.SelectBy_All(end_id, cid_id, pai_id, unf_id, end_cep, end_logradouro, end_bairro, cid_nome, unf_nome, unf_sigla, pai_nome, end_situacao, paginado, currentPage / pageSize, pageSize, out totalRecords);
        }

        /// <summary>
        /// Executar pesquisa incremental de endereços
        /// </summary>
        /// <param name="end_cep">
        /// Campo end_cep da tabela END_Endereco do bd
        /// </param>
        /// <param name="end_logradouro">
        /// Campo end_logradouro da tabela END_Endereco do bd
        /// </param>
        /// <returns>
        /// Lista com os endereços
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<END_Endereco> GetSelectBy_end_cep_end_logradouro(string end_cep, string end_logradouro)
        {
            END_EnderecoDAO dao = new END_EnderecoDAO();
            return dao.SelectBy_end_id_end_logradouro(end_cep, end_logradouro);
        }

        /// <summary>
        /// Valida o endereço de acordo com o padrão do censo escolar.
        /// </summary>
        /// <param name="entityEndereco">
        /// Entidade END_Endereco
        /// </param>
        /// <param name="numero">
        /// Núemro do endereço
        /// </param>
        /// <param name="complemento">
        /// Complemento do endereço
        /// </param>
        public static void ValidaCensoEscolar(END_Endereco entityEndereco, string numero, string complemento)
        {
            Regex regex = new Regex(@"^[\.\,\/a-zA-Z0-9 ÇÁÀÃÂÉÈÊÍÌÓÒÔÕÚÙçáàãâéèêíìóòôõúùªº°-]*$", RegexOptions.None);

            if (!string.IsNullOrEmpty(entityEndereco.end_logradouro)
                && !regex.IsMatch(entityEndereco.end_logradouro))
            {
                throw new ValidationException("Endereço não está no padrão do censo escolar, permitido somente os caracteres especiais: ª, º, –, /, . e ,.");
            }

            if (!string.IsNullOrEmpty(entityEndereco.end_bairro) && !regex.IsMatch(entityEndereco.end_bairro))
            {
                throw new ValidationException("Bairro não está no padrão do censo escolar, permitido somente os caracteres especiais: ª, º, –, /, . e ,.");
            }

            if (!string.IsNullOrEmpty(numero) && !regex.IsMatch(numero))
            {
                throw new ValidationException("Número do endereço não está no padrão do censo escolar, permitido somente os caracteres especiais: ª, º, –, /, . e ,.");
            }

            if (!string.IsNullOrEmpty(complemento) && !regex.IsMatch(complemento))
            {
                throw new ValidationException("Complemento do endereço não está no padrão do censo escolar, permitido somente os caracteres especiais: ª, º, –, /, . e ,.");
            }
        }

        /// <summary>
        /// Inclui ou altera um endereço
        /// </summary>
        /// <param name="entity">
        /// Entidade END_Endereco
        /// </param>
        /// <param name="cid_idAntigo">
        /// Campo cid_id antigo
        /// </param>
        /// <param name="banco">
        /// </param>
        /// <returns>
        /// True = incluído/alterado | False = não incluído/alterado
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static Guid Save
        (
            END_Endereco entity
            , Guid cid_idAntigo
            , CoreLibrary.Data.Common.TalkDBTransaction banco
        )
        {
            END_EnderecoDAO dal = new END_EnderecoDAO();

            if (banco == null)
                dal._Banco.Open(IsolationLevel.ReadCommitted);
            else
                dal._Banco = banco;

            try
            {
                if (entity.Validate())
                {
                    if (entity.cid_id == Guid.Empty)
                        throw new ArgumentException("Cidade é obrigatório.");

                    dal.Salvar(entity);

                    if (entity.IsNew)
                    {
                        //Incrementa um na integridade da cidade
                        END_CidadeDAO cidDAL = new END_CidadeDAO();
                        cidDAL._Banco = dal._Banco;
                        cidDAL.Update_IncrementaIntegridade(entity.cid_id);
                    }
                    else
                    {
                        if (entity.cid_id != cid_idAntigo)
                        {
                            END_CidadeDAO cidDAL = new END_CidadeDAO();
                            cidDAL._Banco = dal._Banco;

                            //Decrementa um na integridade da cidade anterior
                            cidDAL.Update_DecrementaIntegridade(cid_idAntigo);

                            //Incrementa um na integridade da cidade atual
                            cidDAL.Update_IncrementaIntegridade(entity.cid_id);
                        }
                    }
                }
                else
                {
                    throw new ValidationException(entity.PropertiesErrorList[0].Message);
                }

                return entity.end_id;
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

        /// <summary>
        /// Deleta logicamente um Endereço
        /// </summary>
        /// <param name="entity">
        /// Entidade END_Endereco
        /// </param>
        /// <returns>
        /// True = deletado/alterado | False = não deletado/alterado
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public new static bool Delete
        (
            END_Endereco entity
        )
        {
            END_EnderecoDAO dal = new END_EnderecoDAO();
            dal._Banco.Open(IsolationLevel.ReadCommitted);

            try
            {
                //Verifica se a entidade pode ser deletada
                if (dal.Select_Integridade(entity.end_id) > 0)
                {
                    throw new Exception("Não é possível excluir o endereço pois possui outros registros ligados a ele.");
                }
                else
                {
                    //Decrementa um na integridade da cidade
                    END_CidadeDAO cidDAL = new END_CidadeDAO();
                    cidDAL._Banco = dal._Banco;
                    cidDAL.Update_DecrementaIntegridade(entity.cid_id);

                    //Deleta logicamente o endereço
                    dal.Delete(entity);
                }

                return true;
            }
            catch (Exception err)
            {
                dal._Banco.Close(err);
                throw;
            }
            finally
            {
                dal._Banco.Close();
            }
        }

        /// <summary>
        /// Atualiza registro do endereço de referencia. Atualiza os end_id's dos endereços (xDoc)
        /// para end_id do endereço de referencia. Apaga registros fisicamente dos endereços
        /// associados (xDoc)
        /// </summary>
        /// <param name="entity">
        /// Entidade END_Endereco
        /// </param>
        /// <param name="cid_idAntigo">
        /// Campo cid_id antigo
        /// </param>
        /// <param name="xDoc">
        /// XML com IDs dos endereços a serem associados
        /// </param>
        /// <returns>
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static bool AssociarEnderecos
        (
            END_Endereco entity
            , Guid cid_idAntigo
            , XmlDocument xDoc
        )
        {
            END_EnderecoDAO dal = new END_EnderecoDAO();
            dal._Banco.Open(IsolationLevel.ReadCommitted);

            try
            {
                Save(entity, cid_idAntigo, dal._Banco);

                //dal.Carregar(entity);
                //for (int i = 0; i < xDoc.LastChild.ChildNodes.Count; i++)
                //{
                //    END_Endereco EntEndereco = new END_Endereco() { end_id = Convert.ToInt32(xDoc.LastChild.ChildNodes.Item(i).InnerText) };
                //    dal.Carregar(EntEndereco);
                //    entity.end_integridade = entity.end_integridade + EntEndereco.end_integridade;
                //}

                if (dal.AssociarEnderecos(entity.end_id, xDoc, "end_id%", "END_Endereco"))
                    return true;
                else
                    throw new Exception();
            }
            catch (Exception err)
            {
                dal._Banco.Close(err);
                throw;
            }
            finally
            {
                dal._Banco.Close();
            }
        }
    }
}