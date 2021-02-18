using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using RibbonControls = ActiproSoftware.Windows.Controls.Ribbon.Controls;

namespace ActiproSoftware.ProductSamples.RibbonSamples.QuickStart.DynamicPopupContent {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs before a popup is opened.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnPopupButtonPopupOpening(object sender, RoutedEventArgs e) {
			// Insert random content into the popup
			RibbonControls.PopupButton popupOwner = (RibbonControls.PopupButton)sender;
			if (new Random().NextDouble() < 0.65) {
				// Create a menu
				RibbonControls.Menu menu = new RibbonControls.Menu();
				for (int index = 0; index < 3; index++) {
					RibbonControls.Button button = new RibbonControls.Button();
					button.Label = String.Format("Dynamically created menu item #{0}, created at {1}", index + 1, DateTime.Now);
					menu.Items.Add(button);	
				}
				popupOwner.PopupContent = menu;
			}
			else {
				// Create alternate content, the Actipro logo
				StackPanel panel = new StackPanel();
				panel.Children.Add(new TextBlock(new Run("Anything can be placed in a popup")));
				panel.Children.Add(new ActiproSoftware.Windows.Controls.ActiproLogo());
				popupOwner.PopupContent = panel;
			}
		}

	}
}