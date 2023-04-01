using Loken.System.Collections;

namespace Loken.System.Collections;

public class DictionaryInversionExtensionsTests
{
	[Fact]
	public void Invert()
	{
		var values = new[] { "one", "two", "three" };

		Dictionary<int, string> dict = new();

		for (var i = 0; i < values.Length; i++)
			dict.Add(i + 1, values[i]);

		var inverted = dict.Invert();

		Assert.Equal(values, inverted.Keys.ToArray());
		for (var i = 0; i < values.Length; i++)
			Assert.Equal(i + 1, inverted[values[i]]);
	}

	[Fact]
	public void Invert_WithDuplicates_Throws()
	{
		var values = new[] { "one", "two", "two" };

		Dictionary<int, string> dict = new();

		for (var i = 0; i < values.Length; i++)
			dict.Add(i + 1, values[i]);

		_ = Assert.Throws<ArgumentException>(dict.Invert);
	}

	[Fact]
	public void InvertMany()
	{
		Dictionary<int, string> dict = new()
		{
			{ 1, "some" },
			{ 2, "other" },
			{ 3, "other" }
		};

		var inverse = dict.InvertMany();

		Assert.Equal(new[] { 1 }, inverse["some"].ToArray());
		Assert.Equal(new[] { 2, 3 }, inverse["other"].ToArray());
	}


	[Fact]
	public void InvertSet()
	{
		Dictionary<int, string> dict = new()
		{
			{ 1, "some" },
			{ 2, "other" },
			{ 3, "other" }
		};

		var inverse = dict.InvertSet();

		Assert.Equal(new[] { 1 }, inverse["some"].ToArray());
		Assert.Equal(new[] { 2, 3 }, inverse["other"].ToArray());
	}
}