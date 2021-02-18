using ActiproSoftware.Windows.Controls.Views;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Provides the user control for a Backstage overlay.
	/// </summary>
	public partial class ReleaseHistoryBackstageOverlay {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the <c>ReleaseHistoryBackstageOverlay</c> class.
		/// </summary>
		public ReleaseHistoryBackstageOverlay() {
			InitializeComponent();
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the primary scroll viewer.
		/// </summary>
		/// <value>The primary scroll viewer.</value>
		public override InertiaScrollViewer ScrollViewer => scrollViewer;

	}

}
