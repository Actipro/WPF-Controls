using System.Windows;
using Microsoft.Win32;
using ActiproSoftware.Windows.Controls.Grids.PropertyData;
using ActiproSoftware.Windows.Input;

#if WPF
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;
#endif

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridPropertyDialog {

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

			this.InitializeDialogEditorButtonCommands();
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Sets the button commands for the dialog editors.
		/// </summary>
		private void InitializeDialogEditorButtonCommands() {
			editablePathEditor.ButtonCommand = new DelegateCommand<object>(p => {
				var propertyModel = p as IPropertyModel;
				if (propertyModel != null) {
					// Show a file open dialog
					var dialog = new OpenFileDialog();
					dialog.Title = "Select the file path";
					if (dialog.ShowDialog() == true) {
						// Update the property value
						propertyModel.Value = dialog.FileName;
					}
				}
			});

			readOnlyPathEditor.ButtonCommand = new DelegateCommand<object>(p => {
				var propertyModel = p as IPropertyModel;
				if (propertyModel != null) {
					// Show the path
					MessageBox.Show(propertyModel.ValueAsString, "Property Value");
				}
			});

			uneditablePathEditor.ButtonCommand = editablePathEditor.ButtonCommand;
		}

	}

}
