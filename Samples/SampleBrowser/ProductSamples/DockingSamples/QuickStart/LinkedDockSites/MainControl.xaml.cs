namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.LinkedDockSites {

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

			// Link the two dock sites together
			dockSite1.LinkDockSite(dockSite2);
		}
		
	}

}
