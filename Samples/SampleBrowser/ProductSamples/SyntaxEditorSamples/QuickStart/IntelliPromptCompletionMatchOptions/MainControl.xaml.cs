using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IntelliPromptCompletionMatchOptions;

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

		if (isCaseSensitiveCheckBox.IsChecked.HasValue)
			provider.IsCaseSensitive = isCaseSensitiveCheckBox.IsChecked.Value;
		if (requiresExactCheckBox.IsChecked.HasValue)
			provider.RequiresExact = requiresExactCheckBox.IsChecked.Value;
		if (useAcronymsCheckBox.IsChecked.HasValue)
			provider.UseAcronyms = useAcronymsCheckBox.IsChecked.Value;
		if (useShorthandCheckBox.IsChecked.HasValue)
			provider.UseShorthand = useShorthandCheckBox.IsChecked.Value;
		if (highlightMatchesCheckBox.IsChecked.HasValue)
			provider.CanHighlightMatchedText = highlightMatchesCheckBox.IsChecked.Value;
	}

}
