using log4net;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzServer
{
    public sealed class FirstJob : IJob
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(FirstJob));

        public void Execute(IJobExecutionContext context)
        {
            logger.InfoFormat("xuyanhui第一个任务！");
        }
    }
}
