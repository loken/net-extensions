namespace Loken.System;

/// <summary>
/// Extensions for <see cref="Type"/>
/// </summary>
public static class TypeExtensions
{
	/// <summary>
	/// Get friendly name of a type, works better with generic types than default
	/// </summary>
	/// <param name="type">Type to get friendly name of.</param>
	/// <param name="ns">Should we include the ns?</param>
	/// <param name="generics">Should we include generic type parameters?</param>
	/// <returns>Friendly name of Type.</returns>
	public static string FriendlyName(this Type type, bool ns = false, bool generics = false)
	{
		var friendlyName = type.Name;
		if (type.IsGenericType)
		{
			var backtick = friendlyName.IndexOf('`');
			if (backtick > 0)
				friendlyName = friendlyName.Remove(backtick);

			if (generics)
			{
				friendlyName += "<";

				var typeParameters = type.GetGenericArguments();
				for (var i = 0; i < typeParameters.Length; ++i)
				{
					var typeParamName = FriendlyName(typeParameters[i], false);
					friendlyName += i == 0 ? typeParamName : "," + typeParamName;
				}

				friendlyName += ">";
			}

			if (ns)
				friendlyName = type.Namespace + "." + friendlyName;
		}
		else
		{
			friendlyName = ns && type.FullName is not null ? type.FullName : type.Name;
		}

		return friendlyName.Replace('+', '.');
	}

	/// <summary>
	/// Check if the <paramref name="type"/> is assignable from null; that a variable of the <paramref name="type"/> may be assigned a null.
	/// </summary>
	public static bool IsAssignableFromNull(this Type type)
	{
		return !type.IsValueType || Nullable.GetUnderlyingType(type) != null;
	}
}
