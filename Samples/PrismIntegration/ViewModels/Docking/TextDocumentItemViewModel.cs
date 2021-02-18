using System;
using System.Windows.Media.Imaging;

namespace ActiproSoftware.Windows.PrismIntegration.ViewModels {

	/// <summary>
	/// Represents a text view-model for the sample.
	/// </summary>
	/// <remarks>
	/// This view-model derives from a base class that initializes the <c>DocumentWindow</c> from instance properties.
	/// </remarks>
	public class TextDocumentItemViewModel : DocumentItemViewModel {

		private string text;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="TextDocumentItemViewModel"/> class.
		/// </summary>
		public TextDocumentItemViewModel() {
			this.Description = "Text document";
			this.ImageSource = new BitmapImage(new Uri("/Resources/Images/TextDocument16.png", UriKind.Relative));
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the text associated with the view-model.
		/// </summary>
		/// <value>The text associated with the view-model.</value>
		public string Text {
			get {
				return this.text;
			}
			set {
				if (this.text != value) {
					this.text = value;
					this.NotifyPropertyChanged("Text");
				}
			}
		}

	}
}
