namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.DragAndDrop;

/// <summary>
/// Defines a custom object for presenting a snippet of text in a toolbox.
/// </summary>
/// <param name="displayText">The text to be displayed in the toolbox.</param>
/// <param name="snippet">The full snippet text.</param>
public class TextSnippet(string displayText, string snippet) {

	/// <summary>
	/// The text to be displayed in the toolbox.
	/// </summary>
	public string DisplayText { get; } = displayText;

	/// <summary>
	/// The full snippet text.
	/// </summary>
	public string Snippet { get; } = snippet;

}
