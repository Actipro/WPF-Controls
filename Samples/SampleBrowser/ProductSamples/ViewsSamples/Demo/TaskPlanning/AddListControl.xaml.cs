namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.TaskPlanning;

/// <summary>
/// Provides the user control for adding a list.
/// </summary>
public partial class AddListControl : UserControl {

	private bool _isAddMode;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public AddListControl() {
		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnAddListButtonClick(object sender, RoutedEventArgs e) {
		nameTextBox.Text = "New List";

		IsAddMode = true;

		nameTextBox.SelectAll();
		nameTextBox.Focus();
	}

	private void OnCancelButtonClick(object sender, RoutedEventArgs e)
		=> IsAddMode = false;

	private void OnSaveButtonClick(object sender, RoutedEventArgs e) {
		var board = DataContext as TaskBoardModel;
		board?.AddListCommand.Execute(nameTextBox.Text);

		IsAddMode = false;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Indicates whether the control is in add mode.
	/// </summary>
	public bool IsAddMode {
		get => _isAddMode;
		set {
			if (_isAddMode == value)
				return;

			_isAddMode = value;

			addListButton.Visibility = (_isAddMode ? Visibility.Collapsed : Visibility.Visible);
			inputPanel.Visibility = (_isAddMode ? Visibility.Visible : Visibility.Collapsed);
		}
	}

}
