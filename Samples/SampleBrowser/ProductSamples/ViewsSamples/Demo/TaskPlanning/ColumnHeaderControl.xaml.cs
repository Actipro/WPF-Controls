namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.TaskPlanning;

/// <summary>
/// Provides the user control for a column header.
/// </summary>
public partial class ColumnHeaderControl : UserControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public ColumnHeaderControl() {
		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnDeleteListMenuItemClick(object sender, RoutedEventArgs e) {
		var list = DataContext as TaskListModel;
		list?.DeleteListCommand.Execute(parameter: null);
	}

	private void OnDuplicateListMenuItemClick(object sender, RoutedEventArgs e) {
		var list = DataContext as TaskListModel;
		list?.DuplicateListCommand.Execute(parameter: null);
	}

}
