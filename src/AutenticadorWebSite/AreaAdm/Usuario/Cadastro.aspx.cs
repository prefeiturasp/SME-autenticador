using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autenticador.BLL;
using Autenticador.Entities;
using Autenticador.Web.WebProject;
using Autenticador.WebServices.Consumer;
using System.Linq;
using CoreLibrary.Validation.Exceptions;
using System.Text.RegularExpressions;

public partial class AreaAdm_Usuario_Cadastro : MotherPageLogado
{
    #region Propriedades

    //Armazena o índice no combo da opção de outros domínios.
    private int _VS_OutrosDominios
    {
        get
        {
            if (ViewState["_VS_OutrosDominios"] != null)
                return (int)ViewState["_VS_OutrosDominios"];
            return 0;
        }
        set
        {
            ViewState["_VS_OutrosDominios"] = value;
        }
    }

    private Guid _VS_usu_id
    {
        get
        {
            if (ViewState["_VS_usu_id"] != null)
                return new Guid(ViewState["_VS_usu_id"].ToString());
            return Guid.Empty;
        }
        set
        {
            ViewState["_VS_usu_id"] = value;
        }
    }

    private SortedDictionary<Guid, SYS_UsuarioBO.TmpGrupos> _VS_Grupos
    {
        get
        {
            if (ViewState["_VS_Grupos"] == null)
                ViewState["_VS_Grupos"] = new SortedDictionary<Guid, SYS_UsuarioBO.TmpGrupos>();
            return (SortedDictionary<Guid, SYS_UsuarioBO.TmpGrupos>)ViewState["_VS_Grupos"];
        }
    }

    private SortedDictionary<Guid, List<SYS_UsuarioBO.TmpEntidadeUA>> _VS_EntidadeUA
    {
        get
        {
            if (ViewState["_VS_EntidadeUA"] == null)
            {
                SortedDictionary<Guid, List<SYS_UsuarioBO.TmpEntidadeUA>> lt = new SortedDictionary<Guid, List<SYS_UsuarioBO.TmpEntidadeUA>>();
                ViewState["_VS_EntidadeUA"] = lt;
            }
            return (SortedDictionary<Guid, List<SYS_UsuarioBO.TmpEntidadeUA>>)ViewState["_VS_EntidadeUA"];
        }
    }

    private bool VS_ExistsIntegracaoExterna
    {
        get
        {
            if (ViewState["VS_ExistsIntegracaoExterna"] != null)
                return Convert.ToBoolean(ViewState["VS_ExistsIntegracaoExterna"]);
            return false;
        }
        set
        {
            ViewState["VS_ExistsIntegracaoExterna"] = value;
        }
    }

    private SYS_UsuarioBO.eIntegracaoAD integracaoAD
    {
        get
        {
            try
            {
                return (SYS_UsuarioBO.eIntegracaoAD)(Convert.ToByte(ddlUsuarioAD.SelectedValue));
            }
            catch
            {
                return SYS_UsuarioBO.eIntegracaoAD.NaoIntegrado;
            }
        }
    }

    #endregion

    #region Eventos Life Cycle

    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string mensagemTamanho = SYS_MensagemSistemaBO.RetornaValor(SYS_MensagemSistemaChave.MeusDadosMensagemValidacaoComplexidadeSenhaTamanho);

            if (!string.IsNullOrEmpty(mensagemTamanho))
            {
                revSenhaTamanho.ErrorMessage = mensagemTamanho;
            }
            else
            {
                String.Format(revSenhaTamanho.ErrorMessage, UtilBO.GetMessageTamanhoByRegex(revSenhaTamanho.ValidationExpression));
            }

