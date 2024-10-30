namespace Scheduler.Api.Services;

public sealed class JobSchedulerService(
    IBackgroundJobClient backgroundJobClient,
    IRecurringJobManager recurringJobManager)
    : IJobSchedulerService
{
    public string ContinuationsJob()
    {
        var parentJobId = backgroundJobClient.Enqueue(
            () => FireAndForgetJob());

        var jobId = backgroundJobClient.ContinueJobWith(parentJobId,
             () => Console.WriteLine("ContinuationsJob Job!"));

        return jobId;
    }

    public string DelayedJob()
    {
        var jobId = backgroundJobClient.Schedule(
            () => Console.WriteLine("Delayed Job!"),
            TimeSpan.FromMinutes(1));

        return jobId;
    }

    public string FireAndForgetJob()
    {
        var jobId = backgroundJobClient.Enqueue(
            () => Console.WriteLine("Fire-and-Forget Job!"));

        return jobId;
    }

    public void RecurringJob()
    {
        recurringJobManager.AddOrUpdate("jobId",
          () => Console.WriteLine("Recurring Job!"),
          Cron.Minutely);
    }
}
