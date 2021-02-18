using ActiproSoftware.Windows.Controls.Grids.PropertyData;
using ActiproSoftware.Windows.Data.Filtering;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridFiltersCustom {

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

			propGrid.DataFilter = new PredicateFilter(CustomFilterPredicate);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Implements a filter predicate.
		/// </summary>
		/// <param name="model">The model to examine.</param>
		/// <returns>
		/// <c>true</c> if the model is included in the filter; otherwise, <c>false</c>.
		/// </returns>
		private bool CustomFilterPredicate(object model) {
			var propertyModel = model as IPropertyModel;
			return (propertyModel != null) && (propertyModel.Category != propGrid.MiscCategoryName);
		}

	}

}
