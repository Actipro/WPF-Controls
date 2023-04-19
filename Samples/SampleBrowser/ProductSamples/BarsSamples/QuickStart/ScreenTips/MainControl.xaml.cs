using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Bars;
using System.Windows;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.ScreenTips {

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

			// Add command bindings
			this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Help, OnApplicationHelpExecute));
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private void OnApplicationHelpExecute(object sender, ExecutedRoutedEventArgs e) {
			// Quit if already handled
			if (e.Handled)
				return;

			// First look to see if a ScreenTip is displayed, and if so, show the context help for that
			var screenTip = ScreenTipService.Current.CurrentScreenTip;
			if (TryShowScreenTipContextualHelp(screenTip)) {
				// Quit when contextual help is displayed
				e.Handled = true;
				return;
			}

			// Show default help topic
			ThemedMessageBox.Show("Show the default help topic here.", "Contextual Help", MessageBoxButton.OK, MessageBoxImage.Information);
		}

		/// <summary>
		/// Tries to show contextual help for a <see cref="ScreenTip"/>.
		/// </summary>
		/// <param name="screenTip">The <see cref="ScreenTip"/> to examine.</param>
		/// <returns><c>true</c> if contextual help was displayed for the <see cref="ScreenTip"/>; otherwise <c>false</c>.</returns>
		private static bool TryShowScreenTipContextualHelp(ScreenTip screenTip) {
			if (screenTip?.Tag is ScreenTipData screenTipData) {
				var key = screenTipData.Key;

				if (!string.IsNullOrWhiteSpace(key)) {
					// Typically the Key would be used to resolve any contextual help topic for a ScreenTip.
					bool hasContextualHelp = key.EndsWith("FooterHelpContext")
						|| key.EndsWith("ComplexScreenTip");

					if (hasContextualHelp) {
						ThemedMessageBox.Show(
							string.Format("Show the help topic for '{0}' here if appropriate.\r\n\r\nThe ScreenTip Key is: {1}",
								screenTip.Header, screenTipData.Key),
							"Contextual Help", MessageBoxButton.OK, MessageBoxImage.Information);
						return true;
					}

				}

			}
			return false;
		}

	}

}