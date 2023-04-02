namespace Loken.System;

public class TypeExtensionsTests
{
	[Theory]
	[InlineData(typeof(int), "Int32")]
	[InlineData(typeof(double), "Double")]
	[InlineData(typeof(string), "String")]
	[InlineData(typeof(DateTime), "DateTime")]
	public void FriendlyName_BaseTypes(Type type, string expectedName)
	{
		Assert.Equal(expectedName, type.FriendlyName());
	}

	[Theory]
	[InlineData(typeof(Thingy<string>), false, false, "Thingy")]
	[InlineData(typeof(Thingy<string>), true, false, "Loken.System.Thingy")]
	[InlineData(typeof(Thingy<string>), true, true, "Loken.System.Thingy<String>")]
	public void FriendlyName_GenericTypes(Type type, bool ns, bool generics, string expectedName)
	{
		Assert.Equal(expectedName, type.FriendlyName(ns, generics));
	}

	[Theory]
	[InlineData(typeof(Thingy<>), true, true, "Loken.System.Thingy<TPart>")]
	public void FriendlyName_OpenGenericTypes(Type type, bool ns, bool generics, string expectedName)
	{
		Assert.Equal(expectedName, type.FriendlyName(ns, generics));
	}
}

internal class Thingy<TPart> { }
