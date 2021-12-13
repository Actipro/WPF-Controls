using System.Linq;
using System.Text;
using System.Windows;
using ActiproSoftware.ProductSamples.DockingSamples.Common;
using ActiproSoftware.Windows.Controls.Docking;

#if WPF
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;
#endif

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.PromptOnClose {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		private int documentIndex = 3;

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
		/// Occurs before one or more docking windows are closed, allowing for cancellation of the close.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowsEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteWindowsClosing(object sender, DockingWindowsEventArgs e) {
			var documents = e.Windows.OfType<DocumentWindow>().ToArray();
			if (documents.Any()) {
				var message = new StringBuilder("Are you sure you want to close:");
				foreach (var document in documents)
					message.Append("\r\n* " + document.FileName);

				if (MessageBox.Show(message.ToString(), "Confirm Close", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.No) {
					e.Cancel = true;
					e.Handled = true;
				}
			}
		}

		/// <summary>
		/// Occurs when the menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>RoutedEventArgs</c> that contains data related to this event.</param>
		private void OnNewDocumentMenuItemClick(object sender, RoutedEventArgs e) {
			DocumentHelper.CreateTextDocumentWindow(dockSite, ++documentIndex);
		}

		/// <summary>
		/// Occurs when the menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>RoutedEventArgs</c> that contains data related to this event.</param>
		private void OnOpenDocumentMenuItemClick(object sender, RoutedEventArgs e) {
			DocumentHelper.OpenTextDocumentWindow(dockSite);
		}
		
	}

}
