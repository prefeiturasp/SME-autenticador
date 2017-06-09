using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Autenticador.UserControlLibrary.Combos
{
    public partial class UCComboPais : Abstract_UCCombo
    {
        #region PROPRIEDADE
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

        public bool CancelaSelect = true;

        #endregion

        #region METODOS
        public void _Load(byte pai_situacao)
        {
            this._Source.SelectParameters.Clear();
            this._Source.SelectParameters.Add("pai_id", Guid.Empty.ToString());
            this._Source.SelectParameters.Add("pai_nome", "");
            this._Source.SelectParameters.Add("pai_sigla", "");
            this._Source.SelectParameters.Add("pai_situacao", Convert.ToString(pai_situacao));
            this._Source.SelectParameters.Add("paginado", "false");
            this._Source.DataBind();
        }

        protected void _odsCombo_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.Cancel = IsPostBack && CancelaSelect;
        }

        #endregion
    
    }
}