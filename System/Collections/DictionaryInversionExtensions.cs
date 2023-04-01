namespace Loken.System.Collections;

/// <summary>
/// <see cref="IDictionary{TKey,TValue}"/> extensions for inverting the Keys and Values.
/// </summary>
public static class DictionaryInversionExtensions
{
	/// <summary>
	/// Create an inverse dictionary, where each <typeparamref name="TKey"/> is swapped with its <typeparamref name="TValue"/>.
	/// </summary>
	public static IDictionary<TValue, TKey> Invert<TKey, TValue>(this IDictionary<TKey, TValue> source)
		where TValue : notnull
	{
		Dictionary<TValue, TKey> inverse = new();
		foreach (var pair in source)
			inverse.Add(pair.Value, pair.Key);

		return inverse;
	}

	/// <summary>
	/// Create an inverse dictionary where each <typeparamref name="TKey"/> is added to an <see cref="ICollection{TKey}"/> for its <typeparamref name="TValue"/>.
	/// </summary>
	public static IDictionary<TValue, ICollection<TKey>> InvertMany<TKey, TValue>(this IDictionary<TKey, TValue> source)
	  where TValue : notnull
	{
		Dictionary<TValue, ICollection<TKey>> inverse = new();
		foreach (var pair in source)
			inverse.Lazy<TValue, ICollection<TKey>, List<TKey>>(pair.Value).Add(pair.Key);

		return inverse;
	}

	/// <summary>
	/// Create an inverse dictionary where each <typeparamref name="TKey"/> is added to an <see cref="ISet{TKey}"/> for its <typeparamref name="TValue"/>.
	/// </summary>
	public static IDictionary<TValue, ISet<TKey>> InvertSet<TKey, TValue>(this IDictionary<TKey, TValue> source)
		where TValue : notnull
	{
		Dictionary<TValue, ISet<TKey>> inverse = new();
		foreach (var pair in source)
			_ = inverse.LazySet(pair.Value).Add(pair.Key);

		return inverse;
	}
}