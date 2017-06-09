using CoreLibrary.Business.Common;
using Autenticador.DAL;
using Autenticador.Entities;
using CoreLibrary.Data.Common;
using CoreLibrary.Validation.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text.RegularExpressions;
using System.Xml.XPath;

namespace Autenticador.BLL
{
    public class SYS_UnidadeAdministrativaBO : BusinessBase<SYS_UnidadeAdministrativaDAO, SYS_UnidadeAdministrativa>
    {
        #region Métodos de consulta

        /// <summary>
        ///
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelect(
            Guid gru_id
            , Guid usu_id
            , Guid tua_id
            , Guid ent_id
            , Guid uad_id
            , string uad_nome
            , string uad_codigo
            , byte uad_situacao
            , bool paginado
            , int currentPage
            , int pageSize)
        {
            if (pageSize == 0)
                pageSize = 1;

            totalRecords = 0;
            SYS_UnidadeAdministrativaDAO dal = new SYS_UnidadeAdministrativaDAO();
            return dal.SelectBy_Pesquisa(gru_id, usu_id, tua_id, ent_id, uad_id, uad_nome, uad_codigo, uad_situacao, paginado, currentPage, pageSize, out totalRecords);
        }

        /// <summary>
        /// Método que retorna datatable de UA filtrando por Grupo e Usuario
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelectBy_UsuarioGrupoUA(
            Guid gru_id
            , Guid usu_id
            , Guid tua_id
            , Guid ent_id
            , Guid uad_id
            , string uad_nome
            , string uad_codigo
            , byte uad_situacao
            , bool paginado
            , int currentPage
            , int pageSize)
        {
            if (pageSize == 0)
                pageSize = 1;

            totalRecords = 0;
            SYS_UnidadeAdministrativaDAO dal = new SYS_UnidadeAdministrativaDAO();
            return dal.SelectBy_Pesquisa_UsuarioGrupoUA(gru_id, usu_id, tua_id, ent_id, uad_id, uad_nome, uad_codigo, uad_situacao, paginado, currentPage, pageSize, out totalRecords);
        }

        /// <summary>
        /// Método que retorna datatable de unidade administrativa
        /// filtrando pela permissão do usuário.
        /// </summary>
        /// <param name="gru_id">Id do grupo do usuário.</param>
        /// <param name="usu_id">Id do usuário.</param>
        /// <param name="tua_id">Id do tipo de uni. admin.</param>
        /// <param name="ent_id">Id da entidade.</param>
        /// <param name="uad_id">Id da unidade admin.</param>
        /// <param name="uad_nome">Nome da unidade admin.</param>
        /// <param name="uad_codigo">Código da unidade. admin.</param>
        /// <param name="uad_situacao"></param>
        /// <param name="paginado"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelectBy_PermissaoUsuario(
            Guid gru_id
            , Guid usu_id
            , Guid tua_id
            , Guid ent_id
            , Guid uad_id
            , string uad_nome
            , string uad_codigo
            , byte uad_situacao
            , bool paginado
            , int currentPage
            , int pageSize)
        {
            if (pageSize == 0)
                pageSize = 1;

            totalRecords = 0;

            SYS_UnidadeAdministrativaDAO dao = new SYS_UnidadeAdministrativaDAO();
            return dao.SelectBy_Pesquisa_PermissaoUsuario(gru_id, usu_id, tua_id, ent_id, uad_id, uad_nome, uad_codigo, uad_situacao, paginado, currentPage, pageSize, out totalRecords);
        }

        /// <summary>
        /// Retorna as Unidades Administrativas que pertencem ao tipo de UA passado por parâmetro.
        /// </summary>
        /// <param name="tua_id">Tipo de Unidade Administrativa</param>
        /// <param name="uad_situacao">Situação - não usa esse parâmetro</param>
        /// <param name="paginado">Não usa esse parâmetro</param>
        /// <param name="currentPage">Não usa esse parâmetro</param>
        /// <param name="pageSize">Não usa esse parâmetro</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "uad_situacao"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "paginado"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "pageSize"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "currentPage"), DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelectBy_tua_id
        (
            Guid tua_id
            , byte uad_situacao
            , bool paginado
            , int currentPage
            , int pageSize
        )
        {
            SYS_UnidadeAdministrativaDAO dal = new SYS_UnidadeAdministrativaDAO();
            return dal.SelectBy_tua_id(tua_id);
        }

