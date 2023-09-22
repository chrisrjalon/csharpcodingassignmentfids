namespace FidsCodingAssignment.Common.Extensions;

public static class NullableExtensions
{
    public static bool NullOrDefault<T>(this T? value) where T : struct
    {
        return value == null || EqualityComparer<T>.Default.Equals(value.Value, default);
    }
}