using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IntelliPromptCompletionFiltering;

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

		// Load a language from a language definition
		editor.Document.Language = Common.SyntaxEditorHelper.LoadLanguageDefinitionFromResourceStream("CSharp.langdef");

		// Register a custom completion provider on the language used by the editor
		editor.Document.Language.RegisterService<ICompletionProvider>(new CustomCompletionProvider());

		// Update the completion provider options.
		UpdateCompletionProviderOptions();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnCheckedChanged(object sender, RoutedEventArgs e)
		=> UpdateCompletionProviderOptions();

	/// <summary>
	/// Updates the completion provider settings.
	/// </summary>
	private void UpdateCompletionProviderOptions() {
		var provider = editor?.Document.Language.GetService<ICompletionProvider>() as CustomCompletionProvider;
		if (provider is null)
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
