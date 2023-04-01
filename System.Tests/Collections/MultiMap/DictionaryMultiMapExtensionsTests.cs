namespace Loken.System.Collections.MultiMap;

public class DictionaryMultiMapExtensionsTests
{
	[Fact]
	public void MultiMapSerialization_OfStrings()
	{
		const string input = """
		A:A1,A2
		A1:A11,A12
		B:B1,B12
		""";

		var map = input.ParseMultiMap();

		Assert.Equal(new List<string> { "A1", "A2" }, map["A"].ToList());

		var output = map.RenderMultiMap();

		Assert.Equal(input, output);
	}

	[Fact]
	public void MultiMapSerialization_OfInts()
	{
		const string input = """
		1:11,12
		11:111,112
		2:21,212
		""";

		var map = input.ParseMultiMap<int>();

		Assert.Equal(new List<int> { 11, 12 }, map[1].ToList());

		var output = map.RenderMultiMap();

		Assert.Equal(input, output);
	}

	[Fact]
	public void MultiMapSerialization_WithCustomSeparators()
	{
		const string input = @"A>A1+A2#A1>A11+A12#B>B1+B12";
		var sep = MultiMapSeparators.Create("#", '>', '+');

		var map = input.ParseMultiMap(sep);

		Assert.Equal(new List<string> { "A1", "A2" }, map["A"].ToList());

		var output = map.RenderMultiMap(sep);

		Assert.Equal(input, output);
	}
}