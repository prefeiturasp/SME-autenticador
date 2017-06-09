using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Caching;
using Autenticador.Entities;
using CoreLibrary.Security.Cryptography;
using CoreLibrary.Validation.Exceptions;
using System.Web.Script.Serialization;
using System.Data;
using System.Reflection;
using System.Linq;

namespace Autenticador.BLL
{
    #region Enumeradores

    /// <summary>
    /// Temas padrões do site.
    /// </summary>
    [Obsolete("Esse recurso está obsoleto. A implementação deve ser realizada no próprio sistema.", false)]
    public enum TemaPadraoSite : byte
    {
        Default = 1,
        Novo = 3,
        SMESP = 5,
        IntranetSME = 6
    }

    public enum Pagina
    {
        AreaAdm_DiaNaoUtil = 1,
        AreaAdm_Entidade,
        AreaAdm_Grupo,
        AreaAdm_Log,
        AreaAdm_LogErro,
        AreaAdm_ManutencaoCidade,
        AreaAdm_ManutencaoEndereco,
        AreaAdm_ManutencaoPessoa,
        AreaAdm_UA,
        AreaAdm_Usuario,
        AreaAdm_ManutencaoUnidadeFederativa,
    }

    #endregion Enumeradores

    #region Estruturas

    public struct Busca
    {
        /// <summary>
        /// Página que o usuário efetuou a busca.
        /// </summary>
        public Pagina PaginaBusca;

        private string filtros;

        /// <summary>
        /// Filtros utilizados na busca.
        /// </summary>
        public Dictionary<String, String> Filtros
        {
            get
            {
                try
                {
                    Dictionary<String, String> ret = new Dictionary<String, String>();

                    if (!String.IsNullOrEmpty(filtros))
                    {
                        string[] items = filtros.Split(';');

                        foreach (string s in items)
                        {
                            string[] k = s.Split('=');
                            ret.Add(k[0], k[1].Replace("#$12$#", ";"));
                        }
                    }

                    return ret;
                }
                catch
                {
                    return new Dictionary<String, String>();
                }
            }
            set
            {
                string res = "";

                foreach (KeyValuePair<String, String> item in value)
                {
                    string valor = item.Value.Replace(";", "#$12$#");

                    if (!String.IsNullOrEmpty(res))
                        res += ";";

                    res += item.Key + "=" + valor;
                }

                filtros = res;
            }
        }
    }

    /// <summary>
    /// Classe que irá retornar dados em formato JSON aos clientes que utilizarem
    /// o LoginService.ashx e SignonService.ashx para logar com SAML.
    /// </summary>
    public class RetornoLoginJSON
    {
        public string Mensagem { get; set; }
        public string UrlRedirect { get; set; }
        public string UrlRedirectParam { get; set; }
        public string Grupo { get; set; }
        public string UA { get; set; }
    }

    #endregion Estruturas

    public static class UtilBO
    {
        #region Enumerador

        /// <summary>
        /// Tipo de dado contido no atributo Name do FormsAuthentication.
        /// </summary>
        public enum TypeName
        {
            Entidade
            ,
            Login
            ,
            Grupo
        }

        /// <summary>
        /// Tipo de Mensagem de erro a ser mostrada na tela.
        /// </summary>
        public enum TipoMensagem
        {
            Sucesso
            ,
            Erro
            ,
            Alerta
            ,
            Informacao
            ,
            Nenhuma
        }

        #endregion Enumerador

        #region Métodos

        /// <summary>
        /// Retorna um HTML inject com um div contendo uma mensagem.
        /// </summary>
        /// <param name="message">Mensagem a ser exibida</param>
        /// <param name="tipoMensagem">Tipo de mensagem a ser exibida</param>
        /// <returns>HTML Inject</returns>
        public static string GetErroMessage(string message, TipoMensagem tipoMensagem)
        {
            return GetErroMessage(message, tipoMensagem, "");
        }

        /// <summary>
        /// Retorna um HTML inject com um div contendo uma mensagem.
        /// </summary>
        /// <param name="message">Mensagem a ser exibida</param>
        /// <param name="tipoMensagem">Tipo de mensagem a ser exibida</param>
        /// <param name="estilo"></param>
        /// <returns>HTML Inject</returns>
        public static string GetErroMessage(string message, TipoMensagem tipoMensagem, string estilo)
        {
            // Pega o tema da página que chamou.
            string tema = HttpContext.Current.Handler is Page ?
                        ((Page)HttpContext.Current.Handler).Theme ??
                        "Default" :
                        "Default";

            // Setar caminho da imagem, de acordo com o tipo de mensagem.
            string imagePath = VirtualPathUtility.ToAbsolute("~/App_Themes/" + tema + "/images/");

            switch (tipoMensagem)
            {
                case TipoMensagem.Alerta:
                    {
                        imagePath += "warning.png";

                        break;
                    }
                case TipoMensagem.Erro:
                    {
                        imagePath += "error.png";

                        break;
                    }
                case TipoMensagem.Sucesso:
                    {
                        imagePath += "success.png";

                        break;
                    }
                case TipoMensagem.Informacao:
                    {
                        imagePath += "icoInformacoes.png";

                        break;
                    }
                default:
                    {
                        imagePath = "";

                        break;
                    }
            }

            return GetErroMessage(message, imagePath, estilo);
        }

