using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autenticador.BLL;
using Autenticador.Entities;
using Autenticador.Web.WebProject;

public partial class AreaAdm_Permissao_Cadastro : MotherPageLogado
{
    #region Propriedade

    public Guid _VS_gru_id
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

    #endregion Propriedade

    #region Eventos Page Life Cycle

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager sm = ScriptManager.GetCurrent(this);
        if (sm != null)
        {
            sm.Scripts.Add(new ScriptReference(ArquivoJS.UiAriaTabs));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.Tabs));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.PastaScriptRaiz + "Telas/jsModuloSeguranca.js"));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.PastaScriptRaiz + "Telas/jsCadastroPermissoes.js"));
        }

        if (!IsPostBack)
        {
            if ((PreviousPage != null) && (PreviousPage.IsCrossPagePostBack))
            {
                _VS_gru_id = PreviousPage.EditItem;
                _lblSistema.Text = PreviousPage.SistemaNome;
                _lblGrupo.Text = PreviousPage.GrupoNome;
                _lbVisao.Text = PreviousPage.VisaoNome;
            }
            else
            {
                this._btnSalvar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;
            }
        }
    }

    #endregion Eventos Page Life Cycle

    #region Métodos

    /// <summary>
    /// Função recursiva, utilizada para configurar
    /// módulos e grupos de permissões
    /// </summary>
    /// <param name="lstPermissao">List de entidade SYS_GrupoPermissao</param>
    /// <param name="grvPermissao">GridView de permissão</param>
    /// <returns></returns>
    protected List<SYS_GrupoPermissao> AddGrupoPermissao(List<SYS_GrupoPermissao> lstPermissao, GridView grvPermissao)
    {
        List<SYS_GrupoPermissao> lst = new List<SYS_GrupoPermissao>();
        int sis_id = 0;
        int mod_id = 0;

        foreach (GridViewRow row in grvPermissao.Rows)
        {
            sis_id = Convert.ToInt32(grvPermissao.DataKeys[row.DataItemIndex].Values["sis_id"]);
            mod_id = Convert.ToInt32(grvPermissao.DataKeys[row.DataItemIndex].Values["mod_id"]);

            SYS_GrupoPermissao entityGrupoPermissao = new SYS_GrupoPermissao
            {
                gru_id = new Guid(grvPermissao.DataKeys[row.DataItemIndex].Values["gru_id"].ToString())
                ,
                sis_id = sis_id
                ,
                mod_id = mod_id
            };

            CheckBox chkConsulta = (CheckBox)row.FindControl("chkConsulta");
            entityGrupoPermissao.grp_consultar = chkConsulta != null && chkConsulta.Checked;

            CheckBox chkInserir = (CheckBox)row.FindControl("chkInserir");
            entityGrupoPermissao.grp_inserir = chkInserir != null && chkInserir.Checked;

            CheckBox chkAlterar = (CheckBox)row.FindControl("chkEditar");
            entityGrupoPermissao.grp_alterar = chkAlterar != null && chkAlterar.Checked;

            CheckBox chkExcluir = (CheckBox)row.FindControl("chkExcluir");
            entityGrupoPermissao.grp_excluir = chkExcluir != null && chkExcluir.Checked;

            lst.Add(entityGrupoPermissao);

            // Verifica se existe módulos filhos, caso exista utiliza recursividade
            GridView grvPermissoesChild = (GridView)row.FindControl("grvPermissoesChild");
            if (grvPermissoesChild != null)
                if (grvPermissoesChild.Rows.Count > 0)
                    lst = AddGrupoPermissao(lst, grvPermissoesChild);
        }

        // Obtém o módulo pai e Verifica se já está existe na estrutura de dados
        int mod_id_pai = SYS_GrupoBO.GetSelectPermissoesBy_ModuloPai(mod_id, sis_id).FirstOrDefault();
        int mod_id_pai_index = lstPermissao.FindIndex(p => p.mod_id == mod_id_pai && p.sis_id == sis_id);

        // Verifica se algum dos módulos possui permissão
        if (SYS_GrupoBO.GetSelectPermissoesBy_Estado(lst).Count == 0)
        {
            // Caso nenhum módulo possua permissão, o módulo pai também não terá permissão
            SYS_GrupoPermissao p = new SYS_GrupoPermissao
                                       {
                                           gru_id = _VS_gru_id,
                                           sis_id = sis_id,
                                           mod_id = mod_id_pai,
                                           grp_consultar = false,
                                           grp_inserir = false,
                                           grp_alterar = false,
                                           grp_excluir = false
                                       };

            if (mod_id_pai_index < 0)
                lstPermissao.Add(p);
            else
                lstPermissao[mod_id_pai_index] = p;
        }
        else
        {
            // Caso algum módulo possua permissão, o módulo pai também terá permissão
            SYS_GrupoPermissao p = new SYS_GrupoPermissao
                                       {
                                           gru_id = _VS_gru_id,
                                           sis_id = sis_id,
                                           mod_id = mod_id_pai,
                                           grp_consultar = true,
                                           grp_inserir = true,
                                           grp_alterar = true,
                                           grp_excluir = true
                                       };

            if (mod_id_pai_index < 0)
                lstPermissao.Add(p);
            else
                lstPermissao[mod_id_pai_index] = p;
        }

        lstPermissao = lstPermissao.Union(lst).ToList();
        return lstPermissao;
    }

    #endregion Métodos

    #region Eventos

    protected void _btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            List<SYS_GrupoPermissao> lstPermissoes = new List<SYS_GrupoPermissao>();

            // Configura List de entidade SYS_GrupoPermissao
            foreach (RepeaterItem item in _rptModulos.Items)
            {
                GridView grvPermissoes = (GridView)item.FindControl("UCPermissoes1").FindControl("grvPermissoes");
                lstPermissoes = AddGrupoPermissao(lstPermissoes, grvPermissoes);
            }

            // Salva List de entidade SYS_GrupoPermissao
            if (SYS_GrupoBO.SalvarPermissoes(lstPermissoes))
            {
                ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Update, "gru_id: " + _VS_gru_id);
                __SessionWEB.PostMessages = UtilBO.GetErroMessage("Permissões do grupo salvas com sucesso.", UtilBO.TipoMensagem.Sucesso);

                Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "Grupo/Busca.aspx", false);
            }
            else
            {
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar as permissões do grupo.", UtilBO.TipoMensagem.Erro);
            }
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar as permissões do grupo.", UtilBO.TipoMensagem.Erro);
        }
    }

    protected void _btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "Grupo/Busca.aspx", false);
    }

    #endregion Eventos
}