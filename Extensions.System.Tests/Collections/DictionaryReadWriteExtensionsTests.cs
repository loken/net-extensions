namespace Loken.System.Collections;

public class DictionaryReadWriteExtensionsTests
{
	[Fact]
	public void ReadValues()
	{
		var source = "ANull~Str:SomeText~Text:'Some text'~Num:42~Dbl:3.1415~On:true~Off:false~Id:f43f1ec5-04d9-481d-9c10-511577366b59~List:('hi',42,3.1415,true,false)";

		var dictionary = source.ReadValues();

		Assert.Null(dictionary.Get("ANull"));
		Assert.Equal("SomeText", dictionary.Get("Str"));
		Assert.Equal("Some text", dictionary.Get("Text"));
		Assert.Equal(42, dictionary.Get("Num"));
		Assert.Equal(3.1415, dictionary.Get("Dbl"));
		Assert.Equal(true, dictionary.Get("On"));
		Assert.Equal(false, dictionary.Get("Off"));
		Assert.Equal(Guid.Parse("f43f1ec5-04d9-481d-9c10-511577366b59"), dictionary.Get("Id"));
		Assert.Equal(new object[] { "hi", 42, 3.1415, true, false }, dictionary.Get("List"));
	}

	[Fact]
	public void ReadStrings()
	{
		var source = "ANull~Str:SomeText~Text:Some text";

		var dictionary = source.ReadStrings();

		Assert.Null(dictionary.Get("ANull"));
		Assert.Equal("SomeText", dictionary.Get("Str"));
		Assert.Equal("Some text", dictionary.Get("Text"));
	}

	[Fact]
	public void ReadWriteStrings()
	{
		var dictionary = new Dictionary<string, string?>()
		{
			{ "ANull", null },
			{ "Str", "SomeText" },
			{ "Text", "Some text" },
		};

		var formatted = dictionary.WriteString();
		var converted = formatted.ReadStrings();

		Assert.Equal(dictionary, converted);
	}

	[Fact]
	public void ReadWriteValues()
	{
		var dictionary = new Dictionary<string, object?>
		{
			{ "ANull", null },
			{ "Str", "SomeText" },
			{ "Text", "Some text" },
			{ "Num", 42 },
			{ "Dbl", 3.1415 },
			{ "On", true },
			{ "Off", false },
			{ "Id", Guid.Parse("f43f1ec5-04d9-481d-9c10-511577366b59") },
			{ "List", new object[] { "hi", 42, 3.1415, true, false } },
		};

		var formatted = dictionary.WriteString();
		var converted = formatted.ReadValues();

		Assert.Equal(dictionary, converted);
	}
}
