namespace AutenticadorWebSite.WebControls.Combos
{
    using System;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Autenticador.BLL;
    using Autenticador.Web.WebProject;

    public partial class UCComboServico : MotherUserControl
    {
        #region Delegates

        public delegate void SelectedIndexChanged();
        public event SelectedIndexChanged IndexChanged;

        #endregion

        #region Propriedades

        /// <summary>
        /// Retorna valor selecionado no combo
        /// </summary>
        public Int16 Valor
        {
            get
            {
                return Convert.ToInt16(ddlServico.SelectedValue);
            }
        }

        /// <summary>
        /// Retorna texto selecionado no combo
        /// </summary>
        public string Texto
        {
            get
            {
                return ddlServico.SelectedItem.Text;
            }
        }

        #endregion

        #region Eventos

        protected void ddlServico_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IndexChanged != null)
                IndexChanged();
        }

        #endregion

        #region Métodos

        public void Carregar()
        {
            ddlServico.Items.Clear();
            ddlServico.DataSource = SYS_ServicoBO.SelecionaServicos();
            ddlServico.Items.Insert(0, new ListItem("-- Selecione um serviço --", "-1", true));
            ddlServico.DataBind();
        }

        #endregion
    }
}