        /// <summary>
        /// Retorna um HTML inject com um div contendo uma mensagem.
        /// </summary>
        /// <param name="message">Mensagem a ser exibida</param>
        /// <param name="imagePath">Endereço da imagens para ilustrar a mensagem</param>
        /// <returns>HTML Inject</returns>
        [Obsolete("Trocar método - usar sobrecarga do GetErroMessage passando o enum.", false)]
        public static string GetErroMessage(string message, string imagePath)
        {
            return GetErroMessage(message, imagePath, "");
        }

        /// <summary>
        /// Retorna um HTML inject com um div contendo uma mensagem.
        /// </summary>
        /// <param name="message">Mensagem a ser exibida</param>
        /// <param name="imagePath">Endereço da imagens para ilustrar a mensagem</param>
        /// <param name="estilo"></param>
        /// <returns>HTML Inject</returns>
        private static string GetErroMessage(string message, string imagePath, string estilo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"summary\" " +
                "style=\"background: #fff url(" + imagePath + ") no-repeat 45px 50%; " +
                estilo + "\">");
            sb.Append(message);
            sb.Append("</div>");

            return sb.ToString();
        }

        /// <summary>
        /// Retorna um HTML inject com um div contendo uma mensagem.
        /// </summary>
        /// <param name="message">Mensagem a ser exibida</param>
        /// <param name="tipoMensagem">Tipo de mensagem a ser exibida</param>
        /// <returns>HTML Inject</returns>
        public static string GetMessage(string message, TipoMensagem tipoMensagem)
        {
            return GetErroMessage(message, tipoMensagem);
        }

