using System.Diagnostics;

namespace Loken.System.Diagnostics;

/// <summary>
/// <see cref="Stopwatch"/> extensions.
/// </summary>
public static class StopwatchExtensions
{
	/// <summary>
	/// Get the average of total <see cref="Stopwatch.ElapsedMilliseconds"/> over the <paramref name="count"/>.
	/// Useful when you loop over something and want to know how long each iteration takes on average.
	/// </summary>
	public static long AvgElapsedMilliseconds(this Stopwatch stopwatch, int count)
	{
		return count == 0 ? 0 : stopwatch.ElapsedMilliseconds / count;
	}
}
