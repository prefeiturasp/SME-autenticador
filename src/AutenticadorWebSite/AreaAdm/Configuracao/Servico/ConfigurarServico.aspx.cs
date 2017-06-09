namespace AutenticadorWebSite.AreaAdm.Configuracao.Servico
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Autenticador.BLL;
    using Autenticador.QuartzProvider.Domain;
    using Autenticador.Web.WebProject;
    using Quartz;

    public partial class ConfigurarServico : MotherPageLogado
    {
        #region Constantes

        /// <summary>
        /// Constante para armazenar o nome Default do Grupo que os jobs fazem parte.
        /// </summary>
        private const string GroupName = "DEFAULT";

        #endregion

        #region Page Life Cycle

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager sm = ScriptManager.GetCurrent(this);
            if (sm != null)
            {
                sm.Scripts.Add(new ScriptReference(ArquivoJS.JQueryValidation));
                sm.Scripts.Add(new ScriptReference(ArquivoJS.JqueryMask));
                sm.Scripts.Add(new ScriptReference(ArquivoJS.MascarasCampos));
                sm.Scripts.Add(new ScriptReference(String.Format("{0}Telas/jsConfigurarServico.js", ArquivoJS.PastaScriptRaiz))); 
            }

            if (!IsPostBack)
            {
                string message = __SessionWEB.PostMessages;
                if (!string.IsNullOrEmpty(message))
                {
                    lblMessage.Text = message;
                }

                try
                {
                    UCComboServico.Carregar();
                }
                catch (Exception ex)
                {
                    ApplicationWEB._GravaErro(ex);
                    pnlConfigServico.Visible = false;
                    lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o sistema.", UtilBO.TipoMensagem.Erro);
                }

                UCFrequenciaServico.ObrigatorioFrequencia = true;
                UCFrequenciaServico.ObrigatorioHorario = true;
            }

            UCComboServico.IndexChanged += UCComboServico_IndexChanged;
        }

        #endregion

        #region Delegate

        private void UCComboServico_IndexChanged()
        {
            divServico.Visible = UCComboServico.Valor > 0;

            try
            {
                if (divServico.Visible)
                {
                    LimparCampos();
                    string expressao;
                    string nomeProcedimento = SYS_ServicoBO.SelectNomeProcedimento(UCComboServico.Valor);
                    string trigger = String.Format("Trigger_{0}", nomeProcedimento);

                    if (SYS_ServicoBO.SelecionaExpressaoPorTrigger(trigger, out expressao))
                    {
                        ConfigurarFrequencia(expressao);
                        IList<TriggerData> ltTriggerData = ApplicationWEB.SchedulerDataProvider.GetTriggersOfJob(GroupName, nomeProcedimento);
                        if (ltTriggerData.Any())
                        {
                            TriggerData triggerData = ltTriggerData.First();

                            lblUltimaExecucao.Text = triggerData.PreviousFireDate != null ?
                                String.Format("<b>Última execução:</b> {0}", ((DateTimeOffset)ltTriggerData.First().PreviousFireDate).LocalDateTime.ToString("dd/MM/yyyy HH:mm:ss")) :
                                string.Empty;

                            lblProximaExecucao.Text = triggerData.NextFireDate != null ?
                                String.Format("<b>Próxima execução:</b> {0}", ((DateTimeOffset)ltTriggerData.First().NextFireDate).LocalDateTime.ToString("dd/MM/yyyy HH:mm:ss")) :
                                string.Empty;
                        }

                        chkDesativar.Visible = true;
                    }
                    else
                    {
                        chkDesativar.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o sistema.", UtilBO.TipoMensagem.Erro);
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Limpar os campos dos dados do serviço.
        /// </summary>
        private void LimparCampos()
        {
            UCFrequenciaServico.TipoFrequencia = 0;
            UCFrequenciaServico.DiaMesSelectedValue = "1";
            UCFrequenciaServico.LimpaCheckboxList();
            UCFrequenciaServico.LimpaRadioButtonList();
            UCFrequenciaServico.Horario = string.Empty;
            UCFrequenciaServico.LimpaRepeater();
            UCFrequenciaServico.AtualizaDivs();
            chkDesativar.Checked = false;
            lblProximaExecucao.Text =
            lblUltimaExecucao.Text = string.Empty;
            divCampos.Style["display"] = "";
        }

        /// <summary>
        /// Retorna a configuração do serviço de acordo com a expressão salva.
        /// </summary>
        /// <param name="expressaoInteira">Expressão das configurações.</param>
        private void ConfigurarFrequencia(string expressaoInteira)
        {
            string[] expressao = expressaoInteira.Split(' ');

            if (expressao[2].Contains(','))
            {
                UCFrequenciaServico.VariosHorarios = new[] { expressao[1], expressao[2] };
                UCFrequenciaServico.TipoFrequencia = (byte)SYS_ServicoBO.eFrequencia.VariasVezesDia;
            }
            else if (expressao[2].Equals("*"))
            {
                if (expressao[3].Equals("*"))
                {
                    // Configuração da expressão de hora em hora.
                    UCFrequenciaServico.TipoFrequencia = (byte)SYS_ServicoBO.eFrequencia.HoraEmHora;
                    UCFrequenciaServico.Minuto = expressao[1].Split('/')[1];
                }
                else if (expressao[3].Equals("1/1"))
                {
                    // Configuração de intervalo de minutos.
                    UCFrequenciaServico.TipoFrequencia = (byte)SYS_ServicoBO.eFrequencia.IntervaloMinutos;
                    UCFrequenciaServico.MinutoIntervalo = expressao[1].Split('/')[1];
                }
            }
            else
            {
                UCFrequenciaServico.Horario = string.Format("{0}:{1}", expressao[2], expressao[1]);

                switch (expressao[3])
                {
                    case "*":
                        UCFrequenciaServico.TipoFrequencia = (byte)SYS_ServicoBO.eFrequencia.Diario;
                        break;

                    case "?":
                        switch (expressao[5])
                        {
                            case "MON-FRI":
                                UCFrequenciaServico.TipoFrequencia = (byte)SYS_ServicoBO.eFrequencia.SegundaSexta;
                                break;

                            case "SUN,SAT":
                                UCFrequenciaServico.TipoFrequencia = (byte)SYS_ServicoBO.eFrequencia.SabadoDomingo;
                                break;

                            default:
                                string[] listaDias = expressao[5].Split(',');

                                if (listaDias.Count() > 1)
                                {
                                    UCFrequenciaServico.TipoFrequencia = (byte)SYS_ServicoBO.eFrequencia.Personalizado;

                                    foreach (string dia in listaDias)
                                    {
                                        UCFrequenciaServico.DiaSemanaVarios = RetornarTipoDiaSemana(dia);
                                    }
                                }
                                else
                                {
                                    UCFrequenciaServico.TipoFrequencia = (byte)SYS_ServicoBO.eFrequencia.Semanal;
                                    UCFrequenciaServico.DiaSemanaUnico = RetornarTipoDiaSemana(expressao[5]);
                                }

                                break;
                        }

                        break;

                    default:
                        UCFrequenciaServico.TipoFrequencia = (byte)SYS_ServicoBO.eFrequencia.Mensal;
                        UCFrequenciaServico.DiaMesSelectedValue = expressao[3];
                        break;
                }
            }

            UCFrequenciaServico.AtualizaDivs();
        }

        /// <summary>
        /// Retorna o tipo do dia da semana de acordo com a sigla.
        /// </summary>
        /// <param name="dia">Sigla do dia da semana.</param>
        /// <returns>Tipo do dia da semana.</returns>
        private string RetornarTipoDiaSemana(string dia)
        {
            byte tipo = 0;
            switch (dia)
            {
                case "SUN":
                    tipo = (byte)SYS_ServicoBO.eDiasSemana.Domingo;
                    break;
                case "MON":
                    tipo = (byte)SYS_ServicoBO.eDiasSemana.Segunda;
                    break;
                case "TUE":
                    tipo = (byte)SYS_ServicoBO.eDiasSemana.Terca;
                    break;
                case "WED":
                    tipo = (byte)SYS_ServicoBO.eDiasSemana.Quarta;
                    break;
                case "THU":
                    tipo = (byte)SYS_ServicoBO.eDiasSemana.Quinta;
                    break;
                case "FRI":
                    tipo = (byte)SYS_ServicoBO.eDiasSemana.Sexta;
                    break;
                case "SAT":
                    tipo = (byte)SYS_ServicoBO.eDiasSemana.Sabado;
                    break;
            }

            return tipo.ToString();
        }

        /// <summary>
        /// Método para salvar as configurações do serviço.
        /// </summary>
        private void Salvar()
        {
            try
            {
                SalvarTriggerQuartz(UCComboServico.Valor, !chkDesativar.Checked);

                ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Insert, "Serviço: " + UCComboServico.Texto);

                __SessionWEB.PostMessages = UtilBO.GetErroMessage("Configuração do serviço salva com sucesso.", UtilBO.TipoMensagem.Sucesso);

                Response.Redirect("ConfigurarServico.aspx", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar a configuração do serviço.", UtilBO.TipoMensagem.Erro);
            }
        }

        /// <summary>
        /// Salva as configurações do serviço na tabela do quartz (serviço do windows que executa os serviços de integração).
        /// </summary>
        /// <param name="ser_id">ID do serviço.</param>
        /// <param name="ser_ativo">Situação do serviço.</param>
        private void SalvarTriggerQuartz(Int16 ser_id, bool ser_ativo)
        {
            const string groupName = "DEFAULT";
            string jobName = SYS_ServicoBO.SelectNomeProcedimento(ser_id);
            string cronExpression = UCFrequenciaServico.GeraCronExpression();
            string triggerName = "Trigger_" + jobName;

            var jobKey = new JobKey(jobName, groupName);

            // Verifica se a trigger já está criada 
            if (ApplicationWEB.SchedulerProvider.Scheduler.CheckExists(jobKey))
            {
                // Caso a trigger já exista, deleta para cria-lá com as configurações novas
                ApplicationWEB.SchedulerDataProvider.DeleteTrigger(new TriggerKey(triggerName, groupName));
            }

            // Verifica se o serviço está habilitado
            if (ser_ativo)
            {
                // Cria a trigger com as configurações novas
                ApplicationWEB.SchedulerDataProvider.ScheduleCronTriggerForJob(jobKey, triggerName, cronExpression);
            }
        }

        #endregion

        #region Eventos

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
                Salvar();
        }

        #endregion
    }
}