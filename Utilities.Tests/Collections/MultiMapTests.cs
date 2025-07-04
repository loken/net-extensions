namespace Loken.Utilities.Collections;

public class MultiMapTests
{
	[Fact]
	public void Constructor_CreatesEmptyMultiMap()
	{
		var map = new MultiMap<string>();

		Assert.Empty(map);
	}

	[Fact]
	public void GetAll_ReturnsAllKeysAndValues()
	{
		var map = new MultiMap<string>();
		map.Add("A", "A1", "A2");
		map.Add("B", "B1");
		map.Add("C"); // Empty set

		var all = map.GetAll();
		var expected = new HashSet<string> { "A", "A1", "A2", "B", "B1", "C" };
		Assert.Equal(expected, all);
		Assert.Equal(6, all.Count);
	}

	[Fact]
	public void Add_CreatesEmptySetForKey()
	{
		var map = new MultiMap<string>();

		var added = map.Add("emptyKey");
		Assert.Equal(0, added);
		Assert.True(map.ContainsKey("emptyKey"));
		Assert.Empty(map["emptyKey"]);
	}

	[Fact]
	public void Add_WithSingleValue()
	{
		var map = new MultiMap<string>();

		var added = map.Add("key1", "value1");
		Assert.Equal(1, added);
		Assert.True(map["key1"].Contains("value1"));
		Assert.Single(map["key1"]);
	}

	[Fact]
	public void Add_WithMultipleValues()
	{
		var map = new MultiMap<string>();

		var added = map.Add("key1", "value1", "value2", "value3");
		Assert.Equal(3, added);
		var expected = new HashSet<string> { "value1", "value2", "value3" };
		Assert.Equal(expected, map["key1"]);
	}

	[Fact]
	public void Add_WithDuplicateValues()
	{
		var map = new MultiMap<string>();

		var added1 = map.Add("key1", "value1", "value2");
		var added2 = map.Add("key1", "value2", "value3");

		Assert.Equal(2, added1);
		Assert.Equal(1, added2); // Only value3 was new
		var expected = new HashSet<string> { "value1", "value2", "value3" };
		Assert.Equal(expected, map["key1"]);
		Assert.Equal(3, map["key1"].Count);
	}

	[Fact]
	public void Remove_ValuesFromExistingKey()
	{
		var map = new MultiMap<string>();
		map.Add("key1", "value1", "value2", "value3");

		var removed = map.Remove("key1", "value1", "value3");

		var expectedRemoved = new HashSet<string> { "value1", "value3" };
		Assert.Equal(expectedRemoved, removed);
		Assert.Equal(2, removed.Count);
		var expectedRemaining = new HashSet<string> { "value2" };
		Assert.Equal(expectedRemaining, map["key1"]);
		Assert.True(map.ContainsKey("key1")); // Key still exists
	}

	[Fact]
	public void Remove_AllValuesRemovesKey()
	{
		var map = new MultiMap<string>();
		map.Add("key1", "value1", "value2");

		var removed = map.Remove("key1", "value1", "value2");

		var expectedRemoved = new HashSet<string> { "value1", "value2", "key1" }; // Include key when removed
		Assert.Equal(expectedRemoved, removed);
		Assert.Equal(3, removed.Count);
		Assert.False(map.ContainsKey("key1")); // Key was removed
	}

	[Fact]
	public void Remove_FromNonExistentKey_ReturnsEmptySet()
	{
		var map = new MultiMap<string>();

		var removed = map.Remove("nonExistentKey", "value1");

		Assert.Empty(removed);
		Assert.False(map.ContainsKey("nonExistentKey"));
	}

	[Fact]
	public void Remove_NonExistentValues_ReturnsEmptySet()
	{
		var map = new MultiMap<string>();
		map.Add("key1", "value1");

		var removed = map.Remove("key1", "nonExistentValue");

		Assert.Empty(removed);
		var expectedRemaining = new HashSet<string> { "value1" };
		Assert.Equal(expectedRemaining, map["key1"]); // Original value still there
	}

	[Fact]
	public void Remove_PartialMatch()
	{
		var map = new MultiMap<string>();
		map.Add("key1", "value1", "value2");

		var removed = map.Remove("key1", "value1", "nonExistentValue");

		var expectedRemoved = new HashSet<string> { "value1" };
		Assert.Equal(expectedRemoved, removed);
		Assert.Single(removed);
		var expectedRemaining = new HashSet<string> { "value2" };
		Assert.Equal(expectedRemaining, map["key1"]);
	}

	[Fact]
	public void ComplexOperations()
	{
		var map = new MultiMap<int>();

		// Build a tree structure: 1 -> [11, 12], 11 -> [111, 112], 2 -> [21]
		map.Add(1, 11, 12);
		map.Add(11, 111, 112);
		map.Add(2, 21);

		Assert.Equal(3, map.Count);
		var expectedAll = new HashSet<int> { 1, 11, 12, 111, 112, 2, 21 };
		Assert.Equal(expectedAll, map.GetAll());
		Assert.Equal(7, map.GetAll().Count);

		// Remove some values
		var removed1 = map.Remove(1, 12);
		var expectedRemoved1 = new HashSet<int> { 12 };
		Assert.Equal(expectedRemoved1, removed1);
		var expectedRemaining1 = new HashSet<int> { 11 };
		Assert.Equal(expectedRemaining1, map[1]);

		// Remove all values from a key
		var removed2 = map.Remove(11, 111, 112);
		var expectedRemoved2 = new HashSet<int> { 111, 112, 11 }; // Include key
		Assert.Equal(expectedRemoved2, removed2);
		Assert.False(map.ContainsKey(11));

		// Final state
		Assert.Equal(2, map.Count);
		var expectedFinal1 = new HashSet<int> { 11 };
		Assert.Equal(expectedFinal1, map[1]);
		var expectedFinal2 = new HashSet<int> { 21 };
		Assert.Equal(expectedFinal2, map[2]);
	}

	[Fact]
	public void WorksWithDifferentTypes()
	{
		var stringMap = new MultiMap<string>();
		stringMap.Add("str", "a", "b");
		var expectedStringValues = new HashSet<string> { "a", "b" };
		Assert.Equal(expectedStringValues, stringMap["str"]);

		var numberMap = new MultiMap<int>();
		numberMap.Add(1, 2, 3);
		var expectedNumberValues = new HashSet<int> { 2, 3 };
		Assert.Equal(expectedNumberValues, numberMap[1]);
	}
}
