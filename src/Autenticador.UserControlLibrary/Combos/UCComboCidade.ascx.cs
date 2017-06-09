using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Autenticador.UserControlLibrary.Combos
{
    public partial class UCComboCidade : Abstract_UCCombo
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

        #region METODOS

        /// <summary>
        /// Mostra as cidades não excluídas logicamente no dropdownlist    
        /// </summary>
        public void _Load(byte cid_situacao)
        {
            this._Source.SelectParameters.Clear();
            this._Source.SelectMethod = "GetSelect";
            this._Source.SelectParameters.Add("cid_id", Guid.Empty.ToString());
            this._Source.SelectParameters.Add("pai_id", Guid.Empty.ToString());
            this._Source.SelectParameters.Add("unf_id", Guid.Empty.ToString());
            this._Source.SelectParameters.Add("cid_nome", "");
            this._Source.SelectParameters.Add("unf_nome", "");
            this._Source.SelectParameters.Add("unf_sigla", "");
            this._Source.SelectParameters.Add("pai_nome", "");
            this._Source.SelectParameters.Add("cid_situacao", cid_situacao.ToString());
            this._Source.SelectParameters.Add("paginado", "false");
            this._Source.DataBind();
        }

        /// <summary>
        /// Carrega Combo de cidades de um estado
        /// </summary>
        public void _CarregaPorEstado(Guid unf_id)
        { 
            try
            {
                this._ddlCombo.Items.Clear();
                this._Source.SelectParameters.Clear();
                this._Source.SelectMethod = "GetSelectPorEstado";                
                this._Source.SelectParameters.Add("unf_id", unf_id.ToString());
                this._Source.SelectParameters.Add("paginado", "false");
                this._Source.DataBind();

                if (this._ddlCombo.Items.Count == 0)
                {
                    _ddlCombo.Items.Clear();
                    ListItem item = new ListItem("-- Selecione uma opção --", Guid.Empty.ToString());
                    _ddlCombo.Items.Add(item);
                    _ddlCombo.SelectedIndex = -1;
                }
                else
                    this._ddlCombo.Items.Insert(0, new ListItem("-- Selecione uma opção --", Guid.Empty.ToString(), true));
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}