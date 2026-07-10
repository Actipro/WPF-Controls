using ActiproSoftware.Text;
using ActiproSoftware.Windows.Controls.SyntaxEditor;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.SnapshotVersioning;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		// Load a language from a language definition
		topEditor.Document.Language = Common.SyntaxEditorHelper.LoadLanguageDefinitionFromResourceStream("CSharp.langdef");
		bottomEditor.Document.Language = topEditor.Document.Language;

		// Append the first snapshot
		AppendSnapshot(topEditor.Document.CurrentSnapshot);
		snapshotListBox.SelectedIndex = 0;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Appends a snapshot to the list.
	/// </summary>
	/// <param name="snapshot">The <see cref="ITextSnapshot"/> to append.</param>
	private void AppendSnapshot(ITextSnapshot snapshot) {
		snapshotListBox.Items.Add(snapshot);
		snapshotListBox.ScrollIntoView(snapshot);
	}

	private void OnSnapshotListBoxSelectionChanged(object sender, SelectionChangedEventArgs e) {
		if (snapshotListBox.SelectedItem is ITextSnapshot snapshot)
			bottomEditor.Document.SetText(snapshot.Text);
	}

	private void OnTopEditorDocumentTextChanged(object sender, EditorSnapshotChangedEventArgs e)
		=> AppendSnapshot(e.NewSnapshot);

}
