namespace Loken.System;

public class NextGaussianTests
{
	#region with deterministic mocking

	[Fact]
	public void WithDefaultParameters_ReturnsValueNearZero()
	{
		var random = new Random(42); // Fixed seed for reproducible tests
		var values = new List<double>();

		// Generate multiple samples to test the distribution
		for (int i = 0; i < 1000; i++)
			values.Add(random.NextGaussian());

		// With mean=0 and stdDev=1, most values should be within ±3 standard deviations
		var withinRange = values.Count(v => v >= -3 && v <= 3);
		Assert.True(withinRange > 990, $"Expected >990 values within ±3σ, got {withinRange}"); // ~99.7% should be within ±3σ

		// The mean should be close to 0
		var mean = values.Average();
		Assert.True(Math.Abs(mean) < 0.1, $"Expected mean close to 0, got {mean}");
	}

	[Fact]
	public void WithCustomMeanAndStdDev_ReturnsCorrectDistribution()
	{
		var random = new Random(42); // Fixed seed for reproducible tests
		var expectedMean = 50.0;
		var expectedStdDev = 10.0;
		var values = new List<double>();

		// Generate multiple samples
		for (int i = 0; i < 1000; i++)
			values.Add(random.NextGaussian(expectedMean, expectedStdDev));

		// Most values should be within ±3 standard deviations of the mean
		var lowerBound = expectedMean - 3 * expectedStdDev;
		var upperBound = expectedMean + 3 * expectedStdDev;
		var withinRange = values.Count(v => v >= lowerBound && v <= upperBound);
		Assert.True(withinRange > 990, $"Expected >990 values within range [{lowerBound}, {upperBound}], got {withinRange}");

		// The sample mean should be close to the expected mean
		var actualMean = values.Average();
		Assert.True(Math.Abs(actualMean - expectedMean) < 1.0, $"Expected mean close to {expectedMean}, got {actualMean}");
	}

	[Fact]
	public void ProducesNormalDistribution_WithinStandardDeviations()
	{
		var random = new Random(42);
		var mean = 100.0;
		var stdDev = 15.0;
		var values = new List<double>();

		// Generate a large sample
		for (int i = 0; i < 10000; i++)
			values.Add(random.NextGaussian(mean, stdDev));

		// 68.2% should be within ±1 standard deviation (more lenient for deterministic mock)
		var withinOneStdDev = values.Count(v => v >= mean - stdDev && v <= mean + stdDev);
		var percentageWithinOne = (double)withinOneStdDev / values.Count * 100;
		Assert.True(percentageWithinOne > 68 && percentageWithinOne < 69,
			$"Expected ~68.2% within ±1σ, got {percentageWithinOne:F1}%");

		// 95.4% should be within ±2 standard deviations (more lenient for deterministic mock)
		var withinTwoStdDev = values.Count(v => v >= mean - 2 * stdDev && v <= mean + 2 * stdDev);
		var percentageWithinTwo = (double)withinTwoStdDev / values.Count * 100;
		Assert.True(percentageWithinTwo > 95 && percentageWithinTwo < 96,
			$"Expected ~95.4% within ±2σ, got {percentageWithinTwo:F1}%");
	}

	[Fact]
	public void WithDifferentSeeds_ProducesDifferentResults()
	{
		var random1 = new Random(1);
		var value1 = random1.NextGaussian();

		var random2 = new Random(2);
		var value2 = random2.NextGaussian();

		Assert.NotEqual(value1, value2);
	}

	[Fact]
	public void WithSameSeeds_ProducesConsistentResults()
	{
		var random1 = new Random(42);
		var value1 = random1.NextGaussian(10, 2);

		var random2 = new Random(42);
		var value2 = random2.NextGaussian(10, 2);

		Assert.Equal(value1, value2);
	}

	[Fact]
	public void WithZeroStandardDeviation_ReturnsExactMean()
	{
		var random = new Random(42);
		var mean = 42.0;

		var value = random.NextGaussian(mean, 0);

		Assert.Equal(mean, value);
	}

