using ActiproSoftware.SampleBrowser;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.Demo.SdiCodeEditor;

/// <summary>
/// Provides the main window for this sample.
/// </summary>
public partial class MainWindow : Window {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainWindow() {
		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void OnClosed(EventArgs e) {
		var previousSample = Content as ProductItemControl;
		previousSample?.NotifyUnloaded();

		base.OnClosed(e);
	}

}
