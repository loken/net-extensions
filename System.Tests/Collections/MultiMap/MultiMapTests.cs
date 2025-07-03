namespace Loken.System.Collections.MultiMap;

public class MultiMapTests
{
	[Fact]
	public void AddEmpty()
	{
		var map = new MultiMap<string>();

		// Test Add with no values creates empty set
		var valuesAdded = map.Add("Key1");
		Assert.Equal(0, valuesAdded);
		Assert.True(map.ContainsKey("Key1"));
		Assert.Empty(map["Key1"]);
	}

	[Fact]
	public void AddValues()
	{
		var map = new MultiMap<string>();

		// Test Add with params array
		var addedCount = map.Add("Key1", "Value1", "Value2");
		Assert.Equal(2, addedCount);
		Assert.Equal(new List<string> { "Value1", "Value2" }, map["Key1"].ToList());

		// Test Add with IEnumerable
		var moreAdded = map.Add("Key1", new[] { "Value3", "Value1" }); // Value1 is duplicate
		Assert.Equal(1, moreAdded); // Only Value3 was added
		Assert.Equal(new List<string> { "Value1", "Value2", "Value3" }, map["Key1"].OrderBy(x => x).ToList());
	}

	[Fact]
	public void RemoveValues()
	{
		var map = new MultiMap<string>();
		map.Add("Key1", "Value1", "Value2", "Value3");

		// Test removing specific values with params
		var removed1 = map.Remove("Key1", "Value1", "Value3");
		Assert.Equal(new HashSet<string> { "Value1", "Value3" }, removed1);
		Assert.Equal(new List<string> { "Value2" }, map["Key1"].ToList());

		// Test removing with IEnumerable
		var removed2 = map.Remove("Key1", new[] { "Value2" });
		Assert.Equal(new HashSet<string> { "Value2", "Key1" }, removed2); // Include key when it's removed
		Assert.False(map.ContainsKey("Key1")); // Key should be removed when set becomes empty
	}

	[Fact]
	public void GetAll()
	{
		var map = new MultiMap<string>();
		map.Add("Key1", "Value1", "Value2");
		map.Add("Key2", "Value3", "Value1"); // Value1 appears twice

		var all = map.GetAll();
		Assert.Equal(new List<string> { "Key1", "Key2", "Value1", "Value2", "Value3" }, all.OrderBy(x => x).ToList());
	}

	[Fact]
	public void ParseAndRender()
	{
		const string input = """
		A:A1,A2
		A1:A11,A12
		B:B1,B2
		""";

		var map = MultiMap.Parse<string>(input);
		Assert.Equal(new List<string> { "A1", "A2" }, map["A"].ToList());

		var output = MultiMap.Render(map);
		Assert.Equal(input, output);
	}

	[Fact]
	public void ParseInto()
	{
		var map = new MultiMap<string>();
		map.Add("Existing", "Value");

		const string input = """
		A:A1,A2
		B:B1,B2
		""";

		var result = map.Parse(input);
		Assert.Same(map, result); // Should return same instance
		Assert.True(map.ContainsKey("Existing")); // Should preserve existing data
		Assert.Equal(new List<string> { "A1", "A2" }, map["A"].ToList());
	}

	[Fact]
	public void PreservesKeysWithoutValues()
	{
		const string input = """
		KeyWithValues:A,B
		KeyWithoutValues
		""";

		var map = MultiMap.Parse<string>(input);

		Assert.Equal(new List<string> { "A", "B" }, map["KeyWithValues"].ToList());
		Assert.True(map.ContainsKey("KeyWithoutValues"));
		Assert.Empty(map["KeyWithoutValues"]);

		var output = MultiMap.Render(map);
		Assert.Equal(input, output);
	}
}
