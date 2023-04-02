namespace Loken.System;

public static class BooleanExtensions
{
	public static bool IsTrueOrNull(this bool? source)
	{
		return !source.HasValue || source.Value;
	}

	public static bool IsFalseOrNull(this bool? source)
	{
		return !source.HasValue || !source.Value;
	}
}
