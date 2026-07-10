namespace ActiproSoftware.ProductSamples.ShellSamples.QuickStart.ShellTreeListBoxIntro;

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
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override void NotifyUnloaded() {
		base.NotifyUnloaded();

		// Dispose any unmanaged resources held by the shell instances now that the UI is closing
		treeListBox.DisposeShellInstances();
	}

}
