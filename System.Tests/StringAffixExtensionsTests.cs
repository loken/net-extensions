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
}
