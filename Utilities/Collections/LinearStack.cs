using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Loken.Utilities.Collections;

/// <summary>
/// An <see cref="ILinear{T}"/> <see cref="Stack{T}"/>.
/// </summary>
/// <typeparam name="T">The type of items to store in the stack.</typeparam>
public class LinearStack<T> : Stack<T>, ILinear<T>
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public T Detach()
	{
		return Pop();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Attach(T item)
	{
		Push(item);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool TryDetach([MaybeNullWhen(false)] out T item)
	{
		return TryPop(out item);
	}
}
