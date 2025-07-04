using System.Text;

namespace Loken.System.IO;

/// <summary>
/// <see cref="Stream"/> extensions for working with streams of content.
/// </summary>
public static class TextStreamExtensions
{
	/// <summary>
	/// Reads all content content from a <paramref name="stream"/>.
	/// </summary>
	/// <param name="stream">The stream to read.</param>
	/// <param name="encoding">The encoding to use for the stream. (null leads to <see cref="Encoding.UTF8"/>)</param>
	/// <param name="bufferSize">The buffer size to use. (-1 leads to 1024)</param>
	/// <param name="leaveOpen">Leave the <paramref name="stream"/> open? (Default: true)</param>
	/// <returns>The read <see cref="string"/>.</returns>
	public static string ReadAllText(this Stream stream, Encoding? encoding = null, int bufferSize = -1, bool leaveOpen = true)
	{
		if (stream.Position > 0)
			_ = stream.Seek(0, SeekOrigin.Begin);

		using var sr = new StreamReader(stream, encoding ?? Encoding.UTF8, true, bufferSize, leaveOpen);
		return sr.ReadToEnd();
	}

	/// <summary>
	/// Writes the text <paramref name="content"/> to the <paramref name="stream"/> or a new <see cref="MemoryStream"/>.
	/// </summary>
	/// <param name="content">The content to write.</param>
	/// <param name="stream">The stream to write to. If no stream is provided, a <see cref="MemoryStream"/> will be used.</param>
	/// <param name="encoding">The encoding to use for the stream. (null leads to <see cref="Encoding.UTF8"/>)</param>
	/// <param name="bufferSize">The buffer size to use. (-1 leads to 1024)</param>
	/// <param name="leaveOpen">Leave the stream open?</param>
	/// <param name="seek">Seek to this origin if the <paramref name="stream"/> supports it.</param>
	/// <returns>The open <see cref="Stream"/>.</returns>
	public static Stream WriteAllText(this string content, Stream? stream = default, Encoding? encoding = default, int bufferSize = -1, bool leaveOpen = true, SeekOrigin seek = SeekOrigin.Begin)
	{
		stream ??= new MemoryStream();

		using var sw = new StreamWriter(stream, encoding ?? Encoding.UTF8, bufferSize, leaveOpen);

		sw.Write(content);

		if (seek != SeekOrigin.Current && stream.CanSeek)
			_ = stream.Seek(0, seek);

		return stream;
	}
}
