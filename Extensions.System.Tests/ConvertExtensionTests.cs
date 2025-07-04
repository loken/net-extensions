namespace Loken.System;

public class ConvertExtensionTests
{
	[Fact]
	public void ChangeType_StringToInt()
	{
		Assert.Equal(10, "10".ChangeType<int>());
	}
}