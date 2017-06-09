using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autenticador.BLL;
using Autenticador.Web.WebProject;

public partial class Busca_UA : MotherPageLogado
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {        
            this.fdsResultados.Visible = false;

            UCComboEntidade1.Inicialize("Entidade");
            UCComboEntidade1._EnableValidator = false;
            UCComboEntidade1._ShowSelectMessage = true;
            UCComboEntidade1._Load(Guid.Empty, 0);

            this.UCComboTipoUnidadeAdministrativa1._ShowSelectMessage = true;
            this.UCComboTipoUnidadeAdministrativa1._Load(new Guid(), 0);

            this._dgvUA.PageSize = ApplicationWEB._Paginacao;

            if (!string.IsNullOrEmpty(this.Request.QueryString["ent_idObrigatorio"]))
            {
                if (new Guid(this.Request.QueryString["ent_idObrigatorio"]) == Guid.Empty)
                {
                    this._lblMessage.Text = UtilBO.GetErroMessage("Selecione uma entidade na tela principal.", UtilBO.TipoMensagem.Alerta);
                    _btnPesquisar.Visible = false;
                }
                else
                {
                    UCComboEntidade1._Combo.SelectedValue = this.Request.QueryString["ent_idObrigatorio"];
                    _btnPesquisar.Visible = true;
                }

                UCComboEntidade1._Label.CssClass = "hide";
                UCComboEntidade1._Combo.CssClass = "hide";
            }   

            if (this.Request["ent_id"] != null)
                UCComboEntidade1._Combo.SelectedValue = this.Request["ent_id"].ToString();

            if (this.Request["entEnabled"] != null)
            {
                if (!Convert.ToBoolean(this.Request["entEnabled"].ToString()))
                {
                    UCComboEntidade1._Label.CssClass = "hide"; // Visible = Convert.ToBoolean(this.Request["entEnabled"].ToString());
                    UCComboEntidade1._Combo.CssClass = "hide"; // Visible = Convert.ToBoolean(this.Request["entEnabled"].ToString());
                }
            }

            if (this.Request["tua_id"] != null)
                UCComboTipoUnidadeAdministrativa1._Combo.SelectedValue = this.Request["tua_id"].ToString();

            if (this.Request["tuaEnabled"] != null)
            {
                if (!Convert.ToBoolean(this.Request["tuaEnabled"].ToString()))
                {
                    UCComboTipoUnidadeAdministrativa1._Label.CssClass = "hide"; // Visible = Convert.ToBoolean(this.Request["entEnabled"].ToString());
                    UCComboTipoUnidadeAdministrativa1._Combo.CssClass = "hide"; // Visible = Convert.ToBoolean(this.Request["entEnabled"].ToString());
                }
            }
        }
    }
    protected void odsUA_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (e.ExecutingSelectCount)
            e.InputParameters.Clear();
        else
        {
            e.InputParameters.Add("gru_id", "00000000-0000-0000-0000-000000000000"); //this.__SessionWEB.__UsuarioWEB.Grupo.gru_id);
            e.InputParameters.Add("usu_id", "00000000-0000-0000-0000-000000000000"); //this.__SessionWEB.__UsuarioWEB.Usuario.usu_id);
        }
    }
    protected void _btnPesquisar_Click(object sender, EventArgs e)
    {
        this._dgvUA.PageIndex = 0;
        odsUA.DataBind();
        this.fdsResultados.Visible = true;
    }
    protected void _dgvUA_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //Retorna o valor selecionado pelo usuário
        UtilBO.SetScriptRetornoBusca(Page, Request["buscaID"], new string[] { this._dgvUA.DataKeys[e.NewEditIndex].Values[0].ToString(), this._dgvUA.DataKeys[e.NewEditIndex].Values[1].ToString(), this._dgvUA.DataKeys[e.NewEditIndex].Values[2].ToString() });
        e.Cancel = true;
    }
}
