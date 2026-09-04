using ActiproSoftware.Extensions;
using ActiproSoftware.ProductSamples.GridsSamples.Common;
using ActiproSoftware.Windows.Controls.Grids;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.TreeListBoxDragDrop;

/// <summary>
/// An adapter that can provide an item's hierarchy and visual state data for usage in a <see cref="TreeListBox"/>.
/// This default implementation is intended to specifically adapt <see cref="TreeNodeModel"/>
/// and is geared for high-performance due to the various get/set method overrides
/// instead of using bindings for updates.
/// </summary>
public class CustomTreeListBoxItemAdapter : DefaultTreeListBoxItemAdapter {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override DragDropEffects InitializeDataObject(TreeListBox sourceControl, IDataObject dataObject, IEnumerable<object> items) {
		if (sourceControl is null)
			throw new ArgumentNullException(nameof(sourceControl));
		if (dataObject is null)
			throw new ArgumentNullException(nameof(dataObject));
		if (items is null)
			throw new ArgumentNullException(nameof(items));

		// Store the full paths to items in case we drop on the tree itself...
		//   Each item needs to have a unique path, which comes from adapter GetPath() calls
		var fullPaths = new StringBuilder();
		foreach (var item in items)
			fullPaths.AppendLine(sourceControl.GetFullPath(item));
		if (fullPaths.Length > 0)
			dataObject.SetData(TreeListBox.ItemDataFormat, fullPaths.ToString());

		// If there is one item, store its text so that it can be dropped elsewhere
		if (
			items.Count() == 1
			&& items.First() is TreeNodeModel viewModel
		) {
			dataObject.SetData(DataFormats.Text, viewModel.Name);
		}

		return DragDropEffects.Move;
	}

	/// <inheritdoc/>
	public override TreeItemDropArea OnDragOver(DragEventArgs e, TreeListBox targetControl, object? targetItem, TreeItemDropArea dropArea) {
		// If the drag is over an item and there is item data present...
		if (
			targetItem is not null
			&& dropArea != TreeItemDropArea.None
			&& e.Data.GetDataPresent(TreeListBox.ItemDataFormat)
			&& e.Data.GetData(TreeListBox.ItemDataFormat) is string { Length: > 0 } fullPaths
		) {
			// Locate the first item based on full path
			object? firstItem = null;
			foreach (var fullPath in fullPaths.Split(['\r', '\n'])) {
				if (!string.IsNullOrEmpty(fullPath)) {
					var item = targetControl.GetItemByFullPath(fullPath);
					if (item is not null) {
						firstItem = item;
						break;
					}
				}
			}

			if (firstItem is not null) {
				// Ensure that the first item is already in the target control (nav will be null if not)... if allowing drag/drop onto external
				//   controls, you cannot use the item navigator and must rely on your own item hierarchy logic
				var firstItemNav = targetControl.GetItemNavigator(firstItem);
				if (firstItemNav is not null) {
					// Only support a single effect (you could add support for other effects like Copy if the Ctrl key is down here)
					if ((e.AllowedEffects & DragDropEffects.Move) == DragDropEffects.Move) {
						e.Effects = DragDropEffects.Move;
						e.Handled = true;
					}

					switch (e.Effects) {
						case DragDropEffects.Move:
							// Coerce the resulting drop-area so that if dragging 'after' an item that has a next sibling, the drop area
							//   becomes 'on' the item instead... can still get between the items by dragging 'before' the next sibling in this scenario
							if (dropArea == TreeItemDropArea.After) {
								var targetItemNav = targetControl.GetItemNavigator(targetItem);
								if (targetItemNav?.GoToNextSibling() == true)
									dropArea = TreeItemDropArea.On;
							}

							return dropArea;
					}
				}
			}
		}

		e.Effects = DragDropEffects.None;
		return TreeItemDropArea.None;
	}