        private static string GetErrorMessage(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.AppendFormat("** {0} **\r\n", DateTime.Now);
                sb.AppendFormat("Exception Type: {0}\r\n", ex.GetType());

                sb.AppendFormat("Exception: {0}\r\n", ex.Message);
                sb.AppendFormat("Source: {0}\r\n", ex.Source);

                if (ex.StackTrace != null)
                {
                    sb.AppendFormat("Stack Trace: {0}\r\n\r\n", ex.StackTrace);
                }

                Exception exTemp = null;
                if (ex.InnerException != null)
                    exTemp = ex.InnerException;
                while (exTemp != null)
                {
                    sb.AppendFormat("Inner Exception Type: {0}\r\n", exTemp.GetType());
                    sb.AppendFormat("Inner Exception: {0}\r\n", exTemp.Message);
                    sb.AppendFormat("Inner Source: {0}\r\n", exTemp.Source);
                    if (exTemp.StackTrace != null)
                    {
                        sb.AppendFormat("Inner Stack Trace: {0}\r\n\r\n", exTemp.StackTrace);
                    }
                    exTemp = exTemp.InnerException;
                }
            }
            catch
            {
            }
            return sb.ToString();
        }

        /// <summary>
        /// Registra o código de acompanhamento do Google Analytics.
        /// </summary>
        /// <param name="page">Página que irá registrar o código de acompanhamento do Google Analytics</param>
        public static void RegistraGATC(Page page)
        {
            // Caso o parâmetro "ID_GOOGLE_ANALYTICS" esteja definido, registra o GATC para a página.
            string accountId = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.ID_GOOGLE_ANALYTICS);
            if (!String.IsNullOrEmpty(accountId))
            {
                page.Header.Controls.Add(GenerateScriptHeader(GenerateScriptGoogleAnalyticsHead(accountId)));
                page.ClientScript.RegisterStartupScript(page.GetType(), "GoogleAnalytics", GenerateScriptGoogleAnalyticsBody(), true);
            }
        }

        /// <summary>
        /// Retorna o Script do código de acompanhamento do Google Analytics (acompanhamento assíncrono).
        /// http://code.google.com/intl/pt-BR/apis/analytics/docs/tracking/asyncUsageGuide.html
        /// </summary>
        /// <param name="accountId">ID da propriedade da web completo (por exemplo, UA-65432-1) relativo ao objeto de acompanhamento</param>
        /// <returns>Script GATC</returns>
        public static string GenerateScriptGoogleAnalytics(string accountId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(GenerateScriptGoogleAnalyticsHead(accountId));
            sb.Append(GenerateScriptGoogleAnalyticsBody());
            return sb.ToString();
        }

        /// <summary>
        /// Retorna o Script do código de acompanhamento do Google Analytics para Snippet dividido, parte superior.
        /// http://code.google.com/intl/pt-BR/apis/analytics/docs/tracking/asyncUsageGuide.html#SplitSnippet
        /// </summary>
        /// <param name="accountId">ID da propriedade da web completo (por exemplo, UA-65432-1) relativo ao objeto de acompanhamento</param>
        /// <returns>Script GATC</returns>
        public static string GenerateScriptGoogleAnalyticsHead(string accountId)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("var _gaq = _gaq || [];");
            sb.AppendLine(String.Format("_gaq.push(['_setAccount', '{0}']);", accountId));
            sb.AppendLine("_gaq.push(['_trackPageview']);");
            return sb.ToString();
        }

        /// <summary>
        /// Retorna o Script do código de acompanhamento do Google Analytics para Snippet dividido, parte inferior.
        /// http://code.google.com/intl/pt-BR/apis/analytics/docs/tracking/asyncUsageGuide.html#SplitSnippet
        /// </summary>
        /// <returns>Script GATC</returns>
        public static string GenerateScriptGoogleAnalyticsBody()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("(function () {");
            sb.AppendLine("var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;");
            sb.AppendLine("ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';");
            sb.AppendLine("var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);");
            sb.AppendLine("})();");
            return sb.ToString();
        }

        /// <summary>
        /// Adiciona a tag script com o script na tag head
        /// </summary>
        /// <param name="script">Script</param>
        /// <returns>Tag script</returns>
        public static HtmlGenericControl GenerateScriptHeader(string script)
        {
            HtmlGenericControl include = new HtmlGenericControl("script");
            include.Attributes.Add("type", "text/javascript");
            include.InnerHtml = script;
            return include;
        }

        public static string GenerateScriptConfirmDialogButton(string buttonID, string message, bool addScriptTag)
        {
            //Monta a chamada do método SetConfirmDialogButton do arquivo Util.js
            string confirmScript = String.Format("SetConfirmDialogButton('{0}','{1}');", String.Concat("#", buttonID), message);
            //Monta script a ser registrado na página com suporte à UpdatePanel do asp.net(chamadas assincronas)
            StringBuilder sb = new StringBuilder();
            sb.Append("var fnc = function() { ");
            sb.Append(confirmScript);
            sb.Append(" }; ");
            sb.Append("arrFNC.push(fnc); ");
            sb.Append("fnc();");
            //Adiciona a tag script no inicio e no fim da string
            if (addScriptTag)
            {
                sb.Insert(0, "<script type=\"text/javascript\">");
                sb.Insert(sb.Length, "</script>");
            }

            return sb.ToString();
        }

        public static void CreateHtmlFormMessage(TextWriter writer, string title, string message, string urlVoltar)
        {
            // Pega o tema da página que chamou.
            string tema = HttpContext.Current.Handler is Page ?
                        ((Page)HttpContext.Current.Handler).Theme ??
                        "Default" :
                        "Default";

            // Setar caminho da imagem, de acordo com o tipo de mensagem.
            string cssPath = VirtualPathUtility.ToAbsolute("~/App_Themes/" + tema + "/css.css");

            writer.WriteLine("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\" >");
            writer.WriteLine("<html><head>");
            writer.WriteLine("<title>{0}</title>", title);
            writer.WriteLine("<link href=\"{0}\" rel=\"stylesheet\" type=\"text/css\" />", VirtualPathUtility.ToAbsolute(cssPath));
            writer.WriteLine("</head><body>");
            writer.WriteLine("<form id=\"\" method=\"post\" action=\"{0}\">", urlVoltar);
            writer.WriteLine("<div id=\"bd\">");
            writer.WriteLine("<span>{0}</span>", message);
            writer.WriteLine("<input type=\"submit\" value=\"Voltar\" class=\"btn\" style=\"margin: 0 20px;\" />");
            writer.WriteLine("</div>");
            writer.WriteLine("</form>");
            writer.WriteLine("</body></html>");
        }

        /// <summary>
        /// Mensagem de retorno JSON aos clientes que utilizarem o LoginService.ashx
        /// e SignonService.ashx para logar com SAML.
        /// </summary>
        public static void MessageJSON(TextWriter writer, RetornoLoginJSON ret)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            writer.WriteLine(js.Serialize(ret));
        }

        /// <summary>
        /// Formata string para ser utilizada no atributo Name do FormsAuthentication
        /// </summary>
        /// <param name="entityUsuario">Entidade SYS_Usuario</param>
        /// <returns></returns>
        [Obsolete()]
        public static string FormatNameFormsAuthentication(SYS_Usuario entityUsuario)
        {
            return string.Concat(entityUsuario.ent_id, "\\", entityUsuario.usu_login);
        }

        /// <summary>
        /// Formata string para ser utilizada no atributo Name do FormsAuthentication
        /// </summary>
        /// <param name="entityUsuario">Entidade SYS_Usuario</param>
        /// <returns></returns>
        [Obsolete()]
        public static string FormatNameFormsAuthentication(SYS_Usuario entityUsuario, SYS_Grupo entityGrupo)
        {
            return string.Concat(entityUsuario.ent_id, "\\", entityUsuario.usu_login, "\\", entityGrupo.gru_id);
        }

        /// <summary>
        /// Retorna dados do atributo Name do FormsAuthentication
        /// apartir do tipo passado como parâmetro
        /// </summary>
        /// <param name="name">Name do FormsAuthentication</param>
        /// <param name="type">Tipo de dado a ser retornado</param>
        /// <returns></returns>
        [Obsolete()]
        public static string GetNameFormsAuthentication(string name, TypeName type)
        {
            switch (type)
            {
                case TypeName.Entidade:
                    {
                        return name.Split('\\')[0];
                    }
                case TypeName.Login:
                    {
                        return name.Split('\\')[1];
                    }
                case TypeName.Grupo:
                    {
                        if (name.Split('\\').Length == 3)
                            return name.Split('\\')[2];
                        else
                            return string.Empty;
                    }
                default:
                    {
                        return string.Empty;
                    }
            }
        }

        /// <summary>
        /// Adiciona a tag script com o caminho do arquivo js na tag head
        /// </summary>
        /// <param name="scriptPath">Url do arquivo js</param>
        /// <returns>Tag script</returns>
        public static HtmlGenericControl SetScriptHeader(string scriptPath)
        {
            try
            {
                HtmlGenericControl include = new HtmlGenericControl("script");
                include.Attributes.Add("type", "text/javascript");
                include.Attributes.Add("src", scriptPath);
                if (HttpContext.Current.Request.Browser.Browser == "IE")
                {
                    if (HttpContext.Current.Request.Browser.MajorVersion < 7)
                        include.Attributes.Add("charset", "ISO-8859-1");
                }
                return include;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Adiciona a tag link com o caminho do arquivo css na tag head
        /// </summary>
        /// <param name="stylePath">Url do arquivo css</param>
        /// <returns>Tag link</returns>
        public static HtmlLink SetStyleHeader(string stylePath, string styleName, bool alternate)
        {
            try
            {
                HtmlLink include = new HtmlLink();
                include.Attributes.Add("rel", alternate ? "alternate stylesheet" : "stylesheet");
                include.Attributes.Add("href", String.Concat(stylePath, styleName));
                include.Attributes.Add("type", "text/css");
                include.Attributes.Add("title", styleName.Substring(0, styleName.Length - 4));
                return include;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Verifica se o CNPJ é válido
        /// </summary>
        public static bool _ValidaCNPJ(string cnpj)
        {
            if (!string.IsNullOrEmpty(cnpj))
            {
                int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int soma;
                int resto;
                string digito;
                string tempCnpj;

                cnpj = cnpj.Trim();
                cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

                if (cnpj.Length != 14)
                    return false;

                tempCnpj = cnpj.Substring(0, 12);

                soma = 0;
                for (int i = 0; i < 12; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

                resto = (soma % 11);
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                digito = resto.ToString();

                tempCnpj = tempCnpj + digito;
                soma = 0;

                for (int i = 0; i < 13; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

                resto = (soma % 11);

                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                digito = digito + resto.ToString();

                return cnpj.EndsWith(digito);
            }
            else
                return false;
        }

        /// <summary>
        /// Verifica se o CPF é válido
        /// </summary>
        public static bool _ValidaCPF(string valor)
        {
            if (string.IsNullOrEmpty(valor))
                return false;

            string cpf = valor;

            int d1, d2;
            int soma = 0;
            string digitado = "";
            string calculado = "";

            // Pesos para calcular o primeiro digito
            int[] peso1 = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            // Pesos para calcular o segundo digito
            int[] peso2 = new int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] n = new int[11];

            // Se o tamanho for < 11 entao retorna como inválido
            if (cpf.Length != 11)
                return false;

            // Caso coloque todos os numeros iguais
            switch (cpf)
            {
                case "11111111111":
                    return false;

                case "00000000000":
                    return false;

                case "2222222222":
                    return false;

                case "33333333333":
                    return false;

                case "44444444444":
                    return false;

                case "55555555555":
                    return false;

                case "66666666666":
                    return false;

                case "77777777777":
                    return false;

                case "88888888888":
                    return false;

                case "99999999999":
                    return false;
            }

            try
            {
                // Quebra cada digito do CPF
                n[0] = Convert.ToInt32(cpf.Substring(0, 1));
                n[1] = Convert.ToInt32(cpf.Substring(1, 1));
                n[2] = Convert.ToInt32(cpf.Substring(2, 1));
                n[3] = Convert.ToInt32(cpf.Substring(3, 1));
                n[4] = Convert.ToInt32(cpf.Substring(4, 1));
                n[5] = Convert.ToInt32(cpf.Substring(5, 1));
                n[6] = Convert.ToInt32(cpf.Substring(6, 1));
                n[7] = Convert.ToInt32(cpf.Substring(7, 1));
                n[8] = Convert.ToInt32(cpf.Substring(8, 1));
                n[9] = Convert.ToInt32(cpf.Substring(9, 1));
                n[10] = Convert.ToInt32(cpf.Substring(10, 1));
            }
            catch
            {
                return false;
            }

            // Calcula cada digito com seu respectivo peso
            for (int i = 0; i <= peso1.GetUpperBound(0); i++)
                soma += (peso1[i] * Convert.ToInt32(n[i]));

            // Pega o resto da divisao
            int resto = soma % 11;

            if (resto == 1 || resto == 0)
                d1 = 0;
            else
                d1 = 11 - resto;

            soma = 0;

            // Calcula cada digito com seu respectivo peso
            for (int i = 0; i <= peso2.GetUpperBound(0); i++)
                soma += (peso2[i] * Convert.ToInt32(n[i]));

            // Pega o resto da divisao
            resto = soma % 11;
            if (resto == 1 || resto == 0)
                d2 = 0;
            else
                d2 = 11 - resto;

            calculado = d1.ToString() + d2.ToString();
            digitado = n[9].ToString() + n[10].ToString();

            // Se os ultimos dois digitos calculados bater com
            // os dois ultimos digitos do cpf entao é válido
            if (calculado == digitado)
                return (true);
            else
                return (false);
        }

        public static string _CriaSenha(int tamanho)
        {
            if (SYS_ParametroBO.ParametroValorBooleano(SYS_ParametroBO.eChave.GERAR_SENHA_FORMATO_PARAMETRIZADO))
            {
                string formatoSenha = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.FORMATO_SENHA_USUARIO);

                List<string> listaCaracteresValidos = new List<string>();

                if (new Regex("[a-z]").IsMatch(formatoSenha))
                {
                    listaCaracteresValidos.Add("abcdefghijklmnopqrstuvwxyz");
                }

                if (new Regex("[A-Z]").IsMatch(formatoSenha))
                {
                    listaCaracteresValidos.Add("ABCDEFGHIJKLMNOPQRSTUVWXY");
                }

                if (new Regex("[0-9]").IsMatch(formatoSenha))
                {
                    listaCaracteresValidos.Add("1234567890");
                }

                if (new Regex("([!]+[@]+[#]+[$]+[%]+[&]+)").IsMatch(formatoSenha))
                {
                    listaCaracteresValidos.Add("!@#$%&");
                }

                if (listaCaracteresValidos.Count <= 0)
                {
                    listaCaracteresValidos.Add("abcdefghijklmnopqrstuvwxyz");
                    listaCaracteresValidos.Add("ABCDEFGHIJKLMNOPQRSTUVWXY");
                    listaCaracteresValidos.Add("1234567890");
                }

                int valormaximo = listaCaracteresValidos.Count;

                Random random = new Random(DateTime.Now.Millisecond);

                StringBuilder senha = new StringBuilder(tamanho);

                foreach (string cadeia in listaCaracteresValidos)
                {
                    senha.Append(cadeia[random.Next(0, cadeia.Length)]);
                }

                int tamanhoSenhaParcial = tamanho - senha.Length;
                for (int indice = 0; indice < tamanhoSenhaParcial; indice++)
                {
                    string cadeia = listaCaracteresValidos[random.Next(0, valormaximo)];
                    senha.Append(cadeia[random.Next(0, cadeia.Length)]);
                }

                return new String(senha.ToString().OrderBy(c => Guid.NewGuid()).Select(c => c).ToArray());
            }
            else
            {
                const string SenhaCaracteresValidos = "abcdefghijklmnopqrstuvwxyz1234567890";
                int valormaximo = SenhaCaracteresValidos.Length;

                Random random = new Random(DateTime.Now.Millisecond);

                StringBuilder senha = new StringBuilder(tamanho);

                for (int indice = 0; indice < tamanho; indice++)
                    senha.Append(SenhaCaracteresValidos[random.Next(0, valormaximo)]);

                return senha.ToString();
            }
        }

        /// <summary>
        /// Criptografa a senha apartir do tipo de criptografia
        /// </summary>
        /// <param name="senha"></param>
        /// <param name="criptografia"></param>
        /// <returns></returns>
        public static string CriptografarSenha(string senha, eCriptografa criptografia)
        {
            switch (criptografia)
            {
                case eCriptografa.TripleDES:
                    {
                        SymmetricAlgorithm encript = new SymmetricAlgorithm(SymmetricAlgorithm.Tipo.TripleDES);
                        senha = encript.Encrypt(senha);
                        break;
                    }
                case eCriptografa.MD5:
                    {
                        senha = FormsAuthentication.HashPasswordForStoringInConfigFile(senha, eCriptografa.MD5.ToString());
                        break;
                    }
                case eCriptografa.SHA512:
                    {
                        senha = CriptografarSenhaSHA512(senha);
                        break;
                    }
                default:
                    {
                        throw new NotImplementedException();
                    }
            }
            return senha;
        }

        /// <summary>
        /// Criptografa a senha no tipo SHA512
        /// </summary>
        /// <param name="senha">senha a ser criptografada</param>
        /// <returns></returns>
        private static string CriptografarSenhaSHA512(string senha)
        {
            try
            {
                byte[] senhaByte = System.Text.Encoding.Unicode.GetBytes(senha);

                System.Security.Cryptography.SHA512 sha512 = new System.Security.Cryptography.SHA512Managed();

                string pwd = Convert.ToBase64String(sha512.ComputeHash(senhaByte));

                return pwd.TrimStart('/');
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Compara senhas apartir do tipo de criptografia
        /// </summary>
        /// <param name="senha1"></param>
        /// <param name="senha2"></param>
        /// <param name="criptografia"></param>
        /// <returns></returns>
        public static bool EqualsSenha(string senha1, string senha2, eCriptografa criptografia)
        {
            switch (criptografia)
            {
                case eCriptografa.MD5:
                    {
                        return senha1.Equals(senha2, StringComparison.OrdinalIgnoreCase);
                    }
                default:
                    {
                        return senha1.Equals(senha2);
                    }
            }
        }

        /// <summary>
        /// Monta messagem de tamanho, apartir da expressão regular de validação do tamanho
        /// </summary>
        /// <param name="regex_tamanho">Expressão regular utilizada para validar o tamanho</param>
        /// <returns></returns>
        public static string GetMessageTamanhoByRegex(string regex_tamanho)
        {
            StringBuilder sb = new StringBuilder();
            Regex regex = new Regex(@"[.{}]+");
            string[] tam = regex.Replace(regex_tamanho, "").Split(',');
            if (tam.Length == 2)
            {
                if (!string.IsNullOrEmpty(tam[0]))
                {
                    sb.Append(String.Format("mínimo de {0} ", tam[0]));
                    sb.Append(string.IsNullOrEmpty(tam[1]) ? "caracteres" : String.Format("e máximo de {0} caracteres", tam[1]));
                }
                else if (!string.IsNullOrEmpty(tam[1]))
                {
                    sb.Append(String.Format("máximo de {0} caracteres", tam[1]));
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Retorno booleano na qual verifica se a Data1 de entrada é maior que a Data2 de entrada
        /// </summary>
        /// <param name="Data1">Data a ser comparada.</param>
        /// <param name="Data2">Data de referencia para comparação.</param>
        /// <returns>True - caso data maior ou igual que data atual/False - caso data menor que data atual</returns>
        public static bool VerificaDataMaior(DateTime Data1, DateTime Data2)
        {
            try
            {
                if (DateTime.Compare(Data1, Data2) > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorno booleano na qual verifica se a Data1 de entrada é igual a Data2 de entrada
        /// </summary>
        /// <param name="Data1">Data a ser comparada.</param>
        /// <param name="Data2">Data de referencia para comparação.</param>
        /// <returns>True - caso data maior ou igual que data atual/False - caso data menor que data atual</returns>
        public static bool VerificaDataIgual(DateTime Data1, DateTime Data2)
        {
            try
            {
                if (Data1.Date == Data2.Date)
                    return true;
                else
                    return false;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Configura a janela de busca no sistema.
        /// </summary>
        /// <param name="page">Instância da página atual</param>
        /// <param name="buttonBusca">Botão, image ou link responsável pela busca</param>
        /// <param name="resultsID">Id's dos text ou hidden onde serão guardados os resultados</param>
        /// <param name="urlBusca">Url da página da busca</param>
        /// <param name="tituloFrame">Nome da busca que será exibido na barra do topo da janela de busca</param>
        /// <param name="width">largura</param>
        /// <param name="height">altura</param>
        public static void SetScriptBusca(Page page, Control buttonBusca, string[] resultsID, string urlBusca, string tituloFrame, int width, int height)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                if (urlBusca.IndexOf("?") < 0)
                    urlBusca = String.Concat(urlBusca, "?buscaID=", buttonBusca.ClientID);
                else
                    urlBusca = String.Concat(urlBusca, "&buscaID=", buttonBusca.ClientID);
                WebControl webControl = (WebControl)buttonBusca;
                webControl.Attributes.Add("alt", urlBusca);
                webControl.Attributes.Add("title", tituloFrame);
                sb.Append(String.Format("\tvar {0}_result = new Array();", buttonBusca.ClientID));
                sb.Append("\r\n");
                sb.Append(String.Format("\tbuscaDinamica('#{0}', {1}, {2});", buttonBusca.ClientID, width, height));
                sb.Append("\r\n");
                foreach (string resultID in resultsID)
                {
                    sb.Append("\r\n");
                    sb.Append(String.Format("\t{0}_result.push('{1}');", buttonBusca.ClientID, resultID));
                }
                //Registra script
                var scriptManager = ScriptManager.GetCurrent(page);
                if (scriptManager != null && scriptManager.IsInAsyncPostBack)
                    ScriptManager.RegisterStartupScript(page, typeof(Page), buttonBusca.ClientID, sb.ToString(), true);
                else
                    if (!page.ClientScript.IsStartupScriptRegistered(buttonBusca.ClientID))
                    page.ClientScript.RegisterStartupScript(page.GetType(), buttonBusca.ClientID, sb.ToString(), true);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna o resultado da busca para o pai
        /// </summary>
        /// <param name="page">Instância da página atual</param>
        /// <param name="buttonBuscaID">ID do botão, image ou link responsável pela busca</param>
        /// <param name="results">Array com o resultado na ordem que foi inserido os controles do pai</param>
        public static void SetScriptRetornoBusca(Page page, string buttonBuscaID, string[] results)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("<script type=\"text/javascript\">");
                for (int i = 0; i < results.Length; i++)
                {
                    sb.Append("\r\n");
                    sb.Append(String.Format("$(parent.document).find('#' + parent.{0}_result[{1}]).attr('value', '{2}');", buttonBuscaID, i, results[i]));
                }
                sb.Append("\r\n");
                sb.Append(String.Format("parent.fecharBusca('#ifrm{0}');", buttonBuscaID));
                sb.Append("\r\n");
                sb.Append("</script>");
                //Registra script
                var scriptManager = ScriptManager.GetCurrent(page);
                if (scriptManager != null && scriptManager.IsInAsyncPostBack)
                {
                    ScriptManager.RegisterStartupScript(page, typeof(Page), buttonBuscaID, sb.ToString(), false);
                }
                else
                {
                    if (!page.ClientScript.IsStartupScriptRegistered(buttonBuscaID))
                        page.ClientScript.RegisterStartupScript(page.GetType(), buttonBuscaID, sb.ToString());
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Faz upload de uma imagem, redimensionando-a no tamanho informado.
        /// </summary>
        /// <param name="maxSize"> tamanho maximo que o arquivo pode ter</param>
        /// <param name="nomePasta"> pasta pra onde o arquivo vai ser copiado</param>
        /// <param name="nomeArquivo">nome que o arquivo tera na pasta</param>
        /// <param name="postedFile">local de origem do arquivo</param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <returns></returns>
        public static bool SaveThumbnailImage
        (
            Int64 maxSize
            , string nomePasta
            , string nomeArquivo
            , HttpPostedFile postedFile
            , Int32 Width
            , Int32 Height
        )
        {
            try
            {
                if (postedFile != null)
                {
                    string fileName = nomePasta;

                    // Cria diretório, caso não exista.
                    if (CreateDir(fileName))
                    {
                        if (!fileName.EndsWith("\\"))
                            fileName += "\\";

                        fileName += nomeArquivo;

                        // Checa extensão da imagem.
                        if (CheckTypeImage(Path.GetExtension(postedFile.FileName)))
                        {
                            // Checa tamanho da imagem.
                            if (CheckSize(maxSize, postedFile.ContentLength))
                            {
                                // Redimensiona imagem.
                                System.Drawing.Image.GetThumbnailImageAbort myCallback =
                                    new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
                                System.Drawing.Image img = System.Drawing.Image.FromStream(postedFile.InputStream);
                                System.Drawing.Image thumb = img.GetThumbnailImage(Width, Height, myCallback, IntPtr.Zero);
                                thumb.Save(fileName, ImageFormat.Png);

                                img.Dispose();
                                thumb.Dispose();
                                return true;
                            }
                            else
                            {
                                throw new ValidationException("Por favor, selecione arquivos com até " + maxSize + " Kb.");
                            }
                        }
                        else
                        {
                            throw new ValidationException("Tipo de imagem inválida!");
                        }
                    }
                }
                return false;
            }
            catch
            {
                throw;
            }
        }

        public static bool SaveImage
        (
            Int64 maxSize
            , string nomePasta
            , string nomeArquivo
            , HttpPostedFile postedFile
        )
        {
            try
            {
                if (postedFile != null)
                {
                    if (ValidaArquivoImagem(nomeArquivo, postedFile.ContentLength, maxSize))
                    {
                        string fileName = nomePasta;

                        // Cria diretório, caso não exista.
                        if (CreateDir(fileName))
                        {
                            fileName = Path.Combine(fileName, nomeArquivo);
                            System.Drawing.Image img = System.Drawing.Image.FromStream(postedFile.InputStream);
                            img.Save(fileName, ImageFormat.Png);
                            img.Dispose();
                            return true;
                        }
                    }
                }
                return false;
            }
            catch
            {
                throw;
            }

        }

        private static bool ValidaArquivoImagem(string fileName, int ContentLength, Int64 maxSize)
        {
            return CheckTypeImage(Path.GetExtension(fileName))
                           && (CheckSize(maxSize, ContentLength));
        }

        /// <summary>
        /// Verifica se existe o arquivo informado.
        /// </summary>
        /// <param name="arquivo"></param>
        /// <returns></returns>
        public static bool ExisteArquivo(string arquivo)
        {
            return File.Exists(arquivo);
        }

        /// <summary>
        /// Cria a pasta caso não exista.
        /// </summary>
        /// <param name="path">Server path</param>
        /// <returns>if not error true</returns>
        private static bool CreateDir(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Checa se o tipo de imagem é válido.
        /// </summary>
        /// <returns></returns>
        private static bool CheckTypeImage(string type)
        {
            //Called method GetExtesion and verify if extension is valid.
            switch (type.ToLower())
            {
                case ".bm": return true;
                case ".bmp": return true;
                case ".jpe": return true;
                case ".jpg": return true;
                case ".jpeg": return true;
                case ".gif": return true;
                case ".png": return true;
                default:
                    throw new ValidationException("Tipo de imagem inválida!");
            }
        }

        /// <summary>
        /// Checa o tamanho do arquivo.
        /// </summary>
        /// <param name="max">Tamanho máximo arquivo</param>
        /// <param name="size">Tamanho do arquivo</param>
        /// <returns></returns>
        private static bool CheckSize(Int64 max, Int64 size)
        {
            bool ret = false;

            Int64 maxSize = max * 1024;
            if (size <= maxSize)
            {
                ret = true;
            }
            else
            {
                throw new ValidationException("Por favor, selecione arquivos com até " + maxSize + " Kb.");
            }
            return ret;
        }

        /// <summary>
        /// Method to callback
        /// </summary>
        /// <returns></returns>
        private static bool ThumbnailCallback()
        {
            return true;
        }

        /// <summary>
        /// Retorna a lista de erros de validação dos campos
        /// </summary>
        /// <param name="Entidade">Entidade que deseja verificar a lista de validações</param>
        /// <returns>Lista com os erros separados pela tag br</returns>
        public static string ErrosValidacao(CoreLibrary.Data.Common.Abstracts.Abstract_Entity Entidade)
        {
            string listaErros = "";

            foreach (CoreLibrary.Validation.ValidationErrors erro in Entidade.PropertiesErrorList)
            {
                listaErros += erro.Message + "<br />";
            }

            return listaErros;
        }

        /// <summary>
        /// Retorna a url do handler de imagens com o caminho da imagem na QueryString.
        /// </summary>
        /// <param name="caminhoImagem"></param>
        /// <returns></returns>
        public static string UrlImagem(string caminhoImagem)
        {
            string queryString = HttpContext.Current.Server.UrlEncode(caminhoImagem);

            return "~/imagem.ashx?picture=" + queryString;
        }

        /// <summary>
        /// Retorna a url do handler de imagens do core com o caminho da imagem na QueryString.
        /// </summary>
        /// <param name="urlCore"></param>
        /// <param name="caminhoImagem"></param>
        /// <returns></returns>
        public static string UrlImagemGestao(string urlCore, string caminhoImagem)
        {
            string queryString = HttpContext.Current.Server.UrlEncode(caminhoImagem);

            if (!urlCore.EndsWith("/"))
                urlCore += "/";

            return urlCore + "imagem.ashx?picture=" + queryString;
        }

        /// <summary>
        /// Decodifica a queryString.
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public static string DecodificaQueryString(string queryString)
        {
            return HttpContext.Current.Server.UrlDecode(queryString);
        }

        /// <summary>
        /// Retorna um objeto carregado de acordo com os valores do dataTable.
        /// </summary>
        /// <param name="dr">Linha do dataTable</param>
        /// <param name="entity">Entidade utilizada para carregar os valores</param>
        /// <returns>Uma entidade carregada com os valores</returns>
        public static object DataRowToEntity(DataRow dr, object entity)
        {
            Type tp = entity.GetType();

            // Preenche propriedades.
            PropertyInfo[] properties = tp.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in properties)
            {
                if (dr.Table.Columns.Contains(prop.Name))
                {
                    if (dr[prop.Name] != DBNull.Value)
                    {
                        var value = dr[prop.Name];

                        if (prop.PropertyType == typeof(string))
                        {
                            if (value.GetType() == typeof(DateTime))
                            {
                                DateTime date = Convert.ToDateTime(value);
                                prop.SetValue(entity, date.ToString("dd/MM/yyyy HH:mm:ss.fff"), null);
                            }
                            else
                            {
                                prop.SetValue(entity, value.ToString(), null);
                            }
                        }
                        else
                        {
                            prop.SetValue(entity, value, null);
                        }
                    }
                }
            }

            // Preenche variáveis.
            FieldInfo[] fields = tp.GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (FieldInfo field in fields)
            {
                if (dr.Table.Columns.Contains(field.Name))
                {
                    if (dr[field.Name] != DBNull.Value)
                        field.SetValue(entity, dr[field.Name]);
                }
            }

            return entity;
        }

        public static bool GravarErro(Exception ex)
        {
            try
            {
                string strSisID = CFG_ConfiguracaoBO.SelecionaValorPorChave("appSistemaID");
                int sis_id;

                if (!Int32.TryParse(strSisID, out sis_id))
                    sis_id = 1;

                LOG_Erros entity = new LOG_Erros();
                entity.sis_id = sis_id;
                entity.err_descricao = GetErrorMessage(ex);
                entity.err_erroBase = ex.GetBaseException().Message;
                entity.err_tipoErro = ex.GetBaseException().GetType().FullName;
                entity.err_dataHora = DateTime.Now;
                entity.err_machineName = Environment.MachineName;

                string strHostName;
                string clientIPAddress = "";
                try
                {
                    strHostName = System.Net.Dns.GetHostName();
                    clientIPAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(1).ToString();
                }
                catch { }

                entity.err_ip = String.IsNullOrEmpty(clientIPAddress) ? "0.0.0.0" : clientIPAddress;

                LOG_ErrosBO.Save(entity);
            }
            catch { }

            return false;
        }

        /// <summary>
        /// Limpa os caches que possuem a chave informada.
        /// </summary>
        /// <param name="chave">Chave do cache (pode ser apenas um trecho da chave).</param>
        /// <param name="valor">Valor de algum item do cache.</param>
        public static void LimpaCache(string chave, string valor)
        {
            System.Collections.IDictionaryEnumerator myCache = HttpContext.Current.Cache.GetEnumerator();

            myCache.Reset();

            while (myCache.MoveNext())
            {
                string chaveCache = myCache.Key.ToString();
                if (chaveCache.Contains(chave))
                {
                    string[] valoresChave = chaveCache.Split('_');
                    foreach (string valorChave in valoresChave)
                    {
                        if (valorChave == valor)
                        {
                            HttpContext.Current.Cache.Remove(myCache.Key.ToString());
                            break;
                        }
                    }
                }
            }
        }

        #endregion Métodos
    }

    /// <summary>
    /// Classe utilizada para controlar os nomes dos javascripts das páginas.
    /// </summary>
    public static class ArquivoJS
    {
        /// <summary>
        /// Url do CoreSSO
        /// </summary>
        private static string urlCore;

        private static string UrlCoreSSO
        {
            get
            {
                if (string.IsNullOrEmpty(urlCore))
                    urlCore = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.URL_ADMINISTRATIVO);

                if (string.IsNullOrEmpty(urlCore))
                    urlCore = "~";

                return urlCore;
            }
        }

        //Pastas dos scripts
        public static readonly string PastaScriptRaiz = UrlCoreSSO + "/Includes/";

        private static readonly string PastaUtil = PastaScriptRaiz + "Util/";
        private static readonly string PastaJQuery = PastaScriptRaiz + "JQuery/";

        //Padrao - JQuery
        public static readonly string JQueryCore = PastaJQuery + "jquery-1.5.1.min.js";

        public static readonly string JQueryUI = PastaJQuery + "jquery-ui-1.8.12.custom.min.js";
        public static readonly string JQueryScrollTo = PastaJQuery + "jquery.scrollTo-1.4.2-min.js";
        public static readonly string JQueryValidation = PastaJQuery + "jquery.validation-min.js";
        public static readonly string JqueryMask = PastaJQuery + "jquery.meio.mask-min.js";
        public static readonly string Json = PastaJQuery + "json2.min.js";
        public static readonly string UiAriaTabs = PastaJQuery + "ui.ariaTabs.js";
        public static readonly string JQueryBlockUI = PastaJQuery + "jquery.blockUI.js";
        public static readonly string JQueryTableSorter = PastaJQuery + "jquery.tablesorter.min.js";

        //Util
        public static readonly string Init = PastaUtil + "Init.js";

        public static readonly string Util = PastaUtil + "Util.js";
        public static readonly string BuscaDinamica = PastaUtil + "jsBuscaDinamica.js";
        public static readonly string CamposData = PastaUtil + "jsCamposData.js";
        public static readonly string ExitPageConfirm = PastaUtil + "jsExitPageConfirm.js";
        public static readonly string JqueryFixer = PastaUtil + "jquery.fixer.js";
        public static readonly string MascarasCampos = PastaUtil + "jsMascarasCampos.js";
        public static readonly string MsgConfirmBtn = PastaUtil + "jsMsgConfirmBtn.js";
        public static readonly string MsgConfirmExclusao = PastaUtil + "jsMsgConfirmExclusao.js";
        public static readonly string Tabs = PastaUtil + "jsTabs.js";
        public static readonly string AutiFill = PastaUtil + "autofill-event.js";

        //Auto contraste
        public static readonly string StylesheetToggle = PastaScriptRaiz + "stylesheetToggle.js";
    }
}