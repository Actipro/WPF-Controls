#if WINRT
using ActiproSoftware.UI.Xaml.Data.Filtering;
#else
using ActiproSoftware.Windows.Data.Filtering;
#endif

namespace ActiproSoftware.ProductSamples.GridsSamples.Common {
	
	/// <summary>
	/// Provides a common implementation of string-based filter for tree node model.
	/// </summary>
	public class TreeNodeModelStringFilter : StringFilterBase {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Examines the specified item to see if meets filter conditions.
		/// </summary>
		/// <param name="item">The item to examine.</param>
		/// <param name="context">An optional object that provides contextual information for the filter request.</param>
		/// <returns>A <see cref="DataFilterResult"/> that indicates the filter result.</returns>
		public override DataFilterResult Filter(object item, object context) {
			var model = item as TreeNodeModel;
			var source = (model != null ? model.Name : null);

			return (this.FilterByString(source, this.Value) ? this.IncludedFilterResult : DataFilterResult.IncludedByDescendants);
		}
		
	}

}
