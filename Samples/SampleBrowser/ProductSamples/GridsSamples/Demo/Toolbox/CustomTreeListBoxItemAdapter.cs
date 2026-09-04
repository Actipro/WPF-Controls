using ActiproSoftware.Extensions;
using ActiproSoftware.ProductSamples.GridsSamples.Common;
using ActiproSoftware.Windows.Controls.Grids;
using System.Windows.Threading;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.GridsSamples.Demo.Toolbox;

/// <summary>
/// An adapter that can provide an item's hierarchy and visual state data for usage in a <see cref="TreeListBox"/>.
/// This default implementation is intended to specifically adapt <see cref="TreeNodeModel"/>
/// and is geared for high-performance due to the various get/set method overrides
/// instead of using bindings for updates.
/// </summary>
public class CustomTreeListBoxItemAdapter : DefaultTreeListBoxItemAdapter {

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Returns the <see cref="IDataObject"/> text for the specified item.
	/// </summary>
	/// <param name="item">The <see cref="IDataObject"/> to examine.</param>
	private static string? GetDataObjectText(object item) {
		return item switch {
			ToolboxTreeNodeModel toolboxModel => toolboxModel.DataObjectText,
			TreeNodeModel treeNodeModel => treeNodeModel.Name,
			_ => null
		};
	}

	/// <summary>
	/// Returns an enumerable of all dragged items.
	/// </summary>
	/// <param name="dataObject">The <see cref="IDataObject"/> which defines the dragged items.</param>
	/// <param name="control">The control from which items will be derived.</param>
	internal static IEnumerable<ToolboxTreeNodeModel> GetDraggedModels(IDataObject dataObject, TreeListBox control) {
		var format = TreeListBox.ItemDataFormat;
		if (
			dataObject.GetDataPresent(format)
			&& dataObject.GetData(format) is string { Length: > 0 } data
		) {
			foreach (var fullPath in SplitItemFullPaths(data)) {
				if (control.GetItemByFullPath(fullPath) is ToolboxTreeNodeModel itemModel)
					yield return itemModel;
			}
		}
	}

	/// <summary>
	/// Returns whether the specified <see cref="CategoryTreeNodeModel" /> is for the Favorites category.
	/// </summary>
	/// <param name="categoryModel">The <see cref="CategoryTreeNodeModel"/> to examine.</param>
	private static bool IsFavoritesCategory(CategoryTreeNodeModel? categoryModel)
		=> categoryModel is FavoritesCategoryTreeNodeModel;

	/// <summary>
	/// Returns a string containing the full paths of the specified items.
	/// </summary>
	/// <param name="toolboxControl">The <see cref="TreeListBox"/> control.</param>
	/// <param name="items">The items to examine.</param>
	private static string JoinItemFullPaths(TreeListBox toolboxControl, IEnumerable<object> items) {
		var fullPaths = new StringBuilder();
		foreach (var item in items)
			fullPaths.Append(toolboxControl.GetFullPath(item)).Append('\n');
		return fullPaths.ToString().TrimEnd();
	}

	/// <summary>
	/// Splits the multi-line string of full paths.
	/// </summary>
	/// <param name="fullPaths">The string of full paths.</param>
	private static IEnumerable<string> SplitItemFullPaths(string fullPaths)
		=> fullPaths.Split('\n').Where(x => !string.IsNullOrWhiteSpace(x));

	/// <summary>
	/// Tries to get the category model for the specified <see cref="ToolboxTreeNodeModel"/>.
	/// </summary>
	/// <param name="toolboxControl">The <see cref="TreeListBox"/> control.</param>
	/// <param name="itemModel">The <see cref="ToolboxTreeNodeModel"/> to examine.</param>
	/// <param name="categoryModel">The resulting <see cref="CategoryTreeNodeModel"/>.</param>
	/// <returns>
	/// <c>true</c> if a result was found; otherwise, <c>false</c>.
	/// </returns>
	private static bool TryGetCategory(TreeListBox toolboxControl, ToolboxTreeNodeModel itemModel, out CategoryTreeNodeModel? categoryModel) {
		categoryModel = null;
		if (itemModel is CategoryTreeNodeModel localCategoryModel) {
			categoryModel = localCategoryModel;
			return true;
		}

		var navigator = toolboxControl.GetItemNavigator(itemModel);
		if (navigator?.GoToParent() == true) {
			categoryModel = navigator.CurrentItem as CategoryTreeNodeModel;
			return categoryModel is not null;
		}

		return false;
	}

