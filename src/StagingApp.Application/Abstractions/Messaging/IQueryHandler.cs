namespace StagingApp.Application.Abstractions.Messaging;
public interface IQueryHandler<TQuery, TRespone>
    : IRequestHandler<TQuery, ErrorOr<Result<TRespone>>>
    where TQuery : IQuery<TRespone>
{
}
