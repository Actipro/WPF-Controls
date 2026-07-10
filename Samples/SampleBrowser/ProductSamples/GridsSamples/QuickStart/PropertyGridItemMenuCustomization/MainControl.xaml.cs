using ActiproSoftware.Windows.Controls.Grids;
using ActiproSoftware.Windows.Controls.Grids.PropertyData;
using ActiproSoftware.Windows.Input;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridItemMenuCustomization;

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
	/// Appends text to the property model's value.
	/// </summary>
	/// <param name="propertyModel">The model to update.</param>
	private void AppendText(IPropertyModel? propertyModel) {
		if (propertyModel is not null)
			propertyModel.Value = propertyModel.Value + " Foo";
	}

	/// <summary>
	/// Occurs when an item requests a context menu.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnPropertyGridItemMenuRequested(object sender, TreeListBoxItemMenuEventArgs e) {
		switch (e.Item) {
			case IPropertyModel propertyModel: {
				if (propertyModel.ValueType == typeof(Color)) {
					if (e.Menu is null)
						e.Menu = new ContextMenu();
					else
						e.Menu.Items.Add(new Separator());

					var yellowMenuItem = new MenuItem {
						Header = "Set Color to Yellow (custom menu item)",
						Command = new DelegateCommand<IPropertyModel>(SetColorToYellow),
						CommandParameter = propertyModel
					};
					e.Menu.Items.Add(yellowMenuItem);
				}
				else if (propertyModel.ValueType == typeof(string)) {
					if (e.Menu is null)
						e.Menu = new ContextMenu();
					else
						e.Menu.Items.Add(new Separator());

					var appendMenuItem = new MenuItem {
						Header = "Append 'Foo' Text (custom menu item)",
						Command = new DelegateCommand<IPropertyModel>(AppendText),
						CommandParameter = propertyModel
					};
					e.Menu.Items.Add(appendMenuItem);
				}
				break;
			}
			case ICategoryModel categoryModel: {
				if (e.Menu is null)
					e.Menu = new ContextMenu();
				else
					e.Menu.Items.Add(new Separator());

				var toggleMenuItem = new MenuItem {
					Header = "Toggle Expansion (custom menu item)",
					Command = new DelegateCommand<IDataModel>(ToggleExpansion),
					CommandParameter = categoryModel
				};
				e.Menu.Items.Add(toggleMenuItem);
				break;
			}
		}
	}

	/// <summary>
	/// Sets the property model's value to color yellow.
	/// </summary>
	/// <param name="propertyModel">The model to update.</param>
	private void SetColorToYellow(IPropertyModel? propertyModel) {
		if (propertyModel is not null)
			propertyModel.Value = Colors.Yellow;
	}

	/// <summary>
	/// Toggles the model's expansion.
	/// </summary>
	/// <param name="propertyModel">The model to update.</param>
	private void ToggleExpansion(IDataModel? model) {
		if (model is not null)
			model.IsExpanded = !model.IsExpanded;
	}

}
