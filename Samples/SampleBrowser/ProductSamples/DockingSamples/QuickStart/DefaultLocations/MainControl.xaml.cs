using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Docking;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.DefaultLocations;

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

		Loaded += (sender, e) => {
			if (dockSite.ToolWindows.Count == 0)
				OpenToolWindows();
		};
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Occurs when a docking window's default location is requested.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnDockSiteWindowDefaultLocationRequested(object sender, DockingWindowDefaultLocationEventArgs e) {
		if (
			e.Window?.SerializationId == "bottomLeft1"
			&& e.State == DockingWindowState.Docked
		) {
			// Dock in hierarchy under the left tool window
			var targetToolWindow = dockSite.ToolWindows.FirstOrDefault(tw => tw.SerializationId == "left1");
			if (targetToolWindow?.IsOpen == true) {
				e.Target = targetToolWindow;
				e.Side = Side.Bottom;
			}
		}
	}

	/// <summary>
	/// Opens the tool windows for this sample.
	/// </summary>
	private void OpenToolWindows() {
		var toolWindow = new ToolWindow(dockSite, "right1", "Tool Window 1", 
			content: new TextBox() { BorderThickness = new Thickness(0), TextWrapping = TextWrapping.Wrap, Text = "This first tool window has no default dock side set, and will fall back to docking on the right side of the primary dock host." }) {
			WindowGroupName = "Right Group",
			ContainerDockedSize = new Size(150, 200)
		};
		toolWindow.Activate(focus: false);

		toolWindow = new ToolWindow(dockSite, "bottom1", "Tool Window 2",
			content: new TextBox() { BorderThickness = new Thickness(0), TextWrapping = TextWrapping.Wrap, Text = "This second tool window has DefaultDockSide == Bottom and will default to open at the bottom of the primary dock host." }) {
			DefaultDockSide = Side.Bottom,
			WindowGroupName = "Bottom Group",
			ContainerDockedSize = new Size(200, 150)
		};
		toolWindow.Activate(focus: false);

		toolWindow = new ToolWindow(dockSite, "bottom2", "Tool Window 3",
			content: new TextBox() { BorderThickness = new Thickness(0), TextWrapping = TextWrapping.Wrap, Text = "This third tool window has no default dock side set, but is in the same WindowGroupName as 'Tool Window 2', and will default to attach to it." }) {
			WindowGroupName = "Bottom Group",
			ContainerDockedSize = new Size(200, 150)
		};
		toolWindow.Activate(focus: false);

		toolWindow = new ToolWindow(dockSite, "right2", "Tool Window 4",
			content: new TextBox() { BorderThickness = new Thickness(0), TextWrapping = TextWrapping.Wrap, Text = "This fourth tool window has DefaultDockSide == Bottom, but is in the same WindowGroupName as 'Tool Window 1', and will default to attach to it because that takes priority over DefaultDockSide." }) {
			DefaultDockSide = Side.Bottom,
			WindowGroupName = "Right Group",
			ContainerDockedSize = new Size(150, 200)
		};
		toolWindow.Activate(focus: false);

		toolWindow = new ToolWindow(dockSite, "left1", "Tool Window 5",
			content: new TextBox() { BorderThickness = new Thickness(0), TextWrapping = TextWrapping.Wrap, Text = "This fifth tool window specifies the same parameters as 'Tool Window 4' but also has a DefaultLocationRequested event handler that overrides everything by forcing a left side dock." }) {
			DefaultDockSide = Side.Bottom,
			WindowGroupName = "Right Group",
			ContainerDockedSize = new Size(150, 200)
		};
		toolWindow.DefaultLocationRequested += (sender, e) => {
			if (e.State == DockingWindowState.Docked) {
				// Force a left side dock
				e.Target = null;
				e.Side = Side.Left;
			}
		};
		toolWindow.Activate(focus: false);

		toolWindow = new ToolWindow(dockSite, "bottomLeft1", "Tool Window 6",
			content: new TextBox() { BorderThickness = new Thickness(0), TextWrapping = TextWrapping.Wrap, Text = "This sixth tool window's default location is set in a generalized DockSite.WindowDefaultLocationRequested event handler." }) {
			ContainerDockedSize = new Size(150, 200)
		};
		toolWindow.Activate(focus: false);
	}

}