        /// <summary>
        /// Retorna as Unidades Administrativas que pertencem ao tipo de UA passado por parâmetro.
        /// Filtra por Entidade também.
        /// </summary>
        /// <param name="tua_id">Tipo de Unidade Administrativa</param>
        /// <param name="ent_id">Entidade</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelectBy_TipoUA_Entidade
        (
            Guid tua_id
            , Guid ent_id
        )
        {
            SYS_UnidadeAdministrativaDAO dal = new SYS_UnidadeAdministrativaDAO();
            return dal.SelectBy_TipoUA_Entidade(tua_id, ent_id);
        }

        /// <summary>
        /// Retorna as Unidades Administrativas de acordo com os filtros informados.
        /// Os parâmetros ent_id, usu_id e gru_id são obrigatórios e filtra o resultado
        /// pela permissão do usuário na entidade.
        /// </summary>
        /// <param name="tua_id"></param>
        /// <param name="ent_id"></param>
        /// <param name="gru_id"></param>
        /// <param name="usu_id"></param>
        /// <param name="uad_situacao"></param>
        /// <param name="uad_id"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelectBy_Pesquisa_PermissaoUsuario
        (
            Guid tua_id
            , Guid ent_id
            , Guid gru_id
            , Guid usu_id
            , Int16 uad_situacao
            , Guid uad_id
        )
        {
            SYS_UnidadeAdministrativaDAO dal = new SYS_UnidadeAdministrativaDAO();
            return dal.SelectBy_Pesquisa_PermissaoUsuario(tua_id, ent_id, gru_id, usu_id, uad_situacao, uad_id, false, 0, 1, out totalRecords);
        }

        /// <summary>
        /// Retorna as Unidades Administrativas de acordo com os filtros informados.
        /// Os parâmetros ent_id, usu_id e gru_id são obrigatórios e filtra o resultado
        /// pela permissão do usuário na unidade administrativa e na unidade administrativa superior
        /// </summary>
        /// <param name="tua_id"></param>
        /// <param name="ent_id"></param>
        /// <param name="gru_id"></param>
        /// <param name="usu_id"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelectBy_Pesquisa_PermissaoUsuarioUASuperior
        (
            Guid tua_id
            , Guid ent_id
            , Guid gru_id
            , Guid usu_id
        )
        {
            SYS_UnidadeAdministrativaDAO dal = new SYS_UnidadeAdministrativaDAO();
            return dal.SelectBy_Pesquisa_PermissaoUsuarioUASuperior(tua_id, ent_id, gru_id, usu_id);
        }

        /// <summary>
        /// Retorna as Unidades Administrativas de acordo com os filtros informados.
        /// Os parâmetros ent_id, usu_id e gru_id são obrigatórios e filtra o resultado
        /// pela permissão do usuário na unidade administrativa e na unidade administrativa superior
        /// </summary>
        /// <param name="tua_id"></param>
        /// <param name="ent_id"></param>
        /// <param name="gru_id"></param>
        /// <param name="usu_id"></param>
        /// <param name="uad_idSuperior"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelectBy_UadSuperior_PermissaoUsuario
        (
            Guid tua_id
            , Guid ent_id
            , Guid gru_id
            , Guid usu_id
            , Guid uad_idSuperior
        )
        {
            SYS_UnidadeAdministrativaDAO dal = new SYS_UnidadeAdministrativaDAO();
            return dal.GetSelectBy_UadSuperior_PermissaoUsuario(tua_id, ent_id, gru_id, usu_id, uad_idSuperior);
        }

        /// <summary>
        /// Retorna as unidades administrativas, não considerando as permissões do usuário.
        /// </summary>
        /// <param name="tua_id"></param>
        /// <param name="ent_id"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelectBy_Pesquisa_PermissaoTotal(Guid tua_id, Guid ent_id)
        {
            SYS_UnidadeAdministrativaDAO dal = new SYS_UnidadeAdministrativaDAO();
            return dal.SelectBy_Pesquisa_PermissaoTotal(tua_id, ent_id);
        }

