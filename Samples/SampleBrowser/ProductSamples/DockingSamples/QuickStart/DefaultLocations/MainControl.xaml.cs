using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Docking;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.DefaultLocations {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			this.Loaded += (sender, e) => {
				if (dockSite.ToolWindows.Count == 0)
					this.OpenToolWindows();
			};
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when a docking window's default location is requested.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowDefaultLocationEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteWindowDefaultLocationRequested(object sender, DockingWindowDefaultLocationEventArgs e) {
			if (e.Window.SerializationId == "bottomLeft1") {
				if (e.State == DockingWindowState.Docked) {
					// Dock in hierarchy under the left tool window
					var targetToolWindow = dockSite.ToolWindows.FirstOrDefault(tw => tw.SerializationId == "left1");
					if ((targetToolWindow != null) && (targetToolWindow.IsOpen)) {
						e.Target = targetToolWindow;
						e.Side = Side.Bottom;
					}
				}
			}
		}

		/// <summary>
		/// Opens the tool windows for this sample.
		/// </summary>
		private void OpenToolWindows() {
			var toolWindow = new ToolWindow(dockSite, "right1", "Tool Window 1", null,
				new TextBox() { BorderThickness = new Thickness(0), TextWrapping = TextWrapping.Wrap, Text = "This first tool window has no default dock side set, and will fall back to docking on the right side of the primary dock host." });
			toolWindow.WindowGroupName = "Right Group";
			toolWindow.ContainerDockedSize = new Size(150, 200);
			toolWindow.Activate(false);

			toolWindow = new ToolWindow(dockSite, "bottom1", "Tool Window 2", null,
				new TextBox() { BorderThickness = new Thickness(0), TextWrapping = TextWrapping.Wrap, Text = "This second tool window has DefaultDockSide == Bottom and will default to open at the bottom of the primary dock host." });
			toolWindow.DefaultDockSide = Side.Bottom;
			toolWindow.WindowGroupName = "Bottom Group";
			toolWindow.ContainerDockedSize = new Size(200, 150);
			toolWindow.Activate(false);

			toolWindow = new ToolWindow(dockSite, "bottom2", "Tool Window 3", null,
				new TextBox() { BorderThickness = new Thickness(0), TextWrapping = TextWrapping.Wrap, Text = "This third tool window has no default dock side set, but is in the same WindowGroupName as 'Tool Window 2', and will default to attach to it." });
			toolWindow.WindowGroupName = "Bottom Group";
			toolWindow.ContainerDockedSize = new Size(200, 150);
			toolWindow.Activate(false);

			toolWindow = new ToolWindow(dockSite, "right2", "Tool Window 4", null,
				new TextBox() { BorderThickness = new Thickness(0), TextWrapping = TextWrapping.Wrap, Text = "This fourth tool window has DefaultDockSide == Bottom, but is in the same WindowGroupName as 'Tool Window 1', and will default to attach to it because that takes priority over DefaultDockSide." });
			toolWindow.DefaultDockSide = Side.Bottom;
			toolWindow.WindowGroupName = "Right Group";
			toolWindow.ContainerDockedSize = new Size(150, 200);
			toolWindow.Activate(false);
			
			toolWindow = new ToolWindow(dockSite, "left1", "Tool Window 5", null,
				new TextBox() { BorderThickness = new Thickness(0), TextWrapping = TextWrapping.Wrap, Text = "This fifth tool window specifies the same parameters as 'Tool Window 4' but also has a DefaultLocationRequested event handler that overrides everything by forcing a left side dock." });
			toolWindow.DefaultDockSide = Side.Bottom;
			toolWindow.WindowGroupName = "Right Group";
			toolWindow.ContainerDockedSize = new Size(150, 200);
			toolWindow.DefaultLocationRequested += (sender, e) => {
				if (e.State == DockingWindowState.Docked) {
					// Force a left side dock
					e.Target = null;
					e.Side = Side.Left;
				}
			};
			toolWindow.Activate(false);

			toolWindow = new ToolWindow(dockSite, "bottomLeft1", "Tool Window 6", null,
				new TextBox() { BorderThickness = new Thickness(0), TextWrapping = TextWrapping.Wrap, Text = "This sixth tool window's default location is set in a generalized DockSite.WindowDefaultLocationRequested event handler." });
			toolWindow.ContainerDockedSize = new Size(150, 200);
			toolWindow.Activate(false);
		}

	}

}
