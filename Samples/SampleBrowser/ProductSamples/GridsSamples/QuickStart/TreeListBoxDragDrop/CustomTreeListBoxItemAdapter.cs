using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using ActiproSoftware.ProductSamples.GridsSamples.Common;
using ActiproSoftware.Windows.Controls.Grids;

#if WPF
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;
#endif

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.TreeListBoxDragDrop {
	
	/// <summary>
	/// An adapter that can provide an item's hierarchy and visual state data for usage in a <see cref="TreeListBox"/>.
	/// This default implementation is intended to specifically adapt <see cref="TreeNodeModel"/>
	/// and is geared for high-performance due to the various get/set method overrides
	/// instead of using bindings for updates.
	/// </summary>
	public class CustomTreeListBoxItemAdapter : DefaultTreeListBoxItemAdapter {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a <see cref="IDataObject"/> based on the specified items.
		/// </summary>
		/// <param name="sourceControl">The source control that contains the items.</param>
		/// <param name="dataObject">The <see cref="IDataObject"/> to initialize.</param>
		/// <param name="items">The items to examine.</param>
		/// <returns>The <see cref="DragDropEffects"/> that specifies the allowed drag/drop effects for the <see cref="IDataObject"/>.</returns>
		/// <remarks>
		/// The default implementation of this method simply return <c>DragDropEffects.None</c>.
		/// Override this method to initialize the <see cref="IDataObject"/> with text and/or other data formats 
		/// (such as <see cref="TreeListBox.ItemDataFormat"/> for supporting drag/drop),
		/// and to return an appropriate <c>DragDropEffects</c> result.
		/// </remarks>
		public override DragDropEffects InitializeDataObject(TreeListBox sourceControl, IDataObject dataObject, IEnumerable<object> items) {
			if (sourceControl == null)
				throw new ArgumentNullException("sourceControl");
			if (dataObject == null)
				throw new ArgumentNullException("dataObject");
			if (items == null)
				throw new ArgumentNullException("items");

			// Store the full paths to items in case we drop on the tree itself... 
			//   Each item needs to have a unique path, which comes from adapter GetPath() calls
			var fullPaths = new StringBuilder();
			foreach (var item in items)
				fullPaths.AppendLine(sourceControl.GetFullPath(item));
			if (fullPaths.Length > 0)
				dataObject.SetData(TreeListBox.ItemDataFormat, fullPaths.ToString());

			// If there is one item, store its text so that it can be dropped elsewhere
			if (items.Count() == 1) {
				var viewModel = items.First() as TreeNodeModel;
				if (viewModel != null)
					dataObject.SetData(DataFormats.Text, viewModel.Name);
			}

			return DragDropEffects.Move;
		}
		
		/// <summary>
		/// Notifies that a drag event occurred over a target item, requests that appropriate updates are made to the supplied <c>DragEventArgs</c>,
		/// and requests that the allowed drop area is returned for visual target feedback.
		/// </summary>
		/// <param name="e">The <see cref="DragEventArgs"/> whose effects should be updated.</param>
		/// <param name="targetControl">The target control, over which the event occurred</param>
		/// <param name="targetItem">The target item, which could be <c>null</c> if dragging below the last tree item.</param>
		/// <param name="dropArea">A <see cref="TreeItemDropArea"/> indicating the drop area over the target item.</param>
		/// <returns>
		/// A <see cref="TreeItemDropArea"/> indicating the allowed drop area, which will be used for visual feedback to the end user.
		/// Return <c>TreeItemDropArea.None</c> for no visual feedback.
		/// </returns>
		/// <remarks>
		/// The default implementation of this method sets the <c>e.Effects</c> to <c>DragDropEffects.None</c> and returns that no drop area is allowed.
		/// Override this method if you wish to support dropping and add logic to properly handle the dragged data.
		/// </remarks>
		public override TreeItemDropArea OnDragOver(DragEventArgs e, TreeListBox targetControl, object targetItem, TreeItemDropArea dropArea) {
			// If the drag is over an item and there is item data present...
			if ((targetItem != null) && (dropArea != TreeItemDropArea.None) && (e.Data.GetDataPresent(TreeListBox.ItemDataFormat))) {
				var fullPaths = e.Data.GetData(TreeListBox.ItemDataFormat) as string;
				if (!string.IsNullOrEmpty(fullPaths)) {
					// Locate the first item based on full path
					object firstItem = null;
					foreach (var fullPath in fullPaths.Split(new char[] { '\r', '\n' })) {
						if (!string.IsNullOrEmpty(fullPath)) {
							var item = targetControl.GetItemByFullPath(fullPath);
							if (item != null) {
								firstItem = item;
								break;
							}
						}
					}

					if (firstItem != null) {
						// Ensure that the first item is already in the target control (nav will be null if not)... if allowing drag/drop onto external
						//   controls, you cannot use the item navigator and must rely on your own item hierarchy logic
						var firstItemNav = targetControl.GetItemNavigator(firstItem);
						if (firstItemNav != null) {
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
										if ((targetItemNav != null) && (targetItemNav.GoToNextSibling()))
											dropArea = TreeItemDropArea.On;
									}

									return dropArea;
							}
						}
					}
				}
			}

			e.Effects = DragDropEffects.None;
			return TreeItemDropArea.None;
		}

		/// <summary>
		/// Notifies that a drop event occurred over a target item and requests that appropriate updates are made to the supplied <c>DragEventArgs</c>.
		/// </summary>
		/// <param name="e">The <see cref="DragEventArgs"/> whose effects should be updated.</param>
		/// <param name="targetControl">The target control, over which the event occurred</param>
		/// <param name="targetItem">The target item, which could be <c>null</c> if dragging below the last tree item.</param>
		/// <param name="dropArea">A <see cref="TreeItemDropArea"/> indicating the drop area over the target item.</param>
		/// <remarks>
		/// The default implementation of this method sets the <c>e.Effects</c> to <c>DragDropEffects.None</c> and takes no further action.
		/// Override this method if you wish to support dropping and add logic to properly handle the dragged data.
		/// </remarks>
		public override void OnDrop(DragEventArgs e, TreeListBox targetControl, object targetItem, TreeItemDropArea dropArea) {
			var originalEffects = e.Effects;
			e.Effects = DragDropEffects.None;

			// If the drag is over an item and there is item data present...
			var targetModel = targetItem as TreeNodeModel;
			if ((targetModel != null) && (dropArea != TreeItemDropArea.None) && (e.Data.GetDataPresent(TreeListBox.ItemDataFormat))) {
				// Resolve the real target item (in case the drop area is above or below the target item)
				var targetDropIndex = targetModel.Children.Count;
				switch (dropArea) {
					case TreeItemDropArea.Before:
					case TreeItemDropArea.After:
						var nav = targetControl.GetItemNavigator(targetItem);
						if (nav != null) {
							var targetChildModel = targetModel;
							var targetChildItem = targetItem;

							if (!nav.GoToParent())
								return;
							targetItem = nav.CurrentItem;
							targetModel = targetItem as TreeNodeModel;
							if (targetModel == null)
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
					foreach (var fullPath in fullPaths.Split(new char[] { '\r', '\n' })) {
						if (!string.IsNullOrEmpty(fullPath)) {
							var item = targetControl.GetItemByFullPath(fullPath);
							if (item != null)
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
								if (nav == null) {
									MessageBox.Show("Cannot drop from a different control.", "Drag and Drop", MessageBoxButton.OK);
									return;
								}
								else {
									if (nav.GoToCommonAncestor(targetItem)) {
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
						foreach (var item in items) {
							var nav = targetControl.GetItemNavigator(item);
							if (nav.GoToParent()) {
								var itemModel = item as TreeNodeModel;
								var parentModel = nav.CurrentItem as TreeNodeModel;
								if ((itemModel != null) && (parentModel != null)) {
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

								targetModel.Children.Insert(Math.Max(0, Math.Min(targetModel.Children.Count, targetDropIndex++)), itemModel);
								targetModel.IsExpanded = true;
										
								// Focus the last item
								if (items[items.Count - 1] == item)
									targetControl.FocusItem(itemModel);
							}
						}
					}
				}
			}
		}
		
	}

}
