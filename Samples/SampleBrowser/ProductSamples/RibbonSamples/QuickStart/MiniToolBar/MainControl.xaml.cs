using ActiproSoftware.Windows.Controls.Ribbon.UI;
using System.Windows;
using System.Windows.Input;
using RibbonControls = ActiproSoftware.Windows.Controls.Ribbon.Controls;

namespace ActiproSoftware.ProductSamples.RibbonSamples.QuickStart.MiniToolBar {

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
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the mouse button is released.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The event data for the <see cref="UIElement.MouseUp"/> event.</param>
		private void OnPreviewMouseUp(object sender, MouseButtonEventArgs e) {
			// If a selection was just made with the mouse...
			if ((e.ChangedButton == MouseButton.Left) && (!editor.Selection.IsEmpty)) {
				// Get the mini-toolbar in the resources of this UserControl
				RibbonControls.MiniToolBar toolBar = (RibbonControls.MiniToolBar)this.FindResource("SimpleMiniToolBar");
				if (toolBar != null) {
					// Show the mini-toolbar
					MiniToolBarService.Show(toolBar, editor, e.GetPosition(editor));
				}
			}
		}
		
	}
}