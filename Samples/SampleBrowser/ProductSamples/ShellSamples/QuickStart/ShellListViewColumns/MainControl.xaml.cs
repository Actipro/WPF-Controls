using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Shell;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.ShellSamples.QuickStart.ShellListViewColumns;

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

	private void OnListCheckedItemsButtonClick(object sender, RoutedEventArgs e) {
		var checkedItems = listView.Items.OfType<ShellObjectViewModel>().Where(vm => true.Equals(vm.Tag));
		var checkedItemNames = string.Join("\r\n", checkedItems.Select(vm => vm.Name).ToArray());
		if (string.IsNullOrEmpty(checkedItemNames))
			checkedItemNames = "(none)";

		MessageBox.Show(checkedItemNames, "Checked Items");
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override void NotifyUnloaded() {
		base.NotifyUnloaded();

		// Dispose any unmanaged resources held by the shell instances now that the UI is closing
		listView.DisposeShellInstances();
	}

}
