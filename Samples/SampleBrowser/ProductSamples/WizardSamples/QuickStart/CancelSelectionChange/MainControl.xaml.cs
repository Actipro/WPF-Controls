using ActiproSoftware.Windows.Controls.Wizard;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.WizardSamples.Demo.CancelSelectionChange;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl : UserControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnWizardSelectedPageChanged(object sender, WizardSelectedPageChangeEventArgs e) {
		if (e.NewSelectedPage == cancelSelectionChangePage) {
			// Update the selection flags TextBlock to indicate what flags were used in selecting this page
			selectionFlagsTextBlock.Text = e.SelectionFlags.ToString();
		}
	}

	private void OnWizardSelectedPageChanging(object sender, WizardSelectedPageChangeEventArgs e) {
		if (e.OldSelectedPage == cancelSelectionChangePage) {
			// If the cancel selection change CheckBox is checked, cancel the selection change
			if (cancelSelectionChangeCheckBox.IsChecked == true) {
				MessageBox.Show("The selected page change is cancelled because you have the CheckBox set.  Clear the CheckBox to be able to navigate through the wizard again.", "Wizard Sample");
				e.Cancel = true;
			}
		}
	}

}
