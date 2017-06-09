using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Autenticador.UserControlLibrary.Combos
{
    public partial class UCComboEntidade : Abstract_UCCombo
    {
        public override DropDownList _Combo
        {
            get
            {
                return this._ddlCombo;
            }
            set
            {
                this._ddlCombo = value;
            }
        }

        public override Label _Label
        {
            get
            {
                return this._lblRotulo;
            }
            set
            {
                this._lblRotulo = value;
            }
        }

        public override CompareValidator _Validator
        {
            get
            {
                return this._cpvCombo;
            }
            set
            {
                this._cpvCombo = value;
            }
        }

        protected override ObjectDataSource _Source
        {
            get
            {
                return this._odsCombo;
            }
            set
            {
                this._odsCombo = value;
            }
        }

        /// <summary>
        /// Mostra as entidades não excluídas logicamente no dropdownlist    
        /// </summary>
        public void _Load(Guid ent_id, byte ent_situacao)
        {
            this._Source.SelectMethod = "GetSelect";
            this._Source.SelectParameters.Clear();
            this._Source.SelectParameters.Add("ent_id", ent_id.ToString());
            this._Source.SelectParameters.Add("ten_id", Guid.Empty.ToString());
            this._Source.SelectParameters.Add("ent_razaoSocial", string.Empty);
            this._Source.SelectParameters.Add("ent_nomeFantasia", string.Empty);
            this._Source.SelectParameters.Add("ent_cnpj", string.Empty);
            this._Source.SelectParameters.Add("ent_codigo", string.Empty);
            this._Source.SelectParameters.Add("ent_situacao", Convert.ToString(ent_situacao));
            this._Source.SelectParameters.Add("paginado", "false");
            this._Source.DataBind();
        }

        /// <summary>
        /// Mostra as entidades não excluídas logicamente no dropdownlist filtrando por visao
        /// </summary>
        public void _LoadBy_UsuarioGrupoUA(Guid ent_id, Guid gru_id, Guid usu_id, byte ent_situacao)
        {
            this._Source.SelectMethod = "GetSelectBy_UsuarioGrupoUA";
            this._Source.SelectParameters.Clear();
            this._Source.SelectParameters.Add("ent_id", ent_id.ToString());
            this._Source.SelectParameters.Add("gru_id", gru_id.ToString());
            this._Source.SelectParameters.Add("usu_id", usu_id.ToString());
            this._Source.SelectParameters.Add("ent_situacao", Convert.ToString(ent_situacao));
            this._Source.SelectParameters.Add("paginado", "false");
            this._Source.DataBind();
        }

        /// <summary>
        /// Mostra as entidades não excluídas logicamente no dropdownlist    
        /// </summary>
        public void _LoadBy_SistemaEntidade(Guid ent_id, byte ent_situacao)
        {
            this._Source.SelectMethod = "GetSelectBy_SistemaEntidade";
            this._Source.SelectParameters.Clear();
            this._Source.SelectParameters.Add("ent_id", ent_id.ToString());
            this._Source.SelectParameters.Add("ten_id", Guid.Empty.ToString());
            this._Source.SelectParameters.Add("ent_razaoSocial", string.Empty);
            this._Source.SelectParameters.Add("ent_nomeFantasia", string.Empty);
            this._Source.SelectParameters.Add("ent_cnpj", "");
            this._Source.SelectParameters.Add("ent_situacao", Convert.ToString(ent_situacao));
            this._Source.SelectParameters.Add("paginado", "false");
            this._Source.DataBind();
        }

        protected void _odsCombo_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (IsPostBack)
                e.Cancel = true;
        }
    }
}