using System;
using System.Windows;
using System.Windows.Controls;

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.TaskPlanning {

	/// <summary>
	/// Provides the user control for adding a list.
	/// </summary>
	public partial class AddListControl : UserControl {

		private bool isAddMode;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>AddListControl</c> class.
		/// </summary>
		public AddListControl() {
			InitializeComponent();
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains data related to the event.</param>
		private void OnAddListButtonClick(object sender, RoutedEventArgs e) {
			nameTextBox.Text = "New List";

			this.IsAddMode = true;

			nameTextBox.SelectAll();
			nameTextBox.Focus();
		}
		
		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains data related to the event.</param>
		private void OnCancelButtonClick(object sender, RoutedEventArgs e) {
			this.IsAddMode = false;
		}
		
		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains data related to the event.</param>
		private void OnSaveButtonClick(object sender, RoutedEventArgs e) {
			var board = this.DataContext as TaskBoardModel;
			if (board != null)
				board.AddListCommand.Execute(nameTextBox.Text);

			this.IsAddMode = false;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets whether the control is in add mode.
		/// </summary>
		/// <value>
		/// <c>true</c> if the control is in add mode.
		/// </value>
		public bool IsAddMode {
			get {
				return isAddMode;
			}
			set {
				if (isAddMode == value)
					return;

				isAddMode = value;

				addListButton.Visibility = (isAddMode ? Visibility.Collapsed : Visibility.Visible);
				inputPanel.Visibility = (isAddMode ? Visibility.Visible : Visibility.Collapsed);
			}
		}

	}

}
