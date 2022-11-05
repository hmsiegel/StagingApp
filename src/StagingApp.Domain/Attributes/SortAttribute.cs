namespace StagingApp.Domain.Attributes;

[AttributeUsage(AttributeTargets.All)]
public sealed class SortAttribute : Attribute
{
    public int SortOrder { get; set; }

    public SortAttribute()
    {

    }

}
