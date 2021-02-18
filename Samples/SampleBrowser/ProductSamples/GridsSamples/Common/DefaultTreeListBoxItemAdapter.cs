using System.Collections;

#if WINRT
using ActiproSoftware.UI.Xaml.Controls.Grids;
#else
using ActiproSoftware.Windows.Controls.Grids;
#endif

namespace ActiproSoftware.ProductSamples.GridsSamples.Common {
	
	/// <summary>
	/// An adapter that can provide an item's hierarchy and visual state data for usage in a <see cref="TreeListBox"/>.
	/// This default implementation is intended to specifically adapt <see cref="TreeNodeModel"/>
	/// and is geared for high-performance due to the various get/set method overrides
	/// instead of using bindings for updates.
	/// </summary>
	public class DefaultTreeListBoxItemAdapter : TreeListBoxItemAdapter {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultTreeListBoxItemAdapter"/> class.
		/// </summary>
		public DefaultTreeListBoxItemAdapter() {
			// Setting these properties tells the adapter which properties to watch for INotifyPropertyChanged updates
			//   so that the UI can receive the updated values without binding usage
			this.ChildrenPath = "Children";
			this.IsEditingPath = "IsEditing";
			this.IsExpandedPath = "IsExpanded";
			this.IsLoadingPath = "IsLoading";
			this.IsSelectablePath = "IsSelectable";
			this.IsSelectedPath = "IsSelected";
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Returns an <see cref="IEnumerable"/> that will be used to provide child items for the specified parent item.
		/// </summary>
		/// <param name="ownerControl">The owner control.</param>
		/// <param name="item">The item to examine.</param>
		/// <returns>An <see cref="IEnumerable"/> that will be used to provide child items for the specified parent item.</returns>
		public override IEnumerable GetChildren(TreeListBox ownerControl, object item) {
			var model = item as TreeNodeModel;
			return (model != null ? model.Children : null);
		}
		
		/// <summary>
		/// Returns whether the specified item is draggable.
		/// </summary>
		/// <param name="ownerControl">The owner control.</param>
		/// <param name="item">The item to examine.</param>
		/// <returns>
		/// <c>true</c> if the specified item is draggable; otherwise, <c>false</c>.
		/// </returns>
		public override bool GetIsDraggable(TreeListBox ownerControl, object item) {
			var model = item as TreeNodeModel;
			return (model != null ? model.IsDraggable : false);
		}

		/// <summary>
		/// Returns whether the specified item is editable.
		/// </summary>
		/// <param name="ownerControl">The owner control.</param>
		/// <param name="item">The item to examine.</param>
		/// <returns>
		/// <c>true</c> if the specified item is editable; otherwise, <c>false</c>.
		/// </returns>
		public override bool GetIsEditable(TreeListBox ownerControl, object item) {
			var model = item as TreeNodeModel;
			return (model != null ? model.IsEditable : false);
		}

		/// <summary>
		/// Returns whether the specified item is currently being edited.
		/// </summary>
		/// <param name="ownerControl">The owner control.</param>
		/// <param name="item">The item to examine.</param>
		/// <returns>
		/// <c>true</c> if the specified item is currently being edited; otherwise, <c>false</c>.
		/// </returns>
		public override bool GetIsEditing(TreeListBox ownerControl, object item) {
			var model = item as TreeNodeModel;
			return (model != null ? model.IsEditing : false);
		}
		
		/// <summary>
		/// Returns whether the specified item is expanded.
		/// </summary>
		/// <param name="ownerControl">The owner control.</param>
		/// <param name="item">The item to examine.</param>
		/// <returns>
		/// <c>true</c> if the specified item is expanded; otherwise, <c>false</c>.
		/// </returns>
		public override bool GetIsExpanded(TreeListBox ownerControl, object item) {
			var model = item as TreeNodeModel;
			return (model != null ? model.IsExpanded : false);
		}
		
		/// <summary>
		/// Returns whether the specified item is currently loading children asynchronously.
		/// </summary>
		/// <param name="ownerControl">The owner control.</param>
		/// <param name="item">The item to examine.</param>
		/// <returns>
		/// <c>true</c> if the specified item is currently loading children asynchronously; otherwise, <c>false</c>.
		/// </returns>
		/// <remarks>
		/// The <see cref="IsLoadingBinding"/> binding property can be set for a pure XAML-based implementation of this method.
		/// Please note that bindings aren't as performant as code, so for large trees or if you see performance issues,
		/// it is recommended to override this method instead with custom logic to retrieve the appropriate value.
		/// </remarks>
		public override bool GetIsLoading(TreeListBox ownerControl, object item) {
			var model = item as TreeNodeModel;
			return (model != null ? model.IsLoading : false);
		}
		
		/// <summary>
		/// Returns whether the specified item is capable of being selected.
		/// </summary>
		/// <param name="ownerControl">The owner control.</param>
		/// <param name="item">The item to examine.</param>
		/// <returns>
		/// <c>true</c> if the specified item is capable of being selected; otherwise, <c>false</c>.
		/// </returns>
		public override bool GetIsSelectable(TreeListBox ownerControl, object item) {
			var model = item as TreeNodeModel;
			return (model != null ? model.IsSelectable : true);
		}
	
		/// <summary>
		/// Returns whether the specified item is selected.
		/// </summary>
		/// <param name="ownerControl">The owner control.</param>
		/// <param name="item">The item to examine.</param>
		/// <returns>
		/// <c>true</c> if the specified item is selected; otherwise, <c>false</c>.
		/// </returns>
		public override bool GetIsSelected(TreeListBox ownerControl, object item) {
			var model = item as TreeNodeModel;
			return (model != null ? model.IsSelected : false);
		}

		/// <summary>
		/// Returns the item's string path.
		/// </summary>
		/// <param name="ownerControl">The owner control.</param>
		/// <param name="item">The item to examine.</param>
		/// <returns>The item's string path.</returns>
		public override string GetPath(TreeListBox ownerControl, object item) {
			var model = item as TreeNodeModel;
			return (model != null ? model.Name: null);
		}

		/// <summary>
		/// Returns the item's text by which to match when searching.
		/// </summary>
		/// <param name="ownerControl">The owner control.</param>
		/// <param name="item">The item to examine.</param>
		/// <returns>The item's string search text.</returns>
		public override string GetSearchText(TreeListBox ownerControl, object item) {
			var model = item as TreeNodeModel;
			return (model != null ? model.Name: null);
		}
		
		/// <summary>
		/// Sets whether the specified item is currently being edited. 
		/// </summary>
		/// <param name="ownerControl">The owner control.</param>
		/// <param name="item">The item to update.</param>
		/// <param name="value">The new value.</param>
		public override void SetIsEditing(TreeListBox ownerControl, object item, bool value) {
			var model = item as TreeNodeModel;
			if (model != null)
				model.IsEditing = value;
		}

		/// <summary>
		/// Sets whether the specified item is expanded. 
		/// </summary>
		/// <param name="ownerControl">The owner control.</param>
		/// <param name="item">The item to update.</param>
		/// <param name="value">The new value.</param>
		public override void SetIsExpanded(TreeListBox ownerControl, object item, bool value) {
			var model = item as TreeNodeModel;
			if (model != null)
				model.IsExpanded = value;
		}

		/// <summary>
		/// Sets whether the specified item is selected. 
		/// </summary>
		/// <param name="ownerControl">The owner control.</param>
		/// <param name="item">The item to update.</param>
		/// <param name="value">The new value.</param>
		public override void SetIsSelected(TreeListBox ownerControl, object item, bool value) {
			var model = item as TreeNodeModel;
			if (model != null)
				model.IsSelected = value;
		}

	}

}
