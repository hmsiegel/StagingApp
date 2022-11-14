using StagingApp.Domain.Errors;

namespace StagingApp.Application.FileSystem.Commands.CreateMarkerFile;
internal sealed class CreateMarkerFileCommandHandler : ICommandHandler<CreateMarkerFileCommand>
{

    Task<ErrorOr<Domain.Shared.Result>> IRequestHandler<CreateMarkerFileCommand, ErrorOr<Domain.Shared.Result>>.Handle(CreateMarkerFileCommand request, CancellationToken cancellationToken)
    {
        if (File.Exists(request.FileName)
        {
            return Errors.FileSystem.MarkerFileAlreadyExists;
        }
    }
}
