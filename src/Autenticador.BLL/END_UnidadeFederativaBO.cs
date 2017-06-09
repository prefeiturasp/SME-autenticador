using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreLibrary.Business.Common;
using Autenticador.DAL;
using Autenticador.Entities;
using System.ComponentModel;
using CoreLibrary.Data.Common;

namespace Autenticador.BLL
{
    public class END_UnidadeFederativaBO : BusinessBase<END_UnidadeFederativaDAO, END_UnidadeFederativa>
    {
        /// <summary>
        /// Retorna um datatable contendo todos as unidades federativas que não foram excluídas
        /// logicamente, filtradas por id, pais, nome, sigla, situacao e paginado.
        /// </summary>
        /// <param name="unf_id">
        /// Id da tabela END_UnidadeFederativa do bd
        /// </param>
        /// <param name="pai_id">
        /// Campo pai_id END_UnidadeFederativa do bd
        /// </param>
        /// <param name="unf_nome">
        /// Campo unf_nome da tabela END_UnidadeFederativa do bd
        /// </param>
        /// <param name="unf_sigla">
        /// Campo unf_sigla da tabela END_UnidadeFederativa do bd
        /// </param>
        /// <param name="unf_situacao">
        /// Campo unf_situcao da tabela END_UnidadeFederativa do bd
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
        /// DataTable com unidades federativas
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelect
        (
            Guid unf_id
            , Guid pai_id
            , string unf_nome
            , string unf_sigla
            , byte unf_situacao
            , bool paginado
            , int currentPage
            , int pageSize
        )
        {
            totalRecords = 0;

            if (pageSize == 0)
                pageSize = 1;

            END_UnidadeFederativaDAO dal = new END_UnidadeFederativaDAO();
            try
            {
                return dal.SelectBy_All(unf_id, pai_id, unf_nome, unf_sigla, unf_situacao, paginado, currentPage / pageSize, pageSize, out totalRecords);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Seleciona a unidade federativa filtrando pela sigla.
        /// </summary>
        /// <param name="entity">
        /// Entidade da unidade federativa com o campo UNF_SIGLA preenchido.
        /// </param>
        /// <returns>
        /// Caso encontre a unidade federativa retorna true e carrega a entidade, se não retorna false.
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static bool SelecionaPelaSigla
        (
           END_UnidadeFederativa entity
        )
        {
            END_UnidadeFederativaDAO dal = new END_UnidadeFederativaDAO();
            return dal.SelectBy_Sigla(entity);
        }

        /// <summary>
        /// Verifica se o registro é utilizado em outras tabelas.
        /// </summary>
        /// <param name="unf_id">
        /// ID do registro.
        /// </param>
        /// <returns>
        /// </returns>
        public static bool VerificaIntegridade(Guid unf_id)
        {
            END_UnidadeFederativaDAO dal = new END_UnidadeFederativaDAO();
            return dal.VerificaIntegridade(unf_id);
        }

        /// <summary>
        /// Inclui ou altera uma Cidade
        /// </summary>
        /// <param name="entity">
        /// Entidade END_Cidade
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
            END_UnidadeFederativa entity
            , Guid pai_idAntigo
            , CoreLibrary.Data.Common.TalkDBTransaction banco
        )
        {
            END_UnidadeFederativaDAO dal = new END_UnidadeFederativaDAO();

            if (banco == null)
                dal._Banco.Open(IsolationLevel.ReadCommitted);
            else
                dal._Banco = banco;

            try
            {
                if (entity.Validate())
                {
                    if (VerificaUnidadeFederativaExistentePais(entity))
                        throw new DuplicateNameException("Já existe uma unidade federativa cadastrada com este nome nesse país.");

                    if (entity.IsNew)
                    {
                        //Incrementa um na integridade do pais
                        END_PaisDAO paiDAL = new END_PaisDAO { _Banco = dal._Banco };
                        paiDAL.Update_IncrementaIntegridade(entity.pai_id);
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
        /// Verifica se a unidade federativa que está sendo cadastrada já existe na tabela
        /// END_UnidadeFederativa no mesmo país
        /// </summary>
        /// <param name="entity">
        /// Entidade END_UnidadeFederativa
        /// </param>
        /// <returns>
        /// true = a Cidade já existe na tabela END_UnidadeFederativa no mesmo país / false = a
        /// Cidade não existe na tabela END_UnidadeFederativa no mesmo país
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        private static bool VerificaUnidadeFederativaExistentePais
        (
            END_UnidadeFederativa entity
        )
        {
            END_UnidadeFederativaDAO dal = new END_UnidadeFederativaDAO();
            try
            {
                return dal.SelectBy_unf_nome(entity.unf_id, entity.pai_id, entity.unf_nome, 0);
            }
            catch
            {
                throw;
            }
        }
    }
}