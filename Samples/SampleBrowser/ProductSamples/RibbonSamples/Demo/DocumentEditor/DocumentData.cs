namespace ActiproSoftware.ProductSamples.RibbonSamples.Demo.DocumentEditor;

/// <summary>
/// Contains the information about the currently open file.
/// </summary>
/// <param name="path">The full path to the file.</param>
public class DocumentData(string path) {

	internal static int NewDocumentCount = 0;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates a <see cref="DocumentData"/> for a new document.
	/// </summary>
	public static DocumentData CreateNewDocument()
		=> CreateNewDocument(".rtf");

	/// <summary>
	/// Creates a <see cref="DocumentData"/> for a new document.
	/// </summary>
	/// <param name="extension">The extension for the new document.</param>
	public static DocumentData CreateNewDocument(string extension)
		=> new("Document" + (++NewDocumentCount) + extension);

	/// <summary>
	/// The filename's extension.
	/// </summary>
	public string FilenameExtension
		=> System.IO.Path.GetExtension(Path).ToLower();

	/// <summary>
	/// The filename without an extension.
	/// </summary>
	public string FilenameWithoutExtension
		=> System.IO.Path.GetFileNameWithoutExtension(Path);

	/// <summary>
	/// Indicates whether the file has been modified.
	/// </summary>
	public bool Modified { get; set; }

	/// <summary>
	/// The full path to the file.
	/// </summary>
	public string Path { get; set; } = path;

}
