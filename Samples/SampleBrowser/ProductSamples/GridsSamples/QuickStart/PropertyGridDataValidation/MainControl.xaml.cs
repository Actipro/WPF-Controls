using ActiproSoftware.Windows.Controls.Grids;
using ActiproSoftware.Windows.Controls.Grids.PropertyData;
using ActiproSoftware.Windows.Media;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridDataValidation;

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
	/// Called when <c>Validation.Error</c> is fired on the PropertyGrid or one it's descendants.
	/// </summary>
	/// <param name="sender">The sender.</param>
	/// <param name="e">The event data.</param>
	private void OnPropertyGridValidationError(object sender, ValidationErrorEventArgs e) {
		switch (e.Action) {
			case ValidationErrorEventAction.Added:
				errorListBox.Items.Add(e.Error);

				// As a demonstration, show a dialog with the error message for property 'ErrorReporting3'
				if (
					e.OriginalSource is DependencyObject originalSource
					&& VisualTreeHelperExtended.GetAncestor<PropertyGridItem>(originalSource) is { Content: IPropertyModel propertyModel }
					&& propertyModel.Name == "ErrorReporting3"
				) {
					MessageBox.Show(Convert.ToString(e.Error.ErrorContent, CultureInfo.CurrentCulture) ?? string.Empty, "Data Validation", MessageBoxButton.OK, MessageBoxImage.Error);
				}
				break;

			case ValidationErrorEventAction.Removed:
				errorListBox.Items.Add(e.Error);
				break;
		}

	}

}
