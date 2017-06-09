using System;
using System.Data;
using Autenticador.Web.WebProject;
using Autenticador.BLL;
using Autenticador.Entities;

public partial class AreaAdm_Grupo_Cadastro : MotherPageLogado
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                UCComboSistemas1._ShowSelectMessage = true;
                UCComboSistemas1.Inicialize("Sistema *");
                UCComboSistemas1._Load();
                UCComboVisao1._ShowSelectMessage = true;
                UCComboVisao1.Inicialize("Visão *");

                UCComboGrupo1._ShowSelectMessage = true;
                UCComboGrupo1.Inicialize("Copiar permissões de outro grupo?");
                UCComboGrupo1._EnableValidator = false;
                UCComboGrupo1._SelecionaAutomatico = false;
                UCComboGrupo1._Load(-1, -1);

                UCComboGrupo2._ShowSelectMessage = true;
                UCComboGrupo2.Inicialize("Associar usuários de outro grupo?");
                UCComboGrupo2._EnableValidator = false;
                UCComboGrupo2._SelecionaAutomatico = false;
                UCComboGrupo2._Load(-1, -1);

                UCComboGrupo1._Combo.Enabled = false;
                UCComboGrupo2._Combo.Enabled = false;
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o sistema.", UtilBO.TipoMensagem.Erro);
            }

            if ((PreviousPage != null) && (PreviousPage.IsCrossPagePostBack))
            {
                _LoadFromEntity(PreviousPage.EditItem);
                _chkBloqueado.Visible = true;
            }
            else
            {
                _chkBloqueado.Visible = false;
                _chkBloqueado.Checked = false;
                this._btnSalvar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;
            }
            
            Page.Form.DefaultButton = _btnSalvar.UniqueID;
            Page.Form.DefaultFocus = UCComboSistemas1._Combo.ClientID;
        }
        
        UCComboSistemas1.OnSelectedIndexChange = UCComboSistemas1__IndexChanged;
        UCComboVisao1.OnSelectedIndexChange = UCComboVisao1__IndexChanged;
    }

    #region PROPRIEDADES

    private Guid _VS_gru_id
    {
        get
        {
            if (ViewState["_VS_gru_id"] != null)
                return new Guid(ViewState["_VS_gru_id"].ToString());
            return Guid.Empty;
        }
        set
        {
            ViewState["_VS_gru_id"] = value;
        }
    }

    #endregion

    #region METODOS

    private void UCComboSistemas1__IndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (UCComboSistemas1._Combo.SelectedValue != "-1" && UCComboVisao1._Combo.SelectedValue != "-1")
            {
                UCComboGrupo1._Combo.Items.Clear();
                UCComboGrupo1._ShowSelectMessage = true;
                UCComboGrupo1._Combo.Enabled = true;
                UCComboGrupo1._SelecionaAutomatico = false;
                UCComboGrupo1._Load(Convert.ToInt32(UCComboSistemas1._Combo.SelectedValue), Convert.ToInt32(UCComboVisao1._Combo.SelectedValue));                
            }
            else
            {
                UCComboGrupo1._Combo.SelectedValue = Guid.Empty.ToString();
                UCComboGrupo1._Combo.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o sistema.", UtilBO.TipoMensagem.Erro);
        } 
        finally
        {
            _updGrupos.Update();
        }
    }

    private void UCComboVisao1__IndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (UCComboSistemas1._Combo.SelectedValue != "-1" && UCComboVisao1._Combo.SelectedValue != "-1")
            {
                UCComboGrupo1._Combo.Items.Clear();
                UCComboGrupo1._ShowSelectMessage = true;
                UCComboGrupo1._Combo.Enabled = true;
                UCComboGrupo1._SelecionaAutomatico = false;
                UCComboGrupo1._Load(Convert.ToInt32(UCComboSistemas1._Combo.SelectedValue), Convert.ToInt32(UCComboVisao1._Combo.SelectedValue));                
            }
            else
            {
                UCComboGrupo1._Combo.SelectedValue = Guid.Empty.ToString();
                UCComboGrupo1._Combo.Enabled = false;
            }

            if (UCComboVisao1._Combo.SelectedValue != "-1")
            {
                UCComboGrupo2._Combo.Items.Clear();
                UCComboGrupo2._ShowSelectMessage = true;
                UCComboGrupo2._Combo.Enabled = true;
                UCComboGrupo2._SelecionaAutomatico = false;
                UCComboGrupo2._Load(-1, Convert.ToInt32(UCComboVisao1._Combo.SelectedValue));                
            }
            else
            {
                UCComboGrupo2._Combo.SelectedValue = Guid.Empty.ToString();
                UCComboGrupo2._Combo.Enabled = false;                
            }
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o sistema.", UtilBO.TipoMensagem.Erro);
        }
        finally
        {
            _updGrupos.Update();
        } 
    }

    /// <summary>
    /// Carrega os dados do grupo de usuário nos controles caso seja alteração.
    /// </summary>
    /// <param name="gru_id"></param>
    private void _LoadFromEntity(Guid gru_id)
    {
        try
        {
            SYS_Grupo grupo = new SYS_Grupo { gru_id = gru_id };
            SYS_GrupoBO.GetEntity(grupo);

            _VS_gru_id = grupo.gru_id;
            _txtNome.Text = grupo.gru_nome;
            UCComboSistemas1._Combo.SelectedValue = grupo.sis_id.ToString();
            UCComboVisao1._Combo.SelectedValue = grupo.vis_id.ToString();
            if (grupo.gru_situacao != 4)
                _chkBloqueado.Checked = ((grupo.gru_situacao == 1) ? false : true);
            else
            {
                _chkBloqueado.Checked = true;
                _chkBloqueado.Enabled = false;
            }

            UCComboSistemas1._Combo.Enabled = false;
            UCComboVisao1._Combo.Enabled = false;

            UCComboGrupo1.Visible = false;
            UCComboGrupo2.Visible = false;
        }
        catch (Exception e)
        {
            ApplicationWEB._GravaErro(e);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o grupo.", UtilBO.TipoMensagem.Erro);
        }
        finally
        {
            _updGrupos.Update();
        }
    }
    /// <summary>
    /// Insere e altera um grupo de usuário.
    /// </summary>
    private void _Salvar()
    {
        try
        {
            SYS_Grupo grupo = new SYS_Grupo
            {
               gru_id = _VS_gru_id
                , gru_nome = _txtNome.Text
                , gru_situacao = (_chkBloqueado.Checked ? Convert.ToByte(2) : Convert.ToByte(1))
                , sis_id = Convert.ToInt32(UCComboSistemas1._Combo.SelectedValue) > 0 ? Convert.ToInt32(UCComboSistemas1._Combo.SelectedValue) : Convert.ToInt32(null)
                , vis_id = Convert.ToInt32(UCComboVisao1._Combo.SelectedValue) > 0 ? Convert.ToInt32(UCComboVisao1._Combo.SelectedValue) : Convert.ToInt32(null)
                , IsNew = (_VS_gru_id != Guid.Empty) ? false : true
            };

            Guid gru_idPermissao = string.IsNullOrEmpty(UCComboGrupo1._Combo.SelectedValue) || UCComboGrupo1._Combo.SelectedValue == "-1" ? Guid.Empty : new Guid(UCComboGrupo1._Combo.SelectedValue);
            Guid gru_idUsuario = string.IsNullOrEmpty(UCComboGrupo2._Combo.SelectedValue) || UCComboGrupo2._Combo.SelectedValue == "-1" ? Guid.Empty : new Guid(UCComboGrupo2._Combo.SelectedValue);

            if (SYS_GrupoBO.Save(grupo, gru_idPermissao, gru_idUsuario, null))
            {
                if (_VS_gru_id == Guid.Empty)
                {
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Insert, "gru_id: " + grupo.gru_id);
                    __SessionWEB.PostMessages = UtilBO.GetErroMessage("Grupo incluído com sucesso.", UtilBO.TipoMensagem.Sucesso);
                }
                else
                {
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Update, "gru_id: " + grupo.gru_id);
                    __SessionWEB.PostMessages = UtilBO.GetErroMessage("Grupo alterado com sucesso.", UtilBO.TipoMensagem.Sucesso);
                }

                Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "Grupo/Busca.aspx", false);
            }
            else
            {
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar o grupo.", UtilBO.TipoMensagem.Erro);
            }
        }
        catch (CoreLibrary.Validation.Exceptions.ValidationException e)
        {            
            _lblMessage.Text = UtilBO.GetErroMessage(e.Message, UtilBO.TipoMensagem.Alerta);
        }
        catch (ArgumentException e)
        {            
            _lblMessage.Text = UtilBO.GetErroMessage(e.Message, UtilBO.TipoMensagem.Alerta);
        }
        catch (DuplicateNameException e)
        {            
            _lblMessage.Text = UtilBO.GetErroMessage(e.Message, UtilBO.TipoMensagem.Alerta);
        }        
        catch (Exception e)
        {
            ApplicationWEB._GravaErro(e);            
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar o grupo.", UtilBO.TipoMensagem.Erro);            
        }
        finally
        {
            _updGrupos.Update();
        }
    }

    #endregion

    #region EVENTOS

    protected void _btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "Grupo/Busca.aspx", false);
    }

    protected void _btnSalvar_Click(object sender, EventArgs e)
    {
        _Salvar();
    }

    #endregion 
}
