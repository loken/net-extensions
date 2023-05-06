using Loken.System;
using Loken.System.IO;

namespace Loken.Hierarchies.Data.MongoDB;

/// <summary>
/// Load environment variables from files from a directory or an ancestor of that directory.
/// </summary>
public static class DotEnv
{
	/// <summary>
	/// Load the environmentvariables from <c>".env"</c> and "<EnvironmentName>.env"
	/// files from a directory or an ancestor of that directory.
	/// </summary>
	/// <param name="directory">The file search starts in this directory. (Default: current directory.)</param>
	public static void Load(string? directory = default)
	{
		directory ??= Environment.CurrentDirectory;

		LoadFile(directory, ".env");

		var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
		if (!string.IsNullOrEmpty(environment))
			LoadFile(directory, $".{environment}.env");
	}

	/// <summary>
	/// Load the environment variables from a named file in a directory or an ancestor of that directory. 
	/// </summary>
	/// <param name="directory">The file search starts in this directory. (Default: current directory.)</param>
	/// <param name="fileName">The name of the file to look for. (Default: <c>".env"</c>)</param>
	public static void LoadFile(string? directory = default, string fileName = ".env")
	{
		directory ??= Environment.CurrentDirectory;

		if (!directory.TryFindAncestryFile(fileName, out var filePath))
			return;

		var variables = File
			.ReadAllLines(filePath)
			.Where(line => !string.IsNullOrWhiteSpace(line))
			.Select(line => line.SplitKvp('='))
			.Where(pair => !string.IsNullOrEmpty(pair.Value));

		foreach (var (key, value) in variables)
			Environment.SetEnvironmentVariable(key, value);
	}
}