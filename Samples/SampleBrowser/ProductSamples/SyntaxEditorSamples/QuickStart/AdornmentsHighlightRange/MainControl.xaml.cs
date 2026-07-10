namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsHighlightRange;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl : UserControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnHighlightSelectionButtonClick(object sender, RoutedEventArgs e) {
		// Get the current tagger
		if (TryGetTagger(out var tagger)) {
			// Instruct the tagger to highlight the selected range
			tagger?.HighlightRange(editor.ActiveView.Selection.SnapshotRange);
		}

		// Focus the editor
		editor.Focus();
	}

	private void OnClearButtonClick(object sender, RoutedEventArgs e) {
		if (TryGetTagger(out var tagger)) {
			// Remove all tags to clear the highlights
			tagger?.Clear();
		}

		// Focus the editor
		editor.Focus();
	}

	/// <summary>
	/// Tries to get the <see cref="HighlightRangeTagger"/> from the active editor document.
	/// </summary>
	/// <param name="tagger">When successful, outputs the <see cref="HighlightRangeTagger"/>.</param>
	/// <returns><c>true</c> if the tagger was successfully located and output through <paramref name="tagger"/>; otherwise <c>false</c>.</returns>
	#if NET
	private bool TryGetTagger([NotNullWhen(true)] out HighlightRangeTagger? tagger) {
	#else
	private bool TryGetTagger(out HighlightRangeTagger? tagger) {
	#endif

		// NOTE:
		//   When associated with an ICodeDocument, the ISyntaxLanguage will use the registered
		//   CodeDocumentTaggerProvider<HighlightRangeTagger> service to create a new instance of HighlightRangeTagger
		//   and persist that instance in the ICodeDocument.Properties collection as long as the language
		//   is active on the document.

		// Try to get the tagger that was created for the active document
		return editor.Document.Properties.TryGetValue(typeof(HighlightRangeTagger), out tagger);
	}

}
