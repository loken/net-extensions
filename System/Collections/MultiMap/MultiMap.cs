namespace Loken.System.Collections.MultiMap;

/// <summary>
/// A dictionary of keys to sets of values, representing a multi-map data structure.
/// </summary>
public class MultiMap<T> : Dictionary<T, ISet<T>> where T : notnull
{
	/// <summary>
	/// Initializes a new instance of the <see cref="MultiMap{T}"/> class.
	/// </summary>
	public MultiMap() : base() { }

	/// <summary>
	/// Initializes a new instance of the <see cref="MultiMap{T}"/> class with the specified capacity.
	/// </summary>
	public MultiMap(int capacity) : base(capacity) { }

	/// <summary>
	/// Initializes a new instance of the <see cref="MultiMap{T}"/> class with the specified comparer.
	/// </summary>
	public MultiMap(IEqualityComparer<T> comparer) : base(comparer) { }

	/// <summary>
	/// Add one or more values to the set at the specified key.
	/// </summary>
	/// <param name="key">The key.</param>
	/// <param name="values">The values to add.</param>
	/// <returns>The number of values that were actually added (excluding duplicates).</returns>
	public int Add(T key, params T[] values)
	{
		var set = this.LazySet(key);
		var initialCount = set.Count;
		set.AddRange(values);
		return set.Count - initialCount;
	}

	/// <summary>
	/// Add one or more values to the set at the specified key.
	/// </summary>
	/// <param name="key">The key.</param>
	/// <param name="values">The values to add.</param>
	/// <returns>The number of values that were actually added (excluding duplicates).</returns>
	public int Add(T key, IEnumerable<T> values)
	{
		var set = this.LazySet(key);
		var initialCount = set.Count;
		set.AddRange(values);
		return set.Count - initialCount;
	}

	/// <summary>
	/// Remove one or more values from the set at the specified key.
	/// If the set becomes empty, the key is removed from the dictionary.
	/// </summary>
	/// <param name="key">The key.</param>
	/// <param name="values">The values to remove.</param>
	/// <returns>A set of all <typeparamref name="T"/>'s that were removed, including the key if it was also removed.</returns>
	public ISet<T> Remove(T key, params T[] values)
	{
		return RemoveInternal(key, values);
	}

	/// <summary>
	/// Remove one or more values from the set at the specified key.
	/// If the set becomes empty, the key is removed from the dictionary.
	/// </summary>
	/// <param name="key">The key.</param>
	/// <param name="values">The values to remove.</param>
	/// <returns>A set of all <typeparamref name="T"/>'s that were removed, including the key if it was also removed.</returns>
	public ISet<T> Remove(T key, IEnumerable<T> values)
	{
		return RemoveInternal(key, values);
	}

	private HashSet<T> RemoveInternal(T key, IEnumerable<T> values)
	{
		if (!ContainsKey(key))
			return [];

		var set = this[key];
		var removed = new HashSet<T>();

		foreach (var value in values)
		{
			if (set.Remove(value))
				removed.Add(value);
		}

		if (set.Count == 0)
		{
			if (base.Remove(key))
				removed.Add(key);
		}

		return removed;
	}

	/// <summary>
	/// Get a set of all keys and values in the multi map.
	/// </summary>
	/// <returns>A set containing all keys and values.</returns>
	public ISet<T> GetAll()
	{
		var all = new HashSet<T>();

		foreach (var kvp in this)
		{
			all.Add(kvp.Key);
			all.UnionWith(kvp.Value);
		}

		return all;
	}

	/// <summary>
	/// Parse the input string into this MultiMap instance.
	/// </summary>
	/// <param name="input">The string to parse.</param>
	/// <param name="separators">The separators to use for parsing.</param>
	/// <returns>This MultiMap instance for method chaining.</returns>
	public MultiMap<T> Parse(string input, MultiMapSeparators? separators = null)
	{
		var sep = separators ?? MultiMapSeparators.Default;

		foreach (var line in input.SplitBy(sep.Entry))
		{
			var (rawKey, rawValue) = line.SplitKvp(sep.KeyValue);
			var key = rawKey.ChangeType<T>();

			if (rawValue is null)
			{
				Add(key);
			}
			else
			{
				var values = rawValue.SplitBy(sep.Value).Select(v => v.ChangeType<T>());
				Add(key, values);
			}
		}

		return this;
	}

	/// <summary>
	/// Render the MultiMap into a string.
	/// </summary>
	/// <param name="separators">The separators to use for rendering.</param>
	/// <returns>A string representation of the MultiMap.</returns>
	public string Render(MultiMapSeparators? separators = null)
	{
		var sep = separators ?? MultiMapSeparators.Default;

		var lines = this.Select(pair =>
		{
			if (pair.Value.Count == 0)
				return $"{pair.Key}";
			else
				return $"{pair.Key}{sep.KeyValue}{string.Join(sep.Value.ToString(), pair.Value.Select(v => v.ToString()))}";
		});

		return string.Join(sep.Entry, lines);
	}
}

/// <summary>
/// Static factory methods for creating and manipulating MultiMap instances.
/// </summary>
public static class MultiMap
{
	/// <summary>
	/// Parse the specified string into a MultiMap.
	/// </summary>
	/// <typeparam name="T">The type of keys and values in the MultiMap.</typeparam>
	/// <param name="source">The string to parse.</param>
	/// <param name="separators">The separators to use for parsing.</param>
	/// <returns>A new MultiMap instance containing the parsed data.</returns>
	public static MultiMap<T> Parse<T>(string source, MultiMapSeparators? separators = null) where T : notnull
	{
		return new MultiMap<T>().Parse(source, separators);
	}

	/// <summary>
	/// Render the specified MultiMap into a string.
	/// </summary>
	/// <typeparam name="T">The type of keys and values in the MultiMap.</typeparam>
	/// <param name="multiMap">The MultiMap to render.</param>
	/// <param name="separators">The separators to use for rendering.</param>
	/// <returns>A string representation of the MultiMap.</returns>
	public static string Render<T>(MultiMap<T> multiMap, MultiMapSeparators? separators = null) where T : notnull
	{
		return multiMap.Render(separators);
	}
}
