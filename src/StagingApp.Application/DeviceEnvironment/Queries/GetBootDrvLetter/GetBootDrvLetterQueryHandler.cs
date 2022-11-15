namespace StagingApp.Application.DeviceEnvironment.Queries.GetBootDrvLetter;
internal sealed class GetBootDrvLetterQueryHandler : IQueryHandler<GetBootDrvLetterQuery, string>
{
    public async Task<Result<string>> Handle(GetBootDrvLetterQuery request, CancellationToken cancellationToken)
    {
        DriveInfo[] drives = DriveInfo.GetDrives();
        string output = string.Empty;

        foreach (DriveInfo drive in drives)
        {
            if (drive.VolumeLabel == "Aloha Drive")
            {
                output = drive.Name; 
            }
        }

        if (output is null)
        {
            return Result.Failure<string>(Errors.DeviceEnvironment.GetBootDrvLetter);
        }

        return output;
    }
}
