namespace Loken.System.Collections.MultiMap;

/// <summary>
/// Extensions for parsing/rendering multi maps - dictionaries where the values are sets.
/// </summary>
public static class DictionaryMultiMapExtensions
{
	/// <summary>
	/// Parse the <paramref name="source"/> into the <paramref name="multiMap"/>.
	/// </summary>
	public static IDictionary<TKey, ISet<TValue>> ParseMultiMap<TKey, TValue>(this IDictionary<TKey, ISet<TValue>> multiMap, string source, MultiMapSeparators? separators = null)
	{
		var sep = separators ?? MultiMapSeparators.Default;

		foreach (var line in source.SplitBy(sep.Entry))
		{
			var (rawKey, rawValue) = line.SplitKvp(sep.KeyValue);
			if (rawValue is null)
				continue;

			var key = rawKey.ConvertTo<string, TKey>();
			var values = rawValue.SplitBy(sep.Value).Select(v => v.ConvertTo<string, TValue>());
			multiMap.LazySet(key).AddRange(values);
		}

		return multiMap;
	}

	/// <summary>
	/// Parse the <paramref name="source"/> in to a multi map.
	/// </summary>
	public static IDictionary<TKey, ISet<TValue>> ParseMultiMap<TKey, TValue>(this string source, MultiMapSeparators? separators = null)
		where TKey : notnull
	{
		return new Dictionary<TKey, ISet<TValue>>().ParseMultiMap(source, separators);
	}

	/// <summary>
	/// Parse the <paramref name="source"/> in to a multi map.
	/// </summary>
	public static IDictionary<T, ISet<T>> ParseMultiMap<T>(this string source, MultiMapSeparators? separators = null)
		where T : notnull
	{
		return source.ParseMultiMap<T, T>(separators);
	}

	/// <summary>
	/// Parse the <paramref name="source"/> in to a multi map.
	/// </summary>
	public static IDictionary<string, ISet<string>> ParseMultiMap(this string source, MultiMapSeparators? separators = null)
	{
		return source.ParseMultiMap<string, string>(separators);
	}

	/// <summary>
	/// Render the <paramref name="multiMap"/> into a <see cref="string"/>.
	/// </summary>
	public static string RenderMultiMap<TKey, TValue>(this IDictionary<TKey, ISet<TValue>> multiMap, MultiMapSeparators? separators = null)
		where TValue : notnull
	{
		var sep = separators ?? MultiMapSeparators.Default;

		var lines = multiMap.Select(pair =>
		{
			return $"{pair.Key}{sep.KeyValue}{string.Join(sep.Value.ToString(), pair.Value.Select(v => v.ToString()))}";
		});

		return string.Join(sep.Entry, lines);
	}
}