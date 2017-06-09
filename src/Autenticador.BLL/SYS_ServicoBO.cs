/*
	Classe gerada automaticamente pelo Code Creator
*/

namespace Autenticador.BLL
{
	using CoreLibrary.Business.Common;
	using Autenticador.Entities;
	using Autenticador.DAL;
    using System.Collections.Generic;
    using System.Data;
    using System;
    using System.Diagnostics;
    using CoreLibrary.Validation.Exceptions;
    using System.Management;
	
	/// <summary>
	/// Description: SYS_Servico Business Object. 
	/// </summary>
	public class SYS_ServicoBO : BusinessBase<SYS_ServicoDAO, SYS_Servico>
    {
        #region Enumeradores

        /// <summary>
        /// Enumeradores com ID dos servi�os
        /// </summary>
        public enum eChaveServico
        {
            IntegracaoActiveDirectory = 1
            ,
            ExecucaoScriptsFIM = 2
            ,
            ExpiraSenha = 3
        }

        /// <summary>
        /// Enumerador das frequencias utilizadas para o servi�o
        /// </summary>
        public enum eFrequencia : byte
        {
            Diario = 1
            ,
            Semanal = 2
            ,
            Mensal = 3
            ,
            SegundaSexta = 4
            ,
            SabadoDomingo = 5
            ,
            VariasVezesDia = 6
            ,
            HoraEmHora = 7
            ,
            Personalizado = 8
            ,
            IntervaloMinutos = 9
        }

        /// <summary>
        /// Enumerador dos dias da semana
        /// </summary>
        public enum eDiasSemana : byte
        {
            Domingo = 1,
            Segunda = 2,
            Terca = 3,
            Quarta = 4,
            Quinta = 5,
            Sexta = 6,
            Sabado = 7
        }

        #endregion

        #region M�todos de consulta

        /// <summary>
        /// Seleciona os servi�os cadastrados.
        /// </summary>
        /// <returns></returns>
        public static List<SYS_Servico> SelecionaServicos()
        {
            return new SYS_ServicoDAO().SelecionaServicos();
        }

        /// <summary>
        /// Seleciona nome do job pelo nome do servi�o
        /// </summary>
        /// <param name="ser_id">ID do servi�o</param>
        /// <returns></returns>
        public static string SelectNomeProcedimento(Int16 ser_id)
        {
            return new SYS_ServicoDAO().SelectNomeProcedimento(ser_id);
        }

        /// <summary>
        /// Retorna a express�o de configura��o de acordo com o nome do trigger.
        /// </summary>
        /// <param name="trigger">Nome do trigger.</param>
        /// <param name="expressao">Express�o de configura��o.</param>
        /// <returns>Verdadeiro se encontrou uma express�o para o trigger.</returns>
        public static bool SelecionaExpressaoPorTrigger(string trigger, out string expressao)
        {
            expressao = new CoreServicoDAO().SelecionaExpressaoPorTrigger(trigger);
            return !string.IsNullOrEmpty(expressao);
        }

        #endregion

        /// <summary>
        /// Executa o JOB de Integra��o de usu�rios com Active Directory.
        /// </summary>
        public static void ExecJobIntegracaoActiveDirectory()
        {
            SYS_Servico servico = GetEntity(new SYS_Servico() { ser_id = (short)eChaveServico.IntegracaoActiveDirectory });
            if (servico.ser_ativo)
            {
                List<LOG_UsuarioADBO.sLOG_UsuarioAD> ltLogUsuarioAD = LOG_UsuarioADBO.SelecionaNaoProcessados();

                if (ltLogUsuarioAD.Count > 0)
                {
                    ltLogUsuarioAD.ForEach(p =>
                        {
                            LOG_UsuarioAD entityUsuarioAD = p.usuarioAD;
                            entityUsuarioAD.usa_status = (short)LOG_UsuarioAD.eStatus.EmProcessamento;
                            LOG_UsuarioADBO.Save(entityUsuarioAD);
                        });

                    LOG_UsuarioADBO.ProcessaLogUsuarioAD(ltLogUsuarioAD);
                }
            }
        }

        /// <summary>
        /// Executa o JOB que chama os scripts que executam os agentes FIM.
        /// </summary>
        public static void ExecJobExecucaoScriptsFIM()
        {
            SYS_Servico servico = GetEntity(new SYS_Servico() { ser_id = (short)eChaveServico.ExecucaoScriptsFIM });
            if (servico.ser_ativo)
            {
                string nomeRunProfile = CFG_ConfiguracaoBO.SelecionaValorPorChave("AppNomeRunProfileFIM");
                string nomeAgenteAdma = CFG_ConfiguracaoBO.SelecionaValorPorChave("AppNomeAgenteAdma");
                string nomeAgenteSql = CFG_ConfiguracaoBO.SelecionaValorPorChave("AppNomeAgenteSql");
                string IPMaquinaAgentesFIM = CFG_ConfiguracaoBO.SelecionaValorPorChave("AppIPMaquinaAgentesFIM");

                ManagementScope scope = new ManagementScope(String.Format(@"\\{0}\root\MicrosoftIdentityIntegrationServer", IPMaquinaAgentesFIM));
                scope.Options.Authentication = AuthenticationLevel.PacketPrivacy;
                scope.Connect();

                ObjectQuery query = new ObjectQuery(String.Format("select * from MIIS_ManagementAgent where Name = '{0}'", nomeAgenteAdma));

                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

                foreach (ManagementObject ma in searcher.Get())
                {
                    ma.InvokeMethod("Execute", new object[] { nomeRunProfile });
                }

                query = new ObjectQuery(String.Format("select * from MIIS_ManagementAgent where Name = '{0}'", nomeAgenteSql));

                searcher = new ManagementObjectSearcher(scope, query);

                foreach (ManagementObject ma in searcher.Get())
                {
                    ma.InvokeMethod("Execute", new object[] { nomeRunProfile });
                }

            }
        }
        
        /// <summary>
        /// Executa o JOB que expira as senhas que n�o foram alteradas desde o prazo do par�metro
        /// </summary>
        public static void ExecJobExpiraSenha()
        {
            SYS_Servico servico = GetEntity(new SYS_Servico() { ser_id = (short)eChaveServico.ExpiraSenha });
            if (servico.ser_ativo)
            {
                SYS_UsuarioBO.ExpirarSenhas();
            }
        }
    }
}