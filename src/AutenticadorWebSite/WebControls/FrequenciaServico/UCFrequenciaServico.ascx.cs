﻿namespace AutenticadorWebSite.WebControls.FrequenciaServico
{
    using System;
    using System.Linq;
    using System.Data;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Autenticador.BLL;
    using Autenticador.Web.WebProject;

    public partial class UCFrequenciaServico : MotherUserControl
    {
        #region Propriedades

        /// <summary>
        /// Retorna ou seleciona o dia da semana.
        /// </summary>
        public string DiaSemanaVarios
        {
            set
            {
                cblDiasSemana.Items.FindByValue(value).Selected = true;
            }
        }

        /// <summary>
        /// Retorna ou seleciona o dia da semana.
        /// </summary>
        public string DiaSemanaUnico
        {
            get
            {
                return rblDiasSemana.SelectedValue;
            }
            set
            {
                rblDiasSemana.SelectedValue = value;
            }
        }

        /// <summary>
        /// Propriedade em ViewState para setar o ValidationGroup dos validators de cada item
        /// do repeater.
        /// </summary>
        protected string VS_ValidationGroup
        {
            get
            {
                if (ViewState["VS_ValidationGroup"] == null)
                {
                    return "Servico";
                }

                return ViewState["VS_ValidationGroup"].ToString();
            }

            set
            {
                ViewState["VS_ValidationGroup"] = value;
            }
        }

        /// <summary>
        /// Propriedade ValidationGroup de todos os validators
        /// </summary>
        public string ValidationGroupUCFrequenciaServico
        {
            set
            {
                VS_ValidationGroup = value;
                rfvHora.ValidationGroup = value;
                revHora.ValidationGroup = value;
                cpvFrequencia.ValidationGroup = value;
                cvCheckBoxDiasSemana.ValidationGroup = value;
            }
            get
            {
                return VS_ValidationGroup;
            }
        }

        /// <summary>
        /// Propriedade que retorna ou seta o horario colocado no campo.
        /// </summary>
        public string Horario
        {
            get
            {
                return txtHora.Text;
            }
            set
            {
                txtHora.Text = value;
            }
        }

        /// <summary>
        /// Propriedade que retorna ou seta o horarios colocados no campos.
        /// </summary>
        public string[] VariosHorarios
        {
            set
            {
                string hora = value[1];
                string minuto = value[0];
                string[] horas = hora.Split(',');

                DataTable dt = CriaDataTable(false);

                foreach (string h in horas)
                {
                    DataRow dr = dt.NewRow();
                    dr["id"] = Guid.NewGuid();
                    dr["hora"] = h;
                    dr["minuto"] = minuto;

                    dt.Rows.Add(dr);
                }

                CarregarHorarios(dt);

            }
        }

        /// <summary>
        /// Returna o TipoFrequencia selecionado no combo, ou seleciona o tipo frequência passado.
        /// </summary>
        public byte TipoFrequencia
        {
            get
            {
                return Convert.ToByte((ddlFrequencia.SelectedValue == "-1") ? "0" : ddlFrequencia.SelectedValue);
            }
            set
            {
                ddlFrequencia.SelectedValue = (value == 0) ? "-1" : value.ToString();
            }
        }

        /// <summary>
        /// Retorna o dia do mês escolhido caso o TipoFrequencia seja mensal
        /// </summary>
        public Int16 DiaMes
        {
            get
            {
                if (ddlFrequencia.SelectedValue == Convert.ToInt32(SYS_ServicoBO.eFrequencia.Mensal).ToString())
                    return Convert.ToInt16(ddlDiaMes.SelectedValue);
                return Convert.ToInt16(-1);
            }
        }

        /// <summary>
        /// Seta o texto do campo dia do mês
        /// </summary>
        public string DiaMesSelectedValue
        {
            set
            {
                ddlDiaMes.SelectedValue = value;
            }
        }

        /// <summary>
        /// Propriedade em ViewState para setar a obrigatoriedade dos validators de cada item
        /// do repeater.
        /// </summary>
        protected bool VS_ObrigatorioHorario
        {
            get
            {
                if (ViewState["VS_ObrigatorioHorario"] == null)
                {
                    return true;
                }

                return (bool)ViewState["VS_ObrigatorioHorario"];
            }

            set
            {
                ViewState["VS_ObrigatorioHorario"] = value;
            }
        }

        /// <summary>
        /// Seta o campo frequencia como obrigatório
        /// </summary>
        public bool ObrigatorioFrequencia
        {
            set
            {
                cpvFrequencia.Visible = value;
                if (cpvFrequencia.Visible)
                    lblFrequencia.Text += " *";
            }
        }

        /// <summary>
        /// Seta o campo horario como obrigatório
        /// </summary>
        public bool ObrigatorioHorario
        {
            set
            {
                VS_ObrigatorioHorario = value;
                rfvHora.Visible = value;
                if (rfvHora.Visible)
                    lblHorario.Text += " *";
            }

            get
            {
                return VS_ObrigatorioHorario;
            }
        }

        /// <summary>
        /// Seta o enable dos campos
        /// </summary>
        public bool PermiteEditar
        {
            set
            {
                ddlFrequencia.Enabled = value;
                cblDiasSemana.Enabled = value;
                rblDiasSemana.Enabled = value;
                ddlDiaMes.Enabled = value;
                txtHora.Enabled = value;
            }
        }

        /// <summary>
        /// Índice da linha de horários.
        /// </summary>
        private int indiceHorario;

        /// <summary>
        /// Propriedade em ViewState que armazena o valor dos minutos.
        /// </summary>
        protected int VS_Minuto
        {
            get
            {
                if (ViewState["VS_Minuto"] == null)
                {
                    return -1;
                }

                return Convert.ToInt32(ViewState["VS_Minuto"]);
            }

            set
            {
                ViewState["VS_Minuto"] = value;
            }
        }

        /// <summary>
        /// Propriedade que retorna ou seta o minuto colocado no campo.
        /// </summary>
        public string Minuto
        {
            get
            {
                return txtMinutoHora.Text;
            }
            set
            {
                txtMinutoHora.Text = value;
            }
        }

        /// <summary>
        /// Propriedade que retorna ou seta o minuto do intervalo de minutos colocado no campo.
        /// </summary>
        public string MinutoIntervalo
        {
            get
            {
                return txtMinutoIntervalo.Text;
            }
            set
            {
                txtMinutoIntervalo.Text = value;
            }
        }

        #endregion

        #region Page Life Cycle

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #endregion

        #region Eventos

        protected void ddlFrequencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizaDivs();
        }

        protected void cvCheckBoxDiasSemana_ServerValidate(object source, ServerValidateEventArgs args)
        {
            int contador = (from ListItem li in cblDiasSemana.Items
                            where li.Selected
                            select li).Count();

            if (contador > 0)
                args.IsValid = true;
            else
                args.IsValid = false;
        }

        protected void btnNovaHora_Click(object sender, EventArgs e)
        {
            // Adiciona nova linha.
            NovoHorario();

            if (rptHoras.Visible == false)
                rptHoras.Visible = true;

            rptHoras.Items[rptHoras.Items.Count - 1].FindControl("ddlHora").Focus();
        }

        protected void rptHoras_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.Item))
            {
                indiceHorario++;

                DropDownList ddlMinuto = (DropDownList)e.Item.FindControl("ddlMinuto");
                DropDownList ddlHora = (DropDownList)e.Item.FindControl("ddlHora");
                Label lblMinuto = (Label)e.Item.FindControl("lblMinutoDado");
                Label lblHora = (Label)e.Item.FindControl("lblHoraDado");
                CarregaComboHora(ddlHora);
                CarregaComboMinuto(ddlMinuto);

                if (indiceHorario == 1)
                {
                    //Button btnExcluirHora = (Button)e.Item.FindControl("btnExcluirHora");
                    //btnExcluirHora.Visible = false;

                    ddlMinuto.SelectedValue = String.IsNullOrEmpty(lblMinuto.Text) ? "-1" : lblMinuto.Text;
                    VS_Minuto = Convert.ToInt32(String.IsNullOrEmpty(lblMinuto.Text) ? "-1" : lblMinuto.Text);
                }
                else
                {
                    ddlMinuto.SelectedValue = VS_Minuto.ToString().PadLeft(2, '0');
                }

                ddlHora.SelectedValue = String.IsNullOrEmpty(lblHora.Text) ? "-1" : lblHora.Text;
                ddlMinuto.Enabled = indiceHorario == 1;
            }
        }

        protected void btnExcluirHora_Click(object sender, EventArgs e)
        {
            Button btnExcluirHora = (Button)sender;
            RepeaterItem item = (RepeaterItem)btnExcluirHora.NamingContainer;

            ExcluirHora(item.ItemIndex);

            rptHoras.Visible = rptHoras.Items.Count > 0;
        }

        protected void ddlMinuto_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlMinuto = (DropDownList)sender;

            VS_Minuto = Convert.ToInt32(ddlMinuto.SelectedValue);

            AtualizaHorarios();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Atualiza tela conforme tipo de frequencia do serviço selecionado.
        /// </summary>
        public void AtualizaDivs()
        {
            divRadioDiasSemana.Visible = ddlFrequencia.SelectedValue == Convert.ToByte(SYS_ServicoBO.eFrequencia.Semanal).ToString();
            divDiaMes.Visible = ddlFrequencia.SelectedValue == Convert.ToByte(SYS_ServicoBO.eFrequencia.Mensal).ToString();
            divCheckBoxDiasSemana.Visible = ddlFrequencia.SelectedValue == Convert.ToByte(SYS_ServicoBO.eFrequencia.Personalizado).ToString();
            divHora.Visible = (ddlFrequencia.SelectedValue != Convert.ToByte(SYS_ServicoBO.eFrequencia.VariasVezesDia).ToString())
                && (ddlFrequencia.SelectedValue != Convert.ToInt32(SYS_ServicoBO.eFrequencia.HoraEmHora).ToString())
                && (ddlFrequencia.SelectedValue != Convert.ToInt32(SYS_ServicoBO.eFrequencia.IntervaloMinutos).ToString());
            divVariasHoras.Visible = ddlFrequencia.SelectedValue == Convert.ToByte(SYS_ServicoBO.eFrequencia.VariasVezesDia).ToString();
            divHoraEmHora.Visible = ddlFrequencia.SelectedValue == Convert.ToByte(SYS_ServicoBO.eFrequencia.HoraEmHora).ToString();
            divIntervaloMinutos.Visible = ddlFrequencia.SelectedValue == Convert.ToByte(SYS_ServicoBO.eFrequencia.IntervaloMinutos).ToString();
        }

        /// <summary>
        /// Limpa os itens selecionados na checkboxlist
        /// </summary>
        public void LimpaCheckboxList()
        {
            foreach (ListItem li in cblDiasSemana.Items)
                li.Selected = false;

        }

        /// <summary>
        /// Limpa os itens selecionados na radiobuttonlist
        /// </summary>
        public void LimpaRadioButtonList()
        {
            rblDiasSemana.SelectedValue = "2";
        }

        /// <summary>
        /// Retorna o valor selecionado no DropDownList de frequência
        /// </summary>
        public string GeraCronExpression()
        {
            string horario = txtHora.Text;
            string[] hora = horario.Split(':');
            string expression = !String.IsNullOrEmpty(txtHora.Text) ? "0 " + hora[1] + " " + hora[0] + " " : "0 ";
            string dias = "";

            switch ((SYS_ServicoBO.eFrequencia)Convert.ToByte(ddlFrequencia.SelectedValue))
            {
                case SYS_ServicoBO.eFrequencia.Diario:
                    {
                        dias = "* * ?";
                        break;
                    }
                case SYS_ServicoBO.eFrequencia.Semanal:
                    {
                        dias = "? * ";
                        switch (rblDiasSemana.SelectedValue)
                        {
                            case ("1"):
                                {
                                    dias += "SUN";
                                    break;
                                }
                            case ("2"):
                                {
                                    dias += "MON";
                                    break;
                                }
                            case ("3"):
                                {
                                    dias += "TUE";
                                    break;
                                }
                            case ("4"):
                                {
                                    dias += "WED";
                                    break;
                                }
                            case ("5"):
                                {
                                    dias += "THU";
                                    break;
                                }
                            case ("6"):
                                {
                                    dias += "FRI";
                                    break;
                                }
                            case ("7"):
                                {
                                    dias += "SAT";
                                    break;
                                }
                        }
                        break;
                    }
                case SYS_ServicoBO.eFrequencia.Mensal:
                    {
                        dias = ddlDiaMes.SelectedValue + " * ?";
                        break;
                    }
                case SYS_ServicoBO.eFrequencia.SegundaSexta:
                    {
                        dias = "? * MON-FRI";
                        break;
                    }
                case SYS_ServicoBO.eFrequencia.SabadoDomingo:
                    {
                        dias = "? * SUN,SAT";
                        break;
                    }
                case SYS_ServicoBO.eFrequencia.VariasVezesDia:
                    {
                        dias = "* * ?";
                        expression = "0 ";
                        foreach (RepeaterItem item in rptHoras.Items)
                        {
                            string horas = ((DropDownList)item.FindControl("ddlHora")).SelectedValue.PadLeft(2, '0');
                            string minutos = ((DropDownList)item.FindControl("ddlMinuto")).SelectedValue.PadLeft(2, '0');

                            expression += item.ItemIndex == 0 ? minutos + " " + horas + "," : horas + ",";
                        }
                        expression = expression.Substring(0, expression.Length - 1) + " ";
                        break;
                    }
                case SYS_ServicoBO.eFrequencia.HoraEmHora:
                    {
                        dias = "* * * ?";
                        expression = String.Format("0 0/{0} ", txtMinutoHora.Text.PadLeft(2, '0'));
                        break;
                    }
                case SYS_ServicoBO.eFrequencia.Personalizado:
                    {
                        dias = "? * ";
                        foreach (ListItem li in cblDiasSemana.Items)
                            if (li.Selected)
                            {
                                switch (li.Value)
                                {
                                    case ("1"):
                                        {
                                            dias += "SUN";
                                            break;
                                        }
                                    case ("2"):
                                        {
                                            dias += "MON";
                                            break;
                                        }
                                    case ("3"):
                                        {
                                            dias += "TUE";
                                            break;
                                        }
                                    case ("4"):
                                        {
                                            dias += "WED";
                                            break;
                                        }
                                    case ("5"):
                                        {
                                            dias += "THU";
                                            break;
                                        }
                                    case ("6"):
                                        {
                                            dias += "FRI";
                                            break;
                                        }
                                    case ("7"):
                                        {
                                            dias += "SAT";
                                            break;
                                        }
                                }
                                dias += ",";
                            }

                        //retira a vírgula do final
                        dias = dias.Substring(0, dias.Length - 1);
                        break;
                    }

                case SYS_ServicoBO.eFrequencia.IntervaloMinutos:
                    {
                        expression = string.Format("0 0/{0} * 1/1 * ? *", txtMinutoIntervalo.Text);

                        break;
                    }
            }

            expression += dias;

            return expression;
        }

        /// <summary>
        /// Insere um novo horário de execução do serviço no repeater de horários.
        /// </summary>
        private void NovoHorario()
        {
            DataTable dt = RetornaHorarios();

            dt = AdicionaLinha(dt);

            CarregarHorarios(dt);

        }

        /// <summary>
        /// Carrega os horários de execução do serviço no repeater de horários.
        /// </summary>
        /// <param name="dtHorario">DataTable de horários.</param>
        private void CarregarHorarios(DataTable dtHorario)
        {
            indiceHorario = 0;
            rptHoras.DataSource = dtHorario;
            rptHoras.DataBind();
        }

        /// <summary>
        /// Carrega o combo de horas (de hora em hora).
        /// </summary>
        /// <param name="ddlHora">Combo de horas.</param>
        private void CarregaComboHora(DropDownList ddlHora)
        {
            ddlHora.Items.Clear();
            ddlHora.Items.Insert(0, new ListItem("--", "-1", true));
            for (int i = 0; i <= 23; i++)
            {
                string hora = i < 10 ? string.Concat("0", i.ToString()) : i.ToString();
                ddlHora.Items.Insert(i + 1, new ListItem(hora, hora, true));
            }
        }

        /// <summary>
        /// Carrega o combo de minutos (5 em 5 minutos).
        /// </summary>
        /// <param name="ddlMinuto">Combo de minutos.</param>
        private void CarregaComboMinuto(DropDownList ddlMinuto)
        {
            ddlMinuto.Items.Clear();
            ddlMinuto.Items.Insert(0, new ListItem("--", "-1", true));

            int x = 0;
            int index = 1;
            while (x <= 55)
            {
                string minuto = x < 10 ? string.Concat("0", x.ToString()) : x.ToString();
                ddlMinuto.Items.Insert(index, new ListItem(minuto, minuto, true));
                index++;
                x = x + 5;
            }
        }

        /// <summary>
        /// Adiciona uma nora linha em branco no DataTable de horários passado como parâmetro.
        /// </summary>
        /// <param name="dtHorario">DataTable de horários anterior.</param>
        /// <returns></returns>
        private DataTable AdicionaLinha(DataTable dtHorario)
        {
            DataRow dr = dtHorario.NewRow();
            dr["id"] = Guid.NewGuid();

            dtHorario.Rows.Add(dr);

            return dtHorario;
        }

        /// <summary>
        /// Retorna os horários de execução setados para o serviço.
        /// </summary>
        /// <returns></returns>
        private DataTable RetornaHorarios()
        {
            DataTable dt = CriaDataTable(false);

            foreach (RepeaterItem item in rptHoras.Items)
            {
                string hora = ((DropDownList)item.FindControl("ddlHora")).SelectedValue;
                string minuto = ((DropDownList)item.FindControl("ddlMinuto")).SelectedValue;
                Label lblHoraDado = (Label)item.FindControl("lblHoraDado");
                Label lblMinutoDado = (Label)item.FindControl("lblMinutoDado");
                if ((!hora.Equals("-1")) && (!minuto.Equals("-1")))
                {
                    DataRow dr = dt.NewRow();
                    dr["id"] = ((Label)item.FindControl("lblID")).Text;
                    lblHoraDado.Text = hora;
                    dr["hora"] = lblHoraDado.Text;
                    lblMinutoDado.Text = minuto;
                    dr["minuto"] = lblMinutoDado.Text;

                    dt.Rows.Add(dr);
                }
            }

            return dt;
        }

        /// <summary>
        /// Cria o DataTable com os dados de horários.
        /// </summary>
        /// <param name="adicionaLinhaVazia">True - adicionala linha || false - apenas cria o datatable</param>
        /// <returns></returns>
        private DataTable CriaDataTable(bool adicionaLinhaVazia)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("hora");
            dt.Columns.Add("minuto");
            dt.Columns.Add("id");

            if (adicionaLinhaVazia)
            {
                dt = AdicionaLinha(dt);
            }

            return dt;
        }

        /// <summary>
        /// Exclui um horário do repeater.
        /// </summary>
        /// <param name="indice">Índice do horário no repeater.</param>
        private void ExcluirHora(int indice)
        {
            DataTable dt = RetornaHorarios();

            if (dt.Rows.Count > indice)
            {
                dt.Rows[indice].Delete();
            }

            if (dt.Rows.Count == 0)
                dt = AdicionaLinha(dt);
            CarregarHorarios(dt);
        }

        /// <summary>
        /// Atualiza os minutos dos horários (quando o primeiro horário é alterado).
        /// </summary>
        private void AtualizaHorarios()
        {
            foreach (RepeaterItem item in rptHoras.Items)
            {
                DropDownList ddlMinuto = (DropDownList)item.FindControl("ddlMinuto");
                Label lblMinutoDado = (Label)item.FindControl("lblMinutoDado");

                string minuto = VS_Minuto.ToString().PadLeft(2, '0');

                lblMinutoDado.Text = minuto;
                ddlMinuto.SelectedValue = minuto;
            }
        }

        /// <summary>
        /// Remove todos os horários.
        /// </summary>
        public void LimpaRepeater()
        {
            DataTable dt = CriaDataTable(true);
            CarregarHorarios(dt);
        }

        #endregion
    }
}