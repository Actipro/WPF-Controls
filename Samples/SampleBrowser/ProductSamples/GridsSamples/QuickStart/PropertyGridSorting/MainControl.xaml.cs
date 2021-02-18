using System.Linq;
using System.Windows;
using ActiproSoftware.Windows.Controls.Grids.PropertyData;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridSorting {

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
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnToggleSortOrderButtonClick(object sender, RoutedEventArgs e) {
			var categoryModel = propGrid.Items.OfType<ICategoryModel>().FirstOrDefault(m => m.DisplayName == "B");
			if (categoryModel != null) {
				var comparer = categoryModel.SortComparer as NumericValueComparer;
				if (comparer != null)
					categoryModel.SortComparer = new NumericValueComparer() { SortDescending = !comparer.SortDescending };
			}
		}
		
	}

}
