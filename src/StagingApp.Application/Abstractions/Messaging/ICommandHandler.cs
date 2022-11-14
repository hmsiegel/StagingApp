namespace StagingApp.Application.Abstractions.Messaging;
public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, ErrorOr<Domain.Shared.Result>>
    where TCommand : ICommand
{
}

public interface ICommandHandler<TCommand, TResponse> 
    : IRequestHandler<TCommand, ErrorOr<Result<TResponse>>>
    where TCommand : ICommand<TResponse>
{
}
