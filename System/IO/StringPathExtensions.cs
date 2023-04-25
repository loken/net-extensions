using System.Diagnostics.CodeAnalysis;

namespace Loken.System.IO;

/// <summary>
/// <c>string</c> extensions for working with paths.
/// </summary>
public static class StringPathExtensions
{
	/// <summary>
	/// Walk up the parent folders of <paramref name="directory"/>.
	/// </summary>
	/// <param name="directory">The directory.</param>
	/// <returns>An enumeration of directories, starting with the <paramref name="directory"/>.</returns>
	public static IEnumerable<string> DirectoryAncestry(this string directory)
	{
		string? current = Path.TrimEndingDirectorySeparator(directory);
		while (current != null)
		{
			yield return current;
			current = Path.GetDirectoryName(current);
		}
	}

	/// <summary>
	/// Try to find the named file in the parent folders of the <paramref name="directory"/>.
	/// </summary>
	/// <param name="directory">The directory in which to start the search.</param>
	/// <param name="fileName">The name of the file to look for.</param>
	/// <param name="existingPath">The full path of a matching file that exists.</param>
	/// <returns>Whether the file could be found or not.</returns>
	public static bool TryFindAncestryFile(this string directory, string fileName, [MaybeNullWhen(false)] out string existingPath)
	{
		existingPath = directory
			.DirectoryAncestry()
			.Select(dir => Path.Combine(dir, fileName))
			.FirstOrDefault(File.Exists);

		return existingPath is not null;
	}
}
