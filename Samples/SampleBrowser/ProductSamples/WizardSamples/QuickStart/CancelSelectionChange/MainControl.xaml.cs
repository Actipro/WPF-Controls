using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using ActiproSoftware.Windows.Controls.Wizard;

namespace ActiproSoftware.ProductSamples.WizardSamples.Demo.CancelSelectionChange {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl : System.Windows.Controls.UserControl {
		
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
		/// Occurs after the wizard's selected page has changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">Event arguments.</param>
		private void wizard_SelectedPageChanged(object sender, WizardSelectedPageChangeEventArgs e) {
			if (e.NewSelectedPage == cancelSelectionChangePage) {
				// Update the selection flags TextBlock to indicate what flags were used in selecting this page
				selectionFlagsTextBlock.Text = e.SelectionFlags.ToString();
			}
		}

		/// <summary>
		/// Occurs before the wizard's selected page has changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">Event arguments.</param>
		private void wizard_SelectedPageChanging(object sender, WizardSelectedPageChangeEventArgs e) {
			if (e.OldSelectedPage == cancelSelectionChangePage) {
				// If the cancel selection change CheckBox is checked, cancel the selection change
				if (cancelSelectionChangeCheckBox.IsChecked == true) {
					MessageBox.Show("The selected page change is cancelled because you have the CheckBox set.  Clear the CheckBox to be able to navigate through the wizard again.", "Wizard Sample");
					e.Cancel = true;
				}
			}
		}

	}
}