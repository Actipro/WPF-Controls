namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GoToLineOverlay;

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

		// Add a command binding to action
		var goToLineAction = new GoToLineAction();
		var commandBinding = goToLineAction.CreateCommandBinding();
		editor.CommandBindings.Insert(0, commandBinding);

		// Bind to Ctrl+G
		var inputBinding = new KeyBinding(goToLineAction, Key.G, ModifierKeys.Control);
		editor.InputBindings.Add(inputBinding);
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnShowOverlayButtonClick(object sender, RoutedEventArgs e) {
		// Show the overlay for the active editor view
		GoToLineOverlayPane.Show(editor.ActiveView);
	}

}
