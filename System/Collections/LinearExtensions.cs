namespace Loken.System.Collections;

/// <summary>
/// Basic <see cref="ILinear{T}"/> extensions
/// </summary>
public static class LinearExtensions
{
	/// <summary>
	/// <see cref="ILinear{T}.Enqueue"/> each of the <paramref name="items"/>.
	/// </summary>
	public static void Attach<T>(this ILinear<T> linear, params T[] items)
	{
		if (items is null)
			return;

		foreach (var item in items)
			linear.Attach(item);
	}

	/// <summary>
	/// <see cref="ILinear{T}.Attach"/> each of the <paramref name="items"/>.
	/// </summary>
	public static void Attach<T>(this ILinear<T> linear, IEnumerable<T> items)
	{
		if (items is null)
			return;

		foreach (var item in items)
			linear.Attach(item);
	}
}