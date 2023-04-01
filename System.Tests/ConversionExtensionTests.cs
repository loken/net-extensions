namespace Loken.System;

public class ConversionExtensionTests
{
	[Fact]
	public void ConvertTo_StringToInt()
	{
		Assert.Equal(10, "10".ConvertTo<string, int>());
	}
}