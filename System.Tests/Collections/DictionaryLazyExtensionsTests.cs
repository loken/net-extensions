namespace Loken.System.Collections;

public class DictionaryLazyExtensionsTests
{

	[Fact]
	public void Lazy_WithInitializer()
	{
		var initializationCount = 0;
		object Initializer()
		{
			initializationCount++;
			return new object();
		}

		Dictionary<string, object> dict = new();
		var first = dict.Lazy("item", Initializer);
		var second = dict.Lazy("item", Initializer);

		Assert.Same(first, second);
		Assert.Equal(1, initializationCount);
	}

	[Fact]
	public void LazyDefault_ReferenceType_IsNull()
	{
		Dictionary<string, object?> dict = new();
		var defaultReferenceType = dict.LazyDefault("from default(object)");

		Assert.Null(defaultReferenceType);
	}

	[Fact]
	public void LazyDefault_ValueType_IsZero()
	{
		Dictionary<string, int> dict = new();
		var defaultValueType = dict.LazyDefault("from default(int)");

		Assert.Equal(0, defaultValueType);
	}

	[Fact]
	public void LazyDefault_Nullable_IsNull()
	{
		Dictionary<string, int?> dict = new();
		var defaultNullable = dict.LazyDefault("from default(int?)");

		Assert.Null(defaultNullable);
	}

	[Fact]
	public void LazyNew()
	{
		Dictionary<string, HasDefaultValueFromConstructor> dict = new();
		var defaultConstructed = dict.LazyNew("from default constructor");

		Assert.NotNull(defaultConstructed);
		Assert.Equal(HasDefaultValueFromConstructor.DefaultValue, defaultConstructed.Value);
	}

	[Fact]
	public void Lazy_Set()
	{
		Dictionary<string, ISet<int>> dict = new();
		var first = dict.LazySet("item");
		_ = first.Add(10);
		var second = dict.LazySet("item");
		_ = second.Add(20);
		_ = second.Add(20);
		_ = second.Add(30);

		Assert.Same(first, second);
		Assert.Equal(new[] { 10, 20, 30 }, first.ToArray());
	}

	private class HasDefaultValueFromConstructor
	{
		public const string DefaultValue = "MyDefault";

		public string Value { get; set; }

		public HasDefaultValueFromConstructor()
		{
			Value = DefaultValue;
		}
	}
}
