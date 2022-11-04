namespace StagingApp.Domain.Services;
public static class EnumServices
{
    public static IEnumerable<string> GetEnumDescriptions<T>()
    {
        var attributes = typeof(T).GetMembers()
            .SelectMany(member => member.GetCustomAttributes(typeof(DescriptionAttribute), true).Cast<DescriptionAttribute>())
            .ToList();

        return attributes.Select(x => x.Description);
    }
}
