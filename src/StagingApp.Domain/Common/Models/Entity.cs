namespace StagingApp.Domain.Common.Models;
public abstract class Entity : IEquatable<Entity>
{
    public bool Equals(Entity? other)
    {
        return Equals((object?)other);
    }

    public override bool Equals(object? obj)
    {
        return obj is Entity entity && Equals(entity);
    }

    public static bool operator ==(Entity left, Entity right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Entity left, Entity right)
    {
        return !Equals(left, right);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
