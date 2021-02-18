using System;
using System.Windows;
using System.Windows.Controls;

namespace ActiproSoftware.ProductSamples.ThemesSamples.QuickStart.WindowChromeTitleBarBackButton {

	/// <summary>
	/// Represents a page.
	/// </summary>
	public partial class Page1 : Page {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>Page1</c> class.
		/// </summary>
		public Page1() {
			InitializeComponent();
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnNavigateToPage2ButtonClick(object sender, RoutedEventArgs e) {
			this.NavigationService.Navigate(new Page2());
		}

	}

}
