using System;
using System.Windows.Media.Imaging;
using ActiproSoftware.ProductSamples.DockingSamples.Common;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.MvvmDocumentWindows {

	/// <summary>
	/// Represents the image document view-model.
	/// </summary>
	public class ImageDocumentItemViewModel : DocumentItemViewModel {
		
		private Uri uri;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="ImageDocumentItemViewModel"/> class.
		/// </summary>
		public ImageDocumentItemViewModel() {
			this.Description = "Image document";
			this.ImageSource = new BitmapImage(new Uri("/Images/Icons/Picture16.png", UriKind.Relative));
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the image <see cref="Url"/> associated with the view-model.
		/// </summary>
		/// <value>The image <see cref="Url"/> associated with the view-model.</value>
		public Uri Uri {
			get {
				return this.uri;
			}
			set {
				if (this.uri != value) {
					this.uri = value;
					this.NotifyPropertyChanged("Uri");
				}
			}
		}

	}

}
