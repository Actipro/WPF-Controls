using System.Windows.Documents;
using RibbonControls = ActiproSoftware.Windows.Controls.Ribbon.Controls;

namespace ActiproSoftware.ProductSamples.RibbonSamples.QuickStart.DynamicPopupContent;

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
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnPopupButtonPopupOpening(object sender, RoutedEventArgs e) {
		// Insert random content into the popup
		var popupOwner = (RibbonControls.PopupButton)sender;
		if (new Random().NextDouble() < 0.65) {
			// Create a menu
			var menu = new RibbonControls.Menu();
			for (var index = 0; index < 3; index++) {
				var button = new RibbonControls.Button {
					Label = string.Format("Dynamically created menu item #{0}, created at {1}", index + 1, DateTime.Now)
				};
				menu.Items.Add(button);
			}
			popupOwner.PopupContent = menu;
		}
		else {
			// Create alternate content, the Actipro logo
			var panel = new StackPanel();
			panel.Children.Add(new TextBlock(new Run("Anything can be placed in a popup")));
			panel.Children.Add(new Windows.Controls.ActiproLogo());
			popupOwner.PopupContent = panel;
		}
	}

}
