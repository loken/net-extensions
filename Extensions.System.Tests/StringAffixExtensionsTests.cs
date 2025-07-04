namespace Loken.System;

public class StringAffixExtensionsTests
{
	[Fact]
	public void WithoutPrefix()
	{
		Assert.Equal("some", "s.a:some".WithoutPrefix());
	}

	[Fact]
	public void WithoutFirstPrefix()
	{
		Assert.Equal("some", "s:some".WithoutFirstPrefix());
	}

	[Fact]
	public void WithoutSuffix()
	{
		Assert.Equal("some", "some:s.a".WithoutSuffix());
	}

	[Fact]
	public void WithoutLastSuffix()
	{
		Assert.Equal("some", "some:s".WithoutLastSuffix());
	}

	[Fact]
	public void WithoutPrefix_WithCustomSeparators()
	{
		Assert.Equal("value", "path/to/value".WithoutPrefix('/'));
	}

	[Fact]
	public void WithoutPrefix_WithMultipleSeparators()
	{
		Assert.Equal("value", "path:to/value".WithoutPrefix(':', '/'));
	}

	[Fact]
	public void WithoutPrefix_WhenNoSeparatorFound_ReturnsOriginal()
	{
		Assert.Equal("no-separators", "no-separators".WithoutPrefix());
	}

	[Fact]
	public void WithoutFirstPrefix_WithCustomSeparators()
	{
		Assert.Equal("to/value", "path/to/value".WithoutFirstPrefix('/'));
	}

	[Fact]
	public void WithoutFirstPrefix_WhenNoSeparatorFound_ReturnsOriginal()
	{
		Assert.Equal("no-separators", "no-separators".WithoutFirstPrefix());
	}

	[Fact]
	public void WithoutSuffix_WithCustomSeparators()
	{
		Assert.Equal("path", "path/to/value".WithoutSuffix('/'));
	}

	[Fact]
	public void WithoutSuffix_WithMultipleSeparators()
	{
		Assert.Equal("path", "path:to/value".WithoutSuffix(':', '/'));
	}

	[Fact]
	public void WithoutSuffix_WhenNoSeparatorFound_ReturnsOriginal()
	{
		Assert.Equal("no-separators", "no-separators".WithoutSuffix());
	}

	[Fact]
	public void WithoutLastSuffix_WithCustomSeparators()
	{
		Assert.Equal("path/to", "path/to/value".WithoutLastSuffix('/'));
	}

	[Fact]
	public void WithoutLastSuffix_WhenNoSeparatorFound_ReturnsOriginal()
	{
		Assert.Equal("no-separators", "no-separators".WithoutLastSuffix());
	}

	[Theory]
	[InlineData("a.b.c", "c")]
	[InlineData("a:b:c", "c")]
	[InlineData("a;b;c", "c")]
	[InlineData("a,b,c", "c")]
	[InlineData("a|b|c", "c")]
	public void WithoutPrefix_WorksWithAllDefaultSeparators(string input, string expected)
	{
		Assert.Equal(expected, input.WithoutPrefix());
	}

	[Theory]
	[InlineData("a.b.c", "b.c")]
	[InlineData("a:b:c", "b:c")]
	[InlineData("a;b;c", "b;c")]
	[InlineData("a,b,c", "b,c")]
	[InlineData("a|b|c", "b|c")]
	public void WithoutFirstPrefix_WorksWithAllDefaultSeparators(string input, string expected)
	{
		Assert.Equal(expected, input.WithoutFirstPrefix());
	}

	[Theory]
	[InlineData("a.b.c", "a")]
	[InlineData("a:b:c", "a")]
	[InlineData("a;b;c", "a")]
	[InlineData("a,b,c", "a")]
	[InlineData("a|b|c", "a")]
	public void WithoutSuffix_WorksWithAllDefaultSeparators(string input, string expected)
	{
		Assert.Equal(expected, input.WithoutSuffix());
	}

	[Theory]
	[InlineData("a.b.c", "a.b")]
	[InlineData("a:b:c", "a:b")]
	[InlineData("a;b;c", "a;b")]
	[InlineData("a,b,c", "a,b")]
	[InlineData("a|b|c", "a|b")]
	public void WithoutLastSuffix_WorksWithAllDefaultSeparators(string input, string expected)
	{
		Assert.Equal(expected, input.WithoutLastSuffix());
	}
}
