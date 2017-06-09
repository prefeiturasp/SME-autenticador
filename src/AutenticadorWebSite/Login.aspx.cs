using Autenticador.BLL;
using Autenticador.Entities;
using Autenticador.Web.WebProject;
using Autenticador.Web.WebProject.Authentication;
using CoreLibrary.Validation.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.UI;

public partial class Login : MotherPage
{
    protected int GetSistemaID_QueryString
    {
        get
        {
            int sis_idQueryString = 0;
            if (!String.IsNullOrEmpty(Request.QueryString["sis"]))
            {
                Int32.TryParse(Request.QueryString["sis"], out sis_idQueryString);
            }

            return sis_idQueryString;
        }
    }

    [WebMethod]
    public static bool ValidarSenhaAtual(string senhaAtual, Guid usu_id)
    {
        SYS_Usuario entityUsuario = new SYS_Usuario { usu_id = usu_id };
        SYS_UsuarioBO.GetEntity(entityUsuario);

        eCriptografa criptografia = (eCriptografa)Enum.Parse(typeof(eCriptografa), Convert.ToString(entityUsuario.usu_criptografia), true);
        if (!Enum.IsDefined(typeof(eCriptografa), criptografia))
            criptografia = eCriptografa.SHA512;

        return UtilBO.EqualsSenha(entityUsuario.usu_senha, UtilBO.CriptografarSenha(senhaAtual, criptografia), criptografia);
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string formatoSenha = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.FORMATO_SENHA_USUARIO);
            string tamanhoSenha = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.TAMANHO_SENHA_USUARIO);

            string mensagemFormato = SYS_MensagemSistemaBO.RetornaValor(SYS_MensagemSistemaChave.MeusDadosMensagemValidacaoComplexidadeSenhaFormato);

            if (!string.IsNullOrEmpty(mensagemFormato))
            {
                revNovaSenhaFormato.ErrorMessage = mensagemFormato;
            }

            string mensagemTamanho = SYS_MensagemSistemaBO.RetornaValor(SYS_MensagemSistemaChave.MeusDadosMensagemValidacaoComplexidadeSenhaTamanho);

            if (!string.IsNullOrEmpty(mensagemTamanho))
            {
                revNovaSenhaTamanho.ErrorMessage = mensagemTamanho;
            }
            else
            {
                revNovaSenhaTamanho.ErrorMessage = String.Format(revNovaSenhaTamanho.ErrorMessage, UtilBO.GetMessageTamanhoByRegex(revNovaSenhaTamanho.ValidationExpression));
            }

            string mensagemComplexNovaSenha = SYS_MensagemSistemaBO.RetornaValor(SYS_MensagemSistemaChave.MeusDadosMensagemComplexidadeNovaSenha);

            if (!string.IsNullOrEmpty(mensagemComplexNovaSenha))
            {
                lblMsnNovaSenha.Text = mensagemComplexNovaSenha;
            }
            else
            {
                lblMsnNovaSenha.Text = String.Format(lblMsnNovaSenha.Text, UtilBO.GetMessageTamanhoByRegex(revNovaSenhaTamanho.ValidationExpression));
            }

            revNovaSenhaFormato.ValidationExpression = formatoSenha;
            revNovaSenhaTamanho.ValidationExpression = tamanhoSenha;
            _btnEsqueceuSenha.Visible = String.IsNullOrEmpty(SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.REMOVER_OPCAO_ESQUECISENHA)) || !Boolean.Parse(SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.REMOVER_OPCAO_ESQUECISENHA));

            if (!string.IsNullOrEmpty(mensagemFormato))
            {
                revNovaSenhaFormatoEsqueci.ErrorMessage = mensagemFormato;
            }

            if (!string.IsNullOrEmpty(mensagemTamanho))
            {
                revNovaSenhaTamanhoEsqueci.ErrorMessage = mensagemTamanho;
            }
            else
            {
                revNovaSenhaTamanhoEsqueci.ErrorMessage = String.Format(revNovaSenhaTamanhoEsqueci.ErrorMessage, UtilBO.GetMessageTamanhoByRegex(revNovaSenhaTamanhoEsqueci.ValidationExpression));
            }

            if (!string.IsNullOrEmpty(mensagemComplexNovaSenha))
            {
                lblNovaSenhaEsqueciMsg.Text = mensagemComplexNovaSenha;
            }
            else
            {
                lblNovaSenhaEsqueciMsg.Text = String.Format(lblNovaSenhaEsqueciMsg.Text, UtilBO.GetMessageTamanhoByRegex(revNovaSenhaTamanhoEsqueci.ValidationExpression));
            }
            revNovaSenhaFormatoEsqueci.ValidationExpression = formatoSenha;
            revNovaSenhaTamanhoEsqueci.ValidationExpression = tamanhoSenha;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager sm = ScriptManager.GetCurrent(this);
        if (sm != null)
        {
            sm.Scripts.Add(new ScriptReference(ArquivoJS.CamposData));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.JqueryMask));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.MascarasCampos));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.JQueryValidation));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.PastaScriptRaiz + "Telas/jsLogin.js"));

            if (__SessionWEB.TemaPadraoLogado.tep_tipoLogin == (byte)CFG_TemaPadrao.eTipoLogin.SobrescreveLabel)
            {
                sm.Scripts.Add(new ScriptReference(ArquivoJS.PastaScriptRaiz + "Telas/jsLoginRio.js"));
            }

            if (__SessionWEB.TemaPadraoLogado.tep_tipoLogin == (byte)CFG_TemaPadrao.eTipoLogin.CorrigeLayout)
            {
                sm.Scripts.Add(new ScriptReference(ArquivoJS.PastaScriptRaiz + "Telas/jsLoginIntranetSme.js"));
            }
        }

        fdsMensagem.Visible = false;
        fdsLogin.Attributes.Remove("style");

        if (!IsPostBack)
        {
            try
            {
                if (!String.IsNullOrEmpty((SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.MENSAGEM_ALERTA_PRELOGIN))))
                {
                    spnMensagemUsuario.InnerHtml = HttpUtility.HtmlDecode(SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.MENSAGEM_ALERTA_PRELOGIN));
                    fdsMensagem.Visible = true;
                    fdsLogin.Attributes["style"] = "display:none;";
                    btnFechar.OnClientClick = "$('.fdsMensagem').hide();" +
                                              "$('#" + fdsLogin.ClientID + "').show();" +
                                              "$('#login').find('select,input').first().focus();" +
                                              "return false;";
                    btnFechar.Focus();
                }
                else
                {
                    Page.Form.DefaultButton = _btnEntrar.UniqueID;
                    fdsMensagem.Visible = false;
                }

                UCComboEntidade1.Inicialize("Entidade *");
                UCComboEntidade1._EnableValidator = true;
                UCComboEntidade1._ValidationGroup = "Login";
                UCComboEntidade1._LoadBy_SistemaEntidade(Guid.Empty, 1);

                UCComboEntidade2.Inicialize("Entidade *");
                UCComboEntidade2._EnableValidator = true;
                UCComboEntidade2._ValidationGroup = "EsqueciSenha";
                UCComboEntidade2._LoadBy_SistemaEntidade(Guid.Empty, 1);

                if (UserIsAuthenticated())
                {
                    RedirecionarLogin(__SessionWEB.__UsuarioWEB.Usuario.usu_id);
                }
            }
            catch (Exception ex)
            {
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o sistema.", UtilBO.TipoMensagem.Erro);

                ApplicationWEB._GravaErro(ex);
            }
        }

        UtilBO.RegistraGATC(this.Page);
    }

    protected void Page_PreRenderComplete(object sender, EventArgs e)
    {
        if ((!IsPostBack) &&
            (UCComboEntidade1._Combo.SelectedValue != Guid.Empty.ToString()))
        {
            Page.Form.DefaultFocus = _txtLogin.ClientID;

            UCComboEntidade1.Visible = false;
        }
        else
        {
            Page.Form.DefaultFocus = UCComboEntidade1._Combo.ClientID;
        }
    }

    protected void _btnEntrar_Click(object sender, EventArgs e)
    {
        if (ValidarLogin())
        {
            try
            {
                SYS_Usuario entityUsuario = new SYS_Usuario
                {
                    ent_id = new Guid(UCComboEntidade1._Combo.SelectedValue)
                    ,
                    usu_login = _txtLogin.Text
                    ,
                    usu_senha = _txtSenha.Text
                };
                SYS_UsuarioAutenticacaoExternaBO SYS_AutenticacaoExterna = new SYS_UsuarioAutenticacaoExternaBO();
                LoginStatus status = SYS_AutenticacaoExterna.ValidarAutenticacao(entityUsuario);
                bool precisaCaptcha = VerificaDigitacaoCaptcha(entityUsuario.usu_id);

                if (precisaCaptcha)
                {
                    _lblMessage.Text = UtilBO.GetErroMessage("É necessário informar o código de confirmação.", UtilBO.TipoMensagem.Alerta);
                    _txtLogin.Focus();
                }
                else
                {
                    switch (status)
                    {
                        case LoginStatus.Erro:
                            {
                                ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Login,
                                                                "Erro ao tentar entrar no sistema.",
                                                                entityUsuario.usu_login);
                                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar entrar no sistema.",
                                                                         UtilBO.TipoMensagem.Erro);
                                _txtLogin.Focus();
                                break;
                            }
                        case LoginStatus.Bloqueado:
                            {
                                ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Login, "Usuário bloqueado.",
                                                                entityUsuario.usu_login);
                                _lblMessage.Text = UtilBO.GetErroMessage("Usuário bloqueado.",
                                                                         UtilBO.TipoMensagem.Alerta);
                                _txtLogin.Focus();
                                break;
                            }
                        case LoginStatus.NaoEncontrado:
                            {
                                ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Login, "Usuário não encontrado.",
                                                                entityUsuario.usu_login);

                                _lblMessage.Text = UtilBO.GetErroMessage("Usuário e/ou senha inválidos.",
                                                                         UtilBO.TipoMensagem.Alerta);
                                _txtLogin.Focus();
                                break;
                            }
                        case LoginStatus.SenhaInvalida:
                            {
                                ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Login, "Senha inválida.",
                                                                entityUsuario.usu_login);

                                SalvaExibeCaptcha(entityUsuario.usu_id);

                                _lblMessage.Text = UtilBO.GetErroMessage("Usuário e/ou senha inválidos.",
                                                                         UtilBO.TipoMensagem.Alerta);
                                _txtLogin.Focus();

                                break;
                            }

                        case LoginStatus.Expirado:
                            {
                                ConfigurarTelaSenhaExpirada(entityUsuario);
                                break;
                            }
                        case LoginStatus.Sucesso:
                            {
                                SYS_UsuarioFalhaAutenticacaoBO.ZeraFalhaAutenticacaoUsuario(entityUsuario.usu_id);

                                ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Login, "Login efetuado com sucesso.");

                                SignHelper.AutenticarUsuario(entityUsuario);

                                LoadSession(entityUsuario);

                                RedirecionarLogin(entityUsuario.usu_id);

                                break;
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar entrar no sistema.", UtilBO.TipoMensagem.Erro);
            }
        }
    }

    protected void _btnEsqueceuSenha_Click(object sender, EventArgs e)
    {
        try
        {
            chkPossuiEmail.Visible = SYS_ParametroBO.ParametroValorBooleano(SYS_ParametroBO.eChave.PERMITIR_DTNASCIMENTO_CPF_ESQUECISENHA);
            chkPossuiEmail.Checked = true;

            ConfigurarEsqueciSenhaEmail();

            _lblMessageEsqueciSenha.Visible = false;
            _updEsqueciSenha.Update();
            ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Login, "Usuário solicitou uma nova senha.");
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "EsqueciSenhaAbrir", "$('#divEsqueciSenha').dialog('open'); ", true);
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o sistema.", UtilBO.TipoMensagem.Erro);
        }
    }

    protected void chkPossuiEmail_CheckedChanged(object sender, EventArgs e)
    {
        ConfigurarEsqueciSenhaEmail();
    }

    protected void _btnEnviar_Click(object sender, EventArgs e)
    {
        if (ValidarEsqueciSenha() && Page.IsValid)
        {
            try
            {
                DataTable dt = chkPossuiEmail.Checked ?
                    SYS_UsuarioBO.GetSelectBy_ent_id_usu_email(new Guid(UCComboEntidade2._Combo.SelectedValue), _txtEmail.Text) :
                    SYS_UsuarioBO.SelecionaPorCPFDataNascimento(new Guid(UCComboEntidade2._Combo.SelectedValue), txtCPF.Text, Convert.ToDateTime(txtDtNasc.Text));

                if (dt.Rows.Count == 0)
                {
                    string log = chkPossuiEmail.Checked ?
                        "Erro no envio de senha. Usuário não encontrado." :
                        "Erro na alteração de senha. Usuário não encontrado.";
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Login, log);

                    string mensagem = SYS_MensagemSistemaBO.RetornaValor(SYS_MensagemSistemaChave.LoginMensagemUsuarioNaoEncontrado);

                    _lblMessage.Text = UtilBO.GetErroMessage(string.IsNullOrEmpty(mensagem) ? "Usuário não encontrado." : mensagem, UtilBO.TipoMensagem.Alerta);
                }
                else
                {
                    byte usu_integracaoAD = Convert.ToByte(dt.Rows[0]["usu_integracaoAD"]);
                    byte usu_situacao = Convert.ToByte(dt.Rows[0]["usu_situacao"]);
                    Guid usu_id = new Guid(dt.Rows[0]["usu_id"].ToString());
                    string pes_nome = dt.Rows[0]["pes_nome"].ToString();

                    switch (usu_integracaoAD)
                    {
                        case (byte)SYS_UsuarioBO.eIntegracaoAD.NaoIntegrado:
                            if (chkPossuiEmail.Checked)
                                EnviaNovaSenhaEmail(usu_id, usu_situacao, pes_nome, usu_integracaoAD);
                            else
                            {
                                ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Login, "Não é possível alterar a senha de usuários não ligados no Active Directory por data de nascimento e CPF.");
                                _lblMessage.Text = UtilBO.GetErroMessage("Não é possível alterar a senha de usuários não ligados no Active Directory por data de nascimento e CPF.", UtilBO.TipoMensagem.Alerta);
                            }
                            break;

                        case (byte)SYS_UsuarioBO.eIntegracaoAD.IntegradoAD:
                            ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Login, "Não é possível recuperar/alterar a senha pois o usuário solicitado está ligado no Active Directory.");
                            _lblMessage.Text = UtilBO.GetErroMessage("Não é possível recuperar/alterar a senha pois o usuário solicitado está ligado no Active Directory, contate o administrador de rede do seu domínio.", UtilBO.TipoMensagem.Alerta);
                            break;

                        case (byte)SYS_UsuarioBO.eIntegracaoAD.IntegradoADReplicacaoSenha:
                            if (chkPossuiEmail.Checked)
                                EnviaNovaSenhaEmail(usu_id, usu_situacao, pes_nome, usu_integracaoAD);
                            else
                                AlterarSenhaEsqueci(usu_id, usu_situacao, usu_integracaoAD);
                            break;
                    };
                }

                ScriptManager.RegisterClientScriptBlock(this, GetType(), "EsqueciSenha", "$('#divEsqueciSenha').dialog('close');", true);
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                _lblMessageEsqueciSenha.Text = chkPossuiEmail.Checked ?
                    UtilBO.GetErroMessage("Erro ao tentar enviar e-mail com a senha para o usuário.", UtilBO.TipoMensagem.Erro) :
                    UtilBO.GetErroMessage("Erro ao tentar alterar a senha para o usuário.", UtilBO.TipoMensagem.Erro);
            }
        }
        else
        {
            _updEsqueciSenha.Update();
        }
    }

    protected void _btnSalvar_Click(object sender, EventArgs e)
    {
        if (ValidarAlterarSenha())
        {
            try
            {
                SYS_Usuario entityUsuario = new SYS_Usuario
                {
                    ent_id = new Guid(UCComboEntidade1._Combo.SelectedValue)
                    ,
                    usu_login = _txtLogin.Text
                };
                SYS_UsuarioBO.GetSelectBy_ent_id_usu_login(entityUsuario);

                eCriptografa criptografia = (eCriptografa)Enum.Parse(typeof(eCriptografa), Convert.ToString(entityUsuario.usu_criptografia), true);
                if (!Enum.IsDefined(typeof(eCriptografa), criptografia))
                    criptografia = eCriptografa.SHA512;

                if (!UtilBO.EqualsSenha(entityUsuario.usu_senha, UtilBO.CriptografarSenha(_txtSenhaAtual.Text, criptografia), criptografia))
                {
                    string mensagemSenhaAtualInvalida = SYS_MensagemSistemaBO.RetornaValor(SYS_MensagemSistemaChave.MeusDadosMensagemSenhaAtualIncorreta);

                    _lblMessageAlterarSenha.Text = UtilBO.GetErroMessage(string.IsNullOrEmpty(mensagemSenhaAtualInvalida) ? "Senha atual inválida." : mensagemSenhaAtualInvalida, UtilBO.TipoMensagem.Erro);
                    _updAlterarSenha.Update();

                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Login, "Erro ao alterar senha. Senha atual inválida.");
                }
                else
                {
                    entityUsuario.usu_situacao = 1;
                    entityUsuario.usu_senha = _txtNovaSenha.Text;
                    entityUsuario.usu_dataAlteracao = DateTime.Now;
                    entityUsuario.usu_criptografia = (byte)eCriptografa.SHA512;
                    SYS_UsuarioBO.AlterarSenhaAtualizarUsuario(entityUsuario, entityUsuario.usu_integracaoAD == (byte)SYS_UsuarioBO.eIntegracaoAD.IntegradoADReplicacaoSenha);

                    LoadSession(entityUsuario);
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Login, "Senha alterada com sucesso.");

                    string mensagemSenhaAlterada = SYS_MensagemSistemaBO.RetornaValor(SYS_MensagemSistemaChave.LoginMensagemSenhaAlteradaSucesso);

                    __SessionWEB.PostMessages = UtilBO.GetErroMessage(string.IsNullOrEmpty(mensagemSenhaAlterada) ? "Senha alterada com sucesso." : mensagemSenhaAlterada, UtilBO.TipoMensagem.Sucesso);

                    SYS_UsuarioFalhaAutenticacaoBO.ZeraFalhaAutenticacaoUsuario(entityUsuario.usu_id);

                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Login, "Login efetuado com sucesso.");

                    SignHelper.AutenticarUsuario(entityUsuario);

                    LoadSession(entityUsuario);

                    RedirecionarLogin(entityUsuario.usu_id);
                }
            }
            catch (DuplicateNameException ex)
            {
                _lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Alerta);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlterarSenhaErro", "$('#divAlterarSenha').dialog('close');", true);
            }
            catch (ArgumentException ex)
            {
                _lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Alerta);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlterarSenhaErro", "$('#divAlterarSenha').dialog('close');", true);
            }
            catch (ValidationException ex)
            {
                _lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Alerta);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlterarSenhaErro", "$('#divAlterarSenha').dialog('close');", true);
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);

                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar alterar a senha do usuário.", UtilBO.TipoMensagem.Erro);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlterarSenhaErro", "$('#divAlterarSenha').dialog('close');", true);
            }
        }
        else
        {
            _updAlterarSenha.Update();
        }
    }

    private void SalvaExibeCaptcha(Guid usu_id)
    {
        if (SYS_ParametroBO.ParametroValorBooleano(SYS_ParametroBO.eChave.UTILIZAR_CAPTCHA_FALHA_AUTENTICACAO))
        {
            SYS_UsuarioFalhaAutenticacao entFalha = SYS_UsuarioFalhaAutenticacaoBO.InsereFalhaAutenticacaoUsuario(usu_id);

            if (entFalha.ufl_qtdeFalhas >= SYS_ParametroBO.Parametro_QtdeFalhasExibirCaptcha())
            {
                divCaptcha.Visible = true;
            }
        }
    }

    private bool VerificaDigitacaoCaptcha(Guid usu_id)
    {
        if (divCaptcha.Visible || !SYS_ParametroBO.ParametroValorBooleano(SYS_ParametroBO.eChave.UTILIZAR_CAPTCHA_FALHA_AUTENTICACAO))
        {
            return false;
        }

        SYS_UsuarioFalhaAutenticacao entFalha = SYS_UsuarioFalhaAutenticacaoBO.GetEntity
            (new SYS_UsuarioFalhaAutenticacao { usu_id = usu_id });

        int minutosDiferenca = SYS_ParametroBO.Parametro_IntervaloMinutosFalhaAutenticacao();

        if (entFalha.ufl_qtdeFalhas >= SYS_ParametroBO.Parametro_QtdeFalhasExibirCaptcha()
            && entFalha.ufl_dataUltimaTentativa.AddMinutes(minutosDiferenca) >= DateTime.Now)
        {
            divCaptcha.Visible = true;
        }

        return divCaptcha.Visible;
    }

    private bool ValidarLogin()
    {
        if (UCComboEntidade1._Combo.SelectedValue == Guid.Empty.ToString())
        {
            _lblMessage.Text = UtilBO.GetErroMessage("Entidade é obrigatório.", UtilBO.TipoMensagem.Alerta);
            return false;
        }

        if (string.IsNullOrEmpty(_txtLogin.Text.Trim()))
        {
            _lblMessage.Text = UtilBO.GetErroMessage("Login é obrigatório.", UtilBO.TipoMensagem.Alerta);
            return false;
        }

        if (string.IsNullOrEmpty(_txtSenha.Text.Trim()))
        {
            _lblMessage.Text = UtilBO.GetErroMessage("Senha é obrigatório.", UtilBO.TipoMensagem.Alerta);
            return false;
        }

        if (divCaptcha.Visible && string.IsNullOrEmpty(txtCodigoConfirmacao.Text.Trim()))
        {
            _lblMessage.Text = UtilBO.GetErroMessage("Código de confirmação é obrigatório.", UtilBO.TipoMensagem.Alerta);
            return false;
        }

        if (divCaptcha.Visible)
        {
            string valorCapthca = (Session["CaptchaValue"] ?? "").ToString();
            if (!txtCodigoConfirmacao.Text.Equals(valorCapthca, StringComparison.OrdinalIgnoreCase))
            {
                _lblMessage.Text = UtilBO.GetErroMessage("O código de confirmação está incorreto.", UtilBO.TipoMensagem.Alerta);
                txtCodigoConfirmacao.Text = "";
                return false;
            }
        }

        txtCodigoConfirmacao.Text = "";
        return true;
    }

    private bool ValidarEsqueciSenha()
    {
        if (UCComboEntidade2._Combo.SelectedValue == Guid.Empty.ToString())
        {
            _lblMessageEsqueciSenha.Text = UtilBO.GetErroMessage("Entidade é obrigatório.", UtilBO.TipoMensagem.Alerta);
            return false;
        }

        if (chkPossuiEmail.Checked && string.IsNullOrEmpty(_txtEmail.Text.Trim()))
        {
            _lblMessageEsqueciSenha.Text = UtilBO.GetErroMessage("E-mail é obrigatório.", UtilBO.TipoMensagem.Alerta);
            return false;
        }

        if (!chkPossuiEmail.Checked && string.IsNullOrEmpty(txtDtNasc.Text))
        {
            _lblMessageEsqueciSenha.Text = UtilBO.GetErroMessage("Data de nascimento é obrigatório.", UtilBO.TipoMensagem.Alerta);
            return false;
        }

        if (!chkPossuiEmail.Checked && string.IsNullOrEmpty(txtCPF.Text))
        {
            _lblMessageEsqueciSenha.Text = UtilBO.GetErroMessage("CPF é obrigatório.", UtilBO.TipoMensagem.Alerta);
            return false;
        }

        if (!chkPossuiEmail.Checked && !UtilBO._ValidaCPF(txtCPF.Text))
        {
            _lblMessageEsqueciSenha.Text = UtilBO.GetErroMessage("CPF é inválido.", UtilBO.TipoMensagem.Alerta);
            return false;
        }

        return true;
    }

    private bool ValidarAlterarSenha()
    {
        if (string.IsNullOrEmpty(_txtSenhaAtual.Text.Trim()))
        {
            _lblMessageAlterarSenha.Text = UtilBO.GetErroMessage("Senha atual é obrigatório.", UtilBO.TipoMensagem.Alerta);
            return false;
        }

        if (string.IsNullOrEmpty(_txtNovaSenha.Text.Trim()))
        {
            _lblMessageAlterarSenha.Text = UtilBO.GetErroMessage("Nova senha é obrigatório.", UtilBO.TipoMensagem.Alerta);
            return false;
        }

        if (string.IsNullOrEmpty(_txtConfNovaSenha.Text.Trim()))
        {
            _lblMessageAlterarSenha.Text = UtilBO.GetErroMessage("Confirmar nova senha é obrigatório.", UtilBO.TipoMensagem.Alerta);
            return false;
        }

        return true;
    }

    private void RedirecionarLogin(Guid usu_id)
    {
        string caminhoRedirecionar = String.Concat(ApplicationWEB._DiretorioVirtual, "Sistema.aspx");

        if (GetSistemaID_QueryString > 0)
        {
            __SessionWEB.SistemaID_QueryString = GetSistemaID_QueryString;
        }

        if (__SessionWEB.SistemaID_QueryString > 0)
        {
            List<SYS_Sistema> listaSistemaPermissao = new List<SYS_Sistema>(SYS_SistemaBO.GetSelectBy_usu_id(usu_id));

            if (listaSistemaPermissao.Exists(p => p.sis_id == __SessionWEB.SistemaID_QueryString))
            {
                SYS_Sistema sistema = new SYS_Sistema { sis_id = __SessionWEB.SistemaID_QueryString };
                SYS_SistemaBO.GetEntity(sistema);

                if (!sistema.IsNew)
                    caminhoRedirecionar = sistema.sis_caminho;
            }
        }

        Response.Redirect(caminhoRedirecionar, false);
        HttpContext.Current.ApplicationInstance.CompleteRequest();
    }

    private void ConfigurarEsqueciSenhaEmail()
    {
        divEmail.Visible = chkPossuiEmail.Checked || !chkPossuiEmail.Visible;
        divDtNascCpf.Visible = divNovaSenhaEsqueci.Visible = !divEmail.Visible;

        _txtEmail.Text = txtDtNasc.Text = txtCPF.Text = string.Empty;

        string msgEmail = SYS_MensagemSistemaBO.RetornaValor(SYS_MensagemSistemaChave.EsqueciSenhaMensagemInformacaoEmail);

        if (!string.IsNullOrEmpty(msgEmail))
        {
            lblEmailInfo.Text = msgEmail + "<br /><br />";
        }

        if (UCComboEntidade1._Combo.SelectedValue != Guid.Empty.ToString())
        {
            UCComboEntidade2._Combo.SelectedValue = UCComboEntidade1._Combo.SelectedValue;

            if (divEmail.Visible)
                _txtEmail.Focus();
            else
                txtDtNasc.Focus();
            UCComboEntidade2.Visible = false;
        }
        else
        {
            UCComboEntidade2._Combo.SelectedValue = Guid.Empty.ToString();
            UCComboEntidade2.Visible = true;
            UCComboEntidade2._Combo.Focus();
        }

        _btnEnviar.Text = chkPossuiEmail.Checked ? "Enviar" : "Alterar senha";
    }

    private bool ValidarAlterarSenhaEsqueci()
    {
        if (string.IsNullOrEmpty(txtNovaSenhaEsqueci.Text.Trim()))
        {
            _lblMessageEsqueciSenha.Text = UtilBO.GetErroMessage("Nova senha é obrigatório.", UtilBO.TipoMensagem.Alerta);
            return false;
        }

        if (string.IsNullOrEmpty(txtConfirmarSenhaEsqueci.Text.Trim()))
        {
            _lblMessageEsqueciSenha.Text = UtilBO.GetErroMessage("Confirmar nova senha é obrigatório.", UtilBO.TipoMensagem.Alerta);
            return false;
        }

        return true;
    }

    private void EnviaNovaSenhaEmail(Guid usu_id, byte usu_situacao, string pes_nome, byte usu_integracaoAD)
    {
        try
        {
            if (usu_situacao == (byte)SYS_UsuarioBO.eSituacao.Ativo || usu_situacao == (byte)SYS_UsuarioBO.eSituacao.Senha_Expirada)
            {
                SYS_Usuario usu = new SYS_Usuario { usu_id = usu_id };
                SYS_UsuarioBO.GetEntity(usu);

                usu.usu_situacao = (byte)SYS_UsuarioBO.eSituacao.Senha_Expirada;

                SYS_UsuarioBO.Save(usu, pes_nome, __SessionWEB.TituloGeral, ApplicationWEB._EmailHost, ApplicationWEB._EmailSuporte, ApplicationWEB.EmailRemetente);

                ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Login, "Senha enviada para o e-mail com sucesso.");

                string mensagemSucesso = SYS_MensagemSistemaBO.RetornaValor(SYS_MensagemSistemaChave.LoginMensagemSenhaEmailEnviadaSucesso);
                _lblMessage.Text = UtilBO.GetErroMessage(string.IsNullOrEmpty(mensagemSucesso) ? "Senha enviada para o e-mail com sucesso." : mensagemSucesso, UtilBO.TipoMensagem.Sucesso);
            }
            else if (usu_situacao == (byte)SYS_UsuarioBO.eSituacao.Padrao_Sistema)
            {
                ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Login, "Erro no envio de senha. Usuário padrão.");
                _lblMessage.Text = UtilBO.GetErroMessage("Usuário padrão.", UtilBO.TipoMensagem.Alerta);
            }
            else
            {
                ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Login, "Erro no envio de senha. Usuário bloqueado.");
                _lblMessage.Text = UtilBO.GetErroMessage("Usuário bloqueado.", UtilBO.TipoMensagem.Alerta);
            }
        }
        catch (DuplicateNameException ex)
        {
            _lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Alerta);
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "EsqueciSenhaErro", "$('#divEsqueciSenha').dialog('close');", true);
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar enviar e-mail com a senha para o usuário.", UtilBO.TipoMensagem.Erro);
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "EsqueciSenhaErro", "$('#divEsqueciSenha').dialog('close');", true);
        }
    }

    private void AlterarSenhaEsqueci(Guid usu_id, byte usu_situacao, byte usu_integracaoAD)
    {
        if (ValidarAlterarSenhaEsqueci())
        {
            try
            {
                if (usu_situacao == (byte)SYS_UsuarioBO.eSituacao.Ativo || usu_situacao == (byte)SYS_UsuarioBO.eSituacao.Senha_Expirada)
                {
                    SYS_Usuario entityUsuario = new SYS_Usuario
                    {
                        usu_id = usu_id
                    };
                    SYS_UsuarioBO.GetEntity(entityUsuario);

                    entityUsuario.usu_situacao = 1;
                    entityUsuario.usu_senha = txtNovaSenhaEsqueci.Text;
                    entityUsuario.usu_dataAlteracao = DateTime.Now;
                    SYS_UsuarioBO.AlterarSenhaAtualizarUsuario(entityUsuario, usu_integracaoAD == (byte)SYS_UsuarioBO.eIntegracaoAD.IntegradoADReplicacaoSenha);

                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Login, "Senha alterada com sucesso.");
                    _lblMessage.Text = UtilBO.GetErroMessage("Senha alterada com sucesso.", UtilBO.TipoMensagem.Sucesso);
                }
                else if (usu_situacao == (byte)SYS_UsuarioBO.eSituacao.Padrao_Sistema)
                {
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Login, "Erro na alteração de senha. Usuário padrão.");
                    _lblMessage.Text = UtilBO.GetErroMessage("Usuário padrão.", UtilBO.TipoMensagem.Alerta);
                }
                else
                {
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Login, "Erro na alteração de senha. Usuário bloqueado.");
                    _lblMessage.Text = UtilBO.GetErroMessage("Usuário bloqueado.", UtilBO.TipoMensagem.Alerta);
                }
            }
            catch (DuplicateNameException ex)
            {
                _lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Alerta);
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar alterar a senha do usuário.", UtilBO.TipoMensagem.Erro);
            }
        }
    }

    private void ConfigurarTelaSenhaExpirada(SYS_Usuario entityUsuario)
    {
        ScriptManager sm = ScriptManager.GetCurrent(this.Page);
        if (sm != null)
        {
            string script = String.Format(@"var usu_id = '{0}';", entityUsuario.usu_id);

            if (sm.IsInAsyncPostBack)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Usuario", script, true);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Usuario", script, true);
            }
        }

        string mensagem = SYS_MensagemSistemaBO.RetornaValor(SYS_MensagemSistemaChave.MeusDadosMensagemSenhaAtualIncorreta);
        if (!string.IsNullOrEmpty(mensagem))
        {
            cvSenhaAtual.ErrorMessage = mensagem;
        }

        mensagem = SYS_MensagemSistemaBO.RetornaValor(SYS_MensagemSistemaChave.MeusDadosMensagemConfirmarSenhaNaoIdentico);

        if (!string.IsNullOrEmpty(mensagem))
        {
            _cpvConfirmarSenha.ErrorMessage = mensagem;
        }

        mensagem = SYS_MensagemSistemaBO.RetornaValor(SYS_MensagemSistemaChave.MeusDadosMensagemSenhaAtualSenhaNovaDiferenca);

        if (!string.IsNullOrEmpty(mensagem))
        {
            CompareValidator1.ErrorMessage = mensagem;
        }

        _txtSenhaAtual.Text = string.Empty;
        _txtNovaSenha.Text = string.Empty;
        _txtConfNovaSenha.Text = string.Empty;
        _txtSenhaAtual.Focus();

        ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Login, "Senha expirada.",
                                        entityUsuario.usu_login);
        ScriptManager.RegisterStartupScript(this, GetType(), "AlterarSenha",
                                            "$(document).ready(function(){ $('#divAlterarSenha').dialog('open'); }); ",
                                            true);
    }
}