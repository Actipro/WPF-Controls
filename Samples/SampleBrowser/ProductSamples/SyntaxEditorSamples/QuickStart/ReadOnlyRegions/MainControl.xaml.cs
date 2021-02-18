using System.Windows;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.ReadOnlyRegions {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl : System.Windows.Controls.UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			// Load a language from a language definition
			ISyntaxLanguage language = ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common.SyntaxEditorHelper.LoadLanguageDefinitionFromResourceStream("CSharp.langdef");

			// Attach a custom read-only region tagger to the language (use a singleton key so it can be retrieved later)
			language.RegisterService(new CodeDocumentTaggerProvider<CustomReadOnlyRegionTagger>(typeof(CustomReadOnlyRegionTagger)));

			// Assign the language to the document
			editor.Document.Language = language;
        }
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when the checkbox is checked or unchecked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnHighlightRegionsCheckBoxCheckedChanged(object sender, RoutedEventArgs e) {
			CustomReadOnlyRegionTagger tagger;
			if ((editor != null) && (editor.Document.Properties.TryGetValue(typeof(CustomReadOnlyRegionTagger), out tagger)))
				tagger.HighlightReadOnlyRegions = highlightRegionsCheckBox.IsChecked.Value;
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnMakeSelectionReadOnlyButtonClick(object sender, RoutedEventArgs e) {
			CustomReadOnlyRegionTagger tagger;
			if ((editor != null) && (editor.Document.Properties.TryGetValue(typeof(CustomReadOnlyRegionTagger), out tagger))) {
				tagger.Clear();
				if (editor.ActiveView.Selection.Length > 0)
					tagger.Add(editor.ActiveView.Selection.SnapshotRange, new ReadOnlyRegionTag());
			}
		}

    }

}