using System.Collections.Specialized;
using Quartz;
using Quartz.Impl;

namespace Autenticador.Jobs
{
    public class Scheduler
    {
        /// <summary>
        /// Interface Quartz.Net scheduler
        /// </summary>
        private IScheduler scheduler;

        /// <summary>
        /// Initialize Quartz.Net scheduler
        /// </summary>
        public void Run()
        {
            // Get an instance of the Quartz.Net scheduler
            scheduler = GetScheduler();

            ScheduledJobs();

            // Start the scheduler if its in standby
            if (!scheduler.IsStarted)
                scheduler.Start();

            // Não finaliza a execução
            System.Threading.Thread.Sleep(0);
        }

        /// <summary>
        /// Stop Quartz.Net scheduler
        /// </summary>
        public void Stop()
        {
            // Stop the scheduler if its in started
            if (scheduler.IsStarted)
                scheduler.Shutdown();
        }

        /// <summary>
        /// Initialize Jobs Quartz.Net scheduler
        /// </summary>
        private void ScheduledJobs()
        {
            JobDetailImpl jobDetail = new JobDetailImpl(typeof(MS_JOB_IntegracaoActiveDirectory).Name, typeof(MS_JOB_IntegracaoActiveDirectory));
            jobDetail.Durable = true;
            scheduler.AddJob(jobDetail, true);

            jobDetail = new JobDetailImpl(typeof(MS_JOB_ExecucaoScriptsFIM).Name, typeof(MS_JOB_ExecucaoScriptsFIM));
            jobDetail.Durable = true;
            scheduler.AddJob(jobDetail, true);

            jobDetail = new JobDetailImpl(typeof(MS_JOB_ExpiraSenha).Name, typeof(MS_JOB_ExpiraSenha));
            jobDetail.Durable = true;
            scheduler.AddJob(jobDetail, true);
        }

        /// <summary>
        /// Get an instance of the Quartz.Net scheduler
        /// </summary>
        private static IScheduler GetScheduler()
        {
            var properties = new NameValueCollection();
            properties["quartz.scheduler.instanceName"] = "SchedulerCoreSSO";
            properties["quartz.scheduler.instanceId"] = "AUTO";
            properties["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz";
            properties["quartz.threadPool.threadCount"] = "7";
            properties["quartz.threadPool.threadPriority"] = "Normal";
            properties["quartz.jobStore.misfireThreshold"] = "60000";
            properties["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz";
            properties["quartz.jobStore.useProperties"] = "false";
            properties["quartz.jobStore.dataSource"] = "default";
            properties["quartz.jobStore.tablePrefix"] = "QTZ_";
            properties["quartz.jobStore.clustered"] = "true";
            properties["quartz.jobStore.selectWithLockSQL"] = "SELECT * FROM {0}LOCKS UPDLOCK WHERE LOCK_NAME = @lockName";
            properties["quartz.dataSource.default.connectionString"] = Util.GetConnectionString();
            properties["quartz.dataSource.default.provider"] = "SqlServer-20";
            properties["quartz.scheduler.exporter.type"] = "Quartz.Simpl.RemotingSchedulerExporter, Quartz";
            properties["quartz.scheduler.exporter.port"] = "590";
            properties["quartz.scheduler.exporter.bindName"] = "SchedulerCoreSSO";
            properties["quartz.scheduler.exporter.channelType"] = "tcp";

            // Get a reference to the scheduler
            var sf = new StdSchedulerFactory(properties);

            return sf.GetScheduler();
        }
    }
}
