using ActiproSoftware.Windows.Controls.Grids;

namespace ActiproSoftware.ProductSamples.GridsSamples.Common;

/// <summary>
/// An adapter that can provide an item's hierarchy and visual state data for usage in a <see cref="TreeListBox"/>.
/// This default implementation is intended to specifically adapt <see cref="TreeNodeModel"/>
/// and is geared for high-performance due to the various get/set method overrides
/// instead of using bindings for updates.
/// </summary>
public class DefaultTreeListBoxItemAdapter : TreeListBoxItemAdapter {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public DefaultTreeListBoxItemAdapter() {
		// Setting these properties tells the adapter which properties to watch for INotifyPropertyChanged updates
		//   so that the UI can receive the updated values without binding usage
		ChildrenPath = nameof(TreeNodeModel.Children);
		IsEditingPath = nameof(TreeNodeModel.IsEditing);
		IsExpandedPath = nameof(TreeNodeModel.IsExpanded);
		IsLoadingPath = nameof(TreeNodeModel.IsLoading);
		IsSelectablePath = nameof(TreeNodeModel.IsSelectable);
		IsSelectedPath = nameof(TreeNodeModel.IsSelected);
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override IEnumerable? GetChildren(TreeListBox ownerControl, object item) {
		var model = item as TreeNodeModel;
		var enumerable = model?.Children;

		// If a sort description is specified, sort the results in a collection view
		if (SortDescription.HasValue) {
			var collectionViewSource = new CollectionViewSource() {
				SortDescriptions = {
					SortDescription.Value
				},
				Source = enumerable,
			};
			return collectionViewSource.View;
		}

		return enumerable;
	}

	/// <inheritdoc/>
	public override ICommand? GetDefaultActionCommand(TreeListBox ownerControl, object item) {
		var model = item as TreeNodeModel;
		return model?.DefaultActionCommand;
	}

	/// <inheritdoc/>
	public override bool GetIsDraggable(TreeListBox ownerControl, object item) {
		var model = item as TreeNodeModel;
		return model?.IsDraggable == true;
	}

	/// <inheritdoc/>
	public override bool GetIsEditable(TreeListBox ownerControl, object item) {
		var model = item as TreeNodeModel;
		return model?.IsEditable == true;
	}

	/// <inheritdoc/>
	public override bool GetIsEditing(TreeListBox ownerControl, object item) {
		var model = item as TreeNodeModel;
		return model?.IsEditing == true;
	}

	/// <inheritdoc/>
	public override bool GetIsExpanded(TreeListBox ownerControl, object item) {
		var model = item as TreeNodeModel;
		return model?.IsExpanded == true;
	}

	/// <inheritdoc/>
	public override bool GetIsLoading(TreeListBox ownerControl, object item) {
		var model = item as TreeNodeModel;
		return model?.IsLoading == true;
	}

	/// <inheritdoc/>
	public override bool GetIsSelectable(TreeListBox ownerControl, object item) {
		var model = item as TreeNodeModel;
		return model?.IsSelectable == true;
	}

	/// <inheritdoc/>
	public override bool GetIsSelected(TreeListBox ownerControl, object item) {
		var model = item as TreeNodeModel;
		return model?.IsSelected == true;
	}

	/// <inheritdoc/>
	public override string? GetPath(TreeListBox ownerControl, object item) {
		var model = item as TreeNodeModel;
		return model?.Name;
	}

	/// <inheritdoc/>
	public override string? GetSearchText(TreeListBox ownerControl, object item) {
		var model = item as TreeNodeModel;
		return model?.Name;
	}

	/// <inheritdoc/>
	public override void SetIsEditing(TreeListBox ownerControl, object item, bool value) {
		if (item is TreeNodeModel model)
			model.IsEditing = value;
	}

	/// <inheritdoc/>
	public override void SetIsExpanded(TreeListBox ownerControl, object item, bool value) {
		if (item is TreeNodeModel model)
			model.IsExpanded = value;
	}

	/// <inheritdoc/>
	public override void SetIsSelected(TreeListBox ownerControl, object item, bool value) {
		if (item is TreeNodeModel model)
			model.IsSelected = value;
	}

	/// <summary>
	/// The optional <see cref="System.ComponentModel.SortDescription"/> to use for sorting children.
	/// </summary>
	/// <remarks>
	/// When specified, an <see cref="ICollectionView"/> will be returned from the <see cref="GetChildren"/> method.
	/// </remarks>
	public SortDescription? SortDescription { get; set; }

}
