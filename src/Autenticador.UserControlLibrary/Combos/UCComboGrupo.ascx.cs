using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Autenticador.UserControlLibrary.Combos
{
    public partial class UCComboGrupo : Abstract_UCCombo
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

        #endregion

        #region METODOS

        /// <summary>
        /// Mostra os grupos não excluídos logicamente no dropdownlist    
        /// </summary>
        public void _Load(int sis_id, int vis_id)
        {
            this._Source.SelectParameters.Clear();
            this._Source.SelectParameters.Add("sis_id", sis_id.ToString());
            this._Source.SelectParameters.Add("vis_id", vis_id.ToString());            
            this._Source.SelectParameters.Add("paginado", "false");
            this._Source.DataBind();
        }

        #endregion
    }
}