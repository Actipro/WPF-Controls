using System.Windows.Controls;
using System.Windows.Media;
using ActiproSoftware.Windows.Controls.Grids;
using ActiproSoftware.Windows.Controls.Grids.PropertyData;
using ActiproSoftware.Windows.Input;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridItemMenuCustomization {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Appends text to the property model's value.
		/// </summary>
		/// <param name="propertyModel">The model to update.</param>
		private void AppendText(IPropertyModel propertyModel) {
			propertyModel.Value = (string)propertyModel.Value + " Foo";
		}

		/// <summary>
		/// Occurs when an item requests a context menu. 
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>TreeListBoxItemMenuEventArgs</c> that contains the event data.</param>
		private void OnPropertyGridItemMenuRequested(object sender, TreeListBoxItemMenuEventArgs e) {
			var propertyModel = e.Item as IPropertyModel;
			if (propertyModel != null) {
				if (propertyModel.ValueType == typeof(Color)) {
					if (e.Menu == null)
						e.Menu = new ContextMenu();
					else
						e.Menu.Items.Add(new Separator());

					var yellowMenuItem = new MenuItem();
					yellowMenuItem.Header = "Set Color to Yellow (custom menu item)";
					yellowMenuItem.Command = new DelegateCommand<IPropertyModel>(SetColorToYellow);
					yellowMenuItem.CommandParameter = propertyModel;
					e.Menu.Items.Add(yellowMenuItem);
				}
				else if (propertyModel.ValueType == typeof(string)) {
					if (e.Menu == null)
						e.Menu = new ContextMenu();
					else
						e.Menu.Items.Add(new Separator());

					var appendMenuItem = new MenuItem();
					appendMenuItem.Header = "Append 'Foo' Text (custom menu item)";
					appendMenuItem.Command = new DelegateCommand<IPropertyModel>(AppendText);
					appendMenuItem.CommandParameter = propertyModel;
					e.Menu.Items.Add(appendMenuItem);
				}
			}
			else {
				var categoryModel = e.Item as ICategoryModel;
				if (categoryModel != null) {
					if (e.Menu == null)
						e.Menu = new ContextMenu();
					else
						e.Menu.Items.Add(new Separator());

					var toggleMenuItem = new MenuItem();
					toggleMenuItem.Header = "Toggle Expansion (custom menu item)";
					toggleMenuItem.Command = new DelegateCommand<IDataModel>(ToggleExpansion);
					toggleMenuItem.CommandParameter = categoryModel;
					e.Menu.Items.Add(toggleMenuItem);
				}
			}
		}

		/// <summary>
		/// Sets the property model's value to color yellow.
		/// </summary>
		/// <param name="propertyModel">The model to update.</param>
		private void SetColorToYellow(IPropertyModel propertyModel) {
			propertyModel.Value = Colors.Yellow;
		}

		/// <summary>
		/// Toggles the model's expansion.
		/// </summary>
		/// <param name="propertyModel">The model to update.</param>
		private void ToggleExpansion(IDataModel model) {
			model.IsExpanded = !model.IsExpanded;
		}

	}

}
