using ActiproSoftware.Text;
using System.Windows;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.SnapshotTranslation {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		private ITextSnapshot originalSnapshot;

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

			// Store the original snapshot of the bottom document
			originalSnapshot = bottomEditor.Document.CurrentSnapshot;

			// Update the top document with the same content as the bottom
			topEditor.Document.SetText(originalSnapshot.Text);
        }
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnUpdateSelectionButtonClick(object sender, RoutedEventArgs e) {
			ITextSnapshot currentSnapshot = bottomEditor.ActiveView.CurrentSnapshot;
			TextRange textRange = topEditor.ActiveView.Selection.TextRange.Translate(originalSnapshot, currentSnapshot, TextRangeTrackingModes.Default);
			bottomEditor.ActiveView.Selection.TextRange = textRange;
			bottomEditor.Focus();
		}

    }

}