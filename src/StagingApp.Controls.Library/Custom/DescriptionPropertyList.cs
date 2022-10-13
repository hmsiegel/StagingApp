namespace StagingApp.Controls.Library.Custom;
public class DescriptionPropertyList 
{
    private static readonly object _empty = new();
    private static readonly Dictionary<Type, ReadOnlyCollection<DescriptionDto>> _typeDescriptions = new();

    public object? Source { get; }

    public ReadOnlyCollection<DescriptionDto> Descriptions { get; set; }

    public DescriptionPropertyList(object source)
    {
        Source = source ?? throw new ArgumentNullException(nameof(source));

        Type sourceType = source.GetType();

        if (!_typeDescriptions.TryGetValue(sourceType, out ReadOnlyCollection<DescriptionDto>? descriptions))
        {
            PropertyInfo[] properties = sourceType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            DescriptionDto[] descrType = new DescriptionDto[properties.Length];

            for (int i = 0; i < properties.Length; i++)
            {
                PropertyInfo property = properties[i];
                string descr = property.GetCustomAttribute<DescriptionAttribute>()?.Description ?? property.Name;
                descrType[i] = new DescriptionDto(descr, new PropertyPath(property), !property.CanWrite, _empty);
            }
            descriptions = Array.AsReadOnly(descrType);
            _typeDescriptions.Add(sourceType, descriptions);
        }
        DescriptionDto[] descrArr = new DescriptionDto[descriptions.Count];
        for (int i = 0; i < descriptions.Count; i++)
        {
            descrArr[i] = descriptions[i].SetSource(source);
        }
        Descriptions = Array.AsReadOnly(descrArr);
    }
}
