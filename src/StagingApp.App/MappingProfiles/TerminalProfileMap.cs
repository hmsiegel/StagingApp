namespace StagingApp.Main.MappingProfiles;
public sealed class TerminalProfileMap : Profile
{
    public TerminalProfileMap()
    {
        CreateMap<TerminalModel, TerminalConfigureModel>();
    }
}
