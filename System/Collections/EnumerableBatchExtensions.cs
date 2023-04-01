namespace Loken.System.Collections;

public static class EnumerableBatchExtensions
{
	/// <summary>
	/// Enumerate the <paramref name="enumerable"/> in batches of <paramref name="count"/> items at a time.
	/// Does not enumerate the batch until the consumer does, or moves to the next batch.
	/// </summary>
	/// <typeparam name="T">Type of items.</typeparam>
	/// <param name="enumerable">The enumerable.</param>
	/// <param name="count">Number of items per batch. The last batch may contain fewer items.</param>
	/// <returns>The items batched in chunks of up to <paramref name="count"/> items.</returns>
	/// <remarks>
	/// The first item in a batch is accessed when the batch is created, meaning any side effects of
	/// enumerating the first item in each batch will happen when the batch is made rather than when iteration of the batch
	/// itself commences. This was necessary to avoid creating empty batches.
	/// </remarks>
	public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> enumerable, int count)
	{
		if (count <= 0)
			throw new ArgumentOutOfRangeException(nameof(count), count, "Must be larger than 0");

		using var enumerator = enumerable.GetEnumerator();
		Batched<T>? batch = null;

		while (enumerator.MoveNext())
		{
			// Iterate past any remainder of the batch before moving to the next one.
			while (batch != null && ++batch.Used < count && enumerator.MoveNext())
			{
			}

			// Start counting the new batch
			batch = CreateBatch(enumerator, count);
			yield return batch.Enumerable!;
		}
	}

	/// <summary>
	/// Enumerate the <paramref name="enumerable"/> into array batches of <paramref name="count"/> items at a time.
	/// </summary>
	/// <typeparam name="T">Type of items.</typeparam>
	/// <param name="enumerable">The enumerable.</param>
	/// <param name="count">Number of items per batch. The last batch may contain fewer items.</param>
	/// <returns>The items batched in chunks of up to <paramref name="count"/> items.</returns>
	/// <remarks>
	/// The first item in a batch is accessed when the batch is created, meaning any side effects of
	/// enumerating the first item in each batch will happen when the batch is made rather than when iteration of the batch
	/// itself commences. This was necessary to avoid creating empty batches.
	/// </remarks>
	public static T[][] BatchArrays<T>(this IEnumerable<T> enumerable, int count)
	{
		return enumerable.Batch(count).ToBatchArray();
	}

	private static Batched<T> CreateBatch<T>(IEnumerator<T> enumerator, int count)
	{
		var batch = new Batched<T>();
		IEnumerable<T> InnerBatch()
		{
			do
			{
				yield return enumerator.Current;
			}
			while (++batch.Used < count && enumerator.MoveNext());
		}

		batch.Enumerable = InnerBatch();
		return batch;
	}

	private class Batched<T>
	{
		public IEnumerable<T>? Enumerable;

		public int Used;
	}

	/// <summary>
	/// Realize the batch into an array of arrays. This causes the <paramref name="batches"/> to be fully enumerated.
	/// </summary>
	/// <typeparam name="T">Type of items.</typeparam>
	/// <param name="batches">The batches to realize.</param>
	/// <returns>The <paramref name="batches"/> as an array of arrays.</returns>
	public static T[][] ToBatchArray<T>(this IEnumerable<IEnumerable<T>> batches)
	{
		var result = new List<T[]>();
		foreach (var batch in batches)
		{
			var items = new List<T>();
			foreach (var item in batch)
				items.Add(item);

			result.Add(items.ToArray());
		}

		return result.ToArray();
	}
}
