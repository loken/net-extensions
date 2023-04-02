using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Loken.System;

public static class ConvertExtensions
{
	/// <summary>
	/// Convert the <typeparamref name="TSource"/> into a <typeparamref name="TTarget"/> using <see cref="Convert.ChangeType(object?, Type)"/>.
	/// </summary>
	[return: NotNullIfNotNull(nameof(source))]
	public static TTarget? ChangeType<TTarget>(this object? source, IFormatProvider? provider)
	{
		return (TTarget?)Convert.ChangeType(source, typeof(TTarget), provider);
	}

	/// <summary>
	/// Convert the <typeparamref name="TSource"/> into a <typeparamref name="TTarget"/> using <see cref="Convert.ChangeType(object?, Type)"/>.
	/// </summary>
	[return: NotNullIfNotNull(nameof(source))]
	public static TTarget? ChangeType<TTarget>(this object? source)
	{
		return (TTarget?)Convert.ChangeType(source, typeof(TTarget), CultureInfo.CurrentCulture);
	}
}