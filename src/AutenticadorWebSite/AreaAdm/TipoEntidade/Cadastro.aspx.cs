using System;
using System.Data;
using Autenticador.Web.WebProject;
using Autenticador.BLL;
using Autenticador.Entities;

public partial class AreaAdm_TipoEntidade_Cadastro : MotherPageLogado
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

            Page.Form.DefaultFocus = _txtTipoEntidade.ClientID;
            Page.Form.DefaultButton = _btnSalvar.UniqueID;
        }
    }

    #region PROPRIEDADES

    private Guid _VS_ten_id
    {
        get
        {
            if (ViewState["_VS_ten_id"] != null)
                return new Guid(ViewState["_VS_ten_id"].ToString());
            return Guid.Empty;
        }
        set
        {
            ViewState["_VS_ten_id"] = value;
        }
    }

    #endregion

    #region METODOS

    /// <summary>
    /// Insere e altera um Tipo de Entidade.
    /// </summary>
    private void _Salvar()
    {
        try
        {
            SYS_TipoEntidade _TipoEntidade = new SYS_TipoEntidade
            {
                ten_id = _VS_ten_id
                ,
                ten_nome = _txtTipoEntidade.Text
                ,
                ten_situacao = (_ckbBloqueado.Checked ? Convert.ToByte(2) : Convert.ToByte(1))
                ,
                IsNew = (_VS_ten_id != Guid.Empty) ? false : true
            };
            if (SYS_TipoEntidadeBO.Save(_TipoEntidade))
            {
                if (_VS_ten_id != Guid.Empty)
                {
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Update, "ten_id:" + _TipoEntidade.ten_id);
                    __SessionWEB.PostMessages = UtilBO.GetErroMessage("Tipo de entidade alterado com sucesso.", UtilBO.TipoMensagem.Sucesso);
                }
                else
                {
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Insert, "ten_id:" + _TipoEntidade.ten_id);
                    __SessionWEB.PostMessages = UtilBO.GetErroMessage("Tipo de entidade incluído com sucesso.", UtilBO.TipoMensagem.Sucesso);
                }
                Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "TipoEntidade/Busca.aspx", false);
            }
            else
            {
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar o tipo de entidade.", UtilBO.TipoMensagem.Erro);
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
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar o tipo de entidade.", UtilBO.TipoMensagem.Erro);
        }
    }
    /// <summary>
    /// Carrega os dados do Tipo de Entidade nos controles caso seja alteração.
    /// </summary>
    /// <param name="ten_id"></param>
    private void _Carregar(Guid ten_id)
    {
        try
        {
            SYS_TipoEntidade _TipoEntidade = new SYS_TipoEntidade { ten_id = ten_id };
            SYS_TipoEntidadeBO.GetEntity(_TipoEntidade);
            _VS_ten_id = _TipoEntidade.ten_id;
            _txtTipoEntidade.Text = _TipoEntidade.ten_nome;
            if (_TipoEntidade.ten_situacao == 2)
            {
                _ckbBloqueado.Checked = true;
            }
        }
        catch (Exception e)
        {
            ApplicationWEB._GravaErro(e);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o tipo de entidade.", UtilBO.TipoMensagem.Erro);
        }
    }

    #endregion

    protected void _btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "TipoEntidade/Busca.aspx", false);
    }

    protected void _btnSalvar_Click(object sender, EventArgs e)
    {
        _Salvar();
    }
}
