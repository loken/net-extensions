using System.Globalization;
using Loken.System.ComponentModel;

namespace Loken.System.Collections;

/// <summary>
/// Extensions for converting between a string and either a dictionary of strings or a dictionary of converted values.
/// </summary>
public static class DictionaryReadWriteExtensions
{
	/// <summary>
	/// Read a <paramref name="formattedString"/> into a dictionary of strings.
	/// </summary>
	public static IDictionary<string, string?> ReadStrings(this string formattedString, char entrySeparator = '~', char pairSeparator = ':')
	{
		return formattedString
			.SplitBy(entrySeparator)
			.Select(f => f.SplitKvp(pairSeparator))
			.ToDictionary(p => p.Key, p => p.Value);
	}

	/// <summary>
	/// Read a <paramref name="formattedString"/> into a dictionary of converted <see cref="object"/> values.
	/// </summary>
	public static IDictionary<string, object?> ReadValues(this string formattedString, char entrySeparator = '~', char pairSeparator = ':')
	{
		return formattedString
			.SplitBy(entrySeparator)
			.Select(f => f.SplitKvp(pairSeparator))
			.ToDictionary(p => p.Key, p => p.Value?.ToValue());
	}

	/// <summary>
	/// Write the <paramref name="dictionary"/> to a formatted string.
	/// </summary>
	public static string WriteString(this IDictionary<string, string?> dictionary, char entrySeparator = '~', char pairSeparator = ':')
	{
		return dictionary
			.Select(p => p.Value is null
				? p.Key
				: p.Key + pairSeparator + p.Value)
			.Join(entrySeparator);
	}

	/// <summary>
	/// Write the <paramref name="dictionary"/> to a formatted string.
	/// </summary>
	public static string WriteString(this IDictionary<string, object?> dictionary, char entrySeparator = '~', char pairSeparator = ':')
	{
		return dictionary
			.Select(p => p.Value is null
				? p.Key
				: p.Key + pairSeparator + p.Value.ToValueString())
			.Join(entrySeparator);
	}

	private static object ToValue(this string source)
	{
		// The order of attempted conversions is important!
		if (source.TryConvert<bool>(out var boolValue))
			return boolValue;
		if (source.TryConvert<int>(out var intValue))
			return intValue;
		if (source.TryConvert<double>(out var doubleValue))
			return doubleValue;

		// Array microsyntax when wrapped in parenthesis.
		if (source.StartsWith('(') && source.EndsWith(')'))
			return source[1..^1].SplitBy(',').Select(s => s.ToValue()).ToArray();

		// Provides a way to force a string for something that would
		// otherwise be parsed as a boolean or number.
		if (source.StartsWith('\'') && source.EndsWith('\''))
			source = source[1..^1];

		// Because everybody uses GUIDs!
		if (Guid.TryParse(source, out var guidValue))
			return guidValue;

		return source;
	}

	private static string? ToValueString(this object value)
	{
		if (value is double decimalValue)
			return decimalValue.ToString(CaseInsensitiveNumbers);

		if (value is object[] objArray)
			return $"({string.Join(",", objArray.Select(o => o.ToValueString()))})";

		if (value is string stringValue)
			return stringValue.StartsWith('\'') && stringValue.EndsWith('\'')
				? stringValue
				: "'" + stringValue + "'";

		if (value is bool boolValue)
			return boolValue.ToString().ToLowerInvariant();

		return value.ToString();
	}

	private static readonly IFormatProvider CaseInsensitiveNumbers = new CultureInfo(string.Empty)
	{
		NumberFormat =
		{
			NumberDecimalSeparator = ".",
			NumberGroupSeparator = string.Empty,
		},
	};
}
