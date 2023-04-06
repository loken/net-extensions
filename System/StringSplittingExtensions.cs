namespace Loken.System;

/// <summary>
/// Extension methods for splitting <see cref="string"/>s in various ways.
/// </summary>
/// <remarks>Each of these methods will split using <see cref="StringSplitOptions.RemoveEmptyEntries"/>.</remarks>
public static class StringSplittingExtensions
{
	/// <summary>
	/// Split the <paramref name="source"/> by the <see cref="StringDefaults.Separators"/>.
	/// </summary>
	public static string[] SplitByDefault(this string source)
	{
		return source.SplitBy(StringDefaults.Separators.ToArray());
	}

	/// <summary>
	/// Split the <paramref name="source"/> into a string array.
	/// </summary>
	/// <param name="separators">
	/// Separators to use for splitting the <paramref name="source"/>.
	/// Will use the <see cref="StringDefaults.Separators"/> if no separators are provided.
	/// </param>
	public static string[] SplitBy(this string source, params char[] separators)
	{
		return (source ?? string.Empty).Split(StringDefaults.GetCharSeparators(separators), StringSplitOptions.RemoveEmptyEntries);
	}

	/// <summary>
	/// Split the <paramref name="source"/> into a string array.
	/// </summary>
	/// <param name="separators">
	/// Separators to use for splitting the <paramref name="source"/>.
	/// Will use the <see cref="StringDefaults.Separators"/> if no separators are provided.
	/// </param>
	public static string[] SplitBy(this string source, params string[] separators)
	{
		return (source ?? string.Empty).Split(StringDefaults.GetStringSeparators(separators), StringSplitOptions.RemoveEmptyEntries);
	}

	/// <summary>
	/// Split the <paramref name="source"/> by <see cref="Environment.NewLine"/>.
	/// </summary>
	public static string[] SplitLines(this string source)
	{
		return (source ?? string.Empty).Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
	}

	/// <summary>
	/// Split the <paramref name="source"/> into a <see cref="KeyValuePair{TKey,TValue}"/>.
	/// </summary>
	/// <param name="separators">
	/// Separators to use for splitting the <paramref name="source"/>.
	/// Will use the <see cref="StringDefaults.Separators"/> if no separators are provided.
	/// </param>
	public static KeyValuePair<string, string?> SplitKvp(this string source, params char[] separators)
	{
		var parts = source.Split(StringDefaults.GetCharSeparators(separators), 2, StringSplitOptions.None);
		var key = parts[0];
		var value = parts.Length == 2 ? parts[1] : default;

		return new KeyValuePair<string, string?>(key, value);
	}

	/// <summary>
	/// Split the <paramref name="source"/> into a <see cref="KeyValuePair{TKey,TValue}"/> using <see cref="ConvertExtensions.ChangeType{TTarget}(object?)"/>.
	/// </summary>
	/// <param name="separators">
	/// Separators to use for splitting the <paramref name="source"/>.
	/// Will use the <see cref="StringDefaults.Separators"/> if no separators are provided.
	/// </param>
	public static KeyValuePair<TKey, TValue?> SplitKvp<TKey, TValue>(this string source, params char[] separators)
	{
		var parts = source.Split(StringDefaults.GetCharSeparators(separators), 2, StringSplitOptions.None);
		var key = parts[0].ChangeType<TKey>();
		var value = parts.Length == 2 ? parts[1].ChangeType<TValue>() : default;

		return new KeyValuePair<TKey, TValue?>(key, value);
	}
}