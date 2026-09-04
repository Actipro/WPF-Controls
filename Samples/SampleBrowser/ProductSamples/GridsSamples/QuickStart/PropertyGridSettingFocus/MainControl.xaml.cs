using ActiproSoftware.Windows.Controls.Grids.PropertyData;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridSettingFocus;

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

	/// <summary>
	/// Focuses the property grid item with the specified <see cref="IDataModel.Name"/>.
	/// </summary>
	/// <typeparam name="T">The model type.</typeparam>
	/// <param name="name">The model name.</param>
	private void FocusModel<T>(string? name) where T : IDataModel {
		var propertyModel = propGrid.Items.OfType<T>().FirstOrDefault(p => p.Name == name);
		if (propertyModel is not null)
			propGrid.FocusItem(propertyModel);
	}

	/// <summary>
	/// Occurs when the button is clicked.
	/// </summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnFocusFirstPropertyButtonClick(object sender, RoutedEventArgs e) {
		var firstPropertyModel = propGrid.Items.OfType<IPropertyModel>().FirstOrDefault();
		if (firstPropertyModel is not null)
			FocusModel<IPropertyModel>(firstPropertyModel.Name);
	}

	/// <summary>
	/// Occurs when the button is clicked.
	/// </summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnFocusIsTabStopPropertyButtonClick(object sender, RoutedEventArgs e)
		=> FocusModel<IPropertyModel>(nameof(IsTabStop));

	/// <summary>
	/// Occurs when the button is clicked.
	/// </summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnFocusMiscCategoryButtonClick(object sender, RoutedEventArgs e)
		=> FocusModel<ICategoryModel>(propGrid.MiscCategoryName);

	/// <summary>
	/// Occurs when the button is clicked.
	/// </summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnFocusToolTipPropertyButtonClick(object sender, RoutedEventArgs e)
		=> FocusModel<IPropertyModel>(nameof(ToolTip));

}
