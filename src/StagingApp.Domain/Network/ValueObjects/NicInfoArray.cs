namespace StagingApp.Domain.Network.ValueObjects;

[Serializable]
public class NicInfoArray : ValueObject, IEnumerable<NicInfoModel>, IEnumerable
{
    public int? ValidNics;
    public NicInfoModel[]? Nics;

    public IEnumerator<NicInfoModel> GetEnumerator()
    {
        if (Nics is null)
        {
            throw new ArgumentNullException(nameof(Nics));
        }

        return (Nics as IEnumerable<NicInfoModel>).GetEnumerator();
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return ValidNics!;
        yield return Nics!;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        if (Nics is null)
        {
            throw new ArgumentNullException(nameof(Nics));
        }

        return (Nics as IEnumerable<NicInfoModel>).GetEnumerator();
    }
}
