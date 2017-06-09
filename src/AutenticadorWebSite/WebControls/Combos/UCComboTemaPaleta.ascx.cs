namespace AutenticadorWebSite.WebControls.Combos
{
    using System;
    using System.Linq;
    using System.Web.UI.WebControls;
    using Autenticador.BLL;
    using Autenticador.Entities;
    using Autenticador.Web.WebProject;

    public partial class UCComboTemaPaleta : MotherUserControl
    {
        #region Delegates

        public delegate void SelectedIndexChanged();
        public event SelectedIndexChanged IndexChanged;

        #endregion Delegates

        #region Propriedades

        /// <summary>
        /// Retorna valor selecionado no combo
        /// Valor[0] = tep_id
        /// Valor[1] = tpl_id
        /// </summary>
        public int[] Valor
        {
            get
            {
                string[] valor = ddlTemaPaleta.SelectedValue.Split(';');

                return valor.Length > 1 ? new int[] { Convert.ToInt32(valor[0]), Convert.ToInt32(valor[1]) } : new int[] { -1, -1 };
            }

            set
            {
                ddlTemaPaleta.SelectedValue = String.Format("{0};{1}", value[0], value[1]);
            }
        }

        /// <summary>
        /// ID do tema padrão selecionado.
        /// </summary>
        public int tep_id
        {
            get
            {
                return Valor[0];
            }
        }

        /// <summary>
        /// ID do tema de cores selecionado.
        /// </summary>
        public int tpl_id
        {
            get
            {
                return Valor[1];
            }
        }

        /// <summary>
        /// Retorna texto selecionado no combo
        /// </summary>
        public string Texto
        {
            get
            {
                return ddlTemaPaleta.SelectedItem.Text;
            }
        }

        /// <summary>
        /// Retorna e seta o nome do combo
        /// </summary>
        public string NomeCombo
        {
            get
            {
                return lblTemaPaleta.Text;
            }

            set
            {
                lblTemaPaleta.Text = value;
                cpvTemaPaleta.ErrorMessage = value.Replace('*', ' ') + " é obrigatório.";
            }
        }

        /// <summary>
        /// Adciona e remove a mensagem "Selecione um tema de cores" do dropdownlist.
        /// Por padrão é false e a mensagem "Selecione um tema de cores" não é exibida.
        /// </summary>
        public bool MostrarMensagemSelecione
        {
            get
            {
                return ddlTemaPaleta.Items.Cast<ListItem>().Any(p => p.Value.Equals("-1;-1"));
            }

            set
            {
                ddlTemaPaleta.AppendDataBoundItems = value;

                if (value && !ddlTemaPaleta.Items.Cast<ListItem>().Any(p => p.Value.Equals("-1;-1")))
                {
                    ddlTemaPaleta.Items.Insert(0, new ListItem("-- Selecione um tema de cores --", "-1;-1"));
                }

                if (!value && ddlTemaPaleta.Items.Cast<ListItem>().Any(p => p.Value.Equals("-1;-1")))
                {
                    ddlTemaPaleta.Items.Cast<ListItem>().ToList().RemoveAll(p => p.Value.Equals("-1;-1"));
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

                cpvTemaPaleta.Visible = value;
            }
        }

        /// <summary>
        /// Seta o validationGroup do combo.
        /// </summary>
        public string ValidationGroup
        {
            set
            {
                cpvTemaPaleta.ValidationGroup = value;
            }
        }

        /// <summary>
        /// ClientID do combo
        /// </summary>
        public string Combo_ClientID
        {
            get
            {
                return ddlTemaPaleta.ClientID;
            }
        }

        /// <summary>
        /// Propriedade que verifica quantos items existem no combo
        /// </summary>
        public int QuantidadeItensCombo
        {
            get
            {
                return ddlTemaPaleta.Items.Count - (MostrarMensagemSelecione ? 1 : 0);
            }
        }

        /// <summary>
        /// Deixa o combo habilitado de acordo com o valor passado
        /// </summary>
        public bool PermiteEditar
        {
            get
            {
                return ddlTemaPaleta.Enabled;
            }
            set
            {
                ddlTemaPaleta.Enabled = value;
            }
        }

        #endregion Propriedades

        #region Eventos

        protected void ddlTemaPaleta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IndexChanged != null)
                IndexChanged();
        }

        #endregion Eventos

        #region Métodos

        /// <summary>
        /// Carrega o combo de temas com todos os temas de cores ativos no sistema.
        /// </summary>
        public void Carregar()
        {
            bool mostraMensagemSelecione = MostrarMensagemSelecione;

            var temas = from CFG_TemaPaleta tema in CFG_TemaPaletaBO.SelecionaAtivos()
                        select new
                        {
                            id = String.Format("{0};{1}", tema.tep_id, tema.tpl_id)
                            ,
                            tpl_nome = String.Format("{0} - {1}", tema.tep_nome, tema.tpl_nome)
                        };

            ddlTemaPaleta.Items.Clear();
            ddlTemaPaleta.DataSource = temas;
            MostrarMensagemSelecione = mostraMensagemSelecione;
            ddlTemaPaleta.DataBind();

            SelecionaPrimeiroItem();
        }


        /// <summary>
        /// Carrega o combo de temas com todos os temas de cores por tema padrão.
        /// </summary>
        public void CarregarPorTemaPadrao(int tep_id)
        {
            bool mostraMensagemSelecione = MostrarMensagemSelecione;

            var temas = from CFG_TemaPaleta tema in CFG_TemaPaletaBO.SelecionaPorTemaPadrao(tep_id)
                        select new
                        {
                            id = String.Format("{0};{1}", tema.tep_id, tema.tpl_id)
                            ,
                            tpl_nome = String.Format("{0} - {1}", tema.tep_nome, tema.tpl_nome)
                        };

            ddlTemaPaleta.Items.Clear();
            ddlTemaPaleta.DataSource = temas;
            MostrarMensagemSelecione = mostraMensagemSelecione;
            ddlTemaPaleta.DataBind();

            SelecionaPrimeiroItem();
        }

        private void SelecionaPrimeiroItem()
        {
            if (QuantidadeItensCombo == 1)
            {
                ddlTemaPaleta.SelectedIndex = ddlTemaPaleta.Items.Count - 1;
            }
        }

        #endregion Métodos

        #region Page Life Cycle

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ddlTemaPaleta.AutoPostBack = IndexChanged != null;
        }

        #endregion Page Life Cycle
    }
}