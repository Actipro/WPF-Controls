using System.Windows;
using System.Windows.Controls;
using ActiproSoftware.Windows.Controls.Docking;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.HostedFloatingWindowFade {

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
					this.CreateAndFloatToolWindow();
			};
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates and floats a tool window.
		/// </summary>
		private void CreateAndFloatToolWindow() {
			var textBox = new TextBox() {
				BorderThickness = new Thickness(),
				Text = "This floating window will fade out when it loses focus.",
				TextWrapping = TextWrapping.Wrap
			};

			var toolWindow = new ToolWindow(dockSite, "tool", "Floating Tool Window", null, textBox);
			toolWindow.Float(new Point(400, 200));
			toolWindow.Activate();
		}

	}

}
