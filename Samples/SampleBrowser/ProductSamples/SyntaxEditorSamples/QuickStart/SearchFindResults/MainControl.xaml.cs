using System;
using System.Text;
using System.Windows;
using System.Windows.Input;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Searching;
using ActiproSoftware.Windows.Controls.SyntaxEditor;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.SearchFindResults {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl : System.Windows.Controls.UserControl {

		private ISearchResultSet lastResultSet;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			// Load a language from a language definition
			editor.Document.Language = ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common.SyntaxEditorHelper.LoadLanguageDefinitionFromResourceStream("CSharp.langdef");
        }
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when a search operation occurs in a view.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="EditorViewSearchEventArgs"/> that contains the event data.</param>
		private void OnEditorViewSearch(object sender, EditorViewSearchEventArgs e) {
			this.UpdateResults(e.ResultSet);
		}

		/// <summary>
		/// Occurs when the mouse is double-clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="MouseButtonEventArgs"/> that contains the event data.</param>
		private void OnResultsTextBoxDoubleClick(object sender, MouseButtonEventArgs e) {
			// Quit if there is not result set stored yet
			if (lastResultSet == null)
				return;

			int charIndex = resultsTextBox.GetCharacterIndexFromPoint(e.GetPosition(resultsTextBox), true);
			int lineIndex = resultsTextBox.GetLineIndexFromCharacterIndex(charIndex);

			int resultIndex = lineIndex - 1;  // Account for first line in results displaying search info
			if ((resultIndex >= 0) && (resultIndex < lastResultSet.Results.Count)) {
				// A valid result was clicked
				ISearchResult result = lastResultSet.Results[resultIndex];
				if (result.ReplaceSnapshotRange.IsDeleted) {
					// Find result
					editor.ActiveView.Selection.SelectRange(result.FindSnapshotRange.TranslateTo(editor.ActiveView.CurrentSnapshot, TextRangeTrackingModes.Default).TextRange);
				}
				else {
					// Replace result
					editor.ActiveView.Selection.SelectRange(result.ReplaceSnapshotRange.TranslateTo(editor.ActiveView.CurrentSnapshot, TextRangeTrackingModes.Default).TextRange);
				}

				// Focus the editor
				editor.Focus();
			}
		}

		/// <summary>
		/// Updates the results.
		/// </summary>
		/// <param name="resultSet">The <see cref="ISearchResultSet"/> containing results.</param>
		private void UpdateResults(ISearchResultSet resultSet) {
			// Show the results
			resultsToolWindow.Title = String.Format("Find Results - {0} match{1}", resultSet.Results.Count, (resultSet.Results.Count == 1 ? String.Empty : "es"));
			resultsTextBox.Text = resultSet.ToString();

			// Save the result set
			lastResultSet = resultSet;
		}
    }

}