namespace Scheduler.Api.Services;

public interface IJobSchedulerService
{
    string FireAndForgetJob();
    string DelayedJob();
    void RecurringJob();
    string ContinuationsJob();
}
