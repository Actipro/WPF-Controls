using ActiproSoftware.Windows.Controls.Grids.PropertyData;
using ActiproSoftware.Windows.Input;
using Microsoft.Win32;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridPropertyDialog;

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

		InitializeDialogEditorButtonCommands();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Sets the button commands for the dialog editors.
	/// </summary>
	private void InitializeDialogEditorButtonCommands() {
		editablePathEditor.ButtonCommand = new DelegateCommand<object>(p => {
			if (p is IPropertyModel propertyModel) {
				// Show a file open dialog
				var dialog = new OpenFileDialog {
					Title = "Select the file path"
				};
				if (dialog.ShowDialog() == true) {
					// Update the property value
					propertyModel.Value = dialog.FileName;
				}
			}
		});

		readOnlyPathEditor.ButtonCommand = new DelegateCommand<object>(p => {
			if (p is IPropertyModel propertyModel) {
				// Show the path
				MessageBox.Show(propertyModel.ValueAsString, "Property Value");
			}
		});

		uneditablePathEditor.ButtonCommand = editablePathEditor.ButtonCommand;
	}

}
