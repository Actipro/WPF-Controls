using ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.Common;

namespace ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.SlideInOut;

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

		Dispatcher.BeginInvoke(() => {
			TestObject.ResetCounter();
			for (var i = 0; i < 10; i++)
				AddNewItem();
		});
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void AddNewItem()
		=> listBox.Items.Add(new TestObject());

	private void OnClearAllClick(object sender, RoutedEventArgs e)
		=> listBox.Items.Clear();

	private void OnAddItemClick(object sender, RoutedEventArgs e)
		=> AddNewItem();

	private void OnRemoveItemsClick(object sender, RoutedEventArgs e) {
		for (var i = listBox.SelectedItems.Count - 1; i >= 0; i--)
			listBox.Items.Remove(listBox.SelectedItems[i]);
	}

}
