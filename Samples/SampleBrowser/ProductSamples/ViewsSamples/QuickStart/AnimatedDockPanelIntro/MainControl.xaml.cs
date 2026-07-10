using ActiproSoftware.Extensions;
using ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.Common;
using ActiproSoftware.Windows.Controls.Views;

namespace ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.AnimatedDockPanelIntro;

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

		listBox.Items.Add(new ListBoxItem() { Style = Resources["CenterContentListBoxItemStyle"] as Style }); // Placeholder for center
		AddNewItem(Dock.Top);
		AddNewItem(Dock.Bottom);
		AddNewItem(Dock.Left);
		AddNewItem(Dock.Right);
		listBox.SelectedIndex = 0;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void AddNewItem(Dock dock) {
		var item = CreateItem();
		AnimatedDockPanel.SetDock(item, dock);
		listBox.Items.Insert((listBox.Items.Count - 1).ClampToNonnegative(), item);

	}

	/// <summary>
	/// Creates a new <see cref="ProductListBoxItem"/> instance.
	/// </summary>
	private static ProductListBoxItem CreateItem()
		=> new() { IsDockable = true };

	private void OnClearAllClick(object sender, RoutedEventArgs e) {
		for (var i = listBox.Items.Count - 2; i >= 0; i--)
			listBox.Items.RemoveAt(i);
	}

	private void OnAddItemBottomClick(object sender, RoutedEventArgs e)
		=> AddNewItem(Dock.Bottom);

	private void OnAddItemLeftClick(object sender, RoutedEventArgs e)
		=> AddNewItem(Dock.Left);

	private void OnAddItemRightClick(object sender, RoutedEventArgs e)
		=> AddNewItem(Dock.Right);

	private void OnAddItemTopClick(object sender, RoutedEventArgs e)
		=> AddNewItem(Dock.Top);

}
