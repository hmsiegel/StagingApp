namespace StagingApp.Domain.Network.ValueObjects;

[StructLayout(LayoutKind.Sequential)]
public class NetResource
{
   public Enums.ResourceScope Scope;
   public ResourceType ResourceType;
   public ResourceDisplaytype DisplayType;
   public int Usage;
   public string? LocalName;
   public string? RemoteName;
   public string? Comment;
   public string? Provider;
}
