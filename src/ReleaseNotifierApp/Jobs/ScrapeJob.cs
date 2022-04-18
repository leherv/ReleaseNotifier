﻿using Application.UseCases.Base.CQS;
using Application.UseCases.Scrape;
using Domain.Results;
using Quartz;

namespace ReleaseNotifierApp.Jobs;

[DisallowConcurrentExecution]
public class ScrapeJob : IJob
{
    // private readonly ILogger<ScrapeJob> _logger;
    private readonly ICommandDispatcher _commandDispatcher;

    public ScrapeJob(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            // _logger.LogInformation($"{nameof(ScrapeJob)} starting...");
            await _commandDispatcher.Dispatch<ScrapeNewReleasesCommand, Result>(new ScrapeNewReleasesCommand());
            // _logger.LogInformation($"{nameof(ScrapeJob)} finished.");
        }
        catch (Exception e)
        {
            // _logger.LogError(e, "Error in {Job} due to: {Reason}", nameof(ScrapeJob), e.Message);
        }
    }
}