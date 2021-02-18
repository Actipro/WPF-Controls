using System.Windows;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IntelliPromptCompletionFiltering {

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
			editor.Document.Language = ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common.SyntaxEditorHelper.LoadLanguageDefinitionFromResourceStream("CSharp.langdef");

			// Register a custom completion provider on the language used by the editor
			editor.Document.Language.RegisterService<ICompletionProvider>(new CustomCompletionProvider());
		
			// Update the completion provider options.
			this.UpdateCompletionProviderOptions();
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Invoked when the checked state changes for any of the <see cref="CheckBox"/> controls in the sample.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void OnCheckedChanged(object sender, RoutedEventArgs e) {
			this.UpdateCompletionProviderOptions();
		}

		/// <summary>
		/// Updates the completion provider settings.
		/// </summary>
		private void UpdateCompletionProviderOptions() {
			if (editor == null)
				return;

			var provider = editor.Document.Language.GetService<ICompletionProvider>() as CustomCompletionProvider;
			if (provider == null)
				return;

			if (filterTabsVisibleCheckBox.IsChecked.HasValue)
				provider.FilterTabsVisible = filterTabsVisibleCheckBox.IsChecked.Value;
			if (filterUnmatchedItemsCheckBox.IsChecked.HasValue)
				provider.FilterUnmatchedItems = filterUnmatchedItemsCheckBox.IsChecked.Value;
			if (inheritedFilterButtonVisibleCheckBox.IsChecked.HasValue)
				provider.InheritedFilterButtonVisible = inheritedFilterButtonVisibleCheckBox.IsChecked.Value;
			if (memberTypeFilterButtonsVisibleCheckBox.IsChecked.HasValue)
				provider.MemberTypeFilterButtonsVisible = memberTypeFilterButtonsVisibleCheckBox.IsChecked.Value;
		}

	}

}