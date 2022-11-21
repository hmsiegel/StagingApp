namespace StagingApp.Domain.Extensions;
public static class EnumExtensions
{
    public static string ToDescriptionString(this Enum val)
    {
        DescriptionAttribute[] attributes = (DescriptionAttribute[])val
            .GetType()
            .GetField(val.ToString())!
            .GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? attributes[0].Description : string.Empty;
    }

    public static List<string> EnumToListString(this Enum val)
    {
        return Enum.GetNames(val.GetType())
            .Select(x => x.ToString())
            .ToList();
    }

    public static string ConvertToFileExtension(this Enum val)
    {
        return "." + val.ToString().ToLower();
    }
}
