using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Loken.System.ComponentModel;

/// <summary>
/// Extensions for converting between <see cref="Type"/>s using <see cref="TypeConverter"/>s registered with the <see cref="TypeDescriptor"/>.
/// </summary>
public static class TypeConverterExtensions
{
	/// <summary>
	/// Convert the <paramref name="source"/> to <paramref name="targetType"/> using a cast or the build-in <see cref="TypeDescriptor"/> <see cref="TypeConverter"/>s.
	/// </summary>
	/// <exception cref="NotSupportedException">
	/// When no cast or <see cref="TypeConverter"/> is available between the source <see cref="Type"/>
	/// and the <paramref name="targetType"/>.</exception>
	public static object? Convert(this object source, Type targetType, object? defaultValue)
	{
		return source.Convert(targetType, () => defaultValue);
	}

	/// <summary>
	/// Convert the <paramref name="source"/> to <paramref name="targetType"/> using a cast or the build-in <see cref="TypeDescriptor"/> <see cref="TypeConverter"/>s.
	/// </summary>
	/// <exception cref="NotSupportedException">
	/// When no cast or <see cref="TypeConverter"/> is available between the source <see cref="Type"/>
	/// and the <paramref name="targetType"/>.</exception>
	/// <remarks>
	/// When cast is possible, but a <see cref="TypeConverter"/> that makes sense exists but is unable to
	/// convert the <paramref name="source"/> we return the <paramref name="defaultGenerator"/> value.
	/// One might argue that one should return the <paramref name="defaultGenerator"/> value if no such <see cref="TypeConverter"/> exists as well,
	/// but we opt for throwing a <see cref="NotSupportedException"/> in that case, to signify that the operation is never possible between the
	/// <paramref name="source"/> <see cref="Type"/> and the <paramref name="targetType"/>, so it should not be attempted at all.
	/// In short it's a conceptual issue, not simply an issue of the value in question.
	/// </remarks>
	public static object? Convert(this object? source, Type targetType, Func<object?> defaultGenerator)
	{
		if (source == null)
		{
			return targetType.IsAssignableFromNull()
				? null
				: throw new NotSupportedException($"Cannot convert from null to {targetType.FriendlyName()}");
		}

		var sourceType = source.GetType();

		if (targetType.IsAssignableFrom(sourceType))
			return source;

		var converter = TypeDescriptor.GetConverter(targetType);
		if (converter.CanConvertFrom(sourceType))
		{
			try
			{
				return converter.ConvertFromInvariant(source);
			}
			catch
			{
				// When the types are convertible, but the source is formatted incorrectly.
				return defaultGenerator();
			}
		}

		converter = TypeDescriptor.GetConverter(sourceType);
		if (converter.CanConvertTo(targetType))
		{
			try
			{
				return converter.ConvertTo(source, targetType);
			}
			catch
			{
				// When the types are convertible, but the source is formatted incorrectly.
				return defaultGenerator();
			}
		}

		throw new NotSupportedException($"Cannot convert from {sourceType.FriendlyName()} to {targetType.FriendlyName()} since casting is not possible and no registered {nameof(TypeConverter)} is able to convert.");
	}

	/// <summary>
	/// Convert the <paramref name="source"/> to <typeparamref name="T"/> using a cast or the build-in <see cref="TypeDescriptor"/> <see cref="TypeConverter"/>s.
	/// </summary>
	public static T? Convert<T>(this object source, Func<T> defaultGenerator)
	{
		return (T?)Convert(source, typeof(T), defaultGenerator);
	}

	/// <summary>
	/// Convert the <paramref name="source"/> to <typeparamref name="T"/> using a cast or the build-in <see cref="TypeDescriptor"/> <see cref="TypeConverter"/>s.
	/// </summary>
	public static T? Convert<T>(this object source, T? defaultValue = default)
	{
		return (T?)Convert(source, typeof(T), defaultValue);
	}

	/// <summary>
	/// Try converting the <paramref name="source"/> to <paramref name="targetType"/> using a cast or the build-in <see cref="TypeDescriptor"/> <see cref="TypeConverter"/>s.
	/// </summary>
	/// <remarks>
	/// Unlike with the <see cref="Convert{T}(object, T)"/> extension, we don't throw when no <see cref="TypeConverter"/> can ever perform the conversion.
	/// The reason for this is that when implementing the Try-pattern, one should endeavour to never throw.
	/// </remarks>
	public static bool TryConvert(this object source, Type targetType, [MaybeNullWhen(false)] out object value)
	{
		value = source;
		if (source == null)
			return targetType.IsAssignableFromNull();

		var sourceType = source.GetType();

		if (targetType.IsAssignableFrom(sourceType))
			return true;

		var converter = TypeDescriptor.GetConverter(targetType);
		if (converter.CanConvertFrom(sourceType))
		{
			try
			{
				value = converter.ConvertFromInvariant(source);
				return value is not null;
			}
			catch
			{
				// When the types are convertible, but the source is formatted incorrectly.
				return false;
			}
		}

		converter = TypeDescriptor.GetConverter(sourceType);
		if (converter.CanConvertTo(targetType))
		{
			try
			{
				value = converter.ConvertTo(source, targetType);
				return value is not null;
			}
			catch
			{
				// When the types are convertible, but the source is formatted incorrectly.
				return false;
			}
		}

		return false;
	}

	/// <summary>
	/// Try converting the <paramref name="source"/> to <typeparamref name="T"/> using a cast or the build-in <see cref="TypeDescriptor"/> <see cref="TypeConverter"/>s.
	/// </summary>
	public static bool TryConvert<T>(this object source, [MaybeNullWhen(false)] out T value)
	{
		if (TryConvert(source, typeof(T), out var objectValue))
		{
			value = (T)objectValue;
			return true;
		}

		value = default;
		return false;
	}

	/// <summary>
	/// Converts the given <paramref name="value"/> to the type of this <paramref name="converter"/>,
	/// using the <see cref="CultureInfo.InvariantCulture"/> when the <paramref name="value"/> is a <see cref="string"/>.
	/// </summary>
	public static object? ConvertFromInvariant(this TypeConverter converter, object value)
	{
		return value is string sourceString
			? converter.ConvertFromInvariantString(sourceString)
			: converter.ConvertFrom(value);
	}
}
