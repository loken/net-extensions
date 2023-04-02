namespace Loken.System;

public static class StringAffixExtensions
{
	/// <summary>
	/// Retrieve the stem of the <paramref name="source"/> without a prefix.
	/// That means everything after the last occurence of any of the <paramref name="separators"/>
	/// or the <see cref="ListSeparators"/> when no <paramref name="separators"/> are provided.
	/// </summary>
	public static string WithoutPrefix(this string source, params char[] separators)
	{
		var separatorIndex = source.LastIndexOfAny(StringDefaults.GetCharSeparators(separators));
		return separatorIndex >= 0
			? source[(separatorIndex + 1)..]
			: source;
	}

	/// <summary>
	/// Retrieve the stem of the <paramref name="source"/> without its first prefix.
	/// That means everything after the first occurence of any of the <paramref name="separators"/>
	/// or the <see cref="ListSeparators"/> when no <paramref name="separators"/> are provided.
	/// </summary>
	public static string WithoutFirstPrefix(this string source, params char[] separators)
	{
		var separatorIndex = source.IndexOfAny(StringDefaults.GetCharSeparators(separators));
		return separatorIndex >= 0
			? source[(separatorIndex + 1)..]
			: source;
	}

	/// <summary>
	/// Retrieve the stem of the <paramref name="source"/> without a suffix.
	/// That means everything before the first occurence of any of the <paramref name="separators"/>
	/// or the <see cref="ListSeparators"/> when no <paramref name="separators"/> are provided.
	/// </summary>
	public static string WithoutSuffix(this string source, params char[] separators)
	{
		var separatorIndex = source.IndexOfAny(StringDefaults.GetCharSeparators(separators));
		return separatorIndex >= 0
			? source[..separatorIndex]
			: source;
	}

	/// <summary>
	/// Retrieve the stem of the <paramref name="source"/> without its last suffix.
	/// That means everything before the last occurence of any of the <paramref name="separators"/>
	/// or the <see cref="ListSeparators"/> when no <paramref name="separators"/> are provided.
	/// </summary>
	public static string WithoutLastSuffix(this string source, params char[] separators)
	{
		var separatorIndex = source.LastIndexOfAny(StringDefaults.GetCharSeparators(separators));
		return separatorIndex >= 0
			? source[..separatorIndex]
			: source;
	}
}
