namespace Loken.System;

public static class ConversionExtensions
{
	/// <summary>
	/// Convert the <typeparamref name="TSource"/> into a <typeparamref name="TTarget"/> using <see cref="Convert.ChangeType(object?, Type)"/>.
	/// </summary>
	public static TTarget ConvertTo<TSource, TTarget>(this TSource source)
		where TSource : notnull
	{
		return (TTarget)Convert.ChangeType(source, typeof(TTarget));
	}

	/// <summary>
	/// Convert the <see cref="string"/> into a <typeparamref name="TTarget"/> using <see cref="Convert.ChangeType(object?, Type)"/>.
	/// </summary>
	public static TTarget ConvertTo<TTarget>(this string source)
	{
		return source.ConvertTo<string, TTarget>();
	}
}