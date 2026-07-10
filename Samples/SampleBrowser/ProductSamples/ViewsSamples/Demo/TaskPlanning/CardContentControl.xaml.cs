namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.TaskPlanning;

/// <summary>
/// Provides the user control for a card.
/// </summary>
public partial class CardContentControl : UserControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public CardContentControl() {
		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnDeleteTaskMenuItemClick(object sender, RoutedEventArgs e) {
		var task = DataContext as TaskModel;
		task?.DeleteTaskCommand.Execute(parameter: null);
	}

	private void OnSetLabelColorMenuItemClick(object sender, RoutedEventArgs e) {
		var menuItem = (MenuItem)sender;
		var task = DataContext as TaskModel;
		task?.SetLabelColorCommand.Execute(menuItem.CommandParameter.ToString());
	}

}
