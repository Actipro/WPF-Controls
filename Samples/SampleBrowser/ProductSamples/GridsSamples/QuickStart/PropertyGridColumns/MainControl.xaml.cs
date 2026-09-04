using ActiproSoftware.Windows.Controls.Grids;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridColumns;

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

		InitializeAdditionalColumn();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an additional column in the property grid.
	/// </summary>
	private void InitializeAdditionalColumn() {
		var column = new TreeListViewColumn {
			CellBorderThickness = new Thickness(0, 0, 1, 0),
			CellPadding = new Thickness(3, 0, 3, 0),
			CellTemplate = FindResource("IsModifiedTemplate") as DataTemplate,
			MinWidth = 16,
			Width = GridLength.Auto
		};
		propGrid.Columns.Insert(1, column);
	}

}
