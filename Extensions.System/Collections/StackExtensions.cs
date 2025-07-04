namespace Loken.System.Collections;

/// <summary>
/// Basic <see cref="Queue{T}"/> extensions
/// </summary>
public static class StackExtensions
{
	/// <summary>
	/// <see cref="Stack{T}.Push"/> each of the <paramref name="items"/> onto the stack.
	/// </summary>
	public static void Push<T>(this Stack<T> stack, params T[] items)
	{
		if (items is null)
			return;

		foreach (var item in items)
			stack.Push(item);
	}

	/// <summary>
	/// <see cref="Stack{T}.Push"/> each of the <paramref name="items"/> onto the stack.
	/// </summary>
	public static void Push<T>(this Stack<T> stack, IEnumerable<T> items)
	{
		if (items is null)
			return;

		foreach (var item in items)
			stack.Push(item);
	}
}