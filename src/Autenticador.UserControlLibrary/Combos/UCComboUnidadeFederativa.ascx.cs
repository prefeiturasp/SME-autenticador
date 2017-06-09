using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Autenticador.UserControlLibrary.Combos
{
    public partial class UCComboUnidadeFederativa : Abstract_UCCombo
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
        /// Mostra as unidades federativas não excluídas logicamente no dropdownlist    
        /// </summary>
        public void _Load(Guid pai_id, byte unf_situacao)
        {
            this._Source.SelectParameters.Clear();
            this._Source.SelectParameters.Add("unf_id", Guid.Empty.ToString());
            this._Source.SelectParameters.Add("pai_id", pai_id.ToString());
            this._Source.SelectParameters.Add("unf_nome", "");
            this._Source.SelectParameters.Add("unf_sigla", "");
            this._Source.SelectParameters.Add("unf_situacao", unf_situacao.ToString());
            this._Source.SelectParameters.Add("paginado", "false");
            this._Source.DataBind();
        }


        #endregion


        protected void _odsCombo_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (IsPostBack)
                e.Cancel = CancelarBinding;
        }
    }
}