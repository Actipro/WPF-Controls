using System.Windows;
using ActiproSoftware.Windows.Controls.Grids;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridColumns {

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

			this.InitializeAdditionalColumn();
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an additional column in the property grid.
		/// </summary>
		private void InitializeAdditionalColumn() {
			var column = new TreeListViewColumn();
			column.CellBorderThickness = new Thickness(0, 0, 1, 0);
			column.CellPadding = new Thickness(3, 0, 3, 0);
			column.CellTemplate = this.FindResource("IsModifiedTemplate") as DataTemplate;
			column.MinWidth = 16;
			column.Width = GridLength.Auto;
			propGrid.Columns.Insert(1, column);
		}

	}

}
