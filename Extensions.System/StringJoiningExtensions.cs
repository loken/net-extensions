namespace Loken.System;

/// <summary>
/// Extension methods for joining <see cref="IEnumerable{string}"/>s.
/// </summary>
public static class StringJoiningExtensions
{
	/// <summary>
	/// Fluent shorthand for <see cref="string.Join(string, IEnumerable{string})"/>.
	/// </summary>
	public static string Join(this IEnumerable<string> source, string separator)
	{
		return string.Join(separator, source);
	}

	/// <summary>
	/// Fluent shorthand for <see cref="string.Join(char, IEnumerable{string})"/>.
	/// </summary>
	public static string Join(this IEnumerable<string> source, char separator)
	{
		return string.Join(separator, source);
	}
}