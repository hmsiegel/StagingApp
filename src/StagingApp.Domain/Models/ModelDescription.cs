namespace StagingApp.Domain.Models;
public sealed class ModelDescription
{
    public ModelDescription(string? description,
                          PropertyInfo property,
                          object? source)
    {
        if (property.GetGetMethod() is not MethodInfo method)
        {
            throw new ArgumentException("Property must have a public getter.", nameof(property));
        }
        if (!method.IsStatic)
        {
            if (source is null)
                throw new ArgumentException("For Source=null, Property must be static.", nameof(property));

            if (property != source.GetType().GetProperty(property.Name))
                throw new ArgumentException("This property is not from this Source.", nameof(property));
        }

        Description = description ?? string.Empty;
        Property = property;
        Source = source;
        NewValue = property.GetValue(source)?.ToString();
    }

    public string? Description { get; }
    public PropertyInfo Property { get; }
    public object? Source { get; }
    public string? NewValue { get; set; }

    public ModelDescription SetSource(object? newSource) =>
        new(Description!, Property, newSource);

    public override string ToString() =>
        $"{(string.IsNullOrWhiteSpace(Description) ? string.Empty : $"[{Description}] ")}({Source?.GetType().Name}).{Property.Name}: {NewValue}";

}