        /// <summary>
        /// Retorna as Unidades Administrativas de acordo com os filtros informados, com paginação.
        /// Os parâmetros ent_id, usu_id e gru_id são obrigatórios e filtra o resultado
        /// pela permissão do usuário na entidade.
        /// </summary>
        /// <param name="tua_id"></param>
        /// <param name="ent_id"></param>
        /// <param name="gru_id"></param>
        /// <param name="usu_id"></param>
        /// <param name="uad_situacao"></param>
        /// <param name="uad_id"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelectBy_Pesquisa_PermissaoUsuario
        (
            Guid tua_id
            , Guid ent_id
            , Guid gru_id
            , Guid usu_id
            , Int16 uad_situacao
            , Guid uad_id
            , int currentPage
            , int pageSize
        )
        {
            SYS_UnidadeAdministrativaDAO dal = new SYS_UnidadeAdministrativaDAO();
            return dal.SelectBy_Pesquisa_PermissaoUsuario(tua_id, ent_id, gru_id, usu_id, uad_situacao, uad_id, true, currentPage, pageSize, out totalRecords);
        }

        /// <summary>
        /// Retorna as Unidades Administrativas sem verificar a permissão do usuário, com paginação.
        /// </summary>
        /// <param name="tua_id"></param>
        /// <param name="ent_id"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelectBy_PesquisaTodos(Guid tua_id, Guid ent_id)
        {
            SYS_UnidadeAdministrativaDAO dal = new SYS_UnidadeAdministrativaDAO();
            return dal.SelectBy_PesquisaTodos(tua_id, ent_id);
        }

        /// <summary>
        /// Obs: Criado para o sistema BlueGuardWeb
        /// Recebe o id da Entidade e retorna um XML com todas as Unidades Administrativas desta Entidade
        /// </summary>
        /// <param name="ent_id"></param>
        /// <returns></returns>
        public static string GetSelectBy_HierarquiaXML(Guid ent_id)
        {
            SYS_UnidadeAdministrativaDAO dal = new SYS_UnidadeAdministrativaDAO();

            XPathDocument xDoc = dal.SelectBy_ent_id_HierarquiaXML(ent_id);
            XPathNavigator xpNav = xDoc.CreateNavigator();
            return xpNav.InnerXml;
        }

        /// <summary>
        /// Obs: Criado para o sistema BlueGuardWeb
        /// Retorna as unidades administrativas que não foram excluídas logicamente,
        /// filtrando por: entidade e unidade adminsitrativa.
        /// </summary>
        /// <param name="ent_id">Id da entidade</param>
        /// <param name="uad_idSupeior">Id da unidade administrativa</param>
        /// <returns></returns>
        public static DataTable ConsultarPorUASuperior(Guid ent_id, Guid uad_idSupeior)
        {
            SYS_UnidadeAdministrativaDAO dao = new SYS_UnidadeAdministrativaDAO();
            return dao.SelectBy_UASuperior(ent_id, uad_idSupeior);
        }

        /// <summary>
        /// Retorna a unidade administrativa superior que não foram excluídas logicamente,
        /// filtrando por: entidade e unidade adminsitrativa.
        /// </summary>
        /// <param name="ent_id">Id da entidade</param>
        /// <param name="uad_idSupeior">Id da unidade administrativa</param>
        /// <returns></returns>
        public static IList<SYS_UnidadeAdministrativa> ConsultarUASuperior(Guid ent_id, Guid uad_idSupeior)
        {
            SYS_UnidadeAdministrativaDAO dao = new SYS_UnidadeAdministrativaDAO();
            return dao.ConsultarUASuperior(ent_id, uad_idSupeior);
        }

        #endregion Métodos de consulta

        #region Métodos de validação

        /// <summary>
        /// Verifica se o código está cadastrado retornando a respectiva unidade administrativa
        /// </summary>
        /// <param name="entity">Entidade SYS_UnidadeAdministrativa(contendo ent_id e uad_codigo)</param>
        /// <returns></returns>
        public static bool ConsultarCodigoExistente(SYS_UnidadeAdministrativa entity)
        {
            SYS_UnidadeAdministrativaDAO dao = new SYS_UnidadeAdministrativaDAO();
            return dao.SelectBy_Codigo(entity);
        }

