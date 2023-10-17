using Amazon.Runtime.Internal.Util;
using Microsoft.Extensions.Logging;

namespace Gotchi.Core.Services;

public abstract class CoreCommandHandlerBase
{
    ILogger<CoreCommandHandlerBase> _logger;
    public CoreCommandHandlerBase(ILogger<CoreCommandHandlerBase> logger)
    {
        _logger = logger;
    }
    protected void Handle(ICoreCommand command) 
    {
        _logger.LogInformation($"Processing command {command.GetType().FullName}");
    }
}
