using System.Globalization;

namespace Loken.System.ComponentModel;

public class TypeConverterExtensionsTests
{
	[Fact]
	public void Convert_Integer()
	{
		Assert.Equal(350, "350".Convert<int>());
	}

	[Fact]
	public void Convert_DateTime()
	{
		var date = new DateTime(1900, 3, 1, 12, 30, 59, DateTimeKind.Utc);
		var dateString = date.ToString(CultureInfo.InvariantCulture);

		Assert.Equal(date, dateString.Convert<DateTime>());
	}
}
