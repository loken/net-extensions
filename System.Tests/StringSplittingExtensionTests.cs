namespace Loken.System;

public class StringSplittingExtensionTests
{
	[Fact]
	public void SplitByDefault()
	{
		Assert.Equal(new[] { "A", "B", "C" }, "A,B,C".SplitByDefault());
	}

	[Fact]
	public void SplitBy()
	{
		Assert.Equal(new[] { "A", "B", "C" }, "A-B-C".SplitBy('-'));
	}

	[Fact]
	public void SplitLines()
	{
		var expected = new[] { "First", "Second", "Third" };
		var input = """
		First
		Second
		Third
		""";

		var actual = input.SplitLines();

		Assert.Equal(expected.ToList(), actual.ToList());
	}

	[Fact]
	public void SplitKvp_OfStrings()
	{
		var actual = @"A:B".SplitKvp();

		KeyValuePair<string, string?> expected = new("A", "B");
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void SplitKvp_WithExtraSegments()
	{
		var actual = @"A:B:Extra".SplitKvp();

		KeyValuePair<string, string?> expected = new("A", "B:Extra");
		Assert.Equal(expected, actual);
	}
}