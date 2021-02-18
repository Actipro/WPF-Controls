using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Media;
using ActiproSoftware.Windows.Themes;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.NavigationBarContextMenuCustomization {

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
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when the navigation bar's customize button is clicked, allowing you to change the <c>ContextMenu</c> that is displayed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="SelectionChangedEventArgs"/> that contains the event data.</param>
		private void OnNavigationBarCustomizeButtonClick(object sender, ContextMenuItemRoutedEventArgs e) {
			// Add a custom menu item to the end of the context menu that will be displayed
			e.Item.Items.Add(new Separator());
			MenuItem menuItem = new MenuItem();
			menuItem.Header = "Custom menu item";
			e.Item.Items.Add(menuItem);
		}

	}
}