	/// <summary>
	/// Tries to get the category and control models for the specified item.
	/// </summary>
	/// <param name="toolboxControl">The <see cref="TreeListBox"/> control.</param>
	/// <param name="item">The item to examine.</param>
	/// <param name="categoryModel">The resulting <see cref="CategoryTreeNodeModel"/>.</param>
	/// <param name="controlModel">The resulting <see cref="ControlTreeNodeModel"/>.</param>
	/// <returns>
	/// <c>true</c> if results were found; otherwise, <c>false</c>.
	/// </returns>
	private static bool TryGetModelsFromItem(TreeListBox toolboxControl, object? item, out CategoryTreeNodeModel? categoryModel, out ControlTreeNodeModel? controlModel) {
		if (item is ControlTreeNodeModel localControlModel) {
			controlModel = localControlModel;
			return TryGetCategory(toolboxControl, controlModel, out categoryModel);
		}
		else if (item is EmptyPlaceholderTreeNodeModel emptyPlaceholderModel) {
			controlModel = null;
			return TryGetCategory(toolboxControl, emptyPlaceholderModel, out categoryModel);
		}
		else if (item is CategoryTreeNodeModel localCategoryModel) {
			controlModel = null;
			categoryModel = localCategoryModel;
			return true;
		}

		categoryModel = null;
		controlModel = null;
		return false;
	}

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

		// Verify the source items
		foreach (var item in items) {
			if (!TryGetModelsFromItem(sourceControl, item, out _, out _)) {
				// Quit if any item cannot be recognized
				return DragDropEffects.None;
			}
		}

		// Store the full paths to items in case we drop on the tree itself...
		//   Each item needs to have a unique path, which comes from adapter GetPath() calls
		var fullPaths = JoinItemFullPaths(sourceControl, items);
		if (string.IsNullOrWhiteSpace(fullPaths))
			return DragDropEffects.None;
		dataObject.SetData(TreeListBox.ItemDataFormat, fullPaths);

		// If there is one item, store its text so that it can be dropped elsewhere
		if (items.Count() == 1) {
			var text = GetDataObjectText(items.First());
			if (!string.IsNullOrEmpty(text))
				dataObject.SetData(DataFormats.Text, text);
		}