        /// <summary>
        /// Vaerifica se o nome está cadastrado retornando a respectiva unidade administrativa
        /// </summary>
        /// <param name="entity">Entidade SYS_UnidadeAdministrativa(contendo ent_id e uad_nome)</param>
        /// <returns></returns>
        public static bool ConsultarNomeExistente(SYS_UnidadeAdministrativa entity)
        {
            SYS_UnidadeAdministrativaDAO dao = new SYS_UnidadeAdministrativaDAO();
            return dao.SelectBy_Nome(entity);
        }

        /// <summary>
        /// Verifica se o nome que está sendo cadastrado já existe na tabela SYS_UnidadeAdminstrativa na mesma entidade
        /// </summary>
        /// <param name="entity">Entidade SYS_UnidadeAdministrativa</param>
        /// <returns>true = o nome já existe na tabela SYS_UnidadeAdministrativa / false = o nome não existe na tabela SYS_UnidadeAdministrativa</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        protected static bool VerificaNomeExistente(SYS_UnidadeAdministrativa entity)
        {
            SYS_UnidadeAdministrativaDAO dal = new SYS_UnidadeAdministrativaDAO();
            return dal.SelectBy_uad_Nome(entity.ent_id, entity.uad_id, entity.uad_nome, 0);
        }

        /// <summary>
        /// Valida os dados da unidade administrativa de acordo com o padrão do censo escolar.
        /// </summary>
        /// <param name="nomeUnidadeAdministrativa">Nome da unidade administrativa</param>
        /// <param name="termoUnidadeAdministrativa">Termo para referenciar a unidade administrativa</param>
        /// <param name="entityEndereco">Entidade END_Endereco</param>
        /// <param name="numero">Núemro do endereço</param>
        /// <param name="complemento">Complemento do endereço</param>
        public static void ValidaCensoEscolar(string nomeUnidadeAdministrativa, string termoUnidadeAdministrativa, END_Endereco entityEndereco, string numero, string complemento)
        {
            // Valida nome da unidade administrativa.
            Regex regex = new Regex(@"^[\sa-zA-Z0-9ÇÁÀÃÂÉÈÊÍÌÓÒÔÕÚÙçáàãâéèêíìóòôõúùªº°-]*$", RegexOptions.None);
            if (!regex.IsMatch(nomeUnidadeAdministrativa))
            {
                throw new ValidationException(string.Format("Nome da {0} não está no padrão do censo escolar, permitido somente os caracteres especiais: ª, º, -.", termoUnidadeAdministrativa.ToLower()));
            }

            regex = new Regex(@".{4}", RegexOptions.None);
            if (!regex.IsMatch(nomeUnidadeAdministrativa))
            {
                throw new ValidationException(string.Format("Nome da {0} não está no padrão do censo escolar, o nome deve ter no mínimo 4 caracteres.", termoUnidadeAdministrativa.ToLower()));
            }

            // Valida endereço.
            if (entityEndereco != null)
            {
                END_EnderecoBO.ValidaCensoEscolar(entityEndereco, numero, complemento);
            }
        }

        /// <summary>
        /// Valida os dados da unidade administrativa de acordo com o padrão do censo escolar.
        /// </summary>
        /// <param name="entityUnidadeAdministrativa">Entidade SYS_UnidadeAdministrativa</param>
        /// <param name="entityEndereco">Entidade END_Endereco</param>
        /// <param name="entityUnidadeAdministrativaEndereco">Entidade SYS_UnidadeAdministrativaEndereco</param>
        public static void ValidaCensoEscolar(SYS_UnidadeAdministrativa entityUnidadeAdministrativa, END_Endereco entityEndereco, SYS_UnidadeAdministrativaEndereco entityUnidadeAdministrativaEndereco)
        {
            string numero = entityUnidadeAdministrativaEndereco != null ? entityUnidadeAdministrativaEndereco.uae_numero : string.Empty;
            string complemento = entityUnidadeAdministrativaEndereco != null ? entityUnidadeAdministrativaEndereco.uae_complemento : string.Empty;

            ValidaCensoEscolar(entityUnidadeAdministrativa.uad_nome, "Unidade administrativa", entityEndereco, numero, complemento);
        }

