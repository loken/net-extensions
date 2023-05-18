using System.Diagnostics.CodeAnalysis;

namespace Loken.System.Collections;

/// <summary>
/// A wrapper interface for linear data structures such as <see cref="Queue{T}"/> and <see cref="Stack{T}"/>
/// which allows interchangeable use.
/// </summary>
/// <typeparam name="T">The type of items to store in the data structure.</typeparam>
public interface ILinear<T> : IEnumerable<T>, IReadOnlyCollection<T>
{
	/// <summary>
	/// Attach an item.
	/// </summary>
	void Attach(T item);

	/// <summary>
	/// Detach an item.
	/// </summary>
	/// <exception cref="InvalidOperationException">If there are no more items.</exception>
	T Detach();

	/// <summary>
	/// Detach and output an item as long as there are more items.
	/// </summary>
	bool TryDetach([MaybeNullWhen(false)] out T item);

	/// <summary>
	/// Retrieve the next detachable item without detaching it.
	/// </summary>
	/// <exception cref="InvalidOperationException">If there are no more items.</exception>
	T Peek();

	/// <summary>
	/// Retrieve and output the next detachable item, without detaching it, as long as there are more items.
	/// </summary>
	bool TryPeek([MaybeNullWhen(false)] out T item);

	/// <summary>
	/// Clear out remaining items.
	/// </summary>
	void Clear();
}
