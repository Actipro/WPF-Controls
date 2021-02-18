using System.Windows;
using System.Windows.Controls;

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.TaskPlanning {

	/// <summary>
	/// Provides the user control for a column header.
	/// </summary>
	public partial class ColumnHeaderControl : UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>ColumnHeaderControl</c> class.
		/// </summary>
		public ColumnHeaderControl() {
			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> containing data related to this event.</param>
		private void OnDeleteListMenuItemClick(object sender, RoutedEventArgs e) {
			var list = this.DataContext as TaskListModel;
			if (list != null)
				list.DeleteListCommand.Execute(null);
		}
		
		/// <summary>
		/// Occurs when the menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> containing data related to this event.</param>
		private void OnDuplicateListMenuItemClick(object sender, RoutedEventArgs e) {
			var list = this.DataContext as TaskListModel;
			if (list != null)
				list.DuplicateListCommand.Execute(null);
		}
		
	}

}
