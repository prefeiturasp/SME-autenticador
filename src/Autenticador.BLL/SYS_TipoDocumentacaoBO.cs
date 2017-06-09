using CoreLibrary.Business.Common;
using Autenticador.DAL;
using Autenticador.Entities;
using CoreLibrary.Data.Common;
using System;
using System.ComponentModel;
using System.Data;

namespace Autenticador.BLL
{
    public class SYS_TipoDocumentacaoBO : BusinessBase<SYS_TipoDocumentacaoDAO, SYS_TipoDocumentacao>
    {
        #region Enumerador

        /// <summary>
        /// Enumerador para classificar os tipos de documentos
        /// Na tabela SYS_TipoDocumentação, foi criado o campo tdo_classificacao TINYINT DEFAULT (99)
        /// </summary>
        public enum eClassificacao
        {
            CPF = 1,
            RG = 2,
            PIS = 3,
            NIS = 4,
            TituloEleitor = 5,
            CNH = 6,
            Reservista = 7,
            CTPS = 8,
            RNE = 9,
            Guarda = 10,
            Outros = 99
        }

        #endregion Enumerador

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelect
        (
           Guid tdo_id
           , string tdo_nome
           , string tdo_sigla
           , byte tdo_situacao
           , bool paginado
           , int currentPage
           , int pageSize
        )
        {
            totalRecords = 0;
            SYS_TipoDocumentacaoDAO dal = new SYS_TipoDocumentacaoDAO();

            if (pageSize == 0)
                pageSize = 1;

            try
            {
                return dal.SelectBy_All(tdo_id, tdo_nome, tdo_sigla, tdo_situacao, paginado, currentPage / pageSize, pageSize, out totalRecords);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Incrementa 1 no campo integridade.
        /// </summary>
        /// <param name="tdo_id">ID do Tipo de documento - obrigatório</param>
        /// <param name="banco">Transação - obrigatório</param>
        /// <returns>Sucesso na operação</returns>
        public static bool IncrementaIntegridade(Guid tdo_id, TalkDBTransaction banco)
        {
            SYS_TipoDocumentacaoDAO dao = new SYS_TipoDocumentacaoDAO()
            {
                _Banco = banco
            };
            return dao.Update_IncrementaIntegridade(tdo_id);
        }

        /// <summary>
        /// Decrementa 1 do campo integridade.
        /// </summary>
        /// <param name="tdo_id">ID do Tipo de documento - obrigatório</param>
        /// <param name="banco">Transação - obrigatório</param>
        /// <returns>Sucesso na operação</returns>
        public static bool DecrementaIntegridade(Guid tdo_id, TalkDBTransaction banco)
        {
            SYS_TipoDocumentacaoDAO dao = new SYS_TipoDocumentacaoDAO()
            {
                _Banco = banco
            };
            return dao.Update_DecrementaIntegridade(tdo_id);
        }

        /// <summary>
        /// Verifica se parametro que validade duplicidade de documento por classificação esta habilitado
        /// </summary>
        /// <returns>bool</returns>
        //public static bool VerificaParametroDuplicidadeCategoria()
        //{
        //    //VERIFICA SE PARAMETRO QUE VALIDA POR DUPLICIDADE DE DOCUMENTO POR CLASSIFICAÇÃO ESTA HABILITADO
        //    var verificaParametro = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.HABILITAR_VALIDACAO_DUPLICIDADE_TIPO_DOCUMENTO_POR_CLASSIFICACAO);

        //    bool saida;
        //    if(Boolean.TryParse(verificaParametro, out saida))
        //    {
        //        if (!Convert.ToBoolean(verificaParametro))
        //        {
        //            return false;
        //        }
        //    }

        //    return true;
        //}

        /// <summary>
        /// Verifica se parametro que validade duplicidade de documento por classificação esta habilitado
        /// e (caso esteja) se existe documento com mesma classificação
        /// </summary>
        /// <returns>bool</returns>
        public static bool VerificaDuplicidadeDeClassificacao(Autenticador.Entities.SYS_TipoDocumentacao entity)
        {
            //VERIFICA SE PARAMETRO QUE VALIDA POR DUPLICIDADE DE DOCUMENTO POR CLASSIFICAÇÃO ESTA HABILITADO
            var verificaParametro = SYS_ParametroBO.ParametroValorBooleano(SYS_ParametroBO.eChave.HABILITAR_VALIDACAO_DUPLICIDADE_TIPO_DOCUMENTO_POR_CLASSIFICACAO);

            if (!Convert.ToBoolean(verificaParametro))
            {
                return false;
            }
            else
            {
                try
                {
                    SYS_TipoDocumentacaoDAO dal = new SYS_TipoDocumentacaoDAO();
                    return dal.SelectBy_Classificacao(entity.tdo_classificacao, entity.tdo_id);
                }
                catch
                {
                    throw;
                }
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static bool GetSelectNome(Autenticador.Entities.SYS_TipoDocumentacao entity)
        {
            try
            {
                SYS_TipoDocumentacaoDAO dal = new SYS_TipoDocumentacaoDAO();
                return dal.SelectBy_Nome(entity.tdo_nome, entity.tdo_id);
            }
            catch
            {
                throw;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public new static bool Save(Autenticador.Entities.SYS_TipoDocumentacao entity)
        {
            SYS_TipoDocumentacaoDAO dal = new SYS_TipoDocumentacaoDAO();
            dal._Banco.Open(IsolationLevel.ReadCommitted);

            try
            {
                if (entity.Validate())
                {
                    if (VerificaDuplicidadeDeClassificacao(entity) && entity.tdo_classificacao != Convert.ToByte(eClassificacao.Outros))
                    {
                        throw new DuplicateNameException("Já existe um tipo de documentação cadastrado com esta classificação.");
                    }
                    else if (GetSelectNome(entity))
                    {
                        throw new DuplicateNameException("Já existe um tipo de documentação cadastrado com este nome.");
                    }
                    else
                    {
                        dal.Salvar(entity);
                    }
                }
                else
                {
                    throw new CoreLibrary.Validation.Exceptions.ValidationException(entity.PropertiesErrorList[0].Message);
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

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public new static bool Delete(Autenticador.Entities.SYS_TipoDocumentacao entity)
        {
            SYS_TipoDocumentacaoDAO dal = new SYS_TipoDocumentacaoDAO();
            dal._Banco.Open(IsolationLevel.ReadCommitted);

            try
            {
                if (dal.Select_Integridade(entity.tdo_id) > 0)
                {
                    throw new CoreLibrary.Validation.Exceptions.ValidationException("Não é possível excluir o tipo de documentação pois possui outros registros ligados a ele.");
                }
                else
                {
                    //Deleta logicamente o tipo de documento
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
    }
}