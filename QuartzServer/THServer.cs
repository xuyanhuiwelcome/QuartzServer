using log4net;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace QuartzServer
{
    public class THServer : ServiceControl, ServiceSuspend
    {
        private readonly ILog logger;
        private ISchedulerFactory schedulerFactory;
        private IScheduler scheduler;

        /// <summary>
        /// Initializes a new instance of the <see cref="THServer"/> class.
        /// </summary>
        public THServer()
        {
            logger = LogManager.GetLogger(GetType());
        }

        /// <summary>
        /// Initializes the instance of the <see cref="THServer"/> class.
        /// </summary>
        public void Initialize()
        {
            try
            {
                schedulerFactory = CreateSchedulerFactory();
                scheduler = GetScheduler();
            }
            catch (Exception e)
            {
                logger.Error("Server initialization failed:" + e.Message, e);
                throw;
            }
        }

        /// <summary>
        /// Gets the scheduler with which this server should operate with.
        /// </summary>
        /// <returns></returns>
	    protected virtual IScheduler GetScheduler()
        {
            return schedulerFactory.GetScheduler();
        }

        /// <summary>
        /// Returns the current scheduler instance (usually created in <see cref="Initialize" />
        /// using the <see cref="GetScheduler" /> method).
        /// </summary>
	    protected virtual IScheduler Scheduler
        {
            get { return scheduler; }
        }

        /// <summary>
        /// Creates the scheduler factory that will be the factory
        /// for all schedulers on this instance.
        /// </summary>
        /// <returns></returns>
	    protected virtual ISchedulerFactory CreateSchedulerFactory()
        {
            return new StdSchedulerFactory();
        }

        /// <summary>
        /// Starts this instance, delegates to scheduler.
	    /// </summary>
        public bool Start(HostControl hostControl)
        {
            try
            {
                scheduler.Start();
            }
            catch (Exception ex)
            {
                logger.Fatal(string.Format("Scheduler start failed: {0}", ex.Message), ex);
                throw;
            }

            logger.Info("Scheduler started successfully");

            return true;
        }

        /// <summary>
        /// Stops this instance, delegates to scheduler.
        /// </summary>
        public bool Stop(HostControl hostControl)
        {
            try
            {
                scheduler.Shutdown(true);
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("Scheduler stop failed: {0}", ex.Message), ex);
                throw;
            }

            logger.Info("Scheduler shutdown complete");

            return true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public virtual void Dispose()
        {
            // no-op for now
        }

        /// <summary>
        /// Pauses all activity in scheduler.
        /// </summary>
        public bool Pause(HostControl hostControl)
        {
            scheduler.PauseAll();
            return true;
        }

        /// <summary>
        /// Resumes all activity in server.
        /// </summary>
        public bool Continue(HostControl hostControl)
        {
            scheduler.ResumeAll();
            return true;
        }
    }
}
