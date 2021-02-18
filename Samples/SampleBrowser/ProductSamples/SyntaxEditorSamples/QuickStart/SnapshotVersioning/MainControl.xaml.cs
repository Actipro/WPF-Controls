using ActiproSoftware.Text;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using System.Windows.Controls;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.SnapshotVersioning {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			// Load a language from a language definition
			topEditor.Document.Language = ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common.SyntaxEditorHelper.LoadLanguageDefinitionFromResourceStream("CSharp.langdef");
			bottomEditor.Document.Language = topEditor.Document.Language;

			// Append the first snapshot
			this.AppendSnapshot(topEditor.Document.CurrentSnapshot);
			snapshotListBox.SelectedIndex = 0;
        }

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Appends a snapshot to the list.
		/// </summary>
		/// <param name="snapshot">The <see cref="ITextSnapshot"/> to append.</param>
		private void AppendSnapshot(ITextSnapshot snapshot) {
			snapshotListBox.Items.Add(snapshot);
			snapshotListBox.ScrollIntoView(snapshot);
		}

		/// <summary>
		/// Occurs when the selection has changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="SelectionChangedEventArgs"/> that contains the event data.</param>
		private void OnSnapshotListBoxSelectionChanged(object sender, SelectionChangedEventArgs e) {
			ITextSnapshot snapshot = snapshotListBox.SelectedItem as ITextSnapshot;
			if (snapshot != null)
				bottomEditor.Document.SetText(snapshot.Text);
		}

		/// <summary>
		/// Occurs after the editor's document text has changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="EditorSnapshotChangedEventArgs"/> that contains the event data.</param>
		private void OnTopEditorDocumentTextChanged(object sender, EditorSnapshotChangedEventArgs e) {
			this.AppendSnapshot(e.NewSnapshot);
		}

		
    }

}