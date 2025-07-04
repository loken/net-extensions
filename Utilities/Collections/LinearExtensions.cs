namespace Loken.Utilities.Collections;

/// <summary>
/// Basic <see cref="ILinear{T}"/> extensions
/// </summary>
public static class LinearExtensions
{
	/// <summary>
	/// <see cref="ILinear{T}.Enqueue"/> each of the <paramref name="items"/>.
	/// </summary>
	public static int Attach<T>(this ILinear<T> linear, params T[] items)
	{
		if (items is null)
			return 0;

		foreach (var item in items)
			linear.Attach(item);

		return items.Length;
	}

	/// <summary>
	/// <see cref="ILinear{T}.Attach"/> each of the <paramref name="items"/>.
	/// </summary>
	public static int Attach<T>(this ILinear<T> linear, IEnumerable<T> items)
	{
		if (items is null)
			return 0;

		var count = 0;

		foreach (var item in items)
		{
			linear.Attach(item);
			count++;
		}

		return count;
	}
}