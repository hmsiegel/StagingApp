namespace StagingApp.Domain.Models;
public sealed partial class IniFileHelper : ValueObject
{
    private readonly string _path;

    public IniFileHelper(string path)
    {
        _path = path;
    }

    public void IniWriteValue(string section, string key, string value) => WritePrivateProfileString(section, key, value, _path);

    public string IniReadValue(string section, string key)
    {
        StringBuilder retValue = new StringBuilder(byte.MaxValue);
        GetPrivateProfileString(section, key, "", retValue, byte.MaxValue, _path);
        return retValue.ToString();
    }

    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

    [LibraryImport("kernel32", StringMarshalling = StringMarshalling.Utf16)]
    private static partial int WritePrivateProfileString(string section, string key, string value, string? path);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return _path;
    }
}
