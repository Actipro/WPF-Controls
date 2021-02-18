using ActiproSoftware.ProductSamples.GridsSamples.Common;

#if WINRT
using Windows.UI.Xaml;
using ActiproSoftware.UI.Xaml.Controls.Grids;
#else
using System.Windows;
using ActiproSoftware.Windows.Controls.Grids;
#endif

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.TreeListBoxChecking {

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
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>RoutedEventArgs</c> that contains the event data.</param>
		private void OnCheckAllButtonClick(object sender, RoutedEventArgs e) {
			foreach (CheckableTreeNodeModel model in treeListBox.Items)
				SetIsCheckedRecursive(model, true);
		}

		/// <summary>
		/// Occurs before the default action is executed for an item.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>TreeListBoxItemEventArgs</c> that contains the event data.</param>
		private void OnTreeListBoxItemDefaultActionExecuting(object sender, TreeListBoxItemEventArgs e) {
			var model = e.Item as CheckableTreeNodeModel;
			if ((model != null) && (model.IsCheckable)) {
				e.Cancel = true;

				// Toggle the checked state
				model.IsChecked = !model.IsChecked;
			}
		}
		
		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>RoutedEventArgs</c> that contains the event data.</param>
		private void OnUncheckAllButtonClick(object sender, RoutedEventArgs e) {
			foreach (CheckableTreeNodeModel model in treeListBox.Items)
				SetIsCheckedRecursive(model, false);
		}

		/// <summary>
		/// Recursively sets nodes as checked or unchecked.
		/// </summary>
		/// <param name="model">The <see cref="CheckableTreeNodeModel"/> to update.</param>
		/// <param name="isChecked">Whether the model is checked.</param>
		private static void SetIsCheckedRecursive(CheckableTreeNodeModel model, bool isChecked) {
			if (model.IsCheckable)
				model.IsChecked = isChecked;

			foreach (CheckableTreeNodeModel childModel in model.Children)
				SetIsCheckedRecursive(childModel, isChecked);
		}

	}

}
