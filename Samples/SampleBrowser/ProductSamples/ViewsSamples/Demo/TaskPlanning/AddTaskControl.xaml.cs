using System;
using System.Windows;
using System.Windows.Controls;

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.TaskPlanning {

	/// <summary>
	/// Provides the user control for adding a task.
	/// </summary>
	public partial class AddTaskControl : UserControl {

		private bool isAddMode;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>AddTaskControl</c> class.
		/// </summary>
		public AddTaskControl() {
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
			nameTextBox.Text = "New task";

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
			var list = this.DataContext as TaskListModel;
			if (list != null)
				list.AddTaskCommand.Execute(nameTextBox.Text);

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
