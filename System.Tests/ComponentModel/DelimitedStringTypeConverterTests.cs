using System.ComponentModel;

namespace Loken.System.ComponentModel;

public class DelimitedStringTypeConverterTests : IDisposable
{
	private TypeDescriptionProvider DescriptorProvider { get; } = TypeDescriptor.AddAttributes(typeof(ICollection<string>), new TypeConverterAttribute(typeof(DelimitedStringTypeConverter)));

	public void Dispose()
	{
		TypeDescriptor.RemoveProvider(DescriptorProvider, typeof(ICollection<string>));
	}

	[Fact]
	public void Convert_ToArray()
	{
		var expected = new[] { "one", "other" };

		var actual = "one;other".Convert<string[]>();

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void Convert_ToCollection()
	{
		var expected = new[] { "one", "other" };

		var actual = "one;other".Convert<ICollection<string>>();

		Assert.Equal(expected, actual);
	}
}
