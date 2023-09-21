using FidsCodingAssignment.Common.Interfaces;

namespace FidsCodingAssignment.Common.Extensions;

public static class EntityExtensions
{
    public static int NextId<T>(this ICollection<T> entities) where T : IEntity
    {
        var maxId = entities.Max(x => x.Id);
        return maxId + 1;
    }
}