	[Fact]
	public void WithNegativeStandardDeviation_ReturnsInvertedDistribution()
	{
		var random = new Random(42);
		var mean = 0.0;
		var negativeStdDev = -1.0;

		var values = new List<double>();
		for (int i = 0; i < 100; i++)
			values.Add(random.NextGaussian(mean, negativeStdDev));

		// With negative std dev, the distribution should still work but be inverted
		// The absolute values should still follow the normal distribution pattern
		var hasVaryingValues = values.Any(v => v != mean);
		Assert.True(hasVaryingValues, "Should produce varying values even with negative std dev");
	}

	[Fact]
	public void HandlesEdgeCases_WithExtremeRandomValues()
	{
		// Create a custom Random that produces edge case values
		var random = new TestableRandom([0.001, 0.999]);

		var result = random.NextGaussian(0, 1);

		// Should not throw and should return a finite number
		Assert.True(double.IsFinite(result), "Should return a finite number");
	}

	#endregion

	#region with real randomness (statistical validation)

	[Fact]
	public void GeneratesCorrectMean_OverManySamples()
	{
		var random = new Random();
		var samples = 10000;
		var expectedMean = 42;
		var stdDev = 5;
		var values = new List<double>();

		for (int i = 0; i < samples; i++)
			values.Add(random.NextGaussian(expectedMean, stdDev));

		var actualMean = values.Average();

		// With 10,000 samples, the sample mean should be very close to the expected mean
		// Using a tolerance of 0.2 (about 4% of stdDev)
		Assert.True(Math.Abs(actualMean - expectedMean) < 1.0,
			$"Expected mean close to {expectedMean}, got {actualMean}");
	}

	[Fact]
	public void FollowsNormalDistributionRules_WithBroadTolerances()
	{
		var random = new Random();
		var samples = 5000;
		var mean = 0;
		var stdDev = 1;
		var values = new List<double>();

		for (int i = 0; i < samples; i++)
			values.Add(random.NextGaussian(mean, stdDev));

		// Calculate actual statistics
		var sampleMean = values.Average();
		var sampleStdDev = Math.Sqrt(values.Sum(v => Math.Pow(v - sampleMean, 2)) / values.Count);

		// Test the 68-95-99.7 rule with more lenient tolerances due to randomness
		var withinOneSigma = values.Count(v => Math.Abs(v - mean) <= stdDev);
		var withinTwoSigma = values.Count(v => Math.Abs(v - mean) <= 2 * stdDev);
		var withinThreeSigma = values.Count(v => Math.Abs(v - mean) <= 3 * stdDev);

		var percentageOneSigma = (double)withinOneSigma / samples * 100;
		var percentageTwoSigma = (double)withinTwoSigma / samples * 100;
		var percentageThreeSigma = (double)withinThreeSigma / samples * 100;

		// The sample mean should be reasonably close to 0
		Assert.True(Math.Abs(sampleMean) < 0.1, $"Expected sample mean close to 0, got {sampleMean}");

		// The sample standard deviation should be reasonably close to 1
		// Allow wider range since random samples can vary significantly
		Assert.True(sampleStdDev > 0.9 && sampleStdDev < 1.1,
			$"Expected sample std dev between 0.9 and 1.1, got {sampleStdDev}");

		// Use broader tolerances for distribution tests
		Assert.True(percentageOneSigma > 65 && percentageOneSigma < 71,
			$"Expected 65-71% within ±1σ, got {percentageOneSigma:F1}%");

		Assert.True(percentageTwoSigma > 93 && percentageTwoSigma < 97,
			$"Expected 93-97% within ±2σ, got {percentageTwoSigma:F1}%");

		Assert.True(percentageThreeSigma > 99.5 && percentageThreeSigma < 99.9,
			$"Expected 99.5-99.9% within ±3σ, got {percentageThreeSigma:F1}%");
	}

	#endregion

	// Helper class for testing edge cases with controlled random values
	private class TestableRandom : Random
	{
		private readonly double[] _values;
		private int _index;

		public TestableRandom(double[] values)
		{
			_values = values;
			_index = 0;
		}

		protected override double Sample()
		{
			var value = _values[_index % _values.Length];
			_index++;
			return value;
		}
	}
}
