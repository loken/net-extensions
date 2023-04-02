namespace Loken.System;

/// <summary>
/// Defaults for use in various string/text operations.
/// </summary>
public static class StringDefaults
{
	/// <summary>
	/// Separators that will be used by features that need some default separator characters.
	/// You may want to modify this set at boot time if you need other defaults.
	/// </summary>
	public static ISet<char> Separators { get; } = new HashSet<char>() { ':', ';', ',', '|' };

	/// <summary>
	/// Return the <paramref name="separators"/> when there are some or the <see cref="Separators"/> when there aren't.
	/// </summary>
	public static char[] GetCharSeparators(char[] separators)
	{
		return separators is null or { Length: 0 }
			? Separators.ToArray()
			: separators;
	}

	/// <summary>
	/// Return the <paramref name="separators"/> when there are some or the <see cref="Separators"/> when there aren't.
	/// </summary>
	public static string[] GetStringSeparators(string[] separators)
	{
		return separators is null or { Length: 0 }
			? Separators.Cast<string>().ToArray()
			: separators;
	}
}
