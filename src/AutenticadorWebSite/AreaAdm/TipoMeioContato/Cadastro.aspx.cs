using System;
using System.Data;
using Autenticador.Web.WebProject;
using Autenticador.BLL;
using Autenticador.Entities;

public partial class AreaAdm_TipoMeioContato_Cadastro : MotherPageLogado
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((PreviousPage != null) && (PreviousPage.IsCrossPagePostBack))
                _Carregar(PreviousPage.EditItem);
            else
            {
                _ckbBloqueado.Visible = false;
                this._btnSalvar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;
            }

            Page.Form.DefaultFocus = _txtTipoMeioContato.ClientID;
            Page.Form.DefaultButton = _btnSalvar.UniqueID;    
        }
    }

    #region PROPRIEDADES

    private Guid _VS_tmc_id
    {
        get
        {
            if (ViewState["_VS_tmc_id"] != null)
                return new Guid(ViewState["_VS_tmc_id"].ToString());
            return Guid.Empty;
        }
        set
        {
            ViewState["_VS_tmc_id"] = value;
        }
    }
    #endregion

    #region METODOS
    /// <summary>
    /// Insere e altera um Tipo de Meio Contato.
    /// </summary>
    private void _Salvar()
    {
        try
        {
            SYS_TipoMeioContato _TipoMeioContato = new SYS_TipoMeioContato
            {
                tmc_id = _VS_tmc_id
                ,
                tmc_nome = _txtTipoMeioContato.Text
                ,
                tmc_validacao = Convert.ToByte(_ddlValidacao.SelectedValue)
                ,
                tmc_situacao = _ckbBloqueado.Checked ? Convert.ToByte(2) : Convert.ToByte(1)
                ,
                IsNew = (_VS_tmc_id != Guid.Empty) ? false : true
            };

            if (SYS_TipoMeioContatoBO.Save(_TipoMeioContato))
            {
                if (_VS_tmc_id != Guid.Empty)
                {
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Update, "tmc_id: " + _VS_tmc_id);
                    __SessionWEB.PostMessages = UtilBO.GetErroMessage("Tipo de meio contato alterado com sucesso.", UtilBO.TipoMensagem.Sucesso);
                }
                else
                {
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Insert, "tmc_id: " + _VS_tmc_id);
                    __SessionWEB.PostMessages = UtilBO.GetErroMessage("Tipo de meio contato incluído com sucesso.", UtilBO.TipoMensagem.Sucesso);
                }

                Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "TipoMeioContato/Busca.aspx", false);                   
            }
            else
            {
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar o tipo de meio contato.", UtilBO.TipoMensagem.Erro);
            }
        }
        catch (CoreLibrary.Validation.Exceptions.ValidationException e)
        {
            _lblMessage.Text = UtilBO.GetErroMessage(e.Message, UtilBO.TipoMensagem.Alerta);
        }
        catch (DuplicateNameException e)
        {
            _lblMessage.Text = UtilBO.GetErroMessage(e.Message, UtilBO.TipoMensagem.Alerta);
        }
        catch (ArgumentException e)
        {
            _lblMessage.Text = UtilBO.GetErroMessage(e.Message, UtilBO.TipoMensagem.Alerta);
        }
        catch (Exception e)
        {
            ApplicationWEB._GravaErro(e); 
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar o tipo de meio contato.", UtilBO.TipoMensagem.Erro);
        }
    }
    /// <summary>
    /// Carrega os dados do Tipo de Meio Contato nos controles caso seja alteração.
    /// </summary>
    /// <param name="tmc_id"></param>
    private void _Carregar(Guid tmc_id)
    {
        try
        {
            SYS_TipoMeioContato _TipoMeioContato = new SYS_TipoMeioContato { tmc_id = tmc_id };
            SYS_TipoMeioContatoBO.GetEntity(_TipoMeioContato);
            _VS_tmc_id = _TipoMeioContato.tmc_id;
            _txtTipoMeioContato.Text = _TipoMeioContato.tmc_nome;
            _ddlValidacao.SelectedValue = _TipoMeioContato.tmc_validacao.ToString();

            if (_TipoMeioContato.tmc_situacao == 2)
            {
                _ckbBloqueado.Checked = true;
            }
        }
        catch (Exception e)
        {
            ApplicationWEB._GravaErro(e); 
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o tipo de meio contato.", UtilBO.TipoMensagem.Erro);
        }
    }
    #endregion

    protected void _btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "TipoMeioContato/Busca.aspx", false);
    }

    protected void _btnSalvar_Click(object sender, EventArgs e)
    {
        _Salvar();
    }
}
