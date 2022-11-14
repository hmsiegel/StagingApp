namespace StagingApp.Application.Abstractions.Messaging;
public interface IQuery<TResponse> : IRequest<ErrorOr<Result<TResponse>>>
{
}
