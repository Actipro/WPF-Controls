using ActiproSoftware.Windows.Controls.SyntaxEditor;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.TextStatistics;

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

		// Update statistics
		UpdateStatistics();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Occurs after the editor's document text has changed.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnEditorDocumentTextChanged(object sender, EditorSnapshotChangedEventArgs e)
		=> UpdateStatistics();

	/// <summary>
	/// Updates statistics.
	/// </summary>
	private void UpdateStatistics()
		=> resultsListView.ItemsSource = editor.Document.CurrentSnapshot.GetTextStatistics().GetRawStatistics();

}
