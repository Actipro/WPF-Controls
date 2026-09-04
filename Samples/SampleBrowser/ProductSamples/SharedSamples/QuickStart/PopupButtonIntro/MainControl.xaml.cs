using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Controls;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.SharedSamples.QuickStart.PopupButtonIntro;

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
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnDynamicPopupButtonPopupOpening(object sender, CancelRoutedEventArgs e) {
		var popupButton = (PopupButton)sender;

		// Create a new PopupMenu each time the button is clicked
		popupButton.PopupMenu = new ContextMenu() {
			Items = {
				new MenuItem() { Header = "Created at: " + DateTime.Now.ToLongTimeString() }
			}
		};
	}

	private void OnPopupButtonClick(object sender, RoutedEventArgs e) {
		// Ignore clicks on other buttons within the popup
		if (e.OriginalSource is PopupButton)
			MessageBox.Show("You clicked a PopupButton's button portion.", "PopupButton", MessageBoxButton.OK, MessageBoxImage.Information);
	}

	private void OnPopupButtonPopupOpening(object sender, CancelRoutedEventArgs e) {
		if (PreventPopups) {
			MessageBox.Show("The popup is temporarily blocked, please uncheck the prevent popups checkbox to enable it again.",
				"PopupButton", MessageBoxButton.OK, MessageBoxImage.Error);
			e.Cancel = true;
			return;
		}

		// NOTE: The PopupMenu or PopupContent can be fully initialized & customized in this event before the popup/menu is opened

		// For this sample, we'll update the header of a menu item to show the current time
		customMenuItem.Header = "Opened at: " + DateTime.Now.ToLongTimeString();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Indicates whether to prevent popups.
	/// </summary>
	public bool PreventPopups { get; set; }

}
