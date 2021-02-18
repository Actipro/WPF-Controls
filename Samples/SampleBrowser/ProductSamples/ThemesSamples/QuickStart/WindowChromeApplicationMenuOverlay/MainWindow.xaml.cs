using System;
using System.Windows;
using System.Windows.Input;
using ActiproSoftware.Windows.Themes;

namespace ActiproSoftware.ProductSamples.ThemesSamples.QuickStart.WindowChromeApplicationMenuOverlay {

	/// <summary>
	/// Provides the main window for this sample.
	/// </summary>
	public partial class MainWindow {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainWindow</c> class.
		/// </summary>
		public MainWindow() {
			// Force an Office Colorful theme for this sample
			if (!ThemeManager.CurrentTheme.StartsWith("OfficeColorful")) {
				ThemeManager.UnregisterAutomaticThemes();
				ThemeManager.CurrentTheme = ThemeNames.OfficeColorfulIndigo;
			}

			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the help command executes.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private void OnHelpCommandExecuted(object sender, ExecutedRoutedEventArgs e) {
			MessageBox.Show("Show documentation here.", "Documentation", MessageBoxButton.OK, MessageBoxImage.Information);
		}

		/// <summary>
		/// Occurs when overlay is toggled.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnIsOverlayVisibleChanged(object sender, RoutedEventArgs e) {
			// NOTE: This event handler is a good place to programmatically adjust the UI when the overlay state changes, if necessary
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when a key is pressed while the control has focus.
		/// </summary>
		/// <param name="e">A <see cref="KeyEventArgs"/> that contains the event data.</param>
		protected override void OnKeyDown(KeyEventArgs e) {
			if (e == null)
				throw new ArgumentNullException("e");

			// Call the base method
			base.OnKeyDown(e);

			if (!e.Handled) {
				switch (e.Key) {
					case Key.Escape:
						// Ensure the overlay is closed when Esc is pressed
						if (WindowChrome.GetIsOverlayVisible(window))
							WindowChrome.SetIsOverlayVisible(window, false);
						break;
				}
			}
		}

	}

}