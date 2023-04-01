namespace Loken.System.Collections;

/// <summary>
/// Basic <see cref="ICollection{T}"/> extensions.
/// </summary>
public static class CollectionExtensions
{
	/// <summary>
	/// Add each of the <paramref name="items"/> to the <paramref name="collection"/>.
	/// </summary>
	/// <remarks>Not in any way optimized. This is purely convenience.</remarks>
	public static void AddRange<T>(this ICollection<T> collection, params T[] items)
	{
		foreach (var item in items)
			collection.Add(item);
	}

	/// <summary>
	/// Add each of the <paramref name="items"/> to the <paramref name="collection"/>.
	/// </summary>
	/// <remarks>Not in any way optimized. This is purely convenience.</remarks>
	public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
	{
		foreach (var item in items)
			collection.Add(item);
	}
}