using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Threading;

using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Controls.Navigation;
using ActiproSoftware.ProductSamples.NavigationSamples.Common.Breadcrumb.ShellItem;

namespace ActiproSoftware.ProductSamples.NavigationSamples.Demo.BreadcrumbIntro {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		/// <summary>
		/// Holds the items shown in the ComboBox in the Breadcrumb.
		/// </summary>
		private DeferrableObservableCollection<object> comboBoxItems = new DeferrableObservableCollection<object>();

		/// <summary>
		/// Holds a Boolean value indicating whether the selection is currently being synchronized between the TreeView and the
		/// Breadcrumb.
		/// </summary>
		private bool synchronizingSelection;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			// Expand all root TreeView nodes on load
			this.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, (DispatcherOperationCallback)delegate(object arg) {
				if (this.treeView.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated) {
					foreach (object item in this.treeView.Items) {
						TreeViewItem container = (TreeViewItem)this.treeView.ItemContainerGenerator.ContainerFromItem(item);
						if (null != container)
							container.IsExpanded = true;
					}
				}
				return null;
			}, null);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Handles the ConvertItem event of the breadcrumb control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="ActiproSoftware.Windows.Controls.Navigation.BreadcrumbConvertItemEventArgs"/> instance containing the event data.</param>
		private void OnBreadcrumbConvertItem(object sender, BreadcrumbConvertItemEventArgs e) {
			ConvertItemHelper.HandleConvertItem(sender, e);
		}

		/// <summary>
		/// Handles the SelectedItemChanged event of the breadcrumb control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="PropertyChangedRoutedEventArgs{object}"/> instance containing the event data.</param>
		private void OnBreadcrumbSelectedItemChanged(object sender, ObjectPropertyChangedRoutedEventArgs e) {
			if (this.synchronizingSelection)
				return;

			this.synchronizingSelection = true;
			try {
				UpdateComboBoxItems();

				// We will get the trail to the item selected in the Breadcrumb and use that to select the item in the TreeView
				IList trail = ConvertItemHelper.GetTrail(this.breadcrumb.RootItem, this.breadcrumb.SelectedItem);
				if (null != trail && 0 != trail.Count)
					SelectItem(this.treeView, trail, 0);
			}
			finally {
				this.synchronizingSelection = false;
			}
		}

		/// <summary>
		/// Attempts to select a specific node in a TreeView, by recursively drilling down to the item indicated by the specified
		/// trail.
		/// </summary>
		/// <param name="control">The control.</param>
		/// <param name="trail">The trail.</param>
		/// <param name="index">The index.</param>
		public static void SelectItem(ItemsControl control, IList trail, int index) {
			object currentItem = trail[index];

			// If the control has not generated it's containers, then we need to delay our call until it does.
			if (control.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated) {
				// Find the current item in the control's Items
				foreach (object item in control.Items) {
					if (item == currentItem) {
						TreeViewItem container = (TreeViewItem)control.ItemContainerGenerator.ContainerFromItem(item);
						if (++index < trail.Count) {
							// We have more items to drill down into, so use a recursive call with a new control and index
							container.IsExpanded = true;
							SelectItem(container, trail, index);
						}
						else {
							// We found the item, so select it and bring it into view
							container.IsSelected = true;
							container.BringIntoView();
						}
						break;
					}
				}
			}
			else {
				control.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, (DispatcherOperationCallback)delegate(object arg) {
					SelectItem(control, trail, index);
					return null;
				}, null);
			}
		}

		/// <summary>
		/// Handles the SelectedItemChanged event of the treeView control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedPropertyChangedEventArgs{object}"/> instance containing the event data.</param>
		private void OnTreeViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
			if (this.synchronizingSelection)
				return;

			this.synchronizingSelection = true;
			try {
				// In order to synchronize the TreeView's selected item to the Breadcrumb, we simply set the SelectedItem property.
				this.breadcrumb.SelectedItem = this.treeView.SelectedItem;
				UpdateComboBoxItems();
			}
			finally {
				this.synchronizingSelection = false;
			}
		}

		/// <summary>
		/// Updates the <see cref="ComboBoxItems"/>.
		/// </summary>
		private void UpdateComboBoxItems() {
			if (null != this.breadcrumb.SelectedItem) {
				this.ComboBoxItems.BeginUpdate();
				try {
					// Make sure item doesn't already exist in the list
					while (this.ComboBoxItems.Remove(this.breadcrumb.SelectedItem))
						; // No-op

					// Insert it at the beginning
					this.ComboBoxItems.Insert(0, this.breadcrumb.SelectedItem);

					// Cap the size of the list
					while (this.ComboBoxItems.Count > 15)
						this.ComboBoxItems.RemoveAt(15);
				}
				finally {
					this.ComboBoxItems.EndUpdate();
				}
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the combo box items.
		/// </summary>
		/// <value>The combo box items.</value>
		public DeferrableObservableCollection<object> ComboBoxItems {
			get{
				return this.comboBoxItems;
			}
		}
	}
}