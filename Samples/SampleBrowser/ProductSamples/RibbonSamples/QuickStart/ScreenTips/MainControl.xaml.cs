using ActiproSoftware.Windows.Controls.Ribbon.UI;
using RibbonControls = ActiproSoftware.Windows.Controls.Ribbon.Controls;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.RibbonSamples.QuickStart.ScreenTips;

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

		// Add command bindings
		CommandBindings.Add(new CommandBinding(ApplicationCommands.Help, OnApplicationHelpExecute));
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnApplicationHelpExecute(object sender, ExecutedRoutedEventArgs e) {
		// First look to see if a screentip is displayed, and if so, show the context help for that
		if (ScreenTipService.CurrentScreenTip is { } screenTip) {
			MessageBox.Show(string.Format("Show the help topic for '{0}' here if appropriate.\r\n\r\nThe owner element is: {1}\r\nThe pre-defined help URI is: {2}",
				screenTip.Header, screenTip.OwnerElement, screenTip.HelpUri?.AbsoluteUri ?? "<null>"));
			return;
		}

		// Show default help topic
		MessageBox.Show("Show the default help topic here.");
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Occurs when a screen tip is opening.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnScreenTipOpening(object sender, RoutedEventArgs e) {
		// Dynamically generate the screen tip description here
		var button = (RibbonControls.Button)sender;
		button.ScreenTipDescription = "This description was generated dynamically at " + DateTime.Now.ToString() + ".";
	}

}
