using System;
using System.Data;
using Autenticador.Web.WebProject;
using Autenticador.BLL;
using Autenticador.Entities;

public partial class AreaAdm_TipoDeficiencia_Cadastro : MotherPageLogado
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((PreviousPage != null) && (PreviousPage.IsCrossPagePostBack))
                _LoadFromEntity(PreviousPage.EditItem);
            else
            {
                _ckbBloqueado.Visible = false;
                this._btnSalvar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;
            }

            Page.Form.DefaultFocus = _txtTipoDeficiencia.ClientID;
            Page.Form.DefaultButton = _btnSalvar.UniqueID;
        }
    }

    #region PROPRIEDADES

    private Guid _VS_tde_id
    {
        get
        {
            if (ViewState["_VS_tde_id"] != null)
                return new Guid(ViewState["_VS_tde_id"].ToString());
            return Guid.Empty;
        }
        set
        {
            ViewState["_VS_tde_id"] = value;
        }
    }

    #endregion

    #region METODOS

    private void _LoadFromEntity(Guid tde_id)
    {
        try
        {
            PES_TipoDeficiencia TipoDeficiencia = new PES_TipoDeficiencia { tde_id = tde_id };
            PES_TipoDeficienciaBO.GetEntity(TipoDeficiencia);
            _VS_tde_id = TipoDeficiencia.tde_id;
            _txtTipoDeficiencia.Text = TipoDeficiencia.tde_nome;
            _ckbBloqueado.Checked = !TipoDeficiencia.tde_situacao.Equals(1);
            _ckbBloqueado.Visible = true;
        }
        catch (Exception e)
        {
            ApplicationWEB._GravaErro(e);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o tipo de deficiência.", UtilBO.TipoMensagem.Erro);
        }
    }

    protected void _Salvar()
    {
        try
        {
            PES_TipoDeficiencia TipoDeficiencia = new PES_TipoDeficiencia
            {
                tde_id = _VS_tde_id
                ,
                tde_nome = _txtTipoDeficiencia.Text
                ,
                tde_situacao = (_ckbBloqueado.Checked ? Convert.ToByte(2) : Convert.ToByte(1))
                ,
                IsNew = (_VS_tde_id != Guid.Empty) ? false : true
            };

            if (PES_TipoDeficienciaBO.Save(TipoDeficiencia))
            {
                if (_VS_tde_id != Guid.Empty)
                {
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Update, "tde_id:" + TipoDeficiencia.tde_id);
                    __SessionWEB.PostMessages = UtilBO.GetErroMessage("Tipo de deficiência alterado com sucesso.", UtilBO.TipoMensagem.Sucesso);
                }
                else
                {
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Insert, "tde_id:" + TipoDeficiencia.tde_id);
                    __SessionWEB.PostMessages = UtilBO.GetErroMessage("Tipo de deficiência incluído com sucesso.", UtilBO.TipoMensagem.Sucesso);
                }

                Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "TipoDeficiencia/Busca.aspx", false);
            }
            else
            {
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar o tipo de deficiência.", UtilBO.TipoMensagem.Erro);
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
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar o tipo de deficiência.", UtilBO.TipoMensagem.Erro);
        }
    }

    #endregion

    #region EVENTOS

    protected void _btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "TipoDeficiencia/Busca.aspx", false);
    }

    protected void _btnSalvar_Click(object sender, EventArgs e)
    {
        _Salvar();
    }

    #endregion
}
