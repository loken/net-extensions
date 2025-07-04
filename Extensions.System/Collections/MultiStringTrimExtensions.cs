namespace Loken.System.Collections;

public static class MultiStringTrimExtensions
{
	/// <summary>
	/// <see cref="string.Trim"/> each of the <paramref name="strings"/>.
	/// </summary>
	/// <param name="trim">The character to trim.</param>
	/// <param name="keepEmpty">Should we keep entries that are empty after trimming or skip them?</param>
	/// <returns>A potentially shortened sequence of trimmed entries.</returns>
	public static IEnumerable<string> Trim(this IEnumerable<string> strings, char trim = ' ', bool keepEmpty = false)
	{
		foreach (var str in strings)
		{
			var trimmed = str.Trim(trim);

			if (!keepEmpty && trimmed is null or "")
				continue;

			yield return trimmed;
		}
	}

	/// <summary>
	/// <see cref="string.TrimStart"/> each of the <paramref name="strings"/>.
	/// </summary>
	/// <param name="trim">The character to trim.</param>
	/// <param name="keepEmpty">Should we keep entries that are empty after trimming or skip them?</param>
	/// <returns>A potentially shortened sequence of trimmed entries.</returns>
	public static IEnumerable<string> TrimStart(this IEnumerable<string> strings, char trim = ' ', bool keepEmpty = false)
	{
		foreach (var str in strings)
		{
			var trimmed = str.TrimStart(trim);

			if (!keepEmpty && trimmed is null or "")
				continue;

			yield return trimmed;
		}
	}

	/// <summary>
	/// <see cref="string.TrimEnd"/> each of the <paramref name="strings"/>.
	/// </summary>
	/// <param name="trim">The character to trim.</param>
	/// <param name="keepEmpty">Should we keep entries that are empty after trimming or skip them?</param>
	/// <returns>A potentially shortened sequence of trimmed entries.</returns>
	public static IEnumerable<string> TrimEnd(this IEnumerable<string> strings, char trim = ' ', bool keepEmpty = false)
	{
		foreach (var str in strings)
		{
			var trimmed = str.TrimEnd(trim);

			if (!keepEmpty && trimmed is null or "")
				continue;

			yield return trimmed;
		}
	}

	/// <summary>
	/// <see cref="string.Trim"/> each of the <paramref name="list"/>.
	/// Modifies the <paramref name="list"/>.
	/// </summary>
	/// <param name="trim">The character to trim.</param>
	/// <param name="keepEmpty">Should we keep entries that are empty after trimming or skip them?</param>
	/// <returns>The modified <paramref name="list"/>.</returns>
	public static TList Trim<TList>(this TList list, char trim = ' ', bool keepEmpty = false)
		where TList : IList<string>
	{
		for (var i = list.Count - 1; i >= 0; i--)
		{
			var trimmed = list[i].Trim(trim);
			if (!keepEmpty && trimmed is null or "")
				list.RemoveAt(i);
			else
				list[i] = trimmed;
		}

		return list;
	}

	/// <summary>
	/// <see cref="string.TrimStart"/> each of the <paramref name="list"/>.
	/// Modifies the <paramref name="list"/>.
	/// </summary>
	/// <param name="trim">The character to trim.</param>
	/// <param name="keepEmpty">Should we keep entries that are empty after trimming or skip them?</param>
	/// <returns>The modified <paramref name="list"/>.</returns>
	public static TList TrimStart<TList>(this TList list, char trim = ' ', bool keepEmpty = false)
	where TList : IList<string>
	{
		for (var i = list.Count - 1; i >= 0; i--)
		{
			var trimmed = list[i].TrimStart(trim);
			if (!keepEmpty && trimmed is null or "")
				list.RemoveAt(i);
			else
				list[i] = trimmed;
		}

		return list;
	}

	/// <summary>
	/// <see cref="string.TrimEnd"/> each of the <paramref name="list"/>.
	/// Modifies the <paramref name="list"/>.
	/// </summary>
	/// <param name="trim">The character to trim.</param>
	/// <param name="keepEmpty">Should we keep entries that are empty after trimming or skip them?</param>
	/// <returns>The modified <paramref name="list"/>.</returns>
	public static TList TrimEnd<TList>(this TList list, char trim = ' ', bool keepEmpty = false)
	where TList : IList<string>
	{
		for (var i = list.Count - 1; i >= 0; i--)
		{
			var trimmed = list[i].TrimEnd(trim);
			if (!keepEmpty && trimmed is null or "")
				list.RemoveAt(i);
			else
				list[i] = trimmed;
		}

		return list;
	}
}
