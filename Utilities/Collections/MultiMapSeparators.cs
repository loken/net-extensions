namespace Loken.Utilities.Collections;

public readonly struct MultiMapSeparators
{
	/// <summary>
	/// The separator for each entry, which represents <see cref="KeyValuePair{TKey,TValue}"/>s.
	/// </summary>
	public string Entry { get; }

	/// <summary>
	/// The separator between the entry key and entry values.
	/// </summary>
	public char KeyValue { get; }

	/// <summary>
	/// The separator between each value.
	/// </summary>
	public char Value { get; }

	private MultiMapSeparators(string? entry = null, char keyValue = ':', char value = ',')
	{
		Entry = entry ?? Environment.NewLine;
		KeyValue = keyValue;
		Value = value;
	}

	public static MultiMapSeparators Create(string? entry = null, char keyValue = ':', char value = ',')
	{
		return new MultiMapSeparators(entry, keyValue, value);
	}

	public static MultiMapSeparators Default => Create();
}