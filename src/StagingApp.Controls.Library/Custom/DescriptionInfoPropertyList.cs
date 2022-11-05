namespace StagingApp.Controls.Library.Custom;
public class DescriptionInfoPropertyList
{
    private static readonly Dictionary<Type, ReadOnlyCollection<(string description, PropertyInfo property)>> _typeDescriptions = new();

    public static ReadOnlyCollection<DescriptionInfoDto> GetDescriptions(object source)
    {
        Type sourceType = source.GetType();

        if (!_typeDescriptions.TryGetValue(sourceType, out ReadOnlyCollection<(string description, PropertyInfo property)>? descriptions))
        {
            var properties = new List<PropertyInfo>(sourceType.GetProperties(BindingFlags.Instance | BindingFlags.Public));
            var sortedProperties = properties.OrderByDescending(x => x.GetCustomAttribute<SortAttribute>() == null ?  0 :
                    x.GetCustomAttribute<SortAttribute>()?.SortOrder).ToList();
            List<(string description, PropertyInfo property)> descrType = new(sortedProperties.Count);
            {
                for (int i = sortedProperties.Count - 1; i >= 0; i--)
                {
                    PropertyInfo property = sortedProperties[i];
                    var descr = property.GetCustomAttribute<DescriptionAttribute>();
                    if (descr is null)
                    {
                        continue;
                    }

                    descrType.Add((descr.Description, property));
                    sortedProperties.RemoveAt(i);
                }
            }
            {
                FieldInfo[] fields = sourceType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                for (int i = 0; i < fields.Length; i++)
                {
                    FieldInfo field = fields[i];
                    var descr = field.GetCustomAttribute<DescriptionAttribute>();
                    if (descr is null)
                    {
                        continue;
                    }

                    string fieldName = fields[i].Name.Trim('_');

                    if (sortedProperties.Find(pr => pr.Name.Equals(fieldName, StringComparison.OrdinalIgnoreCase)) is not PropertyInfo property)
                    {
                        continue;
                    }

                    descrType.Add((descr.Description, property));
                    sortedProperties.Remove(property);
                }
            }

            descriptions = Array.AsReadOnly(descrType.ToArray());
            _typeDescriptions.Add(sourceType, descriptions);
        }
        DescriptionInfoDto[] descrArr = new DescriptionInfoDto[descriptions.Count];
        for (int i = 0; i < descriptions.Count; i++)
        {
            descrArr[i] = new DescriptionInfoDto(descriptions[i].description, descriptions[i].property, source);
        }

        return Array.AsReadOnly(descrArr);
    }
}
