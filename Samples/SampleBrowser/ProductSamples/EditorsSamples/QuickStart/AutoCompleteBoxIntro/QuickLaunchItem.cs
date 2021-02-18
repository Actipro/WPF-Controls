#if WINRT
using Windows.UI.Xaml.Media;
#else
using System.Windows.Media;
#endif

namespace ActiproSoftware.ProductSamples.EditorsSamples.QuickStart.AutoCompleteBoxIntro {

	/// <summary>
	/// Represents a quick launch item.
	/// </summary>
	public class QuickLaunchItem {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the image source.
		/// </summary>
		/// <value>The image source.</value>
		public ImageSource ImageSource { get; set; }

		/// <summary>
		/// Gets or sets the item text.
		/// </summary>
		/// <value>The item text.</value>
		public string Text { get; set; }

	}

}
