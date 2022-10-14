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
            var properties = new List<PropertyInfo>(sourceType.GetProperties(BindingFlags.Instance | BindingFlags.Public));
            List<DescriptionDto> descrType = new List<DescriptionDto>(properties.Count);
            {
                for (int i = properties.Count - 1; i >= 0; i--)
                {
                    PropertyInfo property = properties[i];
                    var descr = property.GetCustomAttribute<DescriptionAttribute>();
                    if (descr is null)
                        continue;

                    descrType.Add(new DescriptionDto(descr.Description, new PropertyPath(property), !property.CanWrite, _empty));
                    properties.RemoveAt(i);
                }
            }
            {
                FieldInfo[] fields = sourceType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                for (int i = 0; i < fields.Length; i++)
                {
                    FieldInfo fieled = fields[i];
                    var descr = fieled.GetCustomAttribute<DescriptionAttribute>();
                    if (descr is null)
                        continue;
                    string fieldName = fields[i].Name.Trim('_');

                    if (properties.Find(pr => pr.Name.Equals(fieldName, StringComparison.OrdinalIgnoreCase)) is not PropertyInfo property)
                        continue;

                    descrType.Add(new DescriptionDto(descr.Description, new PropertyPath(property), !property.CanWrite, _empty));
                    properties.Remove(property);
                }
            }

            descriptions = Array.AsReadOnly(descrType.ToArray());
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
