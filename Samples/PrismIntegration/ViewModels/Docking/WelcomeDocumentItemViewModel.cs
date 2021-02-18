using System;
using System.Windows.Media.Imaging;

namespace ActiproSoftware.Windows.PrismIntegration.ViewModels {

	/// <summary>
	/// Represents a welcome document view-model.
	/// </summary>
	/// <remarks>
	/// This view-model derives from a base class that initializes the <c>DocumentWindow</c> from instance properties.
	/// </remarks>
	public class WelcomeDocumentItemViewModel : DocumentItemViewModel {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="WelcomeDocumentItemViewModel"/> class.
		/// </summary>
		public WelcomeDocumentItemViewModel() {
			this.Description = "Rich-text document";
			this.ImageSource = new BitmapImage(new Uri("/Resources/Images/RichTextDocument16.png", UriKind.Relative));
			this.Title = "WelcomeDocument.rtf";
		}

	}

}
