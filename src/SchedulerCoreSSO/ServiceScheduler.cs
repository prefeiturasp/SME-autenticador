using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Autenticador.Jobs;

namespace SchedulerCoreSSO
{
    public partial class ServiceScheduler : ServiceBase
    {
        private Scheduler scheduler;

        public ServiceScheduler()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                scheduler = new Scheduler();
                scheduler.Run();
            }
            catch (Exception ex)
            {
                Util.GravarErro(ex);
            }
        }

        protected override void OnStop()
        {
            try
            {
                scheduler.Stop();
            }
            catch (Exception ex)
            {
                Util.GravarErro(ex);
            }
        }
    }
}
