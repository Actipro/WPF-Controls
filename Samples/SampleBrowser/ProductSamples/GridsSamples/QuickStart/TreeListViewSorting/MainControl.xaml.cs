using ActiproSoftware.ProductSamples.GridsSamples.Common;
using ActiproSoftware.Windows.Controls.Grids;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.TreeListViewSorting;

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
	/// Occurs when a column's header is tapped.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnTreeListViewColumnHeaderTapped(object sender, TreeListViewColumnEventArgs e) {
		if (
			sender is TreeListView { ItemAdapter: DefaultTreeListBoxItemAdapter adapter } treeListView
			&& e.Column is not null
		) {
			// Determine the new sort direction
			var newSortDirection = (e.Column.SortDirection == ColumnSortDirection.Ascending)
				? ColumnSortDirection.Descending
				: ColumnSortDirection.Ascending;

			// Update each column's sort direction
			foreach (var column in treeListView.Columns)
				column.SortDirection = (e.Column == column) ? newSortDirection : null;

			// Set the adapter's sort description... the XAML column definition stored the name of the related view model property in its Tag property
			adapter.SortDescription = new SortDescription(
				e.Column.Tag?.ToString(),
				newSortDirection == ColumnSortDirection.Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending
			);

			// Invalidate the root child so that the entire tree refreshes with the same items source
			if (treeListView.RootItem is { } rootItem)
				treeListView.InvalidateChildren(rootItem);
		}
	}
}
