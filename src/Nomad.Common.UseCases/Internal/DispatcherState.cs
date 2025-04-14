namespace Nomad.Common.UseCases.Internal;

internal sealed class DispatcherState(UseCaseDispatcher dispatcher, int index)
{
    public UseCaseDispatcher Dispatcher { get; } = dispatcher;
    public int Index { get; } = index;
}