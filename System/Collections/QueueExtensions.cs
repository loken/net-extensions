namespace Loken.System.Collections;

/// <summary>
/// Basic <see cref="Queue{T}"/> extensions
/// </summary>
public static class QueueExtensions
{
	/// <summary>
	/// <see cref="Queue{T}.Enqueue"/> each of the <paramref name="items"/>.
	/// </summary>
	public static void Enqueue<T>(this Queue<T> queue, params T[] items)
	{
		if (items is null)
			return;

		foreach (var item in items)
			queue.Enqueue(item);
	}

	/// <summary>
	/// <see cref="Queue{T}.Enqueue"/> each of the <paramref name="items"/>.
	/// </summary>
	public static void Enqueue<T>(this Queue<T> queue, IEnumerable<T> items)
	{
		if (items is null)
			return;

		foreach (var item in items)
			queue.Enqueue(item);
	}
}