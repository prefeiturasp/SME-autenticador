using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autenticador.Web.WebProject;
using Autenticador.BLL;
using Autenticador.Entities;

public partial class AreaAdm_ConfiguracoesAuditoria_Cadastro : MotherPageLogado
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((PreviousPage != null) && (PreviousPage.IsCrossPagePostBack))
            {
                this._VS_sis_id = PreviousPage.EditItem_sis_id;
                _Carregar_dgvModulo();
            }
            else
            {
                this._lblMessage.Text = UtilBO.GetErroMessage("É necessário selecionar um sistema.", UtilBO.TipoMensagem.Alerta );
                this._Carregar_dgvModulo();
                this._dgvModulo.Visible = false;
                this._btnSalvar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;
            }
            string message = this.__SessionWEB.PostMessages;
            if (!String.IsNullOrEmpty(message))
                this._lblMessage.Text = message;
            this._dgvModulo.PageIndex = 0;
            this._dgvModulo.PageSize = ApplicationWEB._Paginacao;
        }
    }

    #region PROPRIEDADES
    
    public int EditItem_sis_id
    {
        get
        {
            return Convert.ToInt32(_dgvModulo.DataKeys[_dgvModulo.EditIndex].Value);                
        }
    }

    private int _VS_sis_id
    {
        /// <summary>
        /// Armazena o id do sistema
        /// </summary>
        get
        {
            if (ViewState["_VS_sis_id"] != null)
                return Convert.ToInt32(ViewState["_VS_sis_id"]);
            return -1;
        }
        set
        {
            ViewState["_VS_sis_id"] = value;
        }
    }    

    #endregion

    private void _Carregar_dgvModulo()
    {
       
        this._dgvModulo.PageIndex = 0;
        this._odsModulo.SelectParameters.Clear();
        this._odsModulo.SelectMethod = "GetSelect_by_Sis_id";
        this._odsModulo.SelectParameters.Add("sis_id", Convert.ToString(this._VS_sis_id));
        this._odsModulo.SelectParameters.Add("paginado", "false");
        this._odsModulo.SelectParameters.Add("currentPage", string.Empty);
        this._odsModulo.SelectParameters.Add("pageSize", string.Empty);
        this._odsModulo.DataBind();

        if (this._dgvModulo.Rows.Count == 0)
            this._btnSalvar.Visible = false;
    }

    private void _Salvar()
    {
        try
        {
            GridView grd = _dgvModulo;
            List<SYS_Modulo> lst = new List<SYS_Modulo>();
            foreach (GridViewRow row in grd.Rows)
            {
                SYS_Modulo mod = new SYS_Modulo();
                mod.sis_id = _VS_sis_id;
                mod.mod_id = Convert.ToInt32(grd.DataKeys[row.DataItemIndex].Values[0]);

                CheckBox ckbAuditoria = (CheckBox)row.Cells[2].FindControl("_ckbAuditoria");
                if (ckbAuditoria != null)
                    mod.mod_auditoria = ckbAuditoria.Checked;
                else
                    mod.mod_auditoria = false;

                lst.Add(mod);
            }
            if (new SYS_ModuloBO().SalvarAuditoria(lst)) 
            {
                ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Update, "sis_id: " + _VS_sis_id);
                __SessionWEB.PostMessages = UtilBO.GetErroMessage("Configuração de auditoria salva com sucesso.", UtilBO.TipoMensagem.Sucesso);
                Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "ConfiguracoesAuditoria/Busca.aspx", false);
            }
            else
            {                            
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar a configuração de auditoria.", UtilBO.TipoMensagem.Erro );
            }                        
         
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar a configuração de auditoria.", UtilBO.TipoMensagem.Erro );
        }
    }  

    protected void _odsModulo_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (e.ExecutingSelectCount)
            e.InputParameters.Clear();
    }
    protected void _btnSalvar_Click(object sender, EventArgs e)
    {
        this._Salvar();
    }
    protected void _btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect(this.__SessionWEB._AreaAtual._Diretorio + "ConfiguracoesAuditoria/Busca.aspx", false);
    }
}

