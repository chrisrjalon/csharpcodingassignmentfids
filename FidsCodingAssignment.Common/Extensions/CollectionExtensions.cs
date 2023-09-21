namespace FidsCodingAssignment.Common.Extensions;

public static class CollectionExtensions
{
    public static ICollection<T> EmptyIfNull<T>(this ICollection<T>? source)
    {
        return source ?? Array.Empty<T>();
    }
}