	/// <inheritdoc/>
	public override void OnDrop(DragEventArgs e, TreeListBox targetControl, object? targetItem, TreeItemDropArea dropArea) {
		var originalEffects = e.Effects;
		e.Effects = DragDropEffects.None;

		// If the drag is over an item and there is item data present...
		var targetModel = targetItem as TreeNodeModel;
		if (
			targetModel is not null
			&& dropArea != TreeItemDropArea.None
			&& e.Data.GetDataPresent(TreeListBox.ItemDataFormat)
		) {
			// Resolve the real target item (in case the drop area is above or below the target item)
			var targetDropIndex = targetModel.Children.Count;
			switch (dropArea) {
				case TreeItemDropArea.Before:
				case TreeItemDropArea.After:
					var nav = targetControl.GetItemNavigator(targetItem!);
					if (nav is not null) {
						var targetChildModel = targetModel;

						if (!nav.GoToParent())
							return;
						targetItem = nav.CurrentItem;
						targetModel = targetItem as TreeNodeModel;
						if (targetModel is null)
							return;

						var index = targetModel.Children.IndexOf(targetChildModel);
						if (index != -1)
							targetDropIndex = index + (dropArea == TreeItemDropArea.After ? 1 : 0);
					}
					break;
			}

			// Get the items
			var fullPaths = e.Data.GetData(TreeListBox.ItemDataFormat) as string;
			if (!string.IsNullOrEmpty(fullPaths)) {
				// Locate items based on full path
				var items = new List<object>();
				foreach (var fullPath in fullPaths!.Split(['\r', '\n'])) {
					if (!string.IsNullOrEmpty(fullPath)) {
						var item = targetControl.GetItemByFullPath(fullPath);
						if (item is not null)
							items.Add(item);
					}
				}

				if (items.Count > 0) {
					// Check each item and validate that various drop operations are allowed before actually executing the drop
					foreach (var item in items) {
						if (item == targetItem) {
							MessageBox.Show("Cannot drop an item on itself.", "Drag and Drop", MessageBoxButton.OK);
							return;
						}
						else {
							var nav = targetControl.GetItemNavigator(item);
							if (nav is null) {
								MessageBox.Show("Cannot drop from a different control.", "Drag and Drop", MessageBoxButton.OK);
								return;
							}
							else {
								if (nav.GoToCommonAncestor(targetItem!)) {
									if (nav.CurrentItem == item) {
										MessageBox.Show("Cannot drop onto a descendant item.", "Drag and Drop", MessageBoxButton.OK);
										return;
									}
								}
							}
						}
					}

					// Only support a single effect (you could add support for other effects like Copy if the Ctrl key is down here)
					if ((originalEffects & DragDropEffects.Move) == DragDropEffects.Move) {
						e.Effects = DragDropEffects.Move;
						e.Handled = true;
					}

					// Move items
					var movedItemModels = new List<TreeNodeModel>();
					foreach (var item in items) {
						var nav = targetControl.GetItemNavigator(item);
						if (nav?.GoToParent() == true) {
							var itemModel = item as TreeNodeModel;
							var parentModel = nav.CurrentItem as TreeNodeModel;
							if ((itemModel is not null) && (parentModel is not null)) {
								var index = parentModel.Children.IndexOf(itemModel);
								if (index != -1) {
									if ((parentModel == targetModel) && (index < targetDropIndex))
										targetDropIndex--;

									parentModel.Children.RemoveAt(index);
								}
								else
									break;
							}
							else
								break;

							movedItemModels.Add(itemModel);

							targetModel.Children.Insert((targetDropIndex++).ClampToRange(0, targetModel.Children.Count), itemModel);
							targetModel.IsExpanded = true;
						}
					}

					if (movedItemModels.Count > 0) {
						using (var batch = targetControl.CreateSelectionBatch()) {
							// If the target control supports multi-select, ensure each moved item is reselected
							if (targetControl.SelectionMode != SelectionMode.Single) {
								targetControl.SelectedItem = null;
								foreach (var movedItemModel in movedItemModels)
									movedItemModel.IsSelected = true;
							}

							// Focus the last item
							targetControl.FocusItem(movedItemModels.Last());
						}
					}
				}
			}
		}
	}

}
