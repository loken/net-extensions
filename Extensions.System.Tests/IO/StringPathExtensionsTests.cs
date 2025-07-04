using System.Runtime.InteropServices;

namespace Loken.System.IO;

public class StringPathExtensionsTests
{
	[Fact]
	public void DirectoryAncestry()
	{
		string directory;
		string[] directories;

		if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
		{
			directory = @"C:\Dev\A\B";
			directories = [
				@"C:\Dev\A\B",
				@"C:\Dev\A",
				@"C:\Dev",
				@"C:\",
			];
		}
		else
		{
			directory = @"/Dev/A/B";
			directories =
			[
				@"/Dev/A/B",
				@"/Dev/A",
				@"/Dev",
				@"/",
			];
		}

		var ancestry = directory.DirectoryAncestry();

		Assert.Equal(directories, ancestry);
	}

	[Fact]
	public void TryFindAncestryFile_WithFile2LevelsUp_Succeeds()
	{
		var currentDir = Directory.GetCurrentDirectory();
		var expectedPath = Path.Combine(currentDir, "Loken.Extensions.System.Tests.dll");
		var directory = Path.Combine(currentDir, "sub", "subsub");

		var success = directory.TryFindAncestryFile("Loken.Extensions.System.Tests.dll", out var existingPath);
		Assert.True(success);
		Assert.Equal(expectedPath, existingPath);
	}

	[Fact]
	public void TryFindAncestryFile_WithFileInDirectory_Succeeds()
	{
		var directory = Directory.GetCurrentDirectory();
		var expectedPath = Path.Combine(directory, "Loken.Extensions.System.Tests.dll");

		var success = directory.TryFindAncestryFile("Loken.Extensions.System.Tests.dll", out var existingPath);
		Assert.True(success);
		Assert.Equal(expectedPath, existingPath);
	}

	[Fact]
	public void TryFindAncestryFile_WithoutFile_Fails()
	{
		var directory = Directory.GetCurrentDirectory();
		var success = directory.TryFindAncestryFile("Non.Existing.File.dll", out var existingPath);

		Assert.False(success);
		Assert.Null(existingPath);
	}
}
