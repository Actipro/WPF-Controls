using System;
using ActiproSoftware.SampleBrowser;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.Demo.SdiCodeEditor {

	/// <summary>
	/// Provides the main window for this sample.
	/// </summary>
	public partial class MainWindow : System.Windows.Window {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainWindow</c> class.
		/// </summary>
		public MainWindow() {
			InitializeComponent();
        }
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when the window is closed.
		/// </summary>
		/// <param name="e">The event arguments.</param>
		protected override void OnClosed(EventArgs e) {
			var previousSample = this.Content as ProductItemControl;
			if (previousSample != null)
				previousSample.NotifyUnloaded();

			// Call the base method
			base.OnClosed(e);
		}

	}

}