        #endregion Métodos de validação

        #region Métodos de inserção e alteração

        /// <summary>
        /// Inclui ou Altera a UnidadeAdministrativa, UnidadeAdministrativaEndereco e UnidadeAdministrativaContato
        /// Inclui um novo endereço se necessário
        /// </summary>
        /// <param name="entityUnidadeAdministrativa">Entidade SYS_UnidadeAdministrativa</param>
        /// <param name="entityEndereco">Entidade END_Endereco</param>
        /// <param name="entityUnidadeAdministrativaEndereco">Entidade SYS_UnidadeAdministrativaEndereco</param>
        /// <param name="dtContatos">Datatable Contatos</param>
        /// <param name="uad_idSuperiorAntigo">Campo uad_idSuperior antigo</param>
        /// <param name="end_idAntigo">Campo end_id antigo</param>
        /// <param name="banco"></param>
        /// <returns>True = incluído/alterado | False = não incluído/alterado</returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static bool Save
        (
            SYS_UnidadeAdministrativa entityUnidadeAdministrativa
            , List<END_Endereco> ltEntityEndereco
            , List<SYS_UnidadeAdministrativaEndereco> ltEntityUnidadeAdministrativaEndereco
            , DataTable dtContatos
            , Guid uad_idSuperiorAntigo
            , Guid end_idAntigo
            , TalkDBTransaction banco
        )
        {
            SYS_UnidadeAdministrativaDAO uadDAL = new SYS_UnidadeAdministrativaDAO();

            if (banco == null)
                uadDAL._Banco.Open(IsolationLevel.ReadCommitted);
            else
                uadDAL._Banco = banco;

            try
            {
                if (ltEntityEndereco.Count > 0)
                {
                    for (int i = 0; i < ltEntityEndereco.Count; i++)
                    {
                        // Inclui dados na tabela END_Endereco (se necessário)
                        // if (ltEntityEndereco[i].IsNew)
                        string end_situacao = ltEntityEndereco[i].end_situacao.ToString();
                        if (!end_situacao.Equals("3"))
                        {
                            ltEntityEndereco[i].end_id = END_EnderecoBO.Save(ltEntityEndereco[i], Guid.Empty, uadDAL._Banco);
                            ltEntityUnidadeAdministrativaEndereco[i].end_id = ltEntityEndereco[i].end_id;
                        }
                    }
                }

                // Salva dados na tabela SYS_UnidadeAdministrativa
                if (entityUnidadeAdministrativa.Validate())
                {
                    Func<Guid, Guid, bool> VerificaUASuperior = (x, y) => x != Guid.Empty && y != Guid.Empty && x.Equals(y);

                    //VERIFICA SE O USUÁRIO ESTÁ TENTANDO VINCULA A MESMA UNIDADE ADMINISTRATIVA COMO SUPERIOR
                    if (VerificaUASuperior(entityUnidadeAdministrativa.uad_id, entityUnidadeAdministrativa.uad_idSuperior))
                    {
                        throw new CoreLibrary.Validation.Exceptions.ValidationException("Não é possível vincular a mesma unidade administrativa como superior.");
                    }
                    else if (VerificaNomeExistente(entityUnidadeAdministrativa))
                    {
                        throw new DuplicateNameException("Já existe uma unidade administrativa cadastrada com este nome.");
                    }

                    uadDAL.Salvar(entityUnidadeAdministrativa);
                }
                else
                {
                    throw new ValidationException(entityUnidadeAdministrativa.PropertiesErrorList[0].Message);
                }

                if (ltEntityUnidadeAdministrativaEndereco.Count > 0)
                {
                    for (int i = 0; i < ltEntityUnidadeAdministrativaEndereco.Count; i++)
                    {


                        // Salva dados na tabela SYS_UnidadeAdministrativaEndereco
                        if (ltEntityUnidadeAdministrativaEndereco[i].Validate())
                        {
                            /* SYS_UnidadeAdministrativaEnderecoDAO uaendDAL = new SYS_UnidadeAdministrativaEnderecoDAO { _Banco = uadDAL._Banco };
                             ltEntityUnidadeAdministrativaEndereco[i].uad_id = entityUnidadeAdministrativa.uad_id;
                             uaendDAL.Salvar(ltEntityUnidadeAdministrativaEndereco[i]);*/


                            SYS_UnidadeAdministrativaEnderecoDAO uaendDAL = new SYS_UnidadeAdministrativaEnderecoDAO { _Banco = uadDAL._Banco };
                            //Se for Delete
                            string end_situacao = ltEntityEndereco[i].end_situacao.ToString();
                            if (end_situacao.Equals("3"))
                            {
                                uaendDAL.Delete(ltEntityUnidadeAdministrativaEndereco[i]);
                            }
                            else
                            {// Se for Update ou Save
                                // Salva dados na tabela SYS_UnidadeAdministrativaEndereco
                                ltEntityUnidadeAdministrativaEndereco[i].uad_id = entityUnidadeAdministrativa.uad_id;
                                uaendDAL.Salvar(ltEntityUnidadeAdministrativaEndereco[i]);
                            }
                        }
                        else
                        {
                            throw new ValidationException(ltEntityUnidadeAdministrativaEndereco[i].PropertiesErrorList[0].Message);
                        }

                        if (ltEntityUnidadeAdministrativaEndereco[i].IsNew)
                        {
                            if (ltEntityUnidadeAdministrativaEndereco[i].end_id != Guid.Empty)
                            {
                                // Incrementa um na integridade do endereço
                                END_EnderecoDAO endDAL = new END_EnderecoDAO { _Banco = uadDAL._Banco };
                                endDAL.Update_IncrementaIntegridade(ltEntityUnidadeAdministrativaEndereco[i].end_id);
                            }
                        }
                        else
                        {
                            if (end_idAntigo != ltEntityUnidadeAdministrativaEndereco[i].end_id)
                            {
                                END_EnderecoDAO endDAL = new END_EnderecoDAO { _Banco = uadDAL._Banco };

                                if (ltEntityUnidadeAdministrativaEndereco[i].uae_situacao != 3)
                                {
                                    // Decrementa um na integridade do endereço antigo (se existia)
                                    if (end_idAntigo != Guid.Empty)
                                        endDAL.Update_DecrementaIntegridade(end_idAntigo);

                                    // Incrementa um na integridade do endereço atual (se existir)
                                    if (ltEntityUnidadeAdministrativaEndereco[i].end_id != Guid.Empty)
                                        endDAL.Update_IncrementaIntegridade(ltEntityUnidadeAdministrativaEndereco[i].end_id);
                                }
                            }
                            else
                            {
                                if (ltEntityUnidadeAdministrativaEndereco[i].uae_situacao == 3)
                                {
                                    // Decrementa um na integridade do endereço atual
                                    if (end_idAntigo != Guid.Empty)
                                    {
                                        END_EnderecoDAO endDAL = new END_EnderecoDAO { _Banco = uadDAL._Banco };
                                        endDAL.Update_DecrementaIntegridade(ltEntityUnidadeAdministrativaEndereco[i].end_id);
                                    }
                                }
                            }
                        }
                    }
                }

                // Salva dados na tabela SYS_UnidadeAdministrativaContato
                SYS_UnidadeAdministrativaContato entityContato = new SYS_UnidadeAdministrativaContato
                {
                    ent_id = entityUnidadeAdministrativa.ent_id
                    ,
                    uad_id = entityUnidadeAdministrativa.uad_id
                };

                for (int i = 0; i < dtContatos.Rows.Count; i++)
                {
                    if (dtContatos.Rows[i].RowState != DataRowState.Deleted)
                    {
                        if (dtContatos.Rows[i].RowState == DataRowState.Added)
                        {
                            entityContato.tmc_id = new Guid(dtContatos.Rows[i]["tmc_id"].ToString());
                            entityContato.uac_contato = dtContatos.Rows[i]["contato"].ToString();
                            entityContato.uac_situacao = Convert.ToByte(1);
                            entityContato.uac_id = new Guid(dtContatos.Rows[i]["id"].ToString());
                            entityContato.IsNew = true;
                            SYS_UnidadeAdministrativaContatoBO.Save(entityContato, uadDAL._Banco);

                            // Incrementa um na integridade do tipo de contato
                            SYS_TipoMeioContatoDAO tipoDAL = new SYS_TipoMeioContatoDAO { _Banco = uadDAL._Banco };
                            tipoDAL.Update_IncrementaIntegridade(entityContato.tmc_id);
                        }
                        else if (dtContatos.Rows[i].RowState == DataRowState.Modified)
                        {
                            entityContato.tmc_id = new Guid(dtContatos.Rows[i]["tmc_id"].ToString());
                            entityContato.uac_contato = dtContatos.Rows[i]["contato"].ToString();
                            entityContato.uac_situacao = Convert.ToByte(1);
                            entityContato.uac_id = new Guid(dtContatos.Rows[i]["id"].ToString());
                            entityContato.IsNew = false;
                            SYS_UnidadeAdministrativaContatoBO.Save(entityContato, uadDAL._Banco);
                        }
                    }
                    else
                    {
                        entityContato.uac_id = (Guid)dtContatos.Rows[i]["id", DataRowVersion.Original];
                        entityContato.tmc_id = (Guid)dtContatos.Rows[i]["tmc_id", DataRowVersion.Original];
                        SYS_UnidadeAdministrativaContatoDAO uadconDAL = new SYS_UnidadeAdministrativaContatoDAO { _Banco = uadDAL._Banco };
                        uadconDAL.Delete(entityContato);

                        //Decrementa um na integridade do tipo de contato
                        SYS_TipoMeioContatoDAO tipoDAL = new SYS_TipoMeioContatoDAO { _Banco = uadDAL._Banco };
                        tipoDAL.Update_DecrementaIntegridade(entityContato.tmc_id);
                    }
                }

                if (entityUnidadeAdministrativa.IsNew)
                {
                    // Incrementa um na integridade da entidade
                    SYS_EntidadeDAO entDAL = new SYS_EntidadeDAO { _Banco = uadDAL._Banco };
                    entDAL.Update_IncrementaIntegridade(entityUnidadeAdministrativa.ent_id);

                    // Incrementa um na integridade do tipo de unidade administrativa
                    SYS_TipoUnidadeAdministrativaDAO tipoDAL = new SYS_TipoUnidadeAdministrativaDAO { _Banco = uadDAL._Banco };
                    tipoDAL.Update_IncrementaIntegridade(entityUnidadeAdministrativa.tua_id);

                    // Incrementa um na integridade da unidade administrativa superior (se existir)
                    if (entityUnidadeAdministrativa.uad_idSuperior != Guid.Empty)
                    {
                        uadDAL.Update_IncrementaIntegridade(entityUnidadeAdministrativa.ent_id, entityUnidadeAdministrativa.uad_idSuperior);
                    }
                }
                else
                {
                    if (uad_idSuperiorAntigo != entityUnidadeAdministrativa.uad_idSuperior)
                    {
                        // Decrementa um na integridade da unidade administrativa superior anterior (se existia)
                        if (uad_idSuperiorAntigo != Guid.Empty)
                        {
                            uadDAL.Update_DecrementaIntegridade(entityUnidadeAdministrativa.ent_id, uad_idSuperiorAntigo);
                        }

                        // Incrementa um na integridade da unidade administrativa superior atual (se existir)
                        if (entityUnidadeAdministrativa.uad_idSuperior != Guid.Empty)
                        {
                            uadDAL.Update_IncrementaIntegridade(entityUnidadeAdministrativa.ent_id, entityUnidadeAdministrativa.uad_idSuperior);
                        }
                    }
                }

                return true;
            }
            catch (Exception err)
            {
                if (banco == null)
                    uadDAL._Banco.Close(err);

                throw;
            }
            finally
            {
                if (banco == null)
                    uadDAL._Banco.Close();
            }
        }

