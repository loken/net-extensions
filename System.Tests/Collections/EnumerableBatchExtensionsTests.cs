using System.ComponentModel;

namespace Loken.System.Collections;
public class EnumerableBatchExtensionsTests
{
	[Fact]
	public void Batch_OfZeroItems()
	{
		var expected = Array.Empty<int[]>();
		var actual = Enumerable.Empty<int>().BatchArrays(10);

		AssertAreBatchesEqual(expected, actual);
	}

	[Fact]
	public void Batch_OfOneItem()
	{
		var expected = new[] { new[] { 1 } };
		var actual = Enumerable.Range(1, 1).BatchArrays(10);

		AssertAreBatchesEqual(expected, actual);
	}

	[Fact]
	public void Batch_OfOneLess()
	{
		var expected = new[] { new[] { 1, 2, 3, 4 } };
		var actual = Enumerable.Range(1, 4).BatchArrays(5);

		AssertAreBatchesEqual(expected, actual);
	}

	[Fact]
	public void Batch_OfOneMore()
	{
		var expected = new[] { new[] { 1, 2, 3, 4, 5 }, new[] { 6 } };
		var actual = Enumerable.Range(1, 6).BatchArrays(5);

		AssertAreBatchesEqual(expected, actual);
	}

	[Fact]
	public void Batch_OfExactCount()
	{
		var expected = new[] { new[] { 1, 2, 3, 4, 5 } };
		var actual = Enumerable.Range(1, 5).BatchArrays(5);

		AssertAreBatchesEqual(expected, actual);
	}

	[Fact]
	public void Batch_Skip()
	{
		var expected = new[] { new[] { 3 } };
		var range = Enumerable.Range(1, 3);
		var actual = range.Batch(2).Skip(1).ToBatchArray();

		AssertAreBatchesEqual(expected, actual);
	}

	[Fact]
	public void Batch_SkipMultiple()
	{
		var expected = new[] { new[] { 5, 6 }, new[] { 7 } };
		var range = Enumerable.Range(1, 7);
		var actual = range.Batch(2).Skip(2).ToBatchArray();

		AssertAreBatchesEqual(expected, actual);
	}

	[Fact]
	public void Batch_EnumerateMultiple()
	{
		var range = Enumerable.Range(1, 10);
		var expected = Enumerable.Range(7, 4).ToArray();
		var actual = range.Batch(3).Skip(2);

		var list = new List<int>();
		foreach (var b in actual)
		{
			foreach (var i in b)
				list.Add(i);
		}

		Assert.Equal(list, expected);
	}

	[Fact, Description("The side effects should happen when iterating the batch itself, not when the batches are created!")]
	public void Batch_SideEffects()
	{
		var current = 0;

		foreach (var batch in GenWithSideEffects().Batch(2))
		{
			foreach (var val in batch)
			{
				Assert.Equal(current, val);
			}
		}

		IEnumerable<int> GenWithSideEffects()
		{
			foreach (var i in Enumerable.Range(1, 5))
			{
				current = i;
				yield return i;
			}
		}
	}

	private void AssertAreBatchesEqual<T>(T[][] expected, T[][] actual)
	{
		Assert.Equal(expected.Length, actual.Length);

		for (var i = 0; i < expected.Length; i++)
			Assert.Equal(expected[i], actual[i]);
	}
}
