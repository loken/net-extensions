using System.Collections;

namespace Loken.System.Collections;

public static class EnumerableExtensions
{
	/// <summary>
	/// Returns the only element of a sequence, or a default value if the sequence is empty or if there is more than one element in the sequence.
	/// Similar to <see cref="Enumerable.SingleOrDefault{TSource}(IEnumerable{TSource})"/>,
	/// but differs by returning the default when there are more than one match instead of throwing.
	/// </summary>
	/// <param name="source">An <see cref="IEnumerable{TSource}"/> to return the single element of.</param>
	/// <param name="defaultVal">Optional default value.</param>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
	/// <returns>The single element of the input sequence, or <value>default(TSource)</value> if the sequence contains no- or more than one elements.</returns>
	public static TSource? SingleOrDefaultMany<TSource>(this IEnumerable<TSource> source, TSource? defaultVal = default)
	{
		if (source is IList<TSource> list)
			return list.Count == 1 ? list[0] : defaultVal;

		using var enumerator = source.GetEnumerator();

		if (!enumerator.MoveNext())
			return defaultVal;

		var current = enumerator.Current;
		return enumerator.MoveNext() ? defaultVal : current;
	}

	/// <summary>
	/// Turn a <paramref name="value"/> that may be <c>null</c> into an enumerable.
	/// </summary>
	public static IEnumerable<T> ToEnumerable<T>(this T? value)
	{
		if (value is not null)
			yield return value;
	}

	/// <summary>
	/// Enumerate all items in the <paramref name="enumerable"/>.
	/// Useful when enumerating has side effects and you don't want to create a throwaway collection for the results.
	/// </summary>
	public static void EnumerateAll(this IEnumerable enumerable)
	{
		var enumerator = enumerable.GetEnumerator();
		while (enumerator.MoveNext())
		{
		}
	}
}
