
using Gotchi.CryptoCoins;
using Gotchi.Gotchis;
using Gotchi.Persons;
using Gotchi.Persons.CommandServices;
using Gotchi.Persons.Mangers;
using Gotchi.Persons.Repository;
using Gotchi.Portfolios;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Runtime.ExceptionServices;
using System.Windows.Input;

namespace Gotchi.Core.Services;

public class CommandService
{
    private const string _commandHandlerSuffix = "Handler";
    private const string _commandHandlerMethodName = "Handle";

    public void Start(string[] args, ICoreCommand command) 
    {
        using IHost host = CreateHostBuilder(args).Build();
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var commandName = command.GetType().FullName;
            var commandHandlerType = Type.GetType(commandName + _commandHandlerSuffix);
            commandHandlerType = commandHandlerType ?? throw new CommandHandlerNotFoundException(commandName);

            var commandHandler = services.GetRequiredService(commandHandlerType);
            var commandHandlerMethod = commandHandlerType.GetMethod(_commandHandlerMethodName);
            commandHandlerMethod = commandHandlerMethod ?? throw new MethodNotFoundOnCommandHandlerException(commandName);

            commandHandlerMethod.Invoke(commandHandler, new object[] { command });
        }
        catch (Exception ex)
        {
            ExceptionDispatchInfo.Capture(ex).Throw();
        }
    }

    static IHostBuilder CreateHostBuilder(string[] args) 
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) => 
            {
                CryptoCoinServiceConfig.Register(services);
                PersonServiceConfig.Register(services);
                PortfolioServiceConfig.Register(services);
                GotchiServiceConfig.Register(services);
            });
    }
}
