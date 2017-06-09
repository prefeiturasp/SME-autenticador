using System;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using CoreLibrary.Business.Common;
using Autenticador.DAL;
using Autenticador.Entities;
using System.Xml;

namespace Autenticador.BLL
{
    public class END_CidadeBO : BusinessBase<END_CidadeDAO, END_Cidade>
    {
        /// <summary>
        /// Carrega a Entidade END_Cidade apartir dos parâmetros
        /// </summary>
        /// <param name="cid_nome">
        /// Nome da cidade
        /// </param>
        /// <param name="unf_sigla">
        /// Sigla da unidade federativa
        /// </param>
        /// <param name="entityCidade">
        /// Entidade END_Cidade de retorno que será carregada
        /// </param>
        /// <returns>
        /// </returns>
        public static bool CarregaPorNomeUF
            (
                string cid_nome
                , string unf_sigla
                , out END_Cidade entityCidade
            )
        {
            END_CidadeDAO dao = new END_CidadeDAO();
            return dao.SelectBy_cid_nome_unf_sigla(cid_nome, unf_sigla, out entityCidade);
        }

        /// <summary>
        /// Retorna um datatable contendo todas as cidades que não foram excluídas logicamente,
        /// filtradas por cid_id, unf_id, pai_id, cidade, estado, sigla do estado, pais e situação
        /// </summary>
        /// <param name="cid_id">
        /// ID da cidade
        /// </param>
        /// <param name="pai_id">
        /// ID do pais
        /// </param>
        /// <param name="unf_id">
        /// ID do estado
        /// </param>
        /// <param name="cid_nome">
        /// Nome da cidade
        /// </param>
        /// <param name="unf_nome">
        /// Nome do estado
        /// </param>
        /// <param name="unf_sigla">
        /// Sigla do estado
        /// </param>
        /// <param name="pai_nome">
        /// Nome do pais
        /// </param>
        /// <param name="cid_situacao">
        /// Situacao da cidade
        /// </param>
        /// <param name="paginado">
        /// Indica se vai exibir os registros paginados ou não.
        /// </param>
        /// <param name="currentPage">
        /// Página atual do gridview
        /// </param>
        /// <param name="pageSize">
        /// Total de registros por página
        /// </param>
        /// <returns>
        /// DataTable com as cidades
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelect
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
        )
        {
            totalRecords = 0;

            if (pageSize == 0)
                pageSize = 1;

            END_CidadeDAO dal = new END_CidadeDAO();
            try
            {
                return dal.SelectBy_All(cid_id, pai_id, unf_id, cid_nome, unf_nome, unf_sigla, pai_nome, cid_situacao, paginado, currentPage / pageSize, pageSize, out totalRecords);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna um datatable contendo todas as cidades que não foram excluídas logicamente de um
        /// determinado estado filtradas por unf_id
        /// </summary>
        /// <param name="unf_id">
        /// ID do estado
        /// </param>
        /// <param name="paginado">
        /// Indica se vai exibir os registros paginados ou não.
        /// </param>
        /// <param name="currentPage">
        /// Página atual do gridview
        /// </param>
        /// <param name="pageSize">
        /// Total de registros por página
        /// </param>
        /// <returns>
        /// DataTable com as cidades
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelectPorEstado
        (
             Guid unf_id
            , bool paginado
            , int currentPage
            , int pageSize
        )
        {
            totalRecords = 0;

            if (pageSize == 0)
                pageSize = 1;

            END_CidadeDAO dal = new END_CidadeDAO();
            try
            {
                return dal.SelectBy_unf_id(unf_id, paginado, currentPage / pageSize, pageSize, out totalRecords);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Executar pesquisa incremental de cidades
        /// </summary>
        /// <returns>
        /// Lista com os endereços
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<END_Cidade> GetSelectBy_PesquisaIncremental
        (
            string cid_nome
        )
        {
            END_CidadeDAO dao = new END_CidadeDAO();
            try
            {
                return dao.SelectBy_PesquisaIncremental(cid_nome);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Atualiza registro da cidade de referencia. Atualiza os cid_id´s das cidades (xDoc) para
        /// cid_id da cidade de referencia. Apaga regsitros fisicamente das cidades associadas (xDoc)
        /// </summary>
        /// <param name="entity">
        /// Entidade END_Cidade
        /// </param>
        /// <param name="pai_idAntigo">
        /// Campo pai_id antigo
        /// </param>
        /// <param name="unf_idAntigo">
        /// Campo unf_id antigo
        /// </param>
        /// <param name="xDoc">
        /// XML com IDs das cidades a serem associadas
        /// </param>
        /// <returns>
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static bool AssociarCidades
        (
            END_Cidade entity
            , Guid pai_idAntigo
            , Guid unf_idAntigo
            , XmlDocument xDoc
        )
        {
            END_CidadeDAO dal = new END_CidadeDAO();
            dal._Banco.Open(IsolationLevel.ReadCommitted);

            try
            {
                Save(entity, pai_idAntigo, unf_idAntigo, dal._Banco);

                //dal.Carregar(entity);
                //for (int i = 0; i < xDoc.LastChild.ChildNodes.Count; i++)
                //{
                //    END_Cidade EntCidade = new END_Cidade() { cid_id = Convert.ToInt32(xDoc.LastChild.ChildNodes.Item(i).InnerText) };
                //    dal.Carregar(EntCidade);
                //    entity.cid_integridade = entity.cid_integridade + EntCidade.cid_integridade;
                //}

                if (dal.AssociarCidades(entity.cid_id, xDoc, "cid_id%", "END_Cidade"))
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

        /// <summary>
        /// Inclui ou altera uma Cidade
        /// </summary>
        /// <param name="entity">
        /// Entidade END_Cidade
        /// </param>
        /// <param name="pai_idAntigo">
        /// Campo pai_id antigo
        /// </param>
        /// <param name="unf_idAntigo">
        /// Campo unf_id antigo
        /// </param>
        /// <param name="banco">
        /// Conexão aberta com o banco de dados ou null para uma nova conexão
        /// </param>
        /// <returns>
        /// True = incluído/alterado | False = não incluído/alterado
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static bool Save
        (
            END_Cidade entity
            , Guid pai_idAntigo
            , Guid unf_idAntigo
            , CoreLibrary.Data.Common.TalkDBTransaction banco
        )
        {
            END_CidadeDAO dal = new END_CidadeDAO();

            if (banco == null)
                dal._Banco.Open(IsolationLevel.ReadCommitted);
            else
                dal._Banco = banco;

            try
            {
                if (entity.Validate())
                {
                    if (new Guid(SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.PAIS_PADRAO_BRASIL)) == entity.pai_id)
                    {
                        if (entity.unf_id == Guid.Empty)
                            throw new ArgumentException("Estado é obrigatório para este país.");

                        if (VerificaCidadeExistentePaisEstado(entity))
                            throw new DuplicateNameException("Já existe uma cidade cadastrada com este nome nesse país e estado.");
                    }
                    else
                    {
                        if (VerificaCidadeExistentePais(entity))
                            throw new DuplicateNameException("Já existe uma cidade cadastrada com este nome nesse país.");
                    }

                    if (entity.IsNew)
                    {
                        //Incrementa um na integridade do pais
                        END_PaisDAO paiDAL = new END_PaisDAO { _Banco = dal._Banco };
                        paiDAL.Update_IncrementaIntegridade(entity.pai_id);

                        //Incrementa um na integridade do estado (se existir)
                        if (entity.unf_id != Guid.Empty)
                        {
                            END_UnidadeFederativaDAO unfDAL = new END_UnidadeFederativaDAO { _Banco = dal._Banco };
                            unfDAL.Update_IncrementaIntegridade(entity.unf_id);
                        }
                    }
                    else
                    {
                        if (pai_idAntigo != entity.pai_id)
                        {
                            END_PaisDAO paiDAL = new END_PaisDAO { _Banco = dal._Banco };

                            //Decrementa um na integridade do pais anterior
                            paiDAL.Update_DecrementaIntegridade(pai_idAntigo);

                            //Incrementa um na integridade do pais atual
                            paiDAL.Update_IncrementaIntegridade(entity.pai_id);
                        }

                        if (unf_idAntigo != entity.unf_id)
                        {
                            END_UnidadeFederativaDAO unfDAL = new END_UnidadeFederativaDAO { _Banco = dal._Banco };

                            //Decrementa um na integridade do estado anterior (se existia)
                            if (unf_idAntigo != Guid.Empty)
                                unfDAL.Update_DecrementaIntegridade(unf_idAntigo);

                            //Incrementa um na integridade do estado atual (se existir)
                            if (entity.unf_id != Guid.Empty)
                                unfDAL.Update_IncrementaIntegridade(entity.unf_id);
                        }
                    }

                    dal.Salvar(entity);
                }
                else
                {
                    throw new CoreLibrary.Validation.Exceptions.ValidationException(entity.PropertiesErrorList[0].Message);
                }

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

        /// <summary>
        /// Deleta logicamente uma Cidade
        /// </summary>
        /// <param name="entity">
        /// Entidade END_Cidade
        /// </param>
        /// <returns>
        /// True = deletado/alterado | False = não deletado/alterado
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public new static bool Delete
        (
            END_Cidade entity
        )
        {
            END_CidadeDAO dal = new END_CidadeDAO();
            dal._Banco.Open(IsolationLevel.ReadCommitted);

            try
            {
                //Verifica se a cidade pode ser deletada
                if (dal.Select_Integridade(entity.cid_id) > 0)
                    throw new Exception("Não é possível excluir a cidade pois possui outros registros ligados a ela.");

                //Decrementa um na integridade do pais
                END_PaisDAO paiDAL = new END_PaisDAO { _Banco = dal._Banco };
                paiDAL.Update_DecrementaIntegridade(entity.pai_id);

                //Decrementa um na integridade do estado (se existir)
                if (entity.unf_id != Guid.Empty)
                {
                    END_UnidadeFederativaDAO unfDAL = new END_UnidadeFederativaDAO { _Banco = dal._Banco };
                    unfDAL.Update_DecrementaIntegridade(entity.unf_id);
                }

                //Deleta logicamente a cidade
                dal.Delete(entity);

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
        /// Verifica se a Cidade que está sendo cadastrada já existe na tabela END_Cidade no mesmo país
        /// </summary>
        /// <param name="entity">
        /// Entidade END_Cidade
        /// </param>
        /// <returns>
        /// true = a Cidade já existe na tabela END_Cidade no mesmo país / false = a Cidade não
        /// existe na tabela END_Cidade no mesmo país
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        private static bool VerificaCidadeExistentePais
        (
            END_Cidade entity
        )
        {
            END_CidadeDAO dal = new END_CidadeDAO();
            try
            {
                return dal.SelectBy_cid_nome(entity.cid_id, entity.pai_id, Guid.Empty, entity.cid_nome, 0);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Verifica se a Cidade que está sendo cadastrada já existe na tabela END_Cidade no mesmo
        /// país e estado
        /// </summary>
        /// <param name="entity">
        /// Entidade END_Cidade
        /// </param>
        /// <returns>
        /// true = a Cidade já existe na tabela END_Cidade no mesmo país e estado / false = a Cidade
        /// não existe na tabela END_Cidade no mesmo país e estado
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        private static bool VerificaCidadeExistentePaisEstado
        (
            END_Cidade entity
        )
        {
            END_CidadeDAO dal = new END_CidadeDAO();
            try
            {
                return dal.SelectBy_cid_nome(entity.cid_id, entity.pai_id, entity.unf_id, entity.cid_nome, 0);
            }
            catch
            {
                throw;
            }
        }
    }
}