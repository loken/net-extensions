using System.Diagnostics.CodeAnalysis;

namespace Loken.System.Collections;

/// <summary>
/// Extensions for retrieving values from a dictionary while providing a
/// default value or default value generator.
/// </summary>
public static class DictionaryGetExtensions
{
	/// <summary>
	/// Get the <typeparamref name="TValue"/> stored for the <paramref name="key"/> if it exists, <paramref name="defaultValue"/> otherwise.
	/// </summary>
	[return: NotNullIfNotNull(nameof(defaultValue))]
	public static TValue? Get<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue? defaultValue = default)
	{
		return dictionary.TryGetValue(key, out var result)
			? result
			: defaultValue;
	}

	/// <summary>
	/// Get the <typeparamref name="TValue"/> stored for the <paramref name="key"/> if it exists, <paramref name="defaultGenerator"/> value otherwise.
	/// </summary>
	public static TValue Get<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> defaultGenerator)
	{
		return dictionary.TryGetValue(key, out var result)
			? result
			: defaultGenerator();
	}
}
