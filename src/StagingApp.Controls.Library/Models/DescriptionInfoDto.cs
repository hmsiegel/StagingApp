namespace StagingApp.Controls.Library.Models;
public class DescriptionInfoDto : INotifyPropertyChanged
{
    public DescriptionInfoDto(
        string? description,
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
            {
                throw new ArgumentException("For Source=null, Property must be static.", nameof(property));
            }

            if (property != source.GetType().GetProperty(property.Name))
            {
                throw new ArgumentException("This property is not from this source.", nameof(property));
            }
        }

        Description = description;
        Property = property;
        Source = source;
        RefreshNewValue();
    }

    private string? _newValue;

    public string? NewValue
    {
        get { return _newValue; }
        set 
        {
            _newValue = value;
            PropertyChanged?.Invoke(this, _newValueEventArgs);
        }
    }

    public string? Description { get; }
    public PropertyInfo Property { get; }
    public object? Source { get; }

    public void RefreshNewValue()
    {
        Property.GetValue(Source)?.ToString();
    }

    public void UpdateProperty()
    {
        Property.SetValue(Source, NewValue);
    }

    private static readonly PropertyChangedEventArgs _newValueEventArgs = new(nameof(NewValue));

    public event PropertyChangedEventHandler? PropertyChanged;

    public override string ToString() =>
        $"{(string.IsNullOrWhiteSpace(Description) ? string.Empty : $"[{Description}] ")}({Source?.GetType().Name}).{Property.Name}:{NewValue}";
}
