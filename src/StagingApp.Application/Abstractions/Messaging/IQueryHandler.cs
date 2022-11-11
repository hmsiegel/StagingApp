namespace StagingApp.Application.Abstractions.Messaging;
public interface IQueryHandler<TQuery, TRespone>
    : IRequestHandler<TQuery, Result<TRespone>>
    where TQuery : IQuery<TRespone>
{
}
