namespace Loken.System.Collections.MultiMap;

public class MultiMapSerializationTests
{
	[Fact]
	public void Serialization_OfStrings()
	{
		const string input = """
		A:A1,A2
		A1:A11,A12
		B:B1,B12
		""";

		var map = MultiMap.Parse<string>(input);

		Assert.Equal(["A1", "A2"], map["A"].ToList());

		var output = MultiMap.Render(map);

		Assert.Equal(input, output);
	}

	[Fact]
	public void Serialization_OfInts()
	{
		const string input = """
		1:11,12
		11:111,112
		2:21,212
		""";

		var map = MultiMap.Parse<int>(input);

		Assert.Equal([11, 12], map[1].ToList());

		var output = MultiMap.Render(map);

		Assert.Equal(input, output);
	}

	[Fact]
	public void Serialization_WithCustomSeparators()
	{
		const string input = @"A>A1+A2#A1>A11+A12#B>B1+B12";
		var sep = MultiMapSeparators.Create("#", '>', '+');

		var map = MultiMap.Parse<string>(input, sep);

		Assert.Equal(["A1", "A2"], map["A"].ToList());

		var output = MultiMap.Render(map, sep);

		Assert.Equal(input, output);
	}

	[Fact]
	public void Serialization_PreservesKeysWithoutValues()
	{
		const string input = """
		KeyWithValues:A,B
		KeyWithoutValues
		""";

		var map = MultiMap.Parse<string>(input);

		Assert.Equal(["A", "B"], map["KeyWithValues"].ToList());
		Assert.True(map.ContainsKey("KeyWithoutValues"));
		Assert.Empty(map["KeyWithoutValues"]);

		var output = MultiMap.Render(map);

		Assert.Equal(input, output);
	}
}