namespace StagingApp.Controls.Library.Custom;
public static class DescriptionPropertyList
{
    private static readonly Dictionary<Type, ReadOnlyCollection<(string description, PropertyInfo property)>> _typeDescriptions = new();

    public static ReadOnlyCollection<DescriptionDto> GetDescriptions(object source)
    {
        Type sourceType = source.GetType();

        if (!_typeDescriptions.TryGetValue(sourceType, out ReadOnlyCollection<(string description, PropertyInfo property)>? descriptions))
        {
            var properties = new List<PropertyInfo>(sourceType.GetProperties(BindingFlags.Instance | BindingFlags.Public));
            List<(string description, PropertyInfo property)> descrType = new List<(string description, PropertyInfo property)>(properties.Count);
            {
                for (int i = properties.Count - 1; i >= 0; i--)
                {
                    PropertyInfo property = properties[i];
                    var descr = property.GetCustomAttribute<DescriptionAttribute>();
                    if (descr is null)
                        continue;

                    descrType.Add((descr.Description, property));
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

                    descrType.Add((descr.Description, property));
                    properties.Remove(property);
                }
            }

            descriptions = Array.AsReadOnly(descrType.ToArray());
            _typeDescriptions.Add(sourceType, descriptions);
        }
        DescriptionDto[] descrArr = new DescriptionDto[descriptions.Count];
        for (int i = 0; i < descriptions.Count; i++)
        {
            descrArr[i] = new DescriptionDto(descriptions[i].description, descriptions[i].property, source);
        }

        return Array.AsReadOnly(descrArr);
    }
}
