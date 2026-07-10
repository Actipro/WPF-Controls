using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.HighlightingStyleViewer;

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

		// Create a custom Console Window registry
		var consoleWindowRegistry = new HighlightingStyleRegistry {
			Description = "Console Window"
		};
		console.HighlightingStyleRegistry = consoleWindowRegistry;

		// Register the default display item classification types on the ambient and custom registries
		new BuiltInClassificationTypeProvider().RegisterAll();
		new BuiltInClassificationTypeProvider(consoleWindowRegistry).RegisterAll();

		// Populate the registry combobox
		registryComboBox.Items.Add(AmbientHighlightingStyleRegistry.Instance);
		registryComboBox.Items.Add(consoleWindowRegistry);
		registryComboBox.SelectedIndex = 0;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnRegistryComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e) {
		if (classificationTypeListBox.Items.Count > 0)
			classificationTypeListBox.SelectedIndex = 0;
	}

}
