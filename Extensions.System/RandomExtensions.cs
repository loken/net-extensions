namespace Loken.System;

/// <summary>
/// Extension methods for <see cref="Random"/>.
/// </summary>
public static class RandomExtensions
{
	/// <summary>
	/// Get a random number from the gaussian (or normal) distribution using the Box-Muller transform.
	/// <para>
	/// The result is 68.2% likely to be within ±1 <paramref name="stdDev"/> of the <paramref name="mean"/>
	/// and 95.4% likely to be within within ±2 <paramref name="stdDev"/> of the <paramref name="mean"/>.
	/// </para>
	/// </summary>
	/// <param name="random">The <see cref="Random"/> instance to use for pseudo random numbers.</param>
	/// <param name="mean">The mean.</param>
	/// <param name="stdDev">The standard deviation.</param>
	/// <returns>A random floating-point number.</returns>
	public static double NextGaussian(this Random random, double mean = 0, double stdDev = 1)
	{
		// Random numbers in the interval: (0,1]
		double u1 = 1.0 - random.NextDouble();
		double u2 = 1.0 - random.NextDouble();

		// Generate a normal sample with a mean of 0 and standard deviation of 1.
		double sample = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);

		// Translate and scale the sample to the wanted mean and standard deviation.
		double scaled = mean + stdDev * sample;

		return scaled;
	}
}
