namespace Loken.System.IO;

public class TextStreamExtensionsTests
{
	[Fact]
	public void ReadAndWriteText()
	{
		const string TEXT = "This is some text content";

		using var stream = new MemoryStream();

		_ = TEXT.WriteAllText(stream);

		var text = stream.ReadAllText();

		Assert.Equal(TEXT, text);
	}
}
