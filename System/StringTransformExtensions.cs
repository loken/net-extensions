namespace Loken.System;

/// <summary>
/// Extension methods for transforming a string into another string.
/// </summary>
public static class StringTransformExtensions
{
	/// <summary>
	/// Remove any spaces.
	/// </summary>
	public static string NoSpaces(this string source)
	{
		return source.Replace(" ", "");
	}
}