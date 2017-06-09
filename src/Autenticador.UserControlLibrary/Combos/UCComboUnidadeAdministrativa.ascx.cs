using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Autenticador.UserControlLibrary.Combos
{
    public partial class UCComboUnidadeAdministrativa : Abstract_UCCombo
    {

        #region PROPRIEDADES
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

        public bool CancelarBinding = true;

        #endregion

        #region METODOS

        /// <summary>
        /// Mostra as unidades administrativas não excluídas logicamente no dropdownlist    
        /// </summary>
        public void _Load(Guid ent_id, Guid uad_id, byte uad_situacao)
        {
            this._Source.SelectParameters.Clear();

            this._Source.SelectMethod = "GetSelect";
            this._Source.SelectParameters.Add("gru_id", Guid.Empty.ToString());
            this._Source.SelectParameters.Add("usu_id", Guid.Empty.ToString());
            this._Source.SelectParameters.Add("tua_id", Guid.Empty.ToString());
            this._Source.SelectParameters.Add("ent_id", ent_id.ToString());
            this._Source.SelectParameters.Add("uad_id", uad_id.ToString());
            this._Source.SelectParameters.Add("uad_nome", string.Empty);
            this._Source.SelectParameters.Add("uad_codigo", string.Empty);
            this._Source.SelectParameters.Add("uad_situacao", Convert.ToString(uad_situacao));
            this._Source.SelectParameters.Add("paginado", "false");
            _Source.DataBind();
        }

        public void _LoadBy_UsuarioGrupoUA(Guid ent_id, Guid uad_id, Guid usu_id, Guid gru_id, byte uad_situacao)
        {
            this._Source.SelectParameters.Clear();

            this._Source.SelectMethod = "GetSelectBy_UsuarioGrupoUA";
            this._Source.SelectParameters.Add("gru_id", gru_id.ToString());
            this._Source.SelectParameters.Add("usu_id", usu_id.ToString());
            this._Source.SelectParameters.Add("tua_id", Guid.Empty.ToString());
            this._Source.SelectParameters.Add("ent_id", ent_id.ToString());
            this._Source.SelectParameters.Add("uad_id", uad_id.ToString());
            this._Source.SelectParameters.Add("uad_nome", string.Empty);
            this._Source.SelectParameters.Add("uad_codigo", string.Empty);
            this._Source.SelectParameters.Add("uad_situacao", Convert.ToString(uad_situacao));
            this._Source.SelectParameters.Add("paginado", "false");
            _Source.DataBind();
        }

        #endregion

        protected void _odsCombo_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (IsPostBack)
                e.Cancel = CancelarBinding;
        }
    }
}