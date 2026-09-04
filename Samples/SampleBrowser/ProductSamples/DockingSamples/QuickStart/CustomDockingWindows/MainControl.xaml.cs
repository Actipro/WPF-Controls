using ActiproSoftware.Windows.Controls.Docking;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.CustomDockingWindows;

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

	/// <summary>
	/// Indicates whether to use tabbed MDI.
	/// </summary>
	public bool UseTabbedMdi {
		get => dockSite.MdiKind == MdiKind.Tabbed;
		set => dockSite.MdiKind = (value ? MdiKind.Tabbed : MdiKind.Standard);
	}

}
