using log4net;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzServer
{
    public class SecondJob:IJob
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(SecondJob));

        public void Execute(IJobExecutionContext context)
        {
            logger.InfoFormat("xuyanhui第二个任务！");
        }
    }
}
