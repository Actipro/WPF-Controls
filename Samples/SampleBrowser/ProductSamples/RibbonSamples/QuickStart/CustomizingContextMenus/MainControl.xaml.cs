using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Controls.Ribbon.UI;
using System;
using System.Windows;
using System.Windows.Input;
using RibbonControls = ActiproSoftware.Windows.Controls.Ribbon.Controls;

#if WPF
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;
#endif

namespace ActiproSoftware.ProductSamples.RibbonSamples.QuickStart.CustomizingContextMenus {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes the <c>MainControl</c> class.
		/// </summary>
		static MainControl() {
			// Attach to the popup opening event (used for QAT customize menu only) 
			EventManager.RegisterClassHandler(typeof(MainControl), PopupControlService.PopupOpeningEvent, new EventHandler<CancelRoutedEventArgs>(OnPopupOpeningEvent));
		}

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Dynamically adds a menu item to a context menu.
		/// </summary>
		/// <param name="owner">The owner of the context menu.</param>
		/// <param name="menu">The menu to update.</param>
		internal static void AddCustomMenuItem(object owner, RibbonControls.Menu menu) {
			string customButtonLabel = "Programmatically-Added Menu Item";

			// Determine if there is a Custom menu item already in the menu
			bool hasCustomItem = false;
			foreach (object childObj in menu.Items) {
				if ((childObj is RibbonControls.Button) && (((RibbonControls.Button)childObj).Label == customButtonLabel)) {
					hasCustomItem = true;
					break;
				}
			}

			// If the custom item hasn't been added to this context menu...
			if (!hasCustomItem) {
				// Add a separator and a custom button... normally the button would have a command assigned too
				menu.Items.Add(new RibbonControls.Separator());
				RibbonControls.Button newButton = new RibbonControls.Button();
				newButton.Label = customButtonLabel;
				newButton.Click += new EventHandler<RibbonControls.ExecuteRoutedEventArgs>(OnCustomMenuItemClicked);
				newButton.Tag = new WeakReference(owner);
				menu.Items.Add(newButton);
			}
		}

		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RibbonControls.ExecuteRoutedEventArgs"/> that contains the event data.</param>
		private static void OnCustomMenuItemClicked(object sender, RibbonControls.ExecuteRoutedEventArgs e) {
			e.Handled = true;
			RibbonControls.Button button = (RibbonControls.Button)sender;
			WeakReference ownerRef = (WeakReference)button.Tag;
			MessageBox.Show(String.Format("You clicked the programmatically-added menu item for: {0}", ownerRef.Target));
		}

		/// <summary>
		/// Processes the <see cref="PopupOpeningEvent"/> event.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <c>CancelRoutedEventArgs</c> that contains the event data.</param>
		private static void OnPopupOpeningEvent(object sender, CancelRoutedEventArgs e) {
			RibbonControls.PopupButton popupButton = e.OriginalSource as RibbonControls.PopupButton;
			if (popupButton is RibbonControls.Primitives.QuickAccessToolBarCustomizeButton) {
				RibbonControls.Menu menu = popupButton.PopupContent as RibbonControls.Menu;
				if (menu != null)
					MainControl.AddCustomMenuItem(popupButton, menu);
			}
		}

	}
}