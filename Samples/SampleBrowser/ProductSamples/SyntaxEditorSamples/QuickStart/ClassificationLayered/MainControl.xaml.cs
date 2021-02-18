using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.Text.Utility;
using ActiproSoftware.Windows.Media;
using ActiproSoftware.Windows.Themes;
using ActiproSoftware.Windows.Controls.Docking;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.ClassificationLayered {

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

			// Attach a custom classification tagger to the language (use a singleton key so it can be retrieved later)
			language.RegisterService(new CodeDocumentTaggerProvider<CustomClassificationTagger>(typeof(CustomClassificationTagger)));

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
		private void OnCommentsCheckBoxCheckedChanged(object sender, RoutedEventArgs e) {
			CustomClassificationTagger tagger;
			if ((editor != null) && (editor.Document.Properties.TryGetValue(typeof(CustomClassificationTagger), out tagger)))
				tagger.HighlightDocumentationComments = commentsCheckBox.IsChecked.Value;
		}

		/// <summary>
		/// Occurs when the checkbox is checked or unchecked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnIdentifierCheckBoxCheckedChanged(object sender, RoutedEventArgs e) {
			CustomClassificationTagger tagger;
			if ((editor != null) && (editor.Document.Properties.TryGetValue(typeof(CustomClassificationTagger), out tagger)))
				tagger.HighlightIdentifiers = identifierCheckBox.IsChecked.Value;
		}

    }

}