using System;
using Autenticador.Web.WebProject;
using Autenticador.Entities;
using Autenticador.BLL;
using System.Data;

public partial class AreaAdm_TipoEscolaridade_Cadastro : MotherPageLogado
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((PreviousPage != null) && (PreviousPage.IsCrossPagePostBack))
            {
                _VS_tes_id = PreviousPage.EditItem;
                _LoadFromEntity();
            }
            else
            {
                _ckbBloqueado.Visible = false;
                _btnSalvar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;
            }

            Page.Form.DefaultFocus = _txtTipoEscolaridade.ClientID;
            Page.Form.DefaultButton = _btnSalvar.UniqueID;
        }
    }

    #region PROPRIEDADES

    private Guid _VS_tes_id
    {
        get
        {
            if (ViewState["_VS_tes_id"] != null)
                return new Guid(ViewState["_VS_tes_id"].ToString());
            return Guid.Empty;
        }
        set
        {
            ViewState["_VS_tes_id"] = value;
        }
    }

    #endregion

    #region METODOS

    /// <summary>
    /// Insere e altera um Tipo de Escolaridade.
    /// </summary>
    private void _Salvar()
    {
        try
        {
            PES_TipoEscolaridade entity = new PES_TipoEscolaridade();

            if (_VS_tes_id != Guid.Empty)
            {
                entity.tes_id = _VS_tes_id;
                PES_TipoEscolaridadeBO.GetEntity(entity);
            }

            entity.tes_id = _VS_tes_id;
            entity.tes_nome = _txtTipoEscolaridade.Text;
            entity.tes_ordem = (_VS_tes_id != Guid.Empty) ? entity.tes_ordem : 1;
            entity.tes_situacao = (_ckbBloqueado.Checked ? Convert.ToByte(2) : Convert.ToByte(1));
            entity.IsNew = (_VS_tes_id != Guid.Empty) ? false : true;

            if (PES_TipoEscolaridadeBO.Save(entity))
            {
                if (_VS_tes_id != Guid.Empty)
                {
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Update, "tes_id: " + _VS_tes_id);
                    __SessionWEB.PostMessages = UtilBO.GetErroMessage("Tipo de escolaridade alterado com sucesso.", UtilBO.TipoMensagem.Sucesso);
                }
                else
                {
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Insert, "tes_id: " + _VS_tes_id);
                    __SessionWEB.PostMessages = UtilBO.GetErroMessage("Tipo de escolaridade incluído com sucesso.", UtilBO.TipoMensagem.Sucesso);
                }

                Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "TipoEscolaridade/Busca.aspx", false);
            }
            else
            {
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar o tipo de escolaridade.", UtilBO.TipoMensagem.Erro);
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
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar o tipo de escolaridade.", UtilBO.TipoMensagem.Erro);
        }
    }

    /// <summary>
    /// Carrega os dados do Tipo de Escolaridade nos controles caso seja alteração.
    /// </summary>
    private void _LoadFromEntity()
    {
        try
        {
            PES_TipoEscolaridade entity = new PES_TipoEscolaridade { tes_id = _VS_tes_id };
            PES_TipoEscolaridadeBO.GetEntity(entity);

            _txtTipoEscolaridade.Text = entity.tes_nome;
            if (entity.tes_situacao == 2)
            {
                _ckbBloqueado.Checked = true;
            }
        }
        catch (Exception e)
        {
            ApplicationWEB._GravaErro(e);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o tipo de escolaridade.", UtilBO.TipoMensagem.Erro);
        }
    }

    #endregion

    protected void _btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "TipoEscolaridade/Busca.aspx", false);
    }

    protected void _btnSalvar_Click(object sender, EventArgs e)
    {
        _Salvar();
    }
}
