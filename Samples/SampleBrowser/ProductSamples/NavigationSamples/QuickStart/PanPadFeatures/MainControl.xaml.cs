using ActiproSoftware.Windows.Controls.Navigation.Primitives;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.PanPadFeatures;

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

		panPad.Pan += OnPanPadPan;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Appends a message to the events <see cref="ListBox"/>.
	/// </summary>
	/// <param name="text">The text to append.</param>
	private void AppendMessage(string text) {
		if (eventsListBox is null)
			return;

		var item = new ListBoxItem() { Content = text };
		eventsListBox.Items.Add(item);
		eventsListBox.SelectedItem = item;
		eventsListBox.ScrollIntoView(item);
	}

	private void OnClearButtonClick(object sender, RoutedEventArgs e)
		=> eventsListBox?.Items.Clear();

	/// <summary>
	/// Handles the <see cref="PanPad.Pan"/> event.
	/// </summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnPanPadPan(object? sender, PanEventArgs e)
		=> AppendMessage(string.Format("Pan X={0}, Y={1}", e.XOffset, e.YOffset));

}