		return DragDropEffects.Move | DragDropEffects.Copy;
	}

	/// <inheritdoc/>
	public override TreeItemDropArea OnDragOver(DragEventArgs e, TreeListBox targetControl, object? targetItem, TreeItemDropArea dropArea) {
		// If the drag is over an item, item data present, and the drop target is recognized...
		if (
			dropArea != TreeItemDropArea.None
			&& e.Data.GetDataPresent(TreeListBox.ItemDataFormat)
			&& TryGetModelsFromItem(targetControl, targetItem, out var targetCategoryModel, out _)
		) {
			// Locate the dragged model. If multiple objects were selected, only the first object is used for reference.
			//   The dragged item must be defined in the target control for the drag operation to be allowed.
			var sourceItem = GetDraggedModels(e.Data, targetControl).FirstOrDefault();
			if (
				sourceItem is not null
				&& TryGetModelsFromItem(targetControl, sourceItem, out var sourceCategoryModel, out _)
			) {
				if (IsFavoritesCategory(sourceCategoryModel)) {
					// Dragging from the "Favorites" category is not supported within the Toolbox control. Favorites can only
					//   be dragged outside of the control (i.e. to the designer surface).
					e.Effects = DragDropEffects.None;
					e.Handled = true;
				}
				else if (IsFavoritesCategory(targetCategoryModel)) {
					// Dropping on the "Favorites" category will only change if the control is a favorite and will not
					//   alter the location of the source control in the toolbox, so use the 'Copy' effect.
					if ((e.AllowedEffects & DragDropEffects.Copy) == DragDropEffects.Copy) {
						e.Effects = DragDropEffects.Copy;
						e.Handled = true;
					}
				}
				else {
					// Controls cannot exist in more than one category, so only move effect is allowed
					if ((e.AllowedEffects & DragDropEffects.Move) == DragDropEffects.Move) {
						e.Effects = DragDropEffects.Move;
						e.Handled = true;
					}
				}

				if ((e.Handled) && (e.Effects != DragDropEffects.None)) {

					// TreeItems can be dropped 'Before', 'On', or 'After' a target node, but drop targets may not support
					//   all of these scenarios. Coerce the drop area, as needed, based on the target node.

					if (targetItem is CategoryTreeNodeModel) {
						// Since controls can only be children to a category, not siblings, coerce all drops to 'On'
						dropArea = TreeItemDropArea.On;
					}
					else if (targetItem is EmptyPlaceholderTreeNodeModel) {
						// When the control is dropped, the empty placeholder will be remove and the dropped control
						// will be added as first child to the category, so coerce all drops to 'Before' the placeholder.
						dropArea = TreeItemDropArea.Before;
					}
					else if ((targetItem is ControlTreeNodeModel) && (dropArea == TreeItemDropArea.On)) {
						// A control cannot have children, so coerce dropping 'On' a control to 'After' (i.e. next sibling)
						dropArea = TreeItemDropArea.After;
					}

					return dropArea;
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
		var targetModel = targetItem as ToolboxTreeNodeModel;
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
					// When dropping before or after, a new node will be inserted as a child to the parent of the target
					var nav = targetControl.GetItemNavigator(targetItem!);
					if (nav is not null) {
						// Cache the original target
						var targetChildModel = targetModel;

						// Quit if unable to move to navigate to the parent
						if (!nav.GoToParent())
							return;

						// Update the target item to be the parent of the original target
						targetItem = nav.CurrentItem;
						targetModel = targetItem as ToolboxTreeNodeModel;

						// Quit if the new target is not the expected model
						if (targetModel is null)
							return;

						// Resolve index of the new node based on whether it should be before
						// or after the original target.
						var index = targetModel.Children.IndexOf(targetChildModel);
						if (index != -1)
							targetDropIndex = index + (dropArea == TreeItemDropArea.After ? 1 : 0);
					}
					break;
			}

			// Resolve the category for the drop target
			if (!TryGetCategory(targetControl, targetModel, out var targetCategoryModel)) {
				MessageBox.Show("Unable to determine the drop category.", "Drag and Drop", MessageBoxButton.OK);
				return;
			}

			// Get the dragged controls (only control nodes are currently supported)
			var sourceControlModels = GetDraggedModels(e.Data, targetControl)
				.OfType<ControlTreeNodeModel>()
				.ToList();
			if (sourceControlModels.Count > 0) {
				// Check each item and validate that various drop operations are allowed before actually executing the drop
				foreach (var sourceModel in sourceControlModels) {
					if (sourceModel == targetModel) {
						MessageBox.Show("Cannot drop an item on itself.", "Drag and Drop", MessageBoxButton.OK);
						return;
					}
					else {
						var nav = targetControl.GetItemNavigator(sourceModel);
						if (nav is null) {
							MessageBox.Show("Cannot drop from a different control.", "Drag and Drop", MessageBoxButton.OK);
							return;
						}
						else {
							if (nav.GoToCommonAncestor(targetItem!)) {
								if (nav.CurrentItem == sourceModel) {
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
				else if ((originalEffects & DragDropEffects.Copy) == DragDropEffects.Copy) {
					e.Effects = DragDropEffects.Copy;
					e.Handled = true;
				}

				if (IsFavoritesCategory(targetCategoryModel)) {
					// Controls dragged into favorites category will not actually be moved. They will be added as favorites.
					foreach (var sourceControlModel in sourceControlModels)
						sourceControlModel.IsFavorite = true;
				}
				else {
					// Complete operation
					var isMove = e.Effects == DragDropEffects.Move;
					foreach (var sourceControlModel in sourceControlModels) {
						// Resolve the source category of the dragged control
						if (!TryGetCategory(targetControl, sourceControlModel, out var sourceCategoryModel))
							break;

						if (isMove) {
							// Remove the control from the original category
							var index = sourceCategoryModel!.Children.IndexOf(sourceControlModel);
							if (index != -1) {
								if ((sourceCategoryModel == targetCategoryModel) && (index < targetDropIndex))
									targetDropIndex--;

								sourceCategoryModel.Children.RemoveAt(index);
							}
							else {
								// Quit processing if any source cannot be located
								break;
							}

							// Add the control to the new category (may be the same as the source if it was repositioned)
							int resolvedTargetDropIndex = (targetDropIndex++).ClampToRange(0, targetCategoryModel!.Children.Count);
							targetCategoryModel.Children.Insert(resolvedTargetDropIndex, sourceControlModel);
							targetCategoryModel.IsExpanded = true;

							// Focus the last item that was moved
							if (sourceControlModels[sourceControlModels.Count - 1] == sourceControlModel) {
								// Must dispatch the focus changes to allow the view to update based on changes in the models
								Dispatcher.CurrentDispatcher.BeginInvoke(() => {
									targetControl.FocusItem(sourceControlModel);
								}, DispatcherPriority.Normal);
							}
						}
					}
				}
			}
		}
	}

}
