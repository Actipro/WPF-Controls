using System.Windows;
using ActiproSoftware.ProductSamples.DockingSamples.Common;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.TabbedMdiNewTabButton {

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
		/// Occurs when a new window is requested by the user.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>RoutedEventArgs</c> that containd data related to this event.</param>
		private void OnDockSiteNewWindowRequested(object sender, RoutedEventArgs e) {
			DocumentHelper.CreateTextDocumentWindow(dockSite, ++documentIndex);
		}
		
	}

}
