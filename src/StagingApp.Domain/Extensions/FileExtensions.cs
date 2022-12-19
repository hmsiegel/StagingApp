namespace StagingApp.Domain.Extensions;
public static class FileExtensions
{
    public static void AssertHasText(this string argument, string name)
    {
        if (string.IsNullOrEmpty(argument))
        {
            throw new ArgumentNullException(
                name,
                string.Format(
                    CultureInfo.InvariantCulture,
                    "Argument '{0}' cannot be null or resolve to an empty string : '{1}'.", name, argument));
        }
    }
}
