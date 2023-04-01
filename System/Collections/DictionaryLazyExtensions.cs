namespace Loken.System.Collections;

/// <summary>
/// <see cref="IDictionary{TKey,TValue}"/> extensions for lazy initializing
/// a <see cref="KeyValuePair{TKey,TValue}.Value"/> for a given <see cref="KeyValuePair{TKey,TValue}.Key"/>.
/// </summary>
public static class DictionaryLazyExtensions
{
	/// <summary>
	/// Lazy initialize the <paramref name="key"/> with a <typeparamref name="TValue"/> from the <paramref name="initializer"/>.
	/// </summary>
	public static TValue Lazy<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, Func<TValue> initializer)
	{
		return source.TryGetValue(key, out var value)
			? value
			: (source[key] = initializer());
	}

	/// <summary>
	/// Lazy initialize the <paramref name="key"/> with a new <typeparamref name="TActual"/> from its default constructor as <typeparamref name="TNominal"/>.
	/// </summary>
	public static TNominal Lazy<TKey, TNominal, TActual>(this IDictionary<TKey, TNominal> source, TKey key) where TActual : TNominal, new()
	{
		return source.TryGetValue(key, out var value)
			? value
			: (source[key] = new TActual());
	}

	/// <summary>
	/// Lazy initialize the <paramref name="key"/> with a new <typeparamref name="TValue"/> from its default constructor.
	/// </summary>
	public static TValue LazyNew<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key) where TValue : new()
	{
		return source.TryGetValue(key, out var value)
			? value
			: (source[key] = new TValue());
	}

	/// <summary>
	/// Lazy initialize the <paramref name="key"/> with a <code>default(TValue)</code>.
	/// </summary>
	public static TValue? LazyDefault<TKey, TValue>(this IDictionary<TKey, TValue?> source, TKey key)
	{
		return source.TryGetValue(key, out var value)
			? value
			: (source[key] = default);
	}

	/// <summary>
	/// Lazy initialize the <paramref name="key"/> with an empty <see cref="ICollection{TValue}"/>.
	/// </summary>
	public static ICollection<TValue> LazyCollection<TKey, TValue>(this IDictionary<TKey, ICollection<TValue>> source, TKey key)
	{
		return source.Lazy(key, Array.Empty<TValue>);
	}

	/// <summary>
	/// Lazy initialize the <paramref name="key"/> with an empty <see cref="T:TValue[]"/>.
	/// </summary>
	public static TValue[] LazyArray<TKey, TValue>(this IDictionary<TKey, TValue[]> source, TKey key)
	{
		return source.Lazy(key, Array.Empty<TValue>);
	}

	/// <summary>
	/// Lazy initialize the <paramref name="key"/> with an empty <see cref="IList{TValue}"/>.
	/// </summary>
	public static IList<TValue> LazyList<TKey, TValue>(this IDictionary<TKey, IList<TValue>> source, TKey key)
	{
		return source.Lazy<TKey, IList<TValue>, List<TValue>>(key);
	}

	/// <summary>
	/// Lazy initialize the <paramref name="key"/> with an empty <see cref="List{TValue}"/>.
	/// </summary>
	public static List<TValue> LazyList<TKey, TValue>(this IDictionary<TKey, List<TValue>> source, TKey key)
	{
		return source.Lazy<TKey, List<TValue>, List<TValue>>(key);
	}

	/// <summary>
	/// Lazy initialize the <paramref name="key"/> with an empty <see cref="ISet{TValue}"/>.
	/// </summary>
	public static ISet<TValue> LazySet<TKey, TValue>(this IDictionary<TKey, ISet<TValue>> source, TKey key)
	{
		return source.Lazy<TKey, ISet<TValue>, HashSet<TValue>>(key);
	}

	/// <summary>
	/// Lazy initialize the <paramref name="key"/> with an empty <see cref="HashSet{TValue}"/>.
	/// </summary>
	public static HashSet<TValue> LazyHash<TKey, TValue>(this IDictionary<TKey, HashSet<TValue>> source, TKey key)
	{
		return source.Lazy<TKey, HashSet<TValue>, HashSet<TValue>>(key);
	}
}