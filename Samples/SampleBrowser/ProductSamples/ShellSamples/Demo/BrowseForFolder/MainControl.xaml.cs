using ActiproSoftware.Windows.Controls.Shell;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.ShellSamples.Demo.BrowseForFolder;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

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

	private void OnCancelButtonClick(object sender, RoutedEventArgs e)
		=> MessageBox.Show("The dialog was canceled.");

	private void OnOkButtonClick(object sender, RoutedEventArgs e) {
		var selectedViewModel = treeListBox.SelectedItem as ShellObjectViewModel;
		if (selectedViewModel is not null)
			MessageBox.Show(string.Format("The '{0}' folder with parsing name '{1}' was selected.", selectedViewModel.Name, selectedViewModel.ParsingName));
		else
			MessageBox.Show("Nothing was selected.");
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override void NotifyUnloaded() {
		base.NotifyUnloaded();

		// Dispose any unmanaged resources held by the shell instances now that the UI is closing
		treeListBox.DisposeShellInstances();
	}

}
