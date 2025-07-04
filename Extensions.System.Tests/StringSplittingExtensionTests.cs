namespace Loken.System;

public class StringSplittingExtensionTests
{
	[Fact]
	public void SplitByDefault()
	{
		Assert.Equal([ "A", "B", "C" ], "A,B,C".SplitByDefault());
	}

	[Fact]
	public void SplitBy()
	{
		Assert.Equal([ "A", "B", "C" ], "A-B-C".SplitBy('-'));
	}

	[Fact]
	public void SplitBy_WithMultipleSeparators()
	{
		Assert.Equal([ "A", "B", "C" ], "A-B&C".SplitBy('-', '&'));
	}

	[Fact]
	public void SplitBy_WithStringSeparators()
	{
		Assert.Equal([ "A", "B", "C" ], "A::B<something>C".SplitBy("::", "<something>"));
	}

	[Fact]
	public void SplitBy_RemovesEmptyEntriesByDefault()
	{
		Assert.Equal([ "A", "B", "C" ], "A,,B,C".SplitBy(','));
	}

	[Fact]
	public void SplitBy_HandlesRegexSpecialCharacters()
	{
		Assert.Equal([ "A", "B", "C" ], "A^B$C".SplitBy('^', '$'));
	}

	[Fact]
	public void SplitByDefault_UsesDefaultSeparators()
	{
		Assert.Equal(new[] { "A", "B", "C", "D" }, "A:B;C,D".SplitByDefault());
	}

	[Fact]
	public void SplitByDefault_WithPipeCharacter()
	{
		Assert.Equal([ "A", "B", "C" ], "A|B|C".SplitByDefault());
	}

	[Theory]
	[InlineData("A,B,C", ',', new[]{ "A", "B", "C" })]
	[InlineData("A.B.C", '.', new[]{ "A", "B", "C" })]
	[InlineData("A;B;C", ';', new[]{ "A", "B", "C" })]
	[InlineData("A:B:C", ':', new[]{ "A", "B", "C" })]
	[InlineData("A|B|C", '|', new[]{ "A", "B", "C" })]
	public void SplitBy_WorksWithAllDefaultSeparators(string input, char separator, string[] expected)
	{
		Assert.Equal(expected, input.SplitBy(separator));
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

	[Fact]
	public void SplitKvp_WithoutSeparator()
	{
		var actual = "Key".SplitKvp();

		KeyValuePair<string, string?> expected = new("Key", null);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void SplitKvp_WithEmptyString()
	{
		var actual = "".SplitKvp();

		KeyValuePair<string, string?> expected = new("", null);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void SplitKvp_WithEmptyValuePart()
	{
		var actual = "Key:".SplitKvp();

		KeyValuePair<string, string?> expected = new("Key", null);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void SplitKvp_Generic_WithStringToInt()
	{
		var actual = "Age:25".SplitKvp<string, int>();

		var expected = new KeyValuePair<string, int>("Age", 25);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void SplitKvp_Generic_WithIntToString()
	{
		var actual = "1:First".SplitKvp<int, string>();

		var expected = new KeyValuePair<int, string?>(1, "First");
		Assert.Equal(expected, actual);
	}
}