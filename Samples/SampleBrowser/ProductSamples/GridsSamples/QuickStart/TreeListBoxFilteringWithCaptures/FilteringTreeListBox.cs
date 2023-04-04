using ActiproSoftware.ProductSamples.GridsSamples.Common;
using ActiproSoftware.Windows.Controls.Grids;
using ActiproSoftware.Windows.Media;
using System.Windows;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.TreeListBoxFilteringWithCaptures {

	/// <summary>
	/// Represents an advanced single-column tree view control which can support highlighting of captures
	/// for string-based filters.
	/// </summary>
	public class FilteringTreeListBox : TreeListBox {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Removes captures from a <see cref="TreeListBoxItem"/>.
		/// </summary>
		/// <param name="treeListBoxItem">The item whose captures will be removed.</param>
		private static void RemoveCaptures(TreeListBoxItem treeListBoxItem) {
			if (treeListBoxItem != null)
				treeListBoxItem.Tag = null;
		}

		/// <summary>
		/// Updates captures in all <see cref="TreeListBoxItem"/> instances.
		/// </summary>
		private void UpdateAllCaptures() {
			var allTreeListBoxItems = VisualTreeHelperExtended.GetAllDescendants<TreeListBoxItem>(this);
			foreach (var treeListBoxItem in allTreeListBoxItems)
				this.UpdateCaptures(treeListBoxItem);
		}

		/// <summary>
		/// Updates captures for a <see cref="TreeListBoxItem"/>.
		/// </summary>
		/// <param name="treeListBoxItem">The item whose captures will be updated.</param>
		private void UpdateCaptures(TreeListBoxItem treeListBoxItem) {
			if (this.IsFilterActive
				&& (this.DataFilter is TreeNodeModelStringFilter filter)
				&& (treeListBoxItem?.DataContext is TreeNodeModel treeNodeModel)) {

				// Get the captures associated with the item's name (since the name is used to display the node in the tree)
				var captures = filter.GetCaptures(treeNodeModel.Name, filter.Value);

				// Store the captures
				treeListBoxItem.Tag = captures;
			}
			else {
				// Remove captures when filtering is not active
				RemoveCaptures(treeListBoxItem);
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc/>
		protected override void ClearContainerForItemOverride(DependencyObject element, object item) {
			base.ClearContainerForItemOverride(element, item);

			// Remove captures when the container is cleared
			if (element is TreeListBoxItem treeListBoxItem)
				RemoveCaptures(treeListBoxItem);
		}

		/// <inheritdoc/>
		protected override void PrepareContainerForItemOverride(DependencyObject element, object item) {
			base.PrepareContainerForItemOverride(element, item);

			// Update captures for the new container
			if (element is TreeListBoxItem treeListBoxItem)
				this.UpdateCaptures(treeListBoxItem);
		}

		/// <inheritdoc/>
		protected override void OnFilterApplied(RoutedEventArgs e) {
			base.OnFilterApplied(e);

			// Update all captures when a filter is applied
			this.UpdateAllCaptures();
		}

	}

}
