namespace Scheduler.Api.Endpoints;

public static class JobSchedulerEndpoints
{
    public static void MapJobSchedulerEndpoints(this IEndpointRouteBuilder app)
    {
        var schedulerGroup = app.MapGroup("api/job-schedulers");

        schedulerGroup.MapGet("/fire-and-forget-job", ScheduleFireAndForgetJob)
            .WithName(nameof(ScheduleFireAndForgetJob));

        schedulerGroup.MapGet("/delayed-job", ScheduleDelayedJob)
            .WithName(nameof(ScheduleDelayedJob));

        schedulerGroup.MapGet("/recurring-job", ScheduleRecurringJob)
        .WithName(nameof(ScheduleRecurringJob));

        schedulerGroup.MapGet("/continuations-job", ScheduleContinuationsJob)
        .WithName(nameof(ScheduleContinuationsJob));
    }

    public static IResult ScheduleFireAndForgetJob(
       IJobSchedulerService jobSchedulerService,
       CancellationToken cancellationToken)
    {
        var jobId = jobSchedulerService.FireAndForgetJob();

        return Results.Ok(jobId);
    }

    public static IResult ScheduleDelayedJob(
      IJobSchedulerService jobSchedulerService,
      CancellationToken cancellationToken)
    {
        var jobId = jobSchedulerService.DelayedJob();

        return Results.Ok(jobId);
    }

    public static IResult ScheduleRecurringJob(
      IJobSchedulerService jobSchedulerService,
      CancellationToken cancellationToken)
    {
        jobSchedulerService.RecurringJob();

        return Results.Ok("Recurring Job is scheduled!");
    }

    public static IResult ScheduleContinuationsJob(
      IJobSchedulerService jobSchedulerService,
      CancellationToken cancellationToken)
    {
        var jobId = jobSchedulerService.ContinuationsJob();

        return Results.Ok(jobId);
    }
}
