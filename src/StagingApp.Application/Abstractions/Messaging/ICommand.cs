using StagingApp.Domain.Shared;

namespace StagingApp.Application.Abstractions.Messaging;
public interface ICommand : IRequest<Result>
{
}
