namespace ActiproSoftware.ProductSamples.ShellSamples.QuickStart.ShellListViewIntro {

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
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Notifies the UI that it has been unloaded.
		/// </summary>
		public override void NotifyUnloaded() {
			base.NotifyUnloaded();
			
			// Dispose any unmanaged resources held by the shell instances now that the UI is closing
			listView.DisposeShellInstances();
		}

	}

}
