namespace AutenticadorWebSite.WebControls.Combos
{
    using System;
    using System.Linq;
    using System.Web.UI.WebControls;
    using Autenticador.BLL;
    using Autenticador.Entities;
    using Autenticador.Web.WebProject;

    public partial class UCComboTemaPadrao : MotherUserControl
    {
        #region Delegates

        public delegate void SelectedIndexChanged();
        public event SelectedIndexChanged IndexChanged;

        #endregion Delegates

        #region Propriedades

        /// <summary>
        /// Retorna valor selecionado no combo
        /// </summary>
        public int Valor
        {
            get
            {
                return Convert.ToInt32(ddlTemaPadrao.SelectedValue);
            }

            set
            {
                ddlTemaPadrao.SelectedValue = value.ToString();
            }
        }

        /// <summary>
        /// Retorna texto selecionado no combo
        /// </summary>
        public string Texto
        {
            get
            {
                return ddlTemaPadrao.SelectedItem.Text;
            }
        }

        /// <summary>
        /// Retorna e seta o nome do combo
        /// </summary>
        public string NomeCombo
        {
            get
            {
                return lblTemaPadrao.Text;
            }

            set
            {
                lblTemaPadrao.Text = value;
                cpvTemaPadrao.ErrorMessage = value.Replace('*', ' ') + " é obrigatório.";
            }
        }

        /// <summary>
        /// Adciona e remove a mensagem "Selecione um tema padrão" do dropdownlist.
        /// Por padrão é false e a mensagem "Selecione um tema padrão" não é exibida.
        /// </summary>
        public bool MostrarMensagemSelecione
        {
            get
            {
                return ddlTemaPadrao.Items.Cast<ListItem>().Any(p => Convert.ToInt32(p.Value) <= 0);
            }

            set
            {
                ddlTemaPadrao.AppendDataBoundItems = value;

                if (value && !ddlTemaPadrao.Items.Cast<ListItem>().Any(p => Convert.ToInt32(p.Value) <= 0))
                {
                    ddlTemaPadrao.Items.Insert(0, new ListItem("-- Selecione um tema padrão --", "-1"));
                }

                if (!value && ddlTemaPadrao.Items.Cast<ListItem>().Any(p => Convert.ToInt32(p.Value) <= 0))
                {
                    ddlTemaPadrao.Items.Cast<ListItem>().ToList().RemoveAll(p => Convert.ToInt32(p.Value) <= 0);
                }
            }
        }

        /// <summary>
        /// Propriedade que seta a label e a validação do combo
        /// </summary>
        public bool Obrigatorio
        {
            set
            {
                if (value)
                {
                    if ((!NomeCombo.EndsWith("*")))
                        NomeCombo += " *";
                }
                else
                {
                    if (NomeCombo.EndsWith("*"))
                        NomeCombo = NomeCombo.Replace(" *", "");
                }

                cpvTemaPadrao.Visible = value;
            }
        }

        /// <summary>
        /// Seta o validationGroup do combo.
        /// </summary>
        public string ValidationGroup
        {
            set
            {
                cpvTemaPadrao.ValidationGroup = value;
            }
        }

        /// <summary>
        /// ClientID do combo
        /// </summary>
        public string Combo_ClientID
        {
            get
            {
                return ddlTemaPadrao.ClientID;
            }
        }

        /// <summary>
        /// Propriedade que verifica quantos items existem no combo
        /// </summary>
        public int QuantidadeItensCombo
        {
            get
            {
                return ddlTemaPadrao.Items.Count - (MostrarMensagemSelecione ? 1 : 0);
            }
        }

        /// <summary>
        /// Deixa o combo habilitado de acordo com o valor passado
        /// </summary>
        public bool PermiteEditar
        {
            get
            {
                return ddlTemaPadrao.Enabled;
            }
            set
            {
                ddlTemaPadrao.Enabled = value;
            }
        }

        #endregion Propriedades

        #region Eventos

        protected void ddlTemaPadrao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IndexChanged != null)
                IndexChanged();
        }

        #endregion Eventos

        #region Métodos

        /// <summary>
        /// Carrega o combo de temas com todos os temas ativos no sistema.
        /// </summary>
        public void Carregar()
        {
            bool mostraMensagemSelecione = MostrarMensagemSelecione;

            var temas = from CFG_TemaPadrao tema in CFG_TemaPadraoBO.SelecionaAtivos()
                        select new
                        {
                            tep_id = tema.tep_id
                            ,
                            tep_descricao = tema.tep_nome
                        };

            ddlTemaPadrao.Items.Clear();
            ddlTemaPadrao.DataSource = temas;
            MostrarMensagemSelecione = mostraMensagemSelecione;
            ddlTemaPadrao.DataBind();

            SelecionaPrimeiroItem();
        }

        /// <summary>
        /// Seleciona o primeiro item do combo, caso existir apenas um.
        /// </summary>
        private void SelecionaPrimeiroItem()
        {
            if (QuantidadeItensCombo == 1)
            {
                ddlTemaPadrao.SelectedIndex = ddlTemaPadrao.Items.Count - 1;
            }
        }

        #endregion Métodos

        #region Page Life Cycle

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ddlTemaPadrao.AutoPostBack = IndexChanged != null;
        }

        #endregion Page Life Cycle
    }
}