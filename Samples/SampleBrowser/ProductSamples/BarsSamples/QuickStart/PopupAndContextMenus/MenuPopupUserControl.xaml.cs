using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Primitives;
using System.Media;
using System.Windows;
using System.Windows.Controls;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.PopupAndContextMenus {

	/// <summary>
	/// Represents a set of controls that can be used within a menu context.
	/// </summary>
	public partial class MenuPopupUserControl : UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="MenuPopupUserControl"/> class.
		/// </summary>
		public MenuPopupUserControl() {
			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnCancelButtonClick(object sender, RoutedEventArgs e) {
			ResetFields();
		}
		
		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnSaveButtonClick(object sender, RoutedEventArgs e) {
			if (string.IsNullOrEmpty(nameTextBox.Text)
				|| (positionComboBox.SelectedIndex == -1)
				|| string.IsNullOrEmpty(emailTextBox.Text)) {

				// Playing a system sound to indicate error cause showing a MessageBox or other dialog
				// will trigger the popup to close
				SystemSounds.Exclamation.Play();
				return;
			}

			// IMPORTANT: The Save button is not configured to close popups on click so that it stays open if invalid
			// data is entered. Showing a message box will trigger all popups to close automatically, but it must be
			// manually closed if the confirmation message is not displayed.
			// This same approach would also be necessary if using a standard Button control (not BarButton) since those
			// will not have built-in support for closing popups.
			if (showConfirmationCheckBox.IsChecked == true) {
				ThemedMessageBox.Show($"Data saved that includes...\r\n\r\nName: {nameTextBox.Text}\r\nPosition: {positionComboBox.SelectedValue}\r\nE-mail address: {emailTextBox.Text}", 
					"Save Record", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			else {
				// Close all popups since the current operation is complete
				PopupManager.CloseAllPopups();

				// NOTE: For more advanced popup control, use PopupManager.TopmostPopupAnchor property
				// to access the control that opened this popup. Various methods on PopupManager are
				// available to close a specific popup anchor, focus it, re-open it, etc.
			}

			ResetFields();
		}

		/// <summary>
		/// Resets the fields.
		/// </summary>
		private void ResetFields() {
			nameTextBox.Text = null;
			positionComboBox.SelectedIndex = -1;
			emailTextBox.Text = null;
		}

	}

}
