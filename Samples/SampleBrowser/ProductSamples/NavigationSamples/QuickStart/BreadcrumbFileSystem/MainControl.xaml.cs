using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Controls.Navigation;
using System.Windows.Threading;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.BreadcrumbFileSystem;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	private MyComputerData? _myComputer;

	/// <summary>
	/// Holds a Boolean value indicating whether the selection is currently being synchronized between the TreeView and the Breadcrumb.
	/// </summary>
	private bool _synchronizingSelection;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		// Expand all root TreeView nodes on load
		Dispatcher.BeginInvoke(DispatcherPriority.Loaded, () => {
			if (treeView.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated) {
				foreach (var item in treeView.Items) {
					var container = treeView.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
					if (container is not null)
						container.IsExpanded = true;
				}
			}
		});
	}

	// --------------------------------------------------------------------------------------------------
	// NON- PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Handles the <see cref="Breadcrumb.ConvertItem"/> event.
	/// </summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnBreadcrumbConvertItem(object? sender, BreadcrumbConvertItemEventArgs e)
		=> ConvertItemHelper.HandleConvertItem(sender, e);

	/// <summary>
	/// Handles the <see cref="Breadcrumb.SelectedItemChanged"/> event.
	/// </summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnBreadcrumbSelectedItemChanged(object? sender, ObjectPropertyChangedRoutedEventArgs e) {
		if (_synchronizingSelection)
			return;

		_synchronizingSelection = true;
		try {
			UpdateComboBoxItems();

			// We will get the trail to the item selected in the Breadcrumb and use that to select the item in the TreeView
			var trail = ConvertItemHelper.GetTrail(breadcrumb.RootItem, breadcrumb.SelectedItem);
			if (trail is { Count: > 0 })
				SelectItem(treeView, trail, 0);
		}
		finally {
			_synchronizingSelection = false;
		}
	}

	/// <summary>
	/// Attempts to select a specific node in a TreeView, by recursively drilling down to the item indicated by the specified trail.
	/// </summary>
	/// <param name="control">The control.</param>
	/// <param name="trail">The trail.</param>
	/// <param name="index">The index.</param>
	public static void SelectItem(ItemsControl control, IList trail, int index) {
		var currentItem = trail[index];

		// If the control has not generated it's containers, then we need to delay our call until it does.
		if (control.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated) {
			// Find the current item in the control's Items
			foreach (object item in control.Items) {
				if (item == currentItem) {
					var container = (TreeViewItem)control.ItemContainerGenerator.ContainerFromItem(item);
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
			control.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, () => {
				SelectItem(control, trail, index);
			});
		}
	}

	private void OnTreeViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
		if (_synchronizingSelection)
			return;

		_synchronizingSelection = true;
		try {
			// In order to synchronize the TreeView's selected item to the Breadcrumb, we simply set the SelectedItem property.
			breadcrumb.SelectedItem = treeView.SelectedItem;
			UpdateComboBoxItems();
		}
		finally {
			_synchronizingSelection = false;
		}
	}

	/// <summary>
	/// Updates the <see cref="ComboBoxItems"/>.
	/// </summary>
	private void UpdateComboBoxItems() {
		if (breadcrumb.SelectedItem is { } selectedItem) {
			ComboBoxItems.BeginUpdate();
			try {
				// Make sure item doesn't already exist in the list
				while (ComboBoxItems.Remove(selectedItem)) { /* no-op */ }

				// Insert it at the beginning
				ComboBoxItems.Insert(0, selectedItem);

				// Cap the size of the list
				while (ComboBoxItems.Count > 15)
					ComboBoxItems.RemoveAt(15);
			}
			finally {
				ComboBoxItems.EndUpdate();
			}
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The combo box items.
	/// </summary>
	public DeferrableObservableCollection<object> ComboBoxItems { get; } = [];

	/// <summary>
	/// The root data for the Breadcrumb.
	/// </summary>
	public MyComputerData[] MyComputer {
		get {
			_myComputer ??= new MyComputerData();
			return [_myComputer];
		}
	}

}