            // Configura tamanho senha
            revSenhaTamanho.ValidationExpression = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.TAMANHO_SENHA_USUARIO);
            revSenhaTamanho.ErrorMessage = String.Format(revSenhaTamanho.ErrorMessage, UtilBO.GetMessageTamanhoByRegex(revSenhaTamanho.ValidationExpression));

            #region Validação Obrigatoriedade e-mail usuário

            //Habilita controles de validação de email de acordo com parametro VALIDAR_OBRIGATORIEDADE_EMAIL_USUARIO
            bool validaObrigEmail = SYS_ParametroBO.Parametro_ValidarObrigatoriedadeEmailUsuario();
            _rfvEmail.Enabled = validaObrigEmail;
            _revEmail.Enabled = validaObrigEmail;

            //Se não for obrigatório, remove o indicador do label
            if (!validaObrigEmail)
                this._lblEmail.Text = this._lblEmail.Text.Replace("*", "");

            #endregion
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ScriptManager sm = ScriptManager.GetCurrent(this);
            if (sm != null)
            {
                sm.Scripts.Add(new ScriptReference(ArquivoJS.UiAriaTabs));
                sm.Scripts.Add(new ScriptReference(ArquivoJS.Tabs));
                sm.Scripts.Add(new ScriptReference(ArquivoJS.MsgConfirmExclusao));
                sm.Scripts.Add(new ScriptReference(ArquivoJS.MsgConfirmBtn));
                sm.Scripts.Add(new ScriptReference(ArquivoJS.PastaScriptRaiz + "Telas/jsModuloSeguranca.js"));
            }

            UCUA1.Paginacao = ApplicationWEB._Paginacao;
            UCUA1.ContainerName = "divBuscaUA";
            UCUA1.ReturnValues += UCUA1BuscaUA;
            UCUA1.AddParameters("ent_idVisible", "false");
            UCUA1.AddParameters("ent_id", UCComboEntidadeUsuario._Combo.SelectedValue);

            UCPessoas1.Paginacao = ApplicationWEB._Paginacao;
            UCPessoas1.ContainerName = "divBuscaPessoa";
            UCPessoas1.ReturnValues += UCPessoas1BuscaPessoa;

            if (!IsPostBack)
            {
                CarregarComboIntegracaoAD();
                _CarregarComboDominios();

                string ent_padrao = __SessionWEB.__UsuarioWEB.Usuario.ent_id.ToString();
                if (!string.IsNullOrEmpty(ent_padrao))
                    UCComboEntidadeUsuario._Combo.SelectedValue = ent_padrao;

                UCComboEntidadeUsuario._ShowSelectMessage = true;
                UCComboEntidadeUsuario._Load(Guid.Empty, 1);
                UCComboEntidadeUsuario.Inicialize("Entidade *");
                UCComboEntidadeUsuario._ValidationGroup = "Usuario";

                if (__SessionWEB.__UsuarioWEB.Grupo.vis_id == SysVisaoID.Administracao)
                    UCComboEntidadeUsuario._Load(Guid.Empty, 0);
                else
                    UCComboEntidadeUsuario._LoadBy_UsuarioGrupoUA(Guid.Empty, __SessionWEB.__UsuarioWEB.Grupo.gru_id, __SessionWEB.__UsuarioWEB.Usuario.usu_id, 0);


                chkIntegracaoExterna.Checked = false;
                ddlIntegracaoExternaTipo.Visible = false;

                _chkBloqueado.Visible = false;
                if ((PreviousPage != null) && (PreviousPage.IsCrossPagePostBack))
                {
                    _LoadUsuario(PreviousPage.EditItem);
                    Page.Form.DefaultFocus = _txtEmail.ClientID;
                }
                else
                {
                    _chkSenhaAutomatica.Visible = true;
                    _chkSenhaAutomatica.Checked = false;

                    _txtEmail.Text = string.Empty;
                    _txtSenha.Text = string.Empty;

                    ManageUserLive live = new ManageUserLive();
                    VS_ExistsIntegracaoExterna = live.ExistsIntegracaoExterna();
                    _ckbUsuarioLive.Visible = VS_ExistsIntegracaoExterna;

                    Page.Form.DefaultFocus = UCComboEntidadeUsuario._Combo.ClientID;

                    this._btnSalvar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;
                }
                _LoadGridGrupos();

            }
            else
            {
                _txtSenha.Attributes.Add("value", _txtSenha.Text);
                _txtConfirmacao.Attributes.Add("value", _txtConfirmacao.Text);
            }

            if (__SessionWEB.__UsuarioWEB.Usuario.usu_id == _VS_usu_id)
            {
                string script = String.Format("SetConfirmDialogButton('{0}','{1}');",
                                String.Concat("#", _btnSalvar.ClientID),
                                "Deseja realmente alterar o usuário?<br /><br />Caso confirme você será redirecionado para a página de login.");
                Page.ClientScript.RegisterStartupScript(GetType(), _btnSalvar.ClientID, script, true);
            }
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o sistema.", UtilBO.TipoMensagem.Erro);
        }

        Page.Form.DefaultButton = _btnSalvar.UniqueID;

        //Implementa onSelectedIndexChange do combo de entidade do usuário.
        UCComboEntidadeUsuario.OnSelectedIndexChange = UCComboEntidadeUsuario__IndexChanged;
    }

    #endregion

    #region Eventos

    #region Grupos

    protected void odsGrupos_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.ReturnValue != null)
            _VisibleUa(((List<SYS_Grupo>)e.ReturnValue)[0].vis_id);
    }

    protected void _ddlGrupos_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (new Guid(_ddlGrupos.SelectedValue) != Guid.Empty)
            {
                SYS_Grupo grupo = new SYS_Grupo
                {
                    gru_id = new Guid(_ddlGrupos.SelectedValue)
                };
                SYS_GrupoBO.GetEntity(grupo);

                _VisibleUa(grupo.vis_id);
            }
            else
            {
                divUA.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o grupo.", UtilBO.TipoMensagem.Erro);
        }
    }

    protected void _dgvGrupo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                Label _lbBloqueado = (Label)e.Row.FindControl("_lbBloqueado");
                if (_lbBloqueado != null && e.Row.Cells[1].Text.Equals("1"))
                {
                    _lbBloqueado.Text = "Não";
                }
                else if (_lbBloqueado != null && e.Row.Cells[1].Text.Equals("2"))
                {
                    _lbBloqueado.Text = "Sim";
                }

                Repeater rpt = (Repeater)e.Row.FindControl("_rptEntidadeUA");
                if (rpt != null)
                {
                    Guid gru_id = ((SYS_UsuarioBO.TmpGrupos)e.Row.DataItem).gru_id;
                    if (_VS_EntidadeUA.ContainsKey(gru_id))
                    {
                        rpt.DataSource = _VS_EntidadeUA[gru_id];
                        rpt.DataBind();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    protected void _btnSalvarGrupo_Click(object sender, EventArgs e)
    {
        if ((_lstUAs.Items.Count == 0) && (divUA.Visible))
        {
            _lblMessageInsert.Text = UtilBO.GetErroMessage("Nenhum grupo foi adicionado.", UtilBO.TipoMensagem.Alerta);
        }
        else
        {
            try
            {
                //Carrega lista de entidade e Unidade administrativas
                List<SYS_UsuarioBO.TmpEntidadeUA> lt = new List<SYS_UsuarioBO.TmpEntidadeUA>();
                Guid gru_id = new Guid(_ddlGrupos.SelectedValue);
                int usg_situacao = (_ckbGrupo_Bloqueado.Checked) ? 2 : 1;

                if (_ddlGrupos.SelectedIndex.Equals(0))
                {
                    throw new ArgumentException("Sistema - grupo é obrigatório.");
                }
                else
                {
                    if (divUA.Visible)
                    {
                        foreach (ListItem item in _lstUAs.Items)
                        {
                            Guid ent_id;
                            Guid uad_id = Guid.Empty;

                            //Checa se é Unidade Administrativa ou entidade
                            if (divUA.Visible)
                            {
                                //Se unidade administrativa separa os ids
                                string[] ids = item.Value.Split(';');
                                ent_id = new Guid(ids[0]);
                                uad_id = new Guid(ids[1]);
                            }
                            else
                                ent_id = new Guid(item.Value);

                            //Adciono a lista de entidade e unidade
                            SYS_UsuarioBO.AddTmpEntidadeUA(gru_id, ent_id, uad_id, lt);

                            //List<SYS_UsuarioBO.TmpEntidadeUA> ltUA = new List<SYS_UsuarioBO.TmpEntidadeUA>();

                            //foreach (KeyValuePair<Guid, List<SYS_UsuarioBO.TmpEntidadeUA>> kv in _VS_EntidadeUA)
                            //{
                            //    foreach (SYS_UsuarioBO.TmpEntidadeUA aux in kv.Value.ToList())
                            //        ltUA.Add(aux);
                            //}

                            if (_VS_usu_id == Guid.Empty && _VS_EntidadeUA.Count > 0)
                            {
                                UCComboEntidadeUsuario._Combo.Enabled = false;
                                _updUsuario.Update();
                            }
                        }

                        //Checa se a lista já existe no viewstate para o grupo associado.
                        //Caso já exista uma lista de entidades ou ua, substitui pela nova lista.
                        //Caso contrário adiciona a nova lista
                        if (_VS_EntidadeUA.ContainsKey(new Guid(_ddlGrupos.SelectedValue)))
                            _VS_EntidadeUA[gru_id] = lt;
                        else
                            _VS_EntidadeUA.Add(gru_id, lt);
                    }

                    //Adciona o grupo caso ainda não exista
                    SYS_UsuarioBO.AddTmpGrupo(gru_id, _VS_Grupos, usg_situacao);

                    //Atualiza o grid
                    _LoadGridGrupos();

                    //Registra script para fechar a janela            
                    //ScriptManager.RegisterClientScriptBlock(this._btnSalvarGrupo, this._btnSalvarGrupo.GetType(), "SalvarGrupo", "$('#divAddGrupos').dialog('close');", true);
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "SalvarGrupo", "$('#divAddGrupos').dialog('close');", true);
                }
            }
            catch (ArgumentException err)
            {
                _lblMessageInsert.Text = UtilBO.GetErroMessage(err.Message, UtilBO.TipoMensagem.Alerta);
            }
            catch
            {
                _lblMessageInsert.Text = UtilBO.GetErroMessage("Erro ao tentar incluir o grupo.", UtilBO.TipoMensagem.Erro);
            }
        }
    }

    protected void _dgvGrupo_RowEditing(object sender, GridViewEditEventArgs e)
    {
        _LoadSelectedGrupoUsuario(new Guid(_dgvGrupo.DataKeys[e.NewEditIndex].Value.ToString()));
    }

    protected void _dgvGrupo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SYS_UsuarioBO.RemoveTmpGrupo(new Guid(_dgvGrupo.DataKeys[e.RowIndex].Value.ToString()), _VS_Grupos, _VS_EntidadeUA);
        _LoadGridGrupos();
    }

    protected void btnNovoGrupo_Click(object sender, EventArgs e)
    {
        if (_LoadGrupos(false))
        {
            _LimparDivAddGrupos();
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "NovoGrupo", "$('#divAddGrupos').dialog('open');", true);
        }
        else
        {
            _lblMessage.Text = UtilBO.GetErroMessage("Não existe mais nenhum grupo para ser cadastrado.", UtilBO.TipoMensagem.Alerta);
            _updUsuario.Update();
        }
    }

    #endregion

    #region Unidade Administrativa

    protected void _btnAddUA_Click(object sender, EventArgs e)
    {
        if (_ddlGrupos.SelectedValue != "-1")
        {
            _lstUAs.SelectedIndex = -1;

            if (divUA.Visible)
            {
                if ((!String.IsNullOrEmpty(_txtEnt_id.Value)) && (!String.IsNullOrEmpty(_txtUad_id.Value)))
                {
                    ListItem item = new ListItem(_txtUA.Text, String.Concat(_txtEnt_id.Value, ";", _txtUad_id.Value));
                    if (!_lstUAs.Items.Contains(item))
                        _lstUAs.Items.Add(item);
                    _txtUA.Text = String.Empty;
                    _txtUad_id.Value = String.Empty;
                    _txtEnt_id.Value = String.Empty;
                }
                else
                    _lblMessageInsert.Text = UtilBO.GetErroMessage("Unidade administrativa é obrigatório.", UtilBO.TipoMensagem.Alerta);
            }
        }
        else
        {
            _lblMessageInsert.Text = UtilBO.GetErroMessage("Sistema - grupo é obrigatório", UtilBO.TipoMensagem.Alerta);
        }
    }

    protected void _btnDelUA_Click(object sender, EventArgs e)
    {
        _lstUAs.Items.Remove(_lstUAs.SelectedItem);
    }

    protected void _btnUA_Click(object sender, ImageClickEventArgs e)
    {
        if (UCComboEntidadeUsuario._Combo.SelectedValue != Guid.Empty.ToString())
        {
            UCUA1._Limpar();
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "BuscaUA", "$('#divBuscaUA').dialog('open');", true);
            _updBuscaUA.Update();
        }
        else
        {
            _lblMessageInsert.Text = UtilBO.GetErroMessage("Entidade é obrigatório para a inclusão de unidade(s) administrativa(s).", UtilBO.TipoMensagem.Alerta);
            updGrupos.Update();
        }
    }

    #endregion

    protected void _btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect(String.Concat(__SessionWEB._AreaAtual._Diretorio, "Usuario/Busca.aspx"), false);
    }

    protected void _btnSalvar_Click(object sender, EventArgs e)
    {
        bool validado = true;
        try
        {
            validado = ValidarIntegracaoAD();

            ManageUserLive live = new ManageUserLive();
            if (_ckbUsuarioLive.Checked)
            {
                if (!live.IsContaEmail(_txtEmail.Text))
                {
                    validado = false;
                    _lblMessage.Text = UtilBO.GetErroMessage("E-mail inválido para integrar usuário live.", UtilBO.TipoMensagem.Alerta);
                }
                else
                {
                    if (!live.VerificarContaEmailExistente(new UserLive { email = _txtEmail.Text }))
                    {
                        validado = false;
                        _lblMessage.Text = UtilBO.GetErroMessage("E-mail não encontrado no live para realizar integração.", UtilBO.TipoMensagem.Alerta);
                    }
                }
            }
            else
            {
                if (live.IsContaEmail(_txtEmail.Text))
                {
                    validado = false;
                    _lblMessage.Text = UtilBO.GetErroMessage("Integrar usuário live é obrigatório, o email " + _txtEmail.Text +
                        " contém o domínio para integração com live.", UtilBO.TipoMensagem.Alerta);
                }
            }

            if (integracaoAD == SYS_UsuarioBO.eIntegracaoAD.IntegradoADReplicacaoSenha && !string.IsNullOrEmpty(_txtSenha.Text))
            {
                string regTamanhoSenha = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.TAMANHO_SENHA_USUARIO);

                Regex regex = new Regex(regTamanhoSenha);
                if (!regex.IsMatch(_txtSenha.Text))
                {
                    string mensagemTamanho = SYS_MensagemSistemaBO.RetornaValor(SYS_MensagemSistemaChave.MeusDadosMensagemValidacaoComplexidadeSenhaTamanho);
                    throw new CoreLibrary.Validation.Exceptions.ValidationException(string.IsNullOrEmpty(mensagemTamanho) ? String.Format("A senha deve conter {0}.", UtilBO.GetMessageTamanhoByRegex(regTamanhoSenha)) : mensagemTamanho);
                }

                regex = new Regex(SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.FORMATO_SENHA_USUARIO));
                if (!regex.IsMatch(_txtSenha.Text))
                {
                    // Mensagem de validação do formato da senha.
                    string mensagemFormato = SYS_MensagemSistemaBO.RetornaValor(SYS_MensagemSistemaChave.MeusDadosMensagemValidacaoComplexidadeSenhaFormato);
                    throw new CoreLibrary.Validation.Exceptions.ValidationException(string.IsNullOrEmpty(mensagemFormato) ? "A senha deve conter pelo menos uma combinação de letras e números ou letras maiúsculas e minúsculas ou algum caracter especial (!, @, #, $, %, &) somados a letras e/ou números." : mensagemFormato);
                }

            }


        }
        catch (CoreLibrary.Validation.Exceptions.ValidationException err)
        {
            _lblMessage.Text = UtilBO.GetErroMessage(err.Message, UtilBO.TipoMensagem.Alerta);
            validado = false;
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar o usuário.", UtilBO.TipoMensagem.Erro);
            validado = false;
        }
        if (validado)
        {

            if (chkIntegracaoExterna.Checked && ddlIntegracaoExternaTipo.SelectedIndex == 0)
            {
                _lblMessage.Text = UtilBO.GetErroMessage("Realizar Integração por WebService Externo é obrigatório.", UtilBO.TipoMensagem.Alerta);
            }
            else
            {
                if ((integracaoAD == SYS_UsuarioBO.eIntegracaoAD.IntegradoAD) && ((_ddlDominios.SelectedIndex == 0) || (_ddlDominios.SelectedIndex == _VS_OutrosDominios && string.IsNullOrEmpty(_txtDominio.Text))))
                {
                    _lblMessage.Text = UtilBO.GetErroMessage("Domínio é obrigatório.", UtilBO.TipoMensagem.Alerta);
                }
                else
                {
                    try
                    {
                        SYS_Usuario entity = new SYS_Usuario
                        {
                            ent_id = new Guid(UCComboEntidadeUsuario._Combo.SelectedValue)
                            ,
                            usu_id = _VS_usu_id
                            ,
                            usu_login = _txtLogin.Text
                            ,
                            usu_email = _txtEmail.Text
                            ,
                            usu_senha = string.IsNullOrEmpty(_txtSenha.Text) ? string.Empty : _txtSenha.Text
                            ,
                            pes_id = string.IsNullOrEmpty(_txtPes_id.Value) ? Guid.Empty : new Guid(_txtPes_id.Value)
                            ,
                            usu_criptografia = !string.IsNullOrEmpty(_txtSenha.Text) || string.IsNullOrEmpty(_txtCriptografia.Value) ? Convert.ToByte(eCriptografa.SHA512) : Convert.ToByte(_txtCriptografia.Value)
                            ,
                            usu_situacao = 1
                            ,
                            usu_dataAlteracao = DateTime.Now
                            ,
                            usu_dataCriacao = DateTime.Now
                            ,
                            usu_dominio = (_ddlDominios.SelectedIndex == _VS_OutrosDominios) ? _txtDominio.Text : _ddlDominios.SelectedValue
                            ,
                            usu_integracaoAD = (byte)integracaoAD
                            ,
                            IsNew = (_VS_usu_id != Guid.Empty) ? false : true

                            ,
                            usu_integracaoExterna = Convert.ToInt16(ddlIntegracaoExternaTipo.SelectedIndex.ToString())
                        };
                        if (_chkExpiraSenha.Checked)
                            entity.usu_situacao = 5;
                        if (_chkBloqueado.Checked)
                            entity.usu_situacao = 2;

                        if (SYS_UsuarioBO.Save(entity, _VS_Grupos, _VS_EntidadeUA, _chkSenhaAutomatica.Checked, _txtPessoa.Text, __SessionWEB.TituloGeral, ApplicationWEB._EmailHost, ApplicationWEB._EmailSuporte, null, entity.usu_integracaoAD == (byte)SYS_UsuarioBO.eIntegracaoAD.IntegradoADReplicacaoSenha, ApplicationWEB.EmailRemetente))
                        {
                            if (_VS_usu_id == Guid.Empty)
                            {
                                _VS_usu_id = entity.usu_id;

                                ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Insert, "usu_id: " + _VS_usu_id);
                                __SessionWEB.PostMessages = UtilBO.GetErroMessage(String.Format("Usuário incluído com sucesso."), UtilBO.TipoMensagem.Sucesso);
                            }
                            else
                            {
                                ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Update, "usu_id: " + _VS_usu_id);
                                __SessionWEB.PostMessages = UtilBO.GetErroMessage(String.Format("Usuário alterado com sucesso."), UtilBO.TipoMensagem.Sucesso);
                            }

                            if (__SessionWEB.__UsuarioWEB.Usuario.usu_id == _VS_usu_id)
                                Response.Redirect(ApplicationWEB._DiretorioVirtual + ApplicationWEB._PaginaLogoff, false);
                            else
                                Response.Redirect(String.Concat(__SessionWEB._AreaAtual._Diretorio, "Usuario/Busca.aspx"), false);
                        }
                        else
                        {
                            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar o usuário.", UtilBO.TipoMensagem.Erro);
                        }
                    }
                    catch (CoreLibrary.Validation.Exceptions.ValidationException err)
                    {
                        _lblMessage.Text = UtilBO.GetErroMessage(err.Message, UtilBO.TipoMensagem.Alerta);
                    }
                    catch (ArgumentException err)
                    {
                        _lblMessage.Text = UtilBO.GetErroMessage(err.Message, UtilBO.TipoMensagem.Alerta);
                    }
                    catch (DuplicateNameException err)
                    {
                        _lblMessage.Text = UtilBO.GetErroMessage(err.Message, UtilBO.TipoMensagem.Alerta);
                    }
                    catch (Exception ex)
                    {
                        ApplicationWEB._GravaErro(ex);
                        _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar o usuário.", UtilBO.TipoMensagem.Erro);
                    }
                }
            }// valida integracao
        }
    }

    protected void _chkSenhaAutomatica_CheckedChanged(object sender, EventArgs e)
    {
        ConfiguraSenhaAutomatica();

        _updUsuario.Update();
    }

    protected void ddlUsuarioAD_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConfiguraUsuarioAD();
    }

    protected void _ddlDominios_SelectedIndexChanged(object sender, EventArgs e)
    {
        _TrataOutrosDominios();
    }

    protected void UCComboEntidadeUsuario__IndexChanged(object sender, EventArgs e)
    {
        if (_VS_EntidadeUA.Count > 0)
        {
            _VS_EntidadeUA.Clear();
            _LoadGridGrupos();
            updGruposGrid.Update();
        }

        UCComboEntidadeUsuario._Combo.Focus();
    }

    protected void _btnPessoa_Click(object sender, ImageClickEventArgs e)
    {
        UCPessoas1._Limpar();
        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "BuscaPessoa", "$('#divBuscaPessoa').dialog('open');", true);
        _updBuscaPessoas.Update();
    }

    protected void _ckbUsuarioLive_ChangeChecked(object sender, EventArgs e)
    {
        ConfiguraUsuarioLive();
    }

    protected void chkIntegracaoExterna_OnCheckedChanged(object sender, EventArgs e)
    {
        // EXIBE DDL
        if (chkIntegracaoExterna.Checked)
        {
            ddlIntegracaoExternaTipo.Visible = true;
            SYS_IntegracaoExternaTipoBO bo = new SYS_IntegracaoExternaTipoBO();
            DataTable dt = bo.getAll();



            ddlIntegracaoExternaTipo.Items.Clear();
            ddlIntegracaoExternaTipo.Items.Insert(0, "-- Selecione uma Integração Externa --");
            foreach (DataRow iet in dt.Rows)
            {
                ddlIntegracaoExternaTipo.Items.Insert(Convert.ToUInt16(iet["iet_id"].ToString()), iet["iet_descricao"].ToString());
            }


            // Remove a validação da senha
            _rfvConfirmarSenha.IsValid = chkIntegracaoExterna.Checked;
            _rfvSenha.IsValid = chkIntegracaoExterna.Checked;
            revSenha.IsValid = chkIntegracaoExterna.Checked;
            revSenhaTamanho.IsValid = chkIntegracaoExterna.Checked;
            _cpvConfirmarSenha.IsValid = chkIntegracaoExterna.Checked;
        }
        else {
            ddlIntegracaoExternaTipo.Visible = false;
            ddlIntegracaoExternaTipo.DataSource = null;
            ddlIntegracaoExternaTipo.DataBind();
        }
        // Não exibe opções de senha
        divOpcoesSenha.Visible = !chkIntegracaoExterna.Checked;


    }


    #endregion

    #region Métodos

    #region Grupos

    private bool _LoadGrupos(bool update)
    {

        IList<SYS_Grupo> grupos;
        if (update)
            grupos = SYS_UsuarioBO.GetSistemaGrupo();
        else
            grupos = SYS_UsuarioBO.GetSistemaGrupoNotExists(_VS_Grupos);

        _ddlGrupos.AppendDataBoundItems = true;
        _ddlGrupos.Items.Clear();
        _ddlGrupos.Items.Insert(0, new ListItem("-- Selecione um sistema - grupo --", Guid.Empty.ToString(), true));
        _ddlGrupos.DataValueField = "gru_id";
        _ddlGrupos.DataTextField = "gru_nome";
        _ddlGrupos.DataSource = grupos;
        _ddlGrupos.DataBind();
        //Monta a tela de inserção de grupos
        if (grupos.Count > 0)
            _VisibleUa(grupos[0].vis_id);

        return (grupos.Count > 0);
    }

    private void _LimparDivAddGrupos()
    {
        _ddlGrupos.Enabled = true;
        _ddlGrupos.SelectedIndex = -1;
        _lstUAs.Items.Clear();
        _ckbGrupo_Bloqueado.Checked = false;

        _txtEnt_id.Value = string.Empty;
        _txtUad_id.Value = string.Empty;
        _txtUA.Text = string.Empty;
    }

    private void _LoadSelectedGrupoUsuario(Guid gru_id)
    {
        try
        {
            _LoadGrupos(true);
            _LimparDivAddGrupos();
            _ddlGrupos.SelectedValue = gru_id.ToString();
            _ddlGrupos.Enabled = false;

            SYS_UsuarioBO.TmpGrupos grupo = _VS_Grupos[gru_id];
            _ckbGrupo_Bloqueado.Checked = (grupo.usg_situacao == 2) ? true : false;

            SYS_Grupo entityGrupo = new SYS_Grupo
            {
                gru_id = gru_id
            };
            SYS_GrupoBO.GetEntity(entityGrupo);

            _VisibleUa(entityGrupo.vis_id);

            List<SYS_UsuarioBO.TmpEntidadeUA> lt = _VS_EntidadeUA[gru_id];
            foreach (SYS_UsuarioBO.TmpEntidadeUA ent in lt)
            {
                ListItem li;
                if (ent.Entidade)
                    li = new ListItem(ent.EntidadeOrUA, ent.ent_id.ToString());
                else
                    li = new ListItem(ent.EntidadeOrUA, String.Concat(ent.ent_id, ";", ent.uad_id));
                _lstUAs.Items.Add(li);
            }
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o grupo.", UtilBO.TipoMensagem.Erro);
        }
    }

    private void _LoadGridGrupos()
    {
        try
        {
            if (_VS_usu_id == Guid.Empty && _VS_EntidadeUA.Count > 0)
            {
                UCComboEntidadeUsuario._Combo.Enabled = false;
                _updUsuario.Update();
            }
            else if (_VS_usu_id == Guid.Empty && _VS_EntidadeUA.Count <= 0)
            {
                UCComboEntidadeUsuario._Combo.Enabled = true;
                _updUsuario.Update();
            }

            _dgvGrupo.DataSource = _VS_Grupos.Values;
            _dgvGrupo.DataBind();
        }
        catch (Exception)
        {
            throw;
        }
    }

    #endregion

    #region Unidade Administrativa

    private void UCUA1BuscaUA(IDictionary<string, object> parameters)
    {
        try
        {
            _txtEnt_id.Value = parameters["ent_id"].ToString();
            _txtUad_id.Value = parameters["uad_id"].ToString();
            _txtUA.Text = parameters["uad_nome"].ToString();
            updGrupos.Update();
        }
        catch (Exception ex)
        {
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar a unidade administrativa.", UtilBO.TipoMensagem.Erro);
            ApplicationWEB._GravaErro(ex);
        }
    }

    private void _VisibleUa(int vis_id)
    {
        divUA.Visible = ((vis_id != SysVisaoID.Administracao) && (vis_id != SysVisaoID.Individual));
        _lstUAs.Items.Clear();
    }

    #endregion

    #region Domínio

    private static string _TrataStrDominio(string dominio)
    {
        return (dominio.Split('.')[0]);
    }

    private void _CarregarComboDominios()
    {
        //Carregar domínios.
        IList<string> lsDominios;
        try
        {
            lsDominios = CoreLibrary.LDAP.LdapUtil.GetDomainNames();
        }
        catch
        {
            lsDominios = new List<string>();
        }
        _ddlDominios.Items.Clear();
        _ddlDominios_AddItems("-- Selecione um domínio --", string.Empty);
        int cont = 0; //Índice do selecione.
        foreach (string dominio in lsDominios)
        {
            string strDominio = _TrataStrDominio(dominio);
            _ddlDominios_AddItems(strDominio, strDominio);
            cont++;
        }
        _VS_OutrosDominios = ++cont;
        _ddlDominios_AddItems("Outros domínios...", "Outros domínios...");
    }

    private void _ddlDominios_AddItems(string text, string value)
    {
        ListItem item = new ListItem();
        item.Text = text;
        item.Value = value;
        _ddlDominios.Items.Add(item);
    }

    private void _TrataOutrosDominios()
    {
        divOutrosDominios.Visible = _ddlDominios.SelectedIndex == _VS_OutrosDominios;

        _ddlDominios.Focus();
    }

    #endregion

    #region Pessoa

    private void UCPessoas1BuscaPessoa(IDictionary<string, object> parameters)
    {
        try
        {
            _txtPes_id.Value = parameters["pes_id"].ToString();
            _txtPessoa.Text = parameters["pes_nome"].ToString();
            _updUsuario.Update();
        }
        catch (Exception ex)
        {
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar a pessoa.", UtilBO.TipoMensagem.Erro);
            ApplicationWEB._GravaErro(ex);
        }
    }

    private void _CarregarPessoa(Guid pes_id)
    {
        PES_Pessoa pessoa = new PES_Pessoa();
        pessoa.pes_id = pes_id;
        _txtPes_id.Value = pes_id.ToString();
        PES_PessoaBO.GetEntity(pessoa);
        _txtPessoa.Text = pessoa.pes_nome;
    }

    #endregion

    #region Integracao AD

    /// <summary>
    /// Carrega o combo de integração com AD com os valores do enumerador.
    /// </summary>
    private void CarregarComboIntegracaoAD()
    {
        FieldInfo[] propriedades = (typeof(SYS_UsuarioBO.eIntegracaoAD)).GetFields();

        foreach (FieldInfo fi in propriedades)
        {
            DescriptionAttribute[] atributos = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (atributos.Length > 0)
                ddlUsuarioAD.Items.Add(new ListItem(atributos[0].Description, fi.GetRawConstantValue().ToString()));
        }

        if (ddlUsuarioAD.Items.Cast<ListItem>().Any(p => p.Value.Equals("0")))
            ddlUsuarioAD.SelectedValue = "0";

        ConfiguraUsuarioAD();
    }

    private void ConfiguraUsuarioAD()
    {
        switch (integracaoAD)
        {
            case SYS_UsuarioBO.eIntegracaoAD.NaoIntegrado:
            case SYS_UsuarioBO.eIntegracaoAD.IntegradoADReplicacaoSenha:
                divDominios.Visible = false;
                divOpcoesSenha.Visible = true;
                _ddlDominios.SelectedValue = string.Empty;
                _txtDominio.Text = string.Empty;
                _rfvConfirmarSenha.Enabled = true;
                _rfvSenha.Enabled = _VS_usu_id == Guid.Empty;
                _txtSenha.Text = string.Empty;
                _txtConfirmacao.Text = string.Empty;
                _chkSenhaAutomatica.Checked = false;
                ConfiguraSenhaAutomatica();
                _ckbUsuarioLive.Visible = (VS_ExistsIntegracaoExterna ? true : false);
                break;
            case SYS_UsuarioBO.eIntegracaoAD.IntegradoAD:
                divDominios.Visible = true;
                _TrataOutrosDominios();
                divOpcoesSenha.Visible = false;
                _rfvConfirmarSenha.Enabled = false;
                _rfvSenha.Enabled = false;
                _txtSenha.Text = string.Empty;
                _txtConfirmacao.Text = string.Empty;
                _chkSenhaAutomatica.Checked = false;
                ConfiguraSenhaAutomatica();
                _ckbUsuarioLive.Visible = false;
                _ckbUsuarioLive.Checked = false;
                revSenha.Enabled = false;
                revSenhaTamanho.Enabled = false;
                break;
            default:
                _lblMessage.Text = UtilBO.GetErroMessage("Tipo de integração com AD inválido.", UtilBO.TipoMensagem.Alerta);
                break;
        };
    }

    /// <summary>
    /// Valida o valor selecionado no combo de integração com AD.
    /// </summary>
    /// <returns></returns>
    private bool ValidarIntegracaoAD()
    {
        FieldInfo[] propriedades = (typeof(SYS_UsuarioBO.eIntegracaoAD)).GetFields();

        if (!propriedades.Skip(1).Any(p => p.GetRawConstantValue().ToString() == ddlUsuarioAD.SelectedValue))
            throw new ValidationException("Tipo de integração com AD não foi selecionado ou é inválido.");

        return true;
    }

    #endregion Integracao AD

    private void _LoadUsuario(Guid usu_id)
    {

        SYS_Usuario usuario = new SYS_Usuario();
        try
        {
            _VS_usu_id = usu_id;
            usuario.usu_id = _VS_usu_id;
            SYS_UsuarioBO.GetEntity(usuario);

            UCComboEntidadeUsuario._Combo.SelectedValue = usuario.ent_id.ToString();
            UCComboEntidadeUsuario._Combo.Enabled = false;
            ddlUsuarioAD.SelectedValue = usuario.usu_integracaoAD.ToString();
            _txtCriptografia.Value = usuario.usu_criptografia.ToString();
            _txtLogin.Text = usuario.usu_login;
            _txtEmail.Text = usuario.usu_email;

            if (!string.IsNullOrEmpty(usuario.usu_dominio))
            {
                bool encontrou = false;
                foreach (ListItem item in _ddlDominios.Items)
                {
                    if (item.Value == usuario.usu_dominio)
                    {
                        item.Selected = true;
                        encontrou = true;
                    }
                }
                //Caso não encontre o domínio na lista de disponíveis...
                if (!encontrou)
                {
                    //Seta a opção outros.
                    _ddlDominios.SelectedIndex = _VS_OutrosDominios;
                    _TrataOutrosDominios();
                    _txtDominio.Text = usuario.usu_dominio;
                }
            }
            ConfiguraUsuarioAD();

            if (usuario.usu_situacao == 5)
                _chkExpiraSenha.Checked = true;
            else if (usuario.usu_situacao == 2)
                _chkBloqueado.Checked = true;
            _chkBloqueado.Visible = true;

            //Carrega os dados da pessoa
            _CarregarPessoa(usuario.pes_id);
            _btnPessoa.Enabled = false;

            //VERIFICA SE PARÂMETRO DE LOGIN COM PROVIDER EXTERNO ESTÁ HABILITADO(TRUE = MOSTRA O CAMPO / FALSE = NÃO MOSTRA O CAMPO)
            if (SYS_ParametroBO.ParametroValorBooleano(SYS_ParametroBO.eChave.PERMITIR_LOGIN_COM_PROVIDER_EXTERNO))
            {

                divContasExternas.Visible = true;

                //BUSCA O LOGIN PROVIDER DO USUARIO E CASO NÃO FOR NULO EXIBE NO TEXTBOX
                var logins = SYS_UsuarioLoginProviderBO.SelectBy_usu_id(usu_id);

                if (logins != null && logins.Count > 0)
                {
                    this.rptContasExternas.Visible = true;
                    this.lblInfoContaExterna.Visible = false;

                    this.rptContasExternas.DataSource = logins;
                    this.rptContasExternas.DataBind();
                }
                else
                {
                    this.rptContasExternas.Visible = false;
                    this.lblInfoContaExterna.Visible = true;
                    this.lblInfoContaExterna.Text = UtilBO.GetErroMessage("Usuário não possui contas vinculadas.",
                        UtilBO.TipoMensagem.Informacao);
                }
            }

            //Carrega os grupos do usuário
            SYS_UsuarioBO.GetGruposUsuario(_VS_usu_id, _VS_Grupos, _VS_EntidadeUA);

            _rfvSenha.Visible = false;
            _rfvConfirmarSenha.Visible = false;

            ManageUserLive live = new ManageUserLive();
            VS_ExistsIntegracaoExterna = live.ExistsIntegracaoExterna();
            if (VS_ExistsIntegracaoExterna && !string.IsNullOrEmpty(usuario.usu_email))
            {
                _ckbUsuarioLive.Visible = integracaoAD != SYS_UsuarioBO.eIntegracaoAD.IntegradoAD;
                if (live.IsContaEmail(usuario.usu_email))
                {
                    _ckbUsuarioLive.Checked = true;
                    ConfiguraUsuarioLive();
                }
            }
            else
            {
                _ckbUsuarioLive.Visible = false;
                _ckbUsuarioLive.Checked = false;
            }

            _lblSenha.Text = _VS_usu_id == Guid.Empty ? "Senha *" : "Senha";
            _lblConfirmacao.Text = _VS_usu_id == Guid.Empty ? "Confirmar senha *" : "Confirmar senha";


            if (usuario.usu_integracaoExterna > 0)
            {
                chkIntegracaoExterna.Checked = true;
                SYS_IntegracaoExternaTipoBO bo = new SYS_IntegracaoExternaTipoBO();
                ddlIntegracaoExternaTipo.DataSource = bo.getAll();
                ddlIntegracaoExternaTipo.DataBind();

                // Não exibe opções de senha
                divOpcoesSenha.Visible =
               
                    _rfvConfirmarSenha.IsValid =
                _rfvConfirmarSenha.SetFocusOnError =
                _rfvConfirmarSenha.Enabled =
                 

                _rfvSenha.IsValid =
                _rfvSenha.SetFocusOnError =
                 _rfvSenha.Enabled =

                revSenha.IsValid =
                revSenha.SetFocusOnError =
               revSenha.Enabled =

                revSenhaTamanho.IsValid =
                
                revSenhaTamanho.SetFocusOnError =
                revSenhaTamanho.Enabled =

                _cpvConfirmarSenha.IsValid =
                _cpvConfirmarSenha.SetFocusOnError =
                _cpvConfirmarSenha.Enabled = false;

                revSenhaTamanho.ValidationGroup = "";
                _rfvConfirmarSenha.ValidationGroup = "";
                _rfvSenha.ValidationGroup = "";
                _cpvConfirmarSenha.ValidationGroup = "";
                revSenha.ValidationGroup = "";

                _btnPessoa.CausesValidation = false;


                ddlIntegracaoExternaTipo.Visible = true;
                SYS_IntegracaoExternaTipoBO ietBO = new SYS_IntegracaoExternaTipoBO();
                DataTable dt = ietBO.getAll();



                ddlIntegracaoExternaTipo.Items.Clear();
                ddlIntegracaoExternaTipo.Items.Insert(0, "-- Selecione uma Integração Externa --");
                foreach (DataRow iet in dt.Rows)
                {
                    ddlIntegracaoExternaTipo.Items.Insert(Convert.ToUInt16(iet["iet_id"].ToString()), iet["iet_descricao"].ToString());
                }

                ddlIntegracaoExternaTipo.SelectedIndex = usuario.usu_integracaoExterna;

            }
            else
            {
                chkIntegracaoExterna.Checked = false;
            }
        }
        catch
        {
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o usuário.", UtilBO.TipoMensagem.Erro);
        }
    }

    private void ConfiguraUsuarioLive()
    {
        // Configura visibilidade dos controles
        bool flag = (!_ckbUsuarioLive.Checked);
        if (_VS_usu_id == Guid.Empty)
        {
            divOpcoesSenha.Visible = flag;
            _chkExpiraSenha.Visible = flag;
        }
        _txtLogin.Visible = flag;
        _lblLogin.Visible = flag;
        ddlUsuarioAD.Enabled = flag;
        _chkSenhaAutomatica.Visible = flag;
        _chkBloqueado.Visible = (_VS_usu_id != Guid.Empty);

        // Configura valores dos controles
        divDominios.Visible = false;
        _chkSenhaAutomatica.Checked = false;
        _txtSenha.Text = string.Empty;
        _txtConfirmacao.Text = string.Empty;

        // Configura validações dos controles
        _rfvLogin.Enabled = flag;
        revSenha.Enabled = _ckbUsuarioLive.Checked;
        revSenhaTamanho.Enabled = _ckbUsuarioLive.Checked;
        _rfvSenha.Enabled = ((_VS_usu_id == Guid.Empty) && (!_ckbUsuarioLive.Checked));
        _rfvConfirmarSenha.Enabled = ((_VS_usu_id == Guid.Empty) && (!_ckbUsuarioLive.Checked));
    }

    private void ConfiguraSenhaAutomatica()
    {
        if (_chkSenhaAutomatica.Checked)
        {
            _lblSenha.Visible = false;
            _lblConfirmacao.Visible = false;
            _txtSenha.Visible = false;
            _txtConfirmacao.Visible = false;
            _chkExpiraSenha.Enabled = false;
            _chkExpiraSenha.Visible = false;

            _chkBloqueado.Visible = false;

            _rfvSenha.Visible = false;
            _rfvConfirmarSenha.Visible = false;
            _cpvConfirmarSenha.Visible = false;

            _chkExpiraSenha.Checked = true;
            _chkBloqueado.Checked = false;
        }
        else
        {
            _lblSenha.Visible = true;
            _lblConfirmacao.Visible = true;
            _txtSenha.Visible = true;
            _txtConfirmacao.Visible = true;

            _rfvSenha.Visible = true;

            _rfvConfirmarSenha.Visible = true;
            _cpvConfirmarSenha.Visible = true;

            _chkExpiraSenha.Enabled = true;
            _chkExpiraSenha.Visible = true;

            _chkBloqueado.Visible = _VS_usu_id != Guid.Empty;

            _chkExpiraSenha.Checked = false;
            _chkBloqueado.Checked = false;
        }
    }

    #endregion
}
