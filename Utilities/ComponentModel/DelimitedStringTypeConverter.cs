﻿using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Loken.System;

namespace Loken.Utilities.ComponentModel;

/// <summary>
/// Converts a source string into an array of strings based on delimiters.
/// </summary>
public class DelimitedStringTypeConverter : TypeConverter
{
	/// <inheritdoc/>
	public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
	{
		return sourceType == typeof(string);
	}

	/// <inheritdoc/>
	public override bool CanConvertTo(ITypeDescriptorContext? context, [NotNullWhen(true)] Type? destinationType)
	{
		return destinationType == typeof(string[]);
	}

	/// <inheritdoc/>
	public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
	{
		return ((string)value).SplitByDefault();
	}

	/// <inheritdoc/>
	public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
	{
		return ((string?)value)?.SplitByDefault();
	}
}
