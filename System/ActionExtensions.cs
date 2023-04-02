namespace Loken.System;

/// <summary>
/// <see cref="Action"/> extensions.
/// </summary>
public static class ActionExtensions
{
	/// <summary>
	/// Do the <paramref name="action"/> on the <paramref name="source"/> as long as it is non-null
	/// and return the <paramref name="source"/> to allow method chaining.
	/// </summary>
	public static T Do<T>(this T source, Action<T>? action)
	{
		action?.Invoke(source);

		return source;
	}
}