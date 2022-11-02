namespace StagingApp.Controls.Library.Models;
public class DescriptionDto : INotifyPropertyChanged
{

    public DescriptionDto(string? description,
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
        RefreshNewValue();
    }

    public string? Description { get; }
    public PropertyInfo Property { get; }
    public object? Source { get; }

    private string? _newValue;
    public string? NewValue
    {
        get => _newValue;
        set
        {
            _newValue = value;
            PropertyChanged?.Invoke(this, NewValueEventArgs);
        }
    }
    private static readonly PropertyChangedEventArgs NewValueEventArgs = new PropertyChangedEventArgs(nameof(NewValue));

    public event PropertyChangedEventHandler? PropertyChanged;

    public DescriptionDto SetSource(object? newSource) =>
        new(Description!, Property, newSource);

    public override string ToString() =>
        $"{(string.IsNullOrWhiteSpace(Description) ? string.Empty : $"[{Description}] ")}({Source?.GetType().Name}).{Property.Name}: {NewValue}";


    public void RefreshNewValue()
    {
        Property.GetValue(Source)?.ToString();
    }

    public void UpdateProperty()
    {
        Property.SetValue(Source, NewValue);
    }

}
