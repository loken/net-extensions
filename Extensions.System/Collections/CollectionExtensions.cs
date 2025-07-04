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

	/// <summary>
	/// Remove each of the <paramref name="items"/> from the <paramref name="collection"/>.
	/// </summary>
	/// <returns><c>true</c> when all items were removed, <c>false</c> when some items could not be removed.</returns>
	/// <remarks>Not in any way optimized. This is purely convenience.</remarks>
	public static bool RemoveRange<T>(this ICollection<T> collection, params T[] items)
	{
		var all = true;

		foreach (var item in items)
			all &= collection.Remove(item);

		return all;
	}

	/// <summary>
	/// Add each of the <paramref name="items"/> from the <paramref name="collection"/>.
	/// </summary>
	/// <returns><c>true</c> when all items were removed, <c>false</c> when some items could not be removed.</returns>
	/// <remarks>Not in any way optimized. This is purely convenience.</remarks>
	public static bool RemoveRange<T>(this ICollection<T> collection, IEnumerable<T> items)
	{
		var all = true;

		foreach (var item in items)
			all &= collection.Remove(item);

		return all;
	}
}