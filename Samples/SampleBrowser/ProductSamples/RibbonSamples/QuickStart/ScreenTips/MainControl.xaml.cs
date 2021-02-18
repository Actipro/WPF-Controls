using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using RibbonControls = ActiproSoftware.Windows.Controls.Ribbon.Controls;
using ActiproSoftware.Windows.Controls.Ribbon.UI;

namespace ActiproSoftware.ProductSamples.RibbonSamples.QuickStart.ScreenTips {

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
            this.CommandBindings.Add(new CommandBinding(System.Windows.Input.ApplicationCommands.Help, OnApplicationHelpExecute));
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
			// First look to see if a screentip is displayed, and if so, show the context help for that
			if (ScreenTipService.CurrentScreenTip != null) {
				MessageBox.Show(String.Format("Show the help topic for '{0}' here if appropriate.\r\n\r\nThe owner element is: {1}\r\nThe pre-defined help URI is: {2}", 
					ScreenTipService.CurrentScreenTip.Header, ScreenTipService.CurrentScreenTip.OwnerElement, 
					(ScreenTipService.CurrentScreenTip.HelpUri != null ? ScreenTipService.CurrentScreenTip.HelpUri.AbsoluteUri : "<null>")));
				return;
			}

			// Show default help topic
			MessageBox.Show("Show the default help topic here.");
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when a screen tip is opening.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnScreenTipOpening(object sender, RoutedEventArgs e) {
			// Dynamically generate the screen tip description here
			RibbonControls.Button button = (RibbonControls.Button)sender;
			button.ScreenTipDescription = "This description was generated dynamically at " + DateTime.Now.ToString() + ".";
		}

	}
}