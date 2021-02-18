using System;
using System.Linq;
using System.Windows;
using ActiproSoftware.Windows.Controls.Grids.PropertyData;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridSettingFocus {

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
		/// Focuses the property grid item with the specified <see cref="IDataModel.Name"/>.
		/// </summary>
		/// <typeparam name="T">The model type.</typeparam>
		/// <param name="name">The model name.</param>
		private void FocusModel<T>(string name) where T: IDataModel {
			var propertyModel = propGrid.Items.OfType<T>().FirstOrDefault(p => p.Name == name);
			if (propertyModel != null)
				propGrid.FocusItem(propertyModel);
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnFocusFirstPropertyButtonClick(object sender, RoutedEventArgs e) {
			var firstPropertyModel = propGrid.Items.OfType<IPropertyModel>().FirstOrDefault();
			if (firstPropertyModel != null)
				this.FocusModel<IPropertyModel>(firstPropertyModel.Name);
		}
		
		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnFocusIsTabStopPropertyButtonClick(object sender, RoutedEventArgs e) {
			this.FocusModel<IPropertyModel>("IsTabStop");
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnFocusMiscCategoryButtonClick(object sender, RoutedEventArgs e) {
			this.FocusModel<ICategoryModel>(propGrid.MiscCategoryName);
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnFocusToolTipPropertyButtonClick(object sender, RoutedEventArgs e) {
			this.FocusModel<IPropertyModel>("ToolTip");
		}

	}

}
