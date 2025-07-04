namespace Loken.System.Collections;

public class EnumerableExtensionsTests
{
	[Fact]
	public void SingleOrDefaultMany_Empty_ReturnsDefault()
	{
		var enumerable = Enumerable.Empty<string>();
		var array = Array.Empty<string>();

		Assert.Null(enumerable.SingleOrDefaultMany());
		Assert.Null(array.SingleOrDefaultMany());

		Assert.Equal("def", enumerable.SingleOrDefaultMany("def"));
		Assert.Equal("def", array.SingleOrDefaultMany("def"));
	}

	[Fact]
	public void SingleOrDefaultMany_Multiple_ReturnsDefault()
	{
		var enumerable = Enumerable.Range(1, 3).Select(n => $"Item {n}");
		var array = enumerable.ToArray();

		Assert.Null(enumerable.SingleOrDefaultMany());
		Assert.Null(array.SingleOrDefaultMany());

		Assert.Equal("def", enumerable.SingleOrDefaultMany("def"));
		Assert.Equal("def", array.SingleOrDefaultMany("def"));
	}

	[Fact]
	public void SingleOrDefaultMany_Single_ReturnsItem()
	{
		var enumerable = Enumerable.Range(1, 1).Select(n => $"Item {n}");
		var array = enumerable.ToArray();

		Assert.Equal("Item 1", enumerable.SingleOrDefaultMany());
		Assert.Equal("Item 1", array.SingleOrDefaultMany());

		Assert.Equal("Item 1", enumerable.SingleOrDefaultMany("def"));
		Assert.Equal("Item 1", array.SingleOrDefaultMany("def"));
	}

	[Fact]
	public void EnumerateAll_TriggersSideEffects()
	{
		var items = new HashSet<int>();

		var enumerable = Multiple(5);
		Assert.Empty(items);

		enumerable.EnumerateAll();
		Assert.Equal(new[] { 2, 4 }, items);

		IEnumerable<int> Multiple(int count)
		{
			foreach (var i in Enumerable.Range(1, count))
			{
				if (i % 2 == 0)
					items!.Add(i);
				yield return i;
			}
		}
	}
}
