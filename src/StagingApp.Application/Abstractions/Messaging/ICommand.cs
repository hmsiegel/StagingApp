namespace StagingApp.Application.Abstractions.Messaging;
public interface ICommand : IRequest<ErrorOr<Domain.Shared.Result>>
{
}

public interface ICommand<TResponse> : IRequest<ErrorOr<Result<TResponse>>>
{
}
