
using Gotchi.Core.Managers;
using Gotchi.CryptoCoins;
using Gotchi.Gotchis;
using Gotchi.Persons;
using Gotchi.Persons.CommandServices;
using Gotchi.Persons.Managers;
using Gotchi.Persons.Repository;
using Gotchi.Portfolios;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Runtime.ExceptionServices;
using System.Windows.Input;

namespace Gotchi.Core.Services;

public class CoreCommandService : ICoreCommandService
{
    private const string _commandHandlerSuffix = "Handler";
    private const string _commandHandlerMethodName = "HandlerInvoke";
    private IServiceProvider _serviceProvider;

    public CoreCommandService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Process(ICoreCommand command)
    {
        try
        {
            var commandName = command.GetType().FullName;
            var commandHandlerType = Type.GetType(commandName + _commandHandlerSuffix);
            commandHandlerType = commandHandlerType ?? throw new CommandHandlerNotFoundException(commandName);

            var commandHandler = _serviceProvider.GetRequiredService(commandHandlerType);
            var commandHandlerMethod = commandHandlerType.GetMethod(_commandHandlerMethodName);
            commandHandlerMethod = commandHandlerMethod ?? throw new MethodNotFoundOnCommandHandlerException(command.ToString());

            commandHandlerMethod.Invoke(commandHandler, new object[] { command });
        }
        catch (Exception ex)
        {
            ExceptionDispatchInfo.Capture(ex).Throw();
        }
    }
}
