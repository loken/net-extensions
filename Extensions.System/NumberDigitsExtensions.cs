namespace Loken.System;

/// <summary>
/// Extensions for figuring out how many digits are in a number.
/// </summary>
/// <remarks>
/// Ref: https://stackoverflow.com/questions/4483886/how-can-i-get-a-count-of-the-total-number-of-digits-in-a-number
/// </remarks>
public static class NumberDigitsExtensions
{
	/// <summary>
	/// Get the number of digits in base 10.
	/// </summary>
	/// <remarks>
	/// Considers the minus sign a digit so that an <c><paramref name="n"/>.ToString()</c>
	/// would yield the same result.
	/// </remarks>
	/// <param name="n">The number.</param>
	/// <returns>The number of digits in <paramref name="n"/>.</returns>
	public static int Digits(this int n)
	{
		if (n >= 0)
		{
			if (n < 10) return 1;
			if (n < 100) return 2;
			if (n < 1000) return 3;
			if (n < 10000) return 4;
			if (n < 100000) return 5;
			if (n < 1000000) return 6;
			if (n < 10000000) return 7;
			if (n < 100000000) return 8;
			if (n < 1000000000) return 9;
			return 10;
		}
		else
		{
			if (n > -10) return 2;
			if (n > -100) return 3;
			if (n > -1000) return 4;
			if (n > -10000) return 5;
			if (n > -100000) return 6;
			if (n > -1000000) return 7;
			if (n > -10000000) return 8;
			if (n > -100000000) return 9;
			if (n > -1000000000) return 10;
			return 11;
		}
	}

	/// <summary>
	/// Get the number of digits in base 10.
	/// </summary>
	/// <param name="n">The number.</param>
	/// <remarks>
	/// Considers the minus sign a digit so that an <c><paramref name="n"/>.ToString()</c>
	/// would yield the same result.
	/// </remarks>
	/// <returns>The number of digits in <paramref name="n"/>.</returns>
	public static int Digits(this long n)
	{
		if (n >= 0)
		{
			if (n < 10L) return 1;
			if (n < 100L) return 2;
			if (n < 1000L) return 3;
			if (n < 10000L) return 4;
			if (n < 100000L) return 5;
			if (n < 1000000L) return 6;
			if (n < 10000000L) return 7;
			if (n < 100000000L) return 8;
			if (n < 1000000000L) return 9;
			if (n < 10000000000L) return 10;
			if (n < 100000000000L) return 11;
			if (n < 1000000000000L) return 12;
			if (n < 10000000000000L) return 13;
			if (n < 100000000000000L) return 14;
			if (n < 1000000000000000L) return 15;
			if (n < 10000000000000000L) return 16;
			if (n < 100000000000000000L) return 17;
			if (n < 1000000000000000000L) return 18;
			return 19;
		}
		else
		{
			if (n > -10L) return 2;
			if (n > -100L) return 3;
			if (n > -1000L) return 4;
			if (n > -10000L) return 5;
			if (n > -100000L) return 6;
			if (n > -1000000L) return 7;
			if (n > -10000000L) return 8;
			if (n > -100000000L) return 9;
			if (n > -1000000000L) return 10;
			if (n > -10000000000L) return 11;
			if (n > -100000000000L) return 12;
			if (n > -1000000000000L) return 13;
			if (n > -10000000000000L) return 14;
			if (n > -100000000000000L) return 15;
			if (n > -1000000000000000L) return 16;
			if (n > -10000000000000000L) return 17;
			if (n > -100000000000000000L) return 18;
			if (n > -1000000000000000000L) return 19;
			return 20;
		}
	}
}
