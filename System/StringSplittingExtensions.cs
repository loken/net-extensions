namespace Loken.System;

/// <summary>
/// Extension methods for splitting <see cref="string"/>s in various ways.
/// </summary>
/// <remarks>Each of these methods will split using <see cref="StringSplitOptions.RemoveEmptyEntries"/>.</remarks>
public static class StringSplittingExtensions
{
	public static char[] DefaultSeparators = { ':', ';', ',', '|' };

	/// <summary>
	/// Split the <paramref name="source"/> by the <see cref="DefaultSeparators"/>.
	/// </summary>
	public static string[] SplitDefault(this string source)
	{
		return source.SplitBy(DefaultSeparators);
	}

	/// <summary>
	/// Split the <paramref name="source"/> by the provided <paramref name="separators"/>.
	/// </summary>
	public static string[] SplitBy(this string source, params char[] separators)
	{
		return (source ?? string.Empty).Split(separators, StringSplitOptions.RemoveEmptyEntries);
	}

	/// <summary>
	/// Split the <paramref name="source"/> by the provided <paramref name="separators"/>.
	/// </summary>
	public static string[] SplitBy(this string source, params string[] separators)
	{
		return (source ?? string.Empty).Split(separators, StringSplitOptions.RemoveEmptyEntries);
	}

	/// <summary>
	/// Split the <paramref name="source"/> by <see cref="Environment.NewLine"/>.
	/// </summary>
	public static string[] SplitLines(this string source)
	{
		return (source ?? string.Empty).Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
	}

	/// <summary>
	/// Split the <paramref name="source"/> into <see cref="KeyValuePair{TKey,TValue}"/>s of <see cref="string"/>s using the <see cref="DefaultSeparators"/>.
	/// </summary>
	public static KeyValuePair<string, string?> SplitKvp(this string source)
	{
		return source.SplitKvp(DefaultSeparators);
	}

	/// <summary>
	/// Split the <paramref name="source"/> into a <see cref="KeyValuePair{TKey,TValue}"/> using the provided <paramref name="separators"/>.
	/// </summary>
	public static KeyValuePair<string, string?> SplitKvp(this string source, params char[] separators)
	{
		var parts = source.Split(separators, 2, StringSplitOptions.None);
		var key = parts[0];
		var value = parts.Length == 2 ? parts[1] : null;

		return new KeyValuePair<string, string?>(key, value);
	}

	/// <summary>
	/// Split the <paramref name="source"/> into a <see cref="KeyValuePair{TKey,TValue}"/> using the provided <paramref name="separators"/> and <see cref="ReflectionExtensions.ConvertTo{TSource,TTarget}"/>.
	/// </summary>
	public static KeyValuePair<TKey, TValue?> SplitKvp<TKey, TValue>(this string source, params char[] separators)
	{
		var parts = source.Split(separators, 2, StringSplitOptions.None);
		var key = parts[0].ConvertTo<TKey>();
		var value = parts.Length == 2 ? parts[1].ConvertTo<TValue>() : default;

		return new KeyValuePair<TKey, TValue?>(key, value);
	}
}