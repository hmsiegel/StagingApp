namespace StagingApp.Controls.Library.Models;
public class DescriptionDto
{
    public DescriptionDto(string? description,
                          PropertyPath? path,
                          bool isReadonly,
                          object? source)
    {
        Description = description ?? string.Empty;
        Path = path;
        IsReadonly = isReadonly;
        Source = source;
    }

    public string? Description { get; }
    public PropertyPath? Path { get; }
    public bool IsReadonly { get; }
    public object? Source { get; }


    public DescriptionDto SetSource(object? newSource) =>
        new(Description!, Path!, IsReadonly, newSource);

    public override string ToString() => Description!;
}
