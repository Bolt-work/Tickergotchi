namespace Gotchi.Core.Services
{
    public interface ICoreCommandService
    {
        void Process(ICoreCommand command);
    }
}