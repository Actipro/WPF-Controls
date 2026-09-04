using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Controls.Ribbon.UI;
using RibbonControls = ActiproSoftware.Windows.Controls.Ribbon.Controls;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.RibbonSamples.QuickStart.CustomizingContextMenus;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the class.
	/// </summary>
	static MainControl() {
		// Attach to the popup opening event (used for QAT customize menu only)
		EventManager.RegisterClassHandler(typeof(MainControl), PopupControlService.PopupOpeningEvent, new EventHandler<CancelRoutedEventArgs>(OnPopupOpeningEvent));
	}

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Dynamically adds a menu item to a context menu.
	/// </summary>
	/// <param name="owner">The owner of the context menu.</param>
	/// <param name="menu">The menu to update.</param>
	internal static void AddCustomMenuItem(object owner, RibbonControls.Menu menu) {
		var customButtonLabel = "Programmatically-Added Menu Item";

		// Determine if there is a Custom menu item already in the menu
		var hasCustomItem = false;
		foreach (var childObj in menu.Items) {
			if ((childObj is RibbonControls.Button button) && (button.Label == customButtonLabel)) {
				hasCustomItem = true;
				break;
			}
		}

		// If the custom item hasn't been added to this context menu...
		if (!hasCustomItem) {
			// Add a separator and a custom button... normally the button would have a command assigned too
			menu.Items.Add(new RibbonControls.Separator());
			var newButton = new RibbonControls.Button {
				Label = customButtonLabel
			};
			newButton.Click += OnCustomMenuItemClicked;
			newButton.Tag = new WeakReference(owner);
			menu.Items.Add(newButton);
		}
	}

	private static void OnCustomMenuItemClicked(object? sender, RibbonControls.ExecuteRoutedEventArgs e) {
		e.Handled = true;
		var button = (RibbonControls.Button)sender!;
		var ownerRef = (WeakReference)button.Tag;
		MessageBox.Show(string.Format("You clicked the programmatically-added menu item for: {0}", ownerRef.Target));
	}

	private static void OnPopupOpeningEvent(object? sender, CancelRoutedEventArgs e) {
		var popupButton = e.OriginalSource as RibbonControls.PopupButton;
		if (popupButton is RibbonControls.Primitives.QuickAccessToolBarCustomizeButton) {
			var menu = popupButton.PopupContent as RibbonControls.Menu;
			if (menu is not null)
				AddCustomMenuItem(popupButton, menu);
		}
	}

}
