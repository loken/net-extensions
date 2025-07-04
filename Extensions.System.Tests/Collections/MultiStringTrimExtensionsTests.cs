namespace Loken.System.Collections;

public class MultiStringTrimExtensionsTests
{
	public static readonly string[] Strings = ["  pad-both ", "pad-start", "pad-end ", "no-pad", "", " ", "  "];
	public static readonly string[] Trimmed = ["pad-both", "pad-start", "pad-end", "no-pad"];
	public static readonly string[] TrimmedStart = ["pad-both ", "pad-start", "pad-end ", "no-pad"];
	public static readonly string[] TrimmedEnd = ["  pad-both", "pad-start", "pad-end", "no-pad"];

	[Fact]
	public void Trim_StringSequence_YieldsTrimmedSequence()
	{
		// Since the trim for enumerable won't modify the original we can reuse across calls.
		var input = Strings.AsEnumerable();

		Assert.Equal(Trimmed, input.Trim());
		Assert.Equal(TrimmedStart, input.TrimStart());
		Assert.Equal(TrimmedEnd, input.TrimEnd());
	}

	[Fact]
	public void Trim_StringList_YieldsTrimmedList()
	{
		// Since the trim for lists will modify the originalwe can't reuse across calls.
		// Note: If we used ToArray() instead of ToList() we would get the original array and things would fail, so be careful.
		Assert.Equal(Trimmed, Strings.ToList().Trim());
		Assert.Equal(TrimmedStart, Strings.ToList().TrimStart());
		Assert.Equal(TrimmedEnd, Strings.ToList().TrimEnd());
	}
}
