using System;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;

namespace TwitchBOT
{
    class Program
    {
        private static IScheduler _scheduler;

        static async Task Main(string[] args)
        {
            Bot bot = new Bot();

            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            _scheduler = await schedulerFactory.GetScheduler();
            _scheduler.Start();
            Console.WriteLine("Starting Scheduler");

            AddJob();
            Console.ReadLine();
            await Task.Run(() => Thread.Sleep(Timeout.Infinite));
        }

        public static void AddJob()
        {
            IMyJob myJob = new MyJob(); //This Constructor needs to be parameterless
            JobDetailImpl jobDetail = new JobDetailImpl("Job1", "Group1", myJob.GetType());
            CronTriggerImpl trigger = new CronTriggerImpl("Trigger1", "Group1", "* * * * *"); //run every minute between the hours of 8am and 5pm
            _scheduler.ScheduleJob(jobDetail, trigger);
            DateTimeOffset? nextFireTime = trigger.GetNextFireTimeUtc();
            Console.WriteLine("Next Fire Time:" + nextFireTime.Value);
        }
    }

    internal class MyJob : IMyJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("In MyJob class");
            DoMoreWork();
        }

        public void DoMoreWork()
        {
            Console.WriteLine("Do More Work");
        }
    }

    internal interface IMyJob : IJob
    {
    }
}