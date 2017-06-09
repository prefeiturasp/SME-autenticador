using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Autenticador.UserControlLibrary.Combos
{
    public partial class UCComboTipoDocumentacao : Abstract_UCCombo
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
        /// Mostra os tipos de documentação não excluídos logicamente no dropdownlist    
        /// </summary>
        public void _Load(Guid tdo_id, byte tdo_situacao)
        {
            this._Source.SelectParameters.Clear();

            if (tdo_id != Guid.Empty)
                this._Source.SelectParameters.Add("tdo_id", tdo_id.ToString());
            else
                this._Source.SelectParameters.Add("tdo_id", Guid.Empty.ToString());

            this._Source.SelectParameters.Add("tdo_nome", "");
            this._Source.SelectParameters.Add("tdo_sigla", "");
            this._Source.SelectParameters.Add("tdo_situacao", Convert.ToString(tdo_situacao));
            this._Source.SelectParameters.Add("paginado", "false");
            this._Source.DataBind();
        }

        #endregion

        protected void _odsCombo_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (IsPostBack)
                e.Cancel = true;
        }
    }
}