using ActiproSoftware.ProductSamples.RibbonSamples.Common;
using ActiproSoftware.Windows.Controls;

namespace ActiproSoftware.ProductSamples.RibbonSamples.QuickStart.GalleryCustomDraw {

	/// <summary>
	/// Provides the main control for this sample.
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
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
	
		/// <summary>
		/// Occurs when an underline gallery item needs to be drawn.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="CustomDrawElementCustomDrawEventArgs"/> that contains the event data.</param>
		private void OnCustomDrawUnderlineGalleryItems(object sender, CustomDrawElementCustomDrawEventArgs e) {
			// Draw the underline style onto the specified element
			UnderlineStyleRenderer.Render(e, (UnderlineStyle)e.Element.DataContext);
		}


	}
}