        #endregion Métodos de inserção e alteração

        #region Métodos de exclusão

        /// <summary>
        /// Deleta logicamente uma Unidade Administrativa
        /// </summary>
        /// <param name="entity">Entidade SYS_UnidadeAdministrativa</param>
        /// <returns>True = deletado/alterado | False = não deletado/alterado</returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public new static bool Delete
        (
            SYS_UnidadeAdministrativa entity
        )
        {
            //Chama a função principal de Delete de unidade administrativa passando conexão com banco de dados nula
            return Delete(entity, null);
        }

        /// <summary>
        /// Deleta logicamente uma Unidade Administrativa
        /// </summary>
        /// <param name="entity">Entidade SYS_UnidadeAdministrativa</param>
        /// <param name="banco">Transacao do banco de dados</param>
        /// <returns>True = deletado/alterado | False = não deletado/alterado</returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public new static bool Delete
        (
            SYS_UnidadeAdministrativa entity
            , TalkDBTransaction banco
        )
        {
            SYS_UnidadeAdministrativaDAO dal = new SYS_UnidadeAdministrativaDAO();

            if (banco == null)
                dal._Banco.Open(IsolationLevel.ReadCommitted);
            else
                dal._Banco = banco;

            try
            {
                //Verifica se a unidade administrativa pode ser deletada
                if (dal.Select_Integridade(entity.ent_id, entity.uad_id) > 0)
                    throw new CoreLibrary.Validation.Exceptions.ValidationException("Não é possível excluir a unidade administrativa pois possui outros registros ligados a ela.");

                //Decrementa um na integridade do endereço (se necessário)
                SYS_UnidadeAdministrativaEnderecoDAO uadendDal = new SYS_UnidadeAdministrativaEnderecoDAO { _Banco = dal._Banco };

                SYS_UnidadeAdministrativaEndereco entityUnidadeAdministrativaEndereco = new SYS_UnidadeAdministrativaEndereco { ent_id = entity.ent_id, uad_id = entity.uad_id, uae_id = uadendDal.SelectBy_ent_id_uad_id_top_one(entity.ent_id, entity.uad_id) };

                uadendDal.Carregar(entityUnidadeAdministrativaEndereco);

                if (entityUnidadeAdministrativaEndereco.uae_situacao != 3)
                {
                    END_EnderecoDAO endDal = new END_EnderecoDAO { _Banco = dal._Banco };
                    endDal.Update_DecrementaIntegridade(entityUnidadeAdministrativaEndereco.end_id);
                }

                //Decrementa um na integridade de cada tipo de contato da unidade administrativa
                SYS_UnidadeAdministrativaContatoDAO uadconDal = new SYS_UnidadeAdministrativaContatoDAO { _Banco = dal._Banco };
                SYS_TipoMeioContatoDAO conDal = new SYS_TipoMeioContatoDAO { _Banco = dal._Banco };

                DataTable dt = uadconDal.SelectBy_ent_id(entity.ent_id, entity.uad_id, false, 1, 1, out totalRecords);
                for (int i = 0; i < dt.Rows.Count; i++)
                    conDal.Update_DecrementaIntegridade(new Guid(dt.Rows[i]["tmc_id"].ToString()));

                //Decrementa um na integridade da entidade
                SYS_EntidadeDAO entDal = new SYS_EntidadeDAO { _Banco = dal._Banco };
                entDal.Update_DecrementaIntegridade(entity.ent_id);

                //Decrementa um na integridade do tipo de unidade administrativa
                SYS_TipoUnidadeAdministrativaDAO tipoDAL = new SYS_TipoUnidadeAdministrativaDAO { _Banco = dal._Banco };
                tipoDAL.Update_DecrementaIntegridade(entity.tua_id);

                //Decrementa um na integridade da unidade administrativa superior (se existir)
                if (entity.uad_idSuperior != Guid.Empty)
                    dal.Update_DecrementaIntegridade(entity.ent_id, entity.uad_idSuperior);

                //Deleta logicamente a unidade administrativa
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

        #endregion Métodos de exclusão

        public static SYS_UnidadeAdministrativa GetEntity(Guid IdEntidade, Guid IdUnidade)
        {
            SYS_UnidadeAdministrativaDAO dal = new SYS_UnidadeAdministrativaDAO();
            var entidade = new SYS_UnidadeAdministrativa { ent_id = IdEntidade, uad_id = IdUnidade };

            if (dal.Carregar(entidade))
            {
                return entidade;
            }
            else
            {
                return new SYS_UnidadeAdministrativa();
            }
        }
    }
}