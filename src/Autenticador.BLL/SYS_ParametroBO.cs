using CoreLibrary.Business.Common;
using Autenticador.DAL;
using Autenticador.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace Autenticador.BLL
{
    public class SYS_ParametroBO : BusinessBase<SYS_ParametroDAO, SYS_Parametro>
    {
        #region Enumerador

        public enum eChave
        {
            ENTIDADE_PADRAO
            ,
            PAIS_PADRAO_BRASIL
            ,
            ESTADO_PADRAO_SP
            ,
            TIPO_DOCUMENTACAO_CPF
            ,
            TIPO_DOCUMENTACAO_RG
            ,
            TIPO_MEIOCONTATO_EMAIL
            ,
            TIPO_MEIOCONTATO_TELEFONE
            ,
            TIPO_MEIOCONTATO_SITE
            ,
            TAMANHO_MAX_FOTO_PESSOA
            ,
            URL_ADMINISTRATIVO
            ,
            TITULO_GERAL
            ,
            MENSAGEM_COPYRIGHT
            ,
            LOGO_CLIENTE
            ,
            URL_CLIENTE
            ,
            EXIBIR_LOGO_CLIENTE
            ,
            LOGO_GERAL_SISTEMA
            ,
            SALVAR_SEMPRE_MAIUSCULO
            ,
            QT_ITENS_PAGINACAO
            ,
            FORMATO_SENHA_USUARIO
            ,
            TAMANHO_SENHA_USUARIO
            ,
            LOG_ERROS_GRAVAR_QUERYSTRING
            ,
            LOG_ERROS_GRAVAR_SERVERVARIABLES
            ,
            LOG_ERROS_GRAVAR_PARAMS
            ,
            LOG_ERROS_CHAVES_NAO_GRAVAR
            ,
            HELP_DESK_CONTATO
            ,
            MENSAGEM_ICONE_HELP
            ,
            ID_GOOGLE_ANALYTICS
            ,
            SUPORTE_TECNICO_EMAILS
            ,
            REMOVER_OPCAO_ESQUECISENHA
            ,
            MENSAGEM_ALERTA_PRELOGIN
            ,

            // Parâmtro que indica se deve verificar as falhas de autenticação e exibir o captcha.
            UTILIZAR_CAPTCHA_FALHA_AUTENTICACAO

            ,

            // Intervalo em minutos considerado para reiniciar a contagem de falhas de login (para exibir o captcha).
            INTERVALO_MINUTOS_VERIFICAR_FALHA_AUTENTICACAO

            ,

            // Quantidade de falhas necessárias para exibir o captcha.
            QUANTIDADE_FALHAS_AUTENTICACAO_EXIBIR_CAPTCHA

            ,

            // Permite utilizar data de nascimento e CPF no Esqueci minha senha.
            PERMITIR_DTNASCIMENTO_CPF_ESQUECISENHA

            ,

            // Versão da WebApi do autenticador
            VERSAO_WEBAPI_CORESSO

            ,

            // URL da WebApi do CoreSSO
            URL_WEBAPI_CORESSO

            ,

            // Valida unicidade de email para o usuário
            VALIDAR_UNICIDADE_EMAIL_USUARIO

            ,

            // Valida obrigatoriedade de email para o usuário
            VALIDAR_OBRIGATORIEDADE_EMAIL_USUARIO

            ,

            // Permitir incluir/alterar e-mail na tela de Meus Dados
            PERMITIR_ALTERAR_EMAIL_MEUSDADOS

            ,

            // Salvar histórico de senhas do usuário
            SALVAR_HISTORICO_SENHA_USUARIO

            ,

            // Quantidade de últimas senhas diferentes utilizadas para validar nova senha
            QUANTIDADE_ULTIMAS_SENHAS_VALIDACAO

            ,

            // Permitir a integração de senhas expiradas com o Active Directory
            PERMITIR_INTEGRACAO_SENHA_EXPIRADA_AD

            ,

            // Gerar senha utilizando o formato definido por parâmetro
            GERAR_SENHA_FORMATO_PARAMETRIZADO

            ,

            // Permitir gravar multiplos endereços para unidade administrativas
            PERMITIR_MULTIPLOS_ENDERECOS_UA

            ,

            // Obrigatório o cadastro de endeço para as novas unidades administrativas
            ENDERECO_OBRIGATORIO_CADASTRO_UA

            ,

            // Permitir gravar tipos de contatos repetidos
            PERMITIR_TIPO_CONTATOS_DUPLICADOS

            ,

            // Tipo de documentação - Identificação Funcional
            TIPO_DOCUMENTACAO_IDENTIFICACAO_FUNCIONAL

            ,

            // Tipo de meio de contato - Telefone Celular
            TIPO_MEIOCONTATO_TELEFONE_CELULAR

            ,

            // Permitir gravar multiplos endereços para entidade
            PERMITIR_MULTIPLOS_ENDERECOS_ENTIDADE

            ,

            // Permitir gravar multiplos endereços para pessoa
            PERMITIR_MULTIPLOS_ENDERECOS_PESSOA

            ,

            // Habilitar a validação de duplicidades no tipo de documentação para a mesma classificação
            HABILITAR_VALIDACAO_DUPLICIDADE_TIPO_DOCUMENTO_POR_CLASSIFICACAO

            ,

            // Permitir a manutenção da documentação por classificação do tipo de documento (modelo novo)
            PERMITIR_CADASTRAR_DOCUMENTACAO_POR_CLASSIFICACAO

            ,

            // Permitir o login com provider externo (exemplo: google)
            PERMITIR_LOGIN_COM_PROVIDER_EXTERNO

            ,

            // Prazo em dias para expirar as senhas
            PRAZO_DIAS_EXPIRA_SENHA
        }

        #endregion Enumerador

        #region Propriedades

        private static IDictionary<string, string[]> parametros;

        /// <summary>
        /// Retorna os parâmetros do sistema.
        /// </summary>
        private static IDictionary<string, string[]> Parametros
        {
            get
            {
                if ((parametros == null) || (parametros.Count == 0))
                {
                    // O objeto não pode estar nulo quando lock.
                    parametros = new Dictionary<string, string[]>();
                    lock (parametros)
                    {
                        SelecionaParametrosVigente(out parametros);
                    }
                }
                return parametros;
            }
        }

        #endregion Propriedades

        #region Métodos

        /// <summary>
        /// Retorna os parâmetros ativos e vigentes.
        /// </summary>
        /// <param name="dictionary">Dictionary com parâmetros ativos e vigentes</param>
        private static void SelecionaParametrosVigente(out IDictionary<string, string[]> dictionary)
        {
            SYS_ParametroDAO dao = new SYS_ParametroDAO();
            List<SYS_Parametro> lt = dao.SelectBy_ParametrosVigente();

            dictionary = (from SYS_Parametro par in lt
                          group par by par.par_chave into t
                          select new
                          {
                              chave = t.Key
                              ,
                              valor = t.Select(p => p.par_valor).ToArray()
                          }).ToDictionary(p => p.chave, p => p.valor);
        }

        /// <summary>
        /// Seleciona o valor de um parâmetro filtrado por par_chave.
        /// </summary>
        /// <param name="par_chave">Enum que representa a chave a ser pesquisada</param>
        /// <returns>par_valor</returns>
        public static string ParametroValor(eChave par_chave)
        {
            string valor = string.Empty;
            if (Parametros.ContainsKey(Enum.GetName(typeof(eChave), par_chave)))
                valor = Parametros[Enum.GetName(typeof(eChave), par_chave)].FirstOrDefault();

            return valor;
        }

        /// <summary>
        /// Retorna o valor do parâmetro convertido em Boolean.
        /// Caso o parâmetro esteja vazio, retorna "false".
        /// </summary>
        /// <param name="par_chave">Chave para buscar o parâmetro</param>
        /// <returns></returns>
        public static bool ParametroValorBooleano(eChave par_chave)
        {
            string valor = ParametroValor(par_chave);

            bool ret;

            if (!Boolean.TryParse(valor, out ret))
                return false;

            return ret;
        }

        /// <summary>
        /// Retorna o valor do parâmetro convertido em Int32.
        /// Caso o parâmetro esteja vazio, retorna "0".
        /// </summary>
        /// <param name="par_chave">Chave para buscar o parâmetro</param>
        /// <returns></returns>
        public static int ParametroValorInt32(eChave par_chave)
        {
            string valor = ParametroValor(par_chave);

            Int32 ret;

            if (!Int32.TryParse(valor, out ret))
                return 0;

            return ret;
        }

        /// <summary>
        /// Retorna o valor do parâmetro "INTERVALO_MINUTOS_VERIFICAR_FALHA_AUTENTICACAO",
        /// caso não informado retorna o valor padrão: 30.
        /// </summary>
        /// <returns></returns>
        public static int Parametro_IntervaloMinutosFalhaAutenticacao()
        {
            int valor = ParametroValorInt32(eChave.INTERVALO_MINUTOS_VERIFICAR_FALHA_AUTENTICACAO);

            if (valor == 0)
                valor = 30;

            return valor;
        }

        /// <summary>
        /// Retorna o valor do parâmetro "QUANTIDADE_FALHAS_AUTENTICACAO_EXIBIR_CAPTCHA",
        /// caso não informado retorna o valor padrão: 3.
        /// </summary>
        /// <returns></returns>
        public static int Parametro_QtdeFalhasExibirCaptcha()
        {
            int valor = ParametroValorInt32(eChave.QUANTIDADE_FALHAS_AUTENTICACAO_EXIBIR_CAPTCHA);

            if (valor == 0)
                valor = 3;

            return valor;
        }

        /// <summary>
        /// Retorna o valor do parâmetro "VALIDAR_UNICIDADE_EMAIL_USUARIO",
        /// caso não informado retornar o valor padrão: true.
        /// </summary>
        /// <returns></returns>
        public static bool Parametro_ValidarUnicidadeEmailUsuario()
        {
            string valor = ParametroValor(eChave.VALIDAR_UNICIDADE_EMAIL_USUARIO);

            bool ret;
            if (!Boolean.TryParse(valor, out ret))
                return true;

            return ret;
        }

        /// <summary>
        /// Retorna o valor do parâmetro "VALIDAR_OBRIGATORIEDADE_EMAIL_USUARIO",
        /// caso não informado retornar o valor padrão: true.
        /// </summary>
        /// <returns></returns>
        public static bool Parametro_ValidarObrigatoriedadeEmailUsuario()
        {
            string valor = ParametroValor(eChave.VALIDAR_OBRIGATORIEDADE_EMAIL_USUARIO);

            bool ret;
            if (!Boolean.TryParse(valor, out ret))
                return true;

            return ret;
        }

        /// <summary>
        /// Retorna todos os parâmetros cadastrados não excluidos logicamente.
        /// </summary>
        /// <returns>Datatable com parâmetros</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelect(bool paginado, int currentPage, int pageSize)
        {
            totalRecords = 0;

            if (pageSize == 0)
                pageSize = 1;

            SYS_ParametroDAO dal = new SYS_ParametroDAO();

            DataTable dt = dal.Select(paginado, currentPage / pageSize, pageSize, out totalRecords);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = i; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[i]["par_chave"].Equals(dt.Rows[j]["par_chave"]))
                    {
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// Retorna todos os valores cadastrados para um parâmetro nas quais não foram excluidos logicamente.
        /// filtrados por par_chave.
        /// </summary>
        /// <param name="par_chave">Campo par_chave da tabela SYS_Parametro do bd</param>
        /// <param name="paginado">Indica se será paginado</param>
        /// <param name="currentPage">Página atual</param>
        /// <param name="pageSize">Quantidade de registros por página</param>
        /// <returns>Datatable com parâmetros</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable SelecionaParametroValores(string par_chave, bool paginado, int currentPage, int pageSize)
        {
            totalRecords = 0;

            if (pageSize == 0)
                pageSize = 1;

            SYS_ParametroDAO dal = new SYS_ParametroDAO();
            return dal.SelectBy_par_chave2(par_chave, paginado, currentPage / pageSize, pageSize, out totalRecords);
        }

        /// <summary>
        /// Indica a existência de vigência conflitante com relação as datas de vigência
        /// na entidade SYS_Parametro.
        /// </summary>
        /// <param name="entity">Entidade do SYS_Parametro</param>
        /// <returns>True - caso exista uma vigência em conflito</returns>
        public static bool VerificaVigencia(SYS_Parametro entity)
        {
            SYS_ParametroDAO dal = new SYS_ParametroDAO();
            return dal.SelectBy_Vigencia(entity.par_chave, entity.par_vigenciaInicio, entity.par_vigenciaFim, entity.par_obrigatorio);
        }

        /// <summary>
        /// Retorna um Booleano na qual faz atualização/adequação da data de vigencia final do ultimo parametro (anterior)
        /// ao parametro a ser inserido. Executado somente para parametros obrigatórios;
        /// </summary>
        /// <param name="entity">Entidade SYS_Parametro</param>
        /// <returns>True - caso realize a atualização</returns>
        public static bool AdequaVigencia(SYS_Parametro entity)
        {
            SYS_ParametroDAO dal = new SYS_ParametroDAO();
            return dal.Update_VigenciaFim(entity.par_chave, entity.par_vigenciaInicio.AddDays(-1));
        }

        /// <summary>
        /// Verifica existência parâmetro.
        /// </summary>
        /// <param name="entity">Entidade SYS_Parametro</param>
        /// <returns></returns>
        public static bool _VerificaExistenciaUnicoRegisto(SYS_Parametro entity)
        {
            SYS_ParametroDAO dal = new SYS_ParametroDAO();
            return dal.SelectBy_par_chave3(entity.par_chave);
        }

        /// <summary>
        /// Override do método Save.
        /// </summary>
        /// <param name="entity">Entidade SYS_Parametro</param>
        /// <returns>True - sucesso | False - erro</returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public new static bool Save(SYS_Parametro entity)
        {
            SYS_ParametroDAO dao = new SYS_ParametroDAO();

            if (entity.Validate())
            {
                // Parâmetros que são um valor único e não precisam validar vigência.
                if ((entity.par_chave.Equals("URL_ADMINISTRATIVO")) ||
                    (entity.par_chave.Equals("TITULO_GERAL")) ||
                    (entity.par_chave.Equals("MENSAGEM_COPYRIGHT")) ||
                    (entity.par_chave.Equals("URL_CLIENTE")) ||
                    (entity.par_chave.Equals("QT_ITENS_PAGINACAO")) ||
                    (entity.par_chave.Equals("FORMATO_SENHA_USUARIO")) ||
                    (entity.par_chave.Equals("TAMANHO_SENHA_USUARIO")) ||
                    (entity.par_chave.Equals("LOG_ERROS_GRAVAR_QUERYSTRING")) ||
                    (entity.par_chave.Equals("LOG_ERROS_GRAVAR_SERVERVARIABLES")) ||
                    (entity.par_chave.Equals("LOG_ERROS_GRAVAR_PARAMS")) ||
                    (entity.par_chave.Equals("REMOVER_OPCAO_ESQUECISENHA")) ||
                    (entity.par_chave.Equals("PERMITIR_DTNASCIMENTO_CPF_ESQUECISENHA")) ||
                    (entity.par_chave.Equals("PERMITIR_MULTIPLOS_ENDERECOS")))
                    return dao.Salvar(entity);

                if (entity.par_obrigatorio && UtilBO.VerificaDataMaior(DateTime.Now.Date, entity.par_vigenciaInicio.Date) && entity.IsNew)
                    throw new ArgumentException("Vigência inicial não pode ser anterior à data atual.");

                if (VerificaVigencia(entity))
                {
                    if (entity.par_obrigatorio && entity.IsNew)
                        throw new ArgumentException("Vigência inicial deve ser maior à data do último valor cadastrado.");

                    if (!entity.IsNew)
                        return dao.Salvar(entity);

                    throw new ArgumentException("Parâmetro apresenta conflito nas vigências.");
                }

                if (entity.par_obrigatorio && AdequaVigencia(entity))
                    throw new Exception("Erro na adequação de vigências.");

                return dao.Salvar(entity);
            }

            return false;
        }

        /// <summary>
        /// Overrride do método Delete.
        /// </summary>
        /// <param name="entity">Entidade SYS_Parametro</param>
        /// <returns>True - sucesso | False - erro</returns>
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public new static bool Delete(SYS_Parametro entity)
        {
            SYS_Parametro a = new SYS_Parametro { par_id = entity.par_id };
            GetEntity(a);
            if (_VerificaExistenciaUnicoRegisto(a))
            {
                SYS_ParametroDAO dal = new SYS_ParametroDAO();
                return dal.Delete(a);
            }

            return false;
        }

        /// <summary>
        /// Recarrega os parâmetros do sistema.
        /// </summary>
        public static void RecarregaParametrosVigente()
        {
            // O objeto não pode estar nulo quando lock.
            parametros = new Dictionary<string, string[]>();
            lock (parametros)
            {
                SelecionaParametrosVigente(out parametros);
            }
        }

        #endregion Métodos

        #region Métodos obsoletos

        /// <summary>
        /// Seleciona o valor de um parâmetro filtrados por par_chave.
        /// </summary>
        /// <param name="par_chave">Campo par_chave da tabela SYS_Parametro do bd</param>
        /// <returns>par_valor</returns>
        [Obsolete("Utilizar o novo metodo passando um valor enum.")]
        public static string ParametroValor(string par_chave)
        {
            SYS_ParametroDAO dal = new SYS_ParametroDAO();

            return dal.SelectBy_par_chave(par_chave);
        }

        #endregion Métodos obsoletos
    }
}