using System.Windows;
using System.Windows.Controls;

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.TaskPlanning {

	/// <summary>
	/// Provides the user control for a card.
	/// </summary>
	public partial class CardContentControl : UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>CardContentControl</c> class.
		/// </summary>
		public CardContentControl() {
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
		private void OnDeleteTaskMenuItemClick(object sender, RoutedEventArgs e) {
			var task = this.DataContext as TaskModel;
			if (task != null)
				task.DeleteTaskCommand.Execute(null);
		}
		
		/// <summary>
		/// Occurs when the menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> containing data related to this event.</param>
		private void OnSetLabelColorMenuItemClick(object sender, RoutedEventArgs e) {
			var menuItem = (MenuItem)sender;
			var task = this.DataContext as TaskModel;
			if (task != null)
				task.SetLabelColorCommand.Execute(menuItem.CommandParameter.ToString());
		}
		
	}

}
