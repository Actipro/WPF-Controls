using System;
using System.Windows.Media.Imaging;
using ActiproSoftware.ProductSamples.DockingSamples.Common;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.MvvmDocumentWindows {

	/// <summary>
	/// Represents the text document view-model.
	/// </summary>
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
			this.ImageSource = new BitmapImage(new Uri("/Images/Icons/TextDocument16.png", UriKind.Relative));
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
