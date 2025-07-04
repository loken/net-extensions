using System.Diagnostics.CodeAnalysis;

namespace Loken.System.ComponentModel;

/// <summary>
/// Extensions for retrieving values from a dictionary and converting them between <see cref="Type"/>s
/// using <see cref="TypeConverter"/>s registered with the <see cref="TypeDescriptor"/>.
/// </summary>
public static class TypeConverterDictionaryExtensions
{

	/// <summary>
	/// Get the <see cref="object"/> stored for the <paramref name="key"/> and <see cref="ConversionExtensions.Convert{TValue}(object,TValue)"/>
	/// it to <typeparamref name="TValue"/> if it exists, <paramref name="defaultValue"/> otherwise.
	/// </summary>
	/// <exception cref="NotSupportedException">If the item exists but is of a <see cref="Type"/> inconvertible to <typeparamref name="TValue"/>.</exception>
	[return: NotNullIfNotNull(nameof(defaultValue))]
	public static TValue? Get<TValue>(this IDictionary<string, object> dictionary, string key, TValue? defaultValue = default)
	{
		return dictionary.TryGetValue(key, out var outValue)
			? outValue.Convert(defaultValue)
			: defaultValue;
	}

	/// <summary>
	/// Get the <see cref="object"/> stored for the <paramref name="key"/> and <see cref="ConversionExtensions.Convert{TValue}(object, Func{TValue})"/>
	/// it to <typeparamref name="TValue"/> if it exists, <paramref name="defaultGenerator"/> value otherwise.
	/// </summary>
	/// <exception cref="NotSupportedException">If the item exists but is of a <see cref="Type"/> inconvertible to <typeparamref name="TValue"/>.</exception>
	public static TValue Get<TValue>(this IDictionary<string, object> dictionary, string key, Func<TValue> defaultGenerator)
	{
		return dictionary.TryGetValue(key, out var outValue)
			? outValue.Convert(defaultGenerator)!
			: defaultGenerator();
	}
}
