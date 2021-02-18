using ActiproSoftware.ProductSamples.GridsSamples.Common;

#if WINRT
using ActiproSoftware.UI.Xaml.Controls.Grids;
#else
using ActiproSoftware.Windows.Controls.Grids;
#endif

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.TreeListBoxThreeStateChecking {

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
		/// Occurs before the default action is executed for an item.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>TreeListBoxItemEventArgs</c> that contains the event data.</param>
		private void OnTreeListBoxItemDefaultActionExecuting(object sender, TreeListBoxItemEventArgs e) {
			var model = e.Item as CheckableTreeNodeModel;
			if ((model != null) && (model.IsCheckable) && (model.Children.Count == 0)) {
				e.Cancel = true;

				// Toggle the checked state
				if (model.IsChecked == true)
					model.IsChecked = false;
				else
					model.IsChecked = true;
			}
		}
		
	}

}
