namespace Loken.Utilities.Collections;

public class MultiMapSerializationTests
{
	[Fact]
	public void Render_MultiMapString_ToString()
	{
		const string source = """
		A:A1,A2
		A1:A11,A12
		B:B1,B2
		""";

		var map = new MultiMap<string>();
		map.Add("A", "A1", "A2");
		map.Add("A1", "A11", "A12");
		map.Add("B", "B1", "B2");

		var rendered = MultiMap.Render(map);

		Assert.Equal(source, rendered);
	}

	[Fact]
	public void Parse_String_IntoMultiMapString()
	{
		const string source = """
		A:A1,A2
		A1:A11,A12
		B:B1,B2
		""";

		var expectedMap = new MultiMap<string>();
		expectedMap.Add("A", "A1", "A2");
		expectedMap.Add("A1", "A11", "A12");
		expectedMap.Add("B", "B1", "B2");

		var parsed = MultiMap.Parse<string>(source);

		Assert.Equal(expectedMap.Count, parsed.Count);
		Assert.Equal(expectedMap["A"], parsed["A"]);
		Assert.Equal(expectedMap["A1"], parsed["A1"]);
		Assert.Equal(expectedMap["B"], parsed["B"]);
	}

	[Fact]
	public void ParseAndRender_MultiMapString_UsingCustomSeparators()
	{
		const string input = "A>A1+A2#A1>A11+A12#B>B1+B2";

		var sep = MultiMapSeparators.Create("#", '>', '+');

		var parsed = MultiMap.Parse<string>(input, sep);
		var rendered = MultiMap.Render(parsed, sep);

		Assert.Equal(input, rendered);
	}

	[Fact]
	public void Preserve_KeysWithoutValues_DuringSerialization()
	{
		const string input = """
		KeyWithValues:A,B
		KeyWithoutValues
		""";

		var parsed = MultiMap.Parse<string>(input);
		var expectedKeyWithValues = new HashSet<string> { "A", "B" };
		Assert.Equal(expectedKeyWithValues, parsed["KeyWithValues"]);
		Assert.True(parsed.ContainsKey("KeyWithoutValues"));
		Assert.Empty(parsed["KeyWithoutValues"]);

		var output = parsed.Render();
		Assert.Equal(input, output);
	}

	[Fact]
	public void Handle_EmptyInput()
	{
		var parsed = MultiMap.Parse<string>("");
		Assert.Empty(parsed);

		var rendered = MultiMap.Render(parsed);
		Assert.Equal("", rendered);
	}

	[Fact]
	public void Render_MultiMapNumber_ToString()
	{
		const string source = """
		1:11,12
		11:111,112
		2:21,22
		""";

		var map = new MultiMap<int>();
		map.Add(1, 11, 12);
		map.Add(11, 111, 112);
		map.Add(2, 21, 22);

		var rendered = MultiMap.Render(map);

		Assert.Equal(source, rendered);
	}

	[Fact]
	public void Parse_String_IntoMultiMapNumber()
	{
		const string source = """
		1:11,12
		11:111,112
		2:21,22
		""";

		var expectedMap = new MultiMap<int>();
		expectedMap.Add(1, 11, 12);
		expectedMap.Add(11, 111, 112);
		expectedMap.Add(2, 21, 22);

		var parsed = MultiMap.Parse<int>(source);

		Assert.Equal(expectedMap.Count, parsed.Count);
		Assert.Equal(expectedMap[1], parsed[1]);
		Assert.Equal(expectedMap[11], parsed[11]);
		Assert.Equal(expectedMap[2], parsed[2]);
	}

	[Fact]
	public void Handle_NumberSerialization_WithCustomSeparators()
	{
		const string input = "1>11+12#11>111+112#2>21+22";

		var sep = MultiMapSeparators.Create("#", '>', '+');

		var parsed = MultiMap.Parse<int>(input, sep);
		var rendered = MultiMap.Render(parsed, sep);

		Assert.Equal(input, rendered);
	}

	[Fact]
	public void Handle_KeysWithoutValues_ForNumbers()
	{
		const string input = """
		1:11,12
		2
		3:31
		""";

		var parsed = MultiMap.Parse<int>(input);
		var expected1 = new HashSet<int> { 11, 12 };
		Assert.Equal(expected1, parsed[1]);
		Assert.True(parsed.ContainsKey(2));
		Assert.Empty(parsed[2]);
		var expected3 = new HashSet<int> { 31 };
		Assert.Equal(expected3, parsed[3]);

		var output = MultiMap.Render(parsed);
		Assert.Equal(input, output);
	}

	[Fact]
	public void Handle_ComplexCustomSeparators()
	{
		const string input = "A>A1&A2|A1>A11&A12|B>B1&B2";
		var customSep = MultiMapSeparators.Create("|", '>', '&');

		var parsed = MultiMap.Parse<string>(input, customSep);
		var expectedA = new HashSet<string> { "A1", "A2" };
		Assert.Equal(expectedA, parsed["A"]);
		var expectedB = new HashSet<string> { "B1", "B2" };
		Assert.Equal(expectedB, parsed["B"]);

		var rendered = MultiMap.Render(parsed, customSep);
		Assert.Equal(input, rendered);
	}
}