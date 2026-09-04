using ActiproSoftware.Windows.Controls.Grids.PropertyData;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridSorting;

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

	private void OnToggleSortOrderButtonClick(object sender, RoutedEventArgs e) {
		var categoryModel = propGrid.Items.OfType<ICategoryModel>().FirstOrDefault(m => m.DisplayName == "B");
		if (categoryModel is { SortComparer: NumericValueComparer comparer })
			categoryModel.SortComparer = new NumericValueComparer() { SortDescending = !comparer.SortDescending };
	}

}
