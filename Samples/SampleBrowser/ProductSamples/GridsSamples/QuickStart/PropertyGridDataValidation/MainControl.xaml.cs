using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using ActiproSoftware.Windows.Controls.Grids;
using ActiproSoftware.Windows.Media;
using ActiproSoftware.Windows.Controls.Grids.PropertyData;

#if WPF
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;
#endif

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridDataValidation {

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
		/// Called when <c>Validation.Error</c> is fired on the PropertyGrid or one it's descendants.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="ValidationErrorEventArgs"/> instance containing the event data.</param>
		private void OnPropertyGridValidationError(object sender, ValidationErrorEventArgs e) {
			switch (e.Action) {
				case ValidationErrorEventAction.Added:
					errorListBox.Items.Add(e.Error);

					// As a demonstration, show a dialog with the error message for property 'ErrorReporting3'
					var container = VisualTreeHelperExtended.GetAncestor<PropertyGridItem>(e.OriginalSource as DependencyObject);
					if (container != null) {
						var propertyModel = container.Content as IPropertyModel;
						if ((propertyModel != null) && (propertyModel.Name == "ErrorReporting3"))
							MessageBox.Show(Convert.ToString(e.Error.ErrorContent, CultureInfo.CurrentCulture), "Data Validation", MessageBoxButton.OK, MessageBoxImage.Error);
					}
					break;
				case ValidationErrorEventAction.Removed:
					errorListBox.Items.Add(e.Error);
					break;
			}

		}

	}

}
