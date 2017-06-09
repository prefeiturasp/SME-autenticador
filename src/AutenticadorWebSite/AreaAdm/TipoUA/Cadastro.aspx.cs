using System;
using System.Data;
using Autenticador.Web.WebProject;
using Autenticador.BLL;
using Autenticador.Entities;

public partial class AreaAdm_TipoUA_Cadastro : MotherPageLogado
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

            Page.Form.DefaultFocus = _txtTipoUA.ClientID;
            Page.Form.DefaultButton = _btnSalvar.UniqueID;
        }
    }

    #region PROPRIEDADES

    private Guid _VS_tua_id
    {
        get
        {
            if (ViewState["_VS_tua_id"] != null)
                return new Guid(ViewState["_VS_tua_id"].ToString());
            return Guid.Empty;
        }
        set
        {
            ViewState["_VS_tua_id"] = value;
        }
    }

    #endregion
    
    #region METODOS

    /// <summary>
    /// Insere e altera um Tipo de Unidade Administrativa.
    /// </summary>
    private void _Salvar()
    {
        try
        {
            SYS_TipoUnidadeAdministrativa _TipoUA = new SYS_TipoUnidadeAdministrativa
            {
                tua_id = _VS_tua_id
                ,
                tua_nome = _txtTipoUA.Text
                ,
                tua_situacao = (_ckbBloqueado.Checked ? Convert.ToByte(2) : Convert.ToByte(1))
                ,
                IsNew = (_VS_tua_id != Guid.Empty) ? false : true
            };
            if (SYS_TipoUnidadeAdministrativaBO.Save(_TipoUA))
            {
                if (_VS_tua_id != Guid.Empty)
                {
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Update, "tua_id: " + _VS_tua_id);                
                    __SessionWEB.PostMessages = UtilBO.GetErroMessage("Tipo de unidade administrativa alterado com sucesso.", UtilBO.TipoMensagem.Sucesso);
                }
                else
                {
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Insert, "tua_id: " + _VS_tua_id);
                    __SessionWEB.PostMessages = UtilBO.GetErroMessage("Tipo de unidade administrativa incluído com sucesso.", UtilBO.TipoMensagem.Sucesso);
                }

                Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "TipoUA/Busca.aspx", false);                    
            }
            else
            {
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar o tipo de unidade administrativa.", UtilBO.TipoMensagem.Erro);
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
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar o tipo de unidade administrativa.", UtilBO.TipoMensagem.Erro);
        }
    }
    /// <summary>
    /// Carrega os dados do Tipo de Unidade Administrativa nos controles caso seja alteração.
    /// </summary>
    /// <param name="tua_id"></param>
    private void _Carregar(Guid tua_id)
    {
        try
        {
            SYS_TipoUnidadeAdministrativa _TipoUA = new SYS_TipoUnidadeAdministrativa { tua_id = tua_id };
            SYS_TipoUnidadeAdministrativaBO.GetEntity(_TipoUA);
            _VS_tua_id = _TipoUA.tua_id;
            _txtTipoUA.Text = _TipoUA.tua_nome;

            if (_TipoUA.tua_situacao == 2)
            {
                _ckbBloqueado.Checked = true;
            }
        }
        catch (Exception e)
        {
            ApplicationWEB._GravaErro(e); 
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o tipo de unidade administrativa.", UtilBO.TipoMensagem.Erro);
        }
    }
    #endregion

    protected void _btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "TipoUA/Busca.aspx", false);
    }

    protected void _btnSalvar_Click(object sender, EventArgs e)
    {
        _Salvar();
    }
}
