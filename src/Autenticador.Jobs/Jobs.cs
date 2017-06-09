using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autenticador.BLL;
using Quartz;

namespace Autenticador.Jobs
{
    public class MS_JOB_IntegracaoActiveDirectory : IJob
    {
        #region IJob Members

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                SYS_ServicoBO.ExecJobIntegracaoActiveDirectory();
            }
            catch (Exception ex)
            {
                Util.GravarErro(ex);
            }
        }

        #endregion IJob Members
    }

    public class MS_JOB_ExecucaoScriptsFIM : IJob
    {
        #region IJob Member

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                SYS_ServicoBO.ExecJobExecucaoScriptsFIM();
            }
            catch (Exception ex)
            {
                Util.GravarErro(ex);
            }
        }

        #endregion
    }

    public class MS_JOB_ExpiraSenha : IJob
    {
        #region IJob Member

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                SYS_ServicoBO.ExecJobExpiraSenha();
            }
            catch (Exception ex)
            {
                Util.GravarErro(ex);
            }
        }

        #endregion
    }
}
