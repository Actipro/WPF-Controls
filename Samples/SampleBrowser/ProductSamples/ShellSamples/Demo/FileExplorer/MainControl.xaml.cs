namespace ActiproSoftware.ProductSamples.ShellSamples.Demo.FileExplorer;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnPathTextBoxKeyDown(object sender, KeyEventArgs e) {
		if (e.Key == Key.Enter) {
			var expression = BindingOperations.GetBindingExpression(pathTextBox, TextBox.TextProperty);
			expression?.UpdateSource();
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override void NotifyUnloaded() {
		base.NotifyUnloaded();

		// Dispose any unmanaged resources held by the shell instances now that the UI is closing
		treeListBox.DisposeShellInstances();
		listView.DisposeShellInstances();
	}

}
