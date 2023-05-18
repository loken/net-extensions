using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Loken.System.Collections;

/// <summary>
/// An <see cref="ILinear{T}"/> <see cref="Queue{T}"/>.
/// </summary>
/// <typeparam name="T">The type of items to store in the queue.</typeparam>
public class LinearQueue<T> : Queue<T>, ILinear<T>
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public T Detach()
	{
		return Dequeue();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Attach(T item)
	{
		Enqueue(item);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool TryDetach([MaybeNullWhen(false)] out T item)
	{
		return TryDequeue(out item);